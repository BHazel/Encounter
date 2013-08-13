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
      2: This is not a Gaussian calculation
      3: This is not a counterpoise calculation
      4: No energy values found
      5: Incomplete dataset: can calculate interaction
      6: Incomplete dataset: cannot calculate interaction
      */

    // Return value
    int ret = 0;

    QFile file(filename);
    if (!file.open(QIODevice::ReadOnly | QIODevice::Text))
    {
        return 1;
    }
    QTextStream fileReader(&file);
    QString line = fileReader.readLine(132);
    bool gaussianCalc = false;
    int hyphenLines = 0;
    bool descriptionFound = false;

    _description = "";
    if (energyStrings.count() != 0) energyStrings.clear();

    while (!line.isNull())
    {
        if (!line.startsWith(" Entering Gaussian System") && !gaussianCalc)
        {
            file.close();
            return 2;
        }
        else gaussianCalc = true;

        if (hyphenLines == 5)
        {
            _description = line.trimmed();
            hyphenLines = 0;
            descriptionFound = true;
        }
        if (line.startsWith(" ----") && !descriptionFound) hyphenLines++;
        if (line.startsWith(" # ") && !line.contains("counterpoise=2", Qt::CaseInsensitive))
        {
            file.close();
            return 3;
        }
        if (line.startsWith(" SCF Done:", Qt::CaseInsensitive))
        {
            energyExpression.indexIn(line);
            energyStrings.append(energyExpression.capturedTexts());
        }
        line = fileReader.readLine(132);
    }
    file.close();

    // Set return value depending on dataset
    if (energyStrings.length() == 0) return 4;
    if (energyStrings.length() < 5) ret = 5;
    if (energyStrings.length() < 3) ret = 6;

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
    csv.append(this->getDescription() + "\n");
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
    if (energyStrings.length() >= 3) csv.append(QString::number(this->getInteractionEnergyHartrees(), 'g', 14));
    csv.append("\n/kJ/mol,");
    if (energyStrings.length() >= 3) csv.append(QString::number(this->getInteractionEnergyKjmol(), 'g', 14));
    csv.append("\nBINDING CONSTANT");
    csv.append("\n/1,");
    if (energyStrings.length() >= 3) csv.append(QString::number(this->getBindingConstant(), 'g', 14));
    return csv;
}

QString Encounter::toJson()
{
    QString json;
    json.append("{");
    json.append("\n\t\"Description\" : \"" + this->getDescription() + "\",");
    json.append("\n\t\"Basis\" : [");
    json.append("\n\t\t{ \"Type\" : \"Dimer\", \"Dimer\" : \"");
    if (energyStrings.length() >= 1) json.append(QString::number(this->getDimer()));
    json.append("\", \"MonomerA\" : \"");
    if (energyStrings.length() >= 2) json.append(QString::number(this->getMonomerADimerBasis()));
    json.append("\", \"MonomerB\" : \"");
    if (energyStrings.length() >= 3) json.append(QString::number(this->getMonomerBDimerBasis()));
    json.append("\" },");
    json.append("\n\t\t{ \"Type\" : \"Monomer\", \"MonomerA\" : \"");
    if (energyStrings.length() >= 4) json.append(QString::number(this->getMonomerAMonomerBasis()));
    json.append("\", \"MonomerB\" : \"");
    if (energyStrings.length() == 5) json.append(QString::number(this->getMonomerBMonomerBasis()));
    json.append("\" }");
    json.append("\n\t],");
    json.append("\n\t\"InteractionEnergy\" : {");
    json.append("\n\t\t\"Hartree\" : \"");
    if (energyStrings.length() >= 3) json.append(QString::number(this->getInteractionEnergyHartrees()));
    json.append("\",");
    json.append("\n\t\t\"Kjmol\" : \"");
    if (energyStrings.length() >= 3) json.append(QString::number(this->getInteractionEnergyKjmol()));
    json.append("\"");
    json.append("\n\t},");
    json.append("\n\t\"BindingConstant\" : \"");
    if (energyStrings.length() >= 3) json.append(QString::number(this->getBindingConstant()));
    json.append("\"");
    json.append("\n}");
    return json;
}

QString Encounter::toXml()
{
    QString xml;
    QString xmlns("http://encounter.codeplex.com");
    QXmlStreamWriter writer(&xml);
    writer.setAutoFormatting(true);
    writer.writeNamespace(xmlns, "enc");
    writer.writeStartDocument();
    writer.writeStartElement(xmlns, "Counterpoise");
    writer.writeAttribute(xmlns, "Description", this->getDescription());
    writer.writeStartElement(xmlns, "Basis");
    writer.writeAttribute(xmlns, "Type", "Dimer");
    writer.writeStartElement(xmlns, "Dimer");
    if (energyStrings.length() >= 1) writer.writeCharacters(QString::number(this->getDimer()));
    writer.writeEndElement();
    writer.writeStartElement(xmlns, "MonomerA");
    if (energyStrings.length() >= 2) writer.writeCharacters(QString::number(this->getMonomerADimerBasis()));
    writer.writeEndElement();
    writer.writeStartElement(xmlns, "MonomerB");
    if (energyStrings.length() >= 3) writer.writeCharacters(QString::number(this->getMonomerBDimerBasis()));
    writer.writeEndElement();
    writer.writeEndElement();
    writer.writeStartElement(xmlns, "Basis");
    writer.writeAttribute(xmlns, "Type", "Monomer");
    writer.writeStartElement(xmlns, "MonomerA");
    if (energyStrings.length() >= 4) writer.writeCharacters(QString::number(this->getMonomerAMonomerBasis()));
    writer.writeEndElement();
    writer.writeStartElement(xmlns, "MonomerB");
    if (energyStrings.length() == 5) writer.writeCharacters(QString::number(this->getMonomerBMonomerBasis()));
    writer.writeEndElement();
    writer.writeEndElement();
    writer.writeStartElement(xmlns, "InteractionEnergy");
    writer.writeStartElement(xmlns, "Hartree");
    if (energyStrings.length() >= 3) writer.writeCharacters(QString::number(this->getInteractionEnergyHartrees()));
    writer.writeEndElement();
    writer.writeStartElement(xmlns, "Kjmol");
    if (energyStrings.length() >= 3) writer.writeCharacters(QString::number(this->getInteractionEnergyKjmol()));
    writer.writeEndElement();
    writer.writeEndElement();
    writer.writeStartElement(xmlns, "BindingConstant");
    if (energyStrings.length() >= 3) writer.writeCharacters(QString::number(this->getBindingConstant()));
    writer.writeEndElement();
    writer.writeEndElement();
    return xml;
}

int Encounter::energyCount()
{
    return energyStrings.count();
}

QString Encounter::getDescription()
{
    return this->_description;
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

double Encounter::getInteractionEnergyHartrees()
{
    return this->_interactHartree;
}

double Encounter::getInteractionEnergyKjmol()
{
    return this->_interactKjmol;
}

double Encounter::getBindingConstant()
{
    return this->_bindingConstant;
}

