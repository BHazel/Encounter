#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    encErrors.append("Complete dataset");
    encErrors.append("File cannot be opened");
    encErrors.append("This is not a Gaussian calculation");
    encErrors.append("This is not a counterpoise calculation");
    encErrors.append("No energy values found");
    encErrors.append("Incomplete dataset found, but interaction energy can be calculated");
    encErrors.append("Incomplete dataset found, from which interaction energy cannot be calculated");
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_actionOpen_triggered()
{
    this->openFile();
}

void MainWindow::on_actionExport_triggered()
{
    this->exportFile();
}

void MainWindow::on_actionExit_triggered()
{
    QApplication::quit();
}

void MainWindow::on_actionAbout_triggered()
{
    QMessageBox::information(this, "About", ENCOUNTER_ABOUT);
}

void MainWindow::on_actionAbout_Qt_triggered()
{
    QMessageBox::aboutQt(this, "About Qt");
}

void MainWindow::openFile()
{
    QString filename = QFileDialog::getOpenFileName(this, tr("Open Gaussian Calculation"), QDir::currentPath(), tr("Gaussian Calculation Files (*.log);;All files (*.*)"));
    if (filename != "")
    {
        this->resetUi();
        int ret = encounter.setEnergies(filename);
        if (ret != 0)
        {
            QMessageBox::warning(this, "Encounter", encErrors.at(ret));
            if (ret >= 3) encounter.setInteractionEnergies();
        }
        else encounter.setInteractionEnergies();
        this->setUi();
    }
}

void MainWindow::exportFile()
{
    QString exportFile = QFileDialog::getSaveFileName(this, tr("Export Data"), QDir::currentPath(), tr("Comma Separated Values (*.csv)"));
    if (exportFile != "")
    {
        QFile csvFile(exportFile);
        if (!csvFile.open(QIODevice::WriteOnly))
        {
            QMessageBox::critical(this, "Encounter", "File cannot be saved");
        }
        QTextStream csvStream(&csvFile);
        csvStream << encounter.toCsv();
        csvStream.flush();
        csvFile.close();
    }
}

void MainWindow::setUi()
{
    if (encounter.energyCount() >= 1) ui->txtDimerDimer->setText(QString::number(encounter.getDimer(), 'g', 14));
    if (encounter.energyCount() >= 2) ui->txtMonADimer->setText(QString::number(encounter.getMonomerADimerBasis(), 'g', 14));
    if (encounter.energyCount() >= 3)
    {
        ui->txtMonBDimer->setText(QString::number(encounter.getMonomerBDimerBasis(), 'g', 14));
        if (encounter.energyCount() >= 4) ui->txtMonAMon->setText(QString::number(encounter.getMonomerAMonomerBasis(), 'g', 14));
        if (encounter.energyCount() == 5) ui->txtMonBMon->setText(QString::number(encounter.getMonomerBMonomerBasis(), 'g', 14));
        ui->txtInteractionHartree->setText(QString::number(encounter.getInteractionHartree(), 'g', 14));
        ui->txtInteractionKjmol->setText(QString::number(encounter.getInteractionKjmol(), 'g', 14));
        ui->txtBindingConstant->setText(QString::number(encounter.getBindingConstant(), 'g', 14));
    }
}

void MainWindow::resetUi()
{
    ui->txtDimerDimer->setText("");
    ui->txtMonADimer->setText("");
    ui->txtMonBDimer->setText("");
    ui->txtMonAMon->setText("");
    ui->txtMonBMon->setText("");
    ui->txtInteractionHartree->setText("");
    ui->txtInteractionKjmol->setText("");
    ui->txtBindingConstant->setText("");
}
