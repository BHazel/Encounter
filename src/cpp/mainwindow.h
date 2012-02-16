#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#define ENCOUNTER_ABOUT "Encounter (C++/Qt Implementation) v.1.0.5.48\n\n(c) Benedict W. Hazel, 2011-2012"

#include <encounter.h>
#include <QFileDialog>
#include <QList>
#include <QMainWindow>
#include <QMessageBox>
#include <QString>
#include <QTextStream>

namespace Ui {
    class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    Encounter encounter;
    QList<QString> encErrors;
    explicit MainWindow(QWidget *parent = 0);
    ~MainWindow();

private:
    Ui::MainWindow *ui;

private slots:
    void on_actionOpen_triggered();
    void on_actionExport_triggered();
    void on_actionExit_triggered();
    void on_actionAbout_triggered();
    void on_actionAbout_Qt_triggered();
    void openFile();
    void exportFile();
    void setUi();
    void resetUi();
};

#endif // MAINWINDOW_H
