#include <encounter.h>
#include <math.h>

Encounter::Encounter()
{
    energyExpression = QRegExp("-\\d+\\.\\d+");
}

int Encounter::setEnergies(QString filename)
{
    /*
      RETURN CODES:
      0: Complete dataset
      1: File cannot be opened
      2: This is not a counterpoise calculation
      3: No energy values found
      4: Incomplete dataset: can calculate interaction
      5: Incomplete dataset: cannot calculate interaction
      */

    // Return value
    int ret = 0;

    energyStrings.clear();

    QFile file(filename);
    if (!file.open(QIODevice::ReadOnly | QIODevice::Text))
    {
        return 1;
    }
    QTextStream fileReader(&file);
    QString line = fileReader.readLine(132);
    while (!line.isNull())
    {
        if (line.startsWith(" # ") && !line.contains("counterpoise=2", Qt::CaseInsensitive)) return 2;
        if (line.startsWith(" SCF Done:", Qt::CaseInsensitive))
        {
            energyExpression.indexIn(line);
            energyStrings.append(energyExpression.capturedTexts());
        }
        line = fileReader.readLine(132);
    }
    file.close();

    // Set return value depending on dataset
    if (energyStrings.length() == 0) return 3;
    if (energyStrings.length() < 5) ret = 4;
    if (energyStrings.length() < 3) ret = 5;

    if (energyStrings.length() >= 1) this->_dimer = energyStrings.at(0).toDouble();
    if (energyStrings.length() >= 2) this->_monAdimer = energyStrings.at(1).toDouble();
    if (energyStrings.length() >= 3) this->_monBdimer = energyStrings.at(2).toDouble();
    if (energyStrings.length() >= 4) this->_monAmonA = energyStrings.at(3).toDouble();
    if (energyStrings.length() == 5) this->_monBmonB = energyStrings.at(4).toDouble();

    return ret;
}

void Encounter::setInteractionEnergies()
{
    this->_interactHartree = this->_dimer - (this->_monAdimer + this->_monBdimer);
    this->_interactKjmol = this->_interactHartree * 2625.5;
    this->_bindingConstant = exp((this->_interactKjmol * 1000) / (-1 * 8.314 * 298));
}

QString Encounter::toCsv()
{
    QString csv;
    csv.append("DIMER BASIS /au");
    csv.append("\nDimer,");
    if (energyStrings.length() >= 1) csv.append(QString::number(this->getDimer(), 'g', 14));
    csv.append("\nMonomer A,");
    if (energyStrings.length() >= 2) csv.append(QString::number(this->getMonomerADimerBasis(), 'g', 14));
    csv.append("\nMonomer B,");
    if (energyStrings.length() >= 3) csv.append(QString::number(this->getMonomerBDimerBasis(), 'g', 14));
    csv.append("\nMONOMER BASIS /au");
    csv.append("\nMonomer A,");
    if (energyStrings.length() >= 4) csv.append(QString::number(this->getMonomerAMonomerBasis(), 'g', 14));
    csv.append("\nMonomer B,");
    if (energyStrings.length() == 5) csv.append(QString::number(this->getMonomerBMonomerBasis(), 'g', 14));
    csv.append("\nINTERACTION ENERGY");
    csv.append("\n/au,");
    if (energyStrings.length() >= 3) csv.append(QString::number(this->getInteractionHartree(), 'g', 14));
    csv.append("\n/kJ/mol,");
    if (energyStrings.length() >= 3) csv.append(QString::number(this->getInteractionKjmol(), 'g', 14));
    csv.append("\nBINDING CONSTANT");
    csv.append("\n/1,");
    if (energyStrings.length() >= 3) csv.append(QString::number(this->getBindingConstant(), 'g', 14));
    return csv;
}

double Encounter::getDimer()
{
    return this->_dimer;
}

double Encounter::getMonomerADimerBasis()
{
    return this->_monAdimer;
}

double Encounter::getMonomerBDimerBasis()
{
    return this->_monBdimer;
}

double Encounter::getMonomerAMonomerBasis()
{
    return this->_monAmonA;
}

double Encounter::getMonomerBMonomerBasis()
{
    return this->_monBmonB;
}

double Encounter::getInteractionHartree()
{
    return this->_interactHartree;
}

double Encounter::getInteractionKjmol()
{
    return this->_interactKjmol;
}

double Encounter::getBindingConstant()
{
    return this->_bindingConstant;
}

