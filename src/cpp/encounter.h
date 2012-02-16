#ifndef ENCOUNTER_H
#define ENCOUNTER_H

#include <encounter.h>
#include <math.h>
#include <QRegExp>
#include <QFile>
#include <QMessageBox>
#include <QTextStream>
#include <QString>
#include <QList>

/*
 Encounter: "ENergy COUNTERpoise"
     A tool for determining the interaction energy for a host-guest molecular system
     Requires Qt 4.7 to be installed
 */
class Encounter
{

private:
    QRegExp energyExpression;
    double _dimer;
    double _monAdimer;
    double _monBdimer;
    double _monAmonA;
    double _monBmonB;
    double _interactHartree;
    double _interactKjmol;
    double _bindingConstant;
    QList<QString> energyStrings;

public:
    Encounter();
    int setEnergies(QString filename);
    void setInteractionEnergies();
    QString toCsv();
    int energyCount();
    double getDimer();
    double getMonomerADimerBasis();
    double getMonomerBDimerBasis();
    double getMonomerAMonomerBasis();
    double getMonomerBMonomerBasis();
    double getInteractionHartree();
    double getInteractionKjmol();
    double getBindingConstant();
};

#endif // ENCOUNTER_H
