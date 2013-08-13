/**
 * Copyright: (c) Benedict W. Hazel, 2011-2012
 * Encounter: Class to process and store counterpoise correction calculation
 * data.
 */

package uk.co.bwhazel.jcounterpoise;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * Processes and stores counterpoise correction calculation data.
 * @author Benedict Hazel
 */
public class Encounter implements IEncounter {
    /** Regular expression to detect energy values in calculation file. */
    private Pattern energyExpression;

    /** Calculation description. */
    String _description;

    /** Dimer energy. */
    double _dimer;

    /** Monomer A energy in dimer basis. */
    double _monAdimer;

    /** Monomer B energy in dimer basis. */
    double _monBdimer;

    /** Monomer A energy in Monomer A basis. */
    double _monAmonA;

    /** Monomer B energy in Monomer B basis. */
    double _monBmonB;

    /** Interaction energy in Hartree atomic units. */
    double _interactHartree;

    /** Interaction energy in kJ/mol. */
    double _interactKjmol;

    /** Binding constant. */
    double _bindingConstant;

    /** Variable to store energy values extracted from the calculation file. */
    private ArrayList<String> energyStrings;

    /**
     * Initialises a new instance of the Encounter class.
     */
    public Encounter() {
        energyExpression = Pattern.compile("-\\d+\\.\\d+");
        energyStrings = new ArrayList<String>();
    }

    /**
     * Processes the calculation file and stores energy values.
     * @param filename Counterpoise correction calculation file.
     * @throws IOException Exception thrown if an error occurs while reading
     * the file.
     */
    public void setEnergies(String filename)
            throws IOException {
        BufferedReader reader = new BufferedReader(new FileReader(filename));
        String line;
        boolean gaussianCalc = false;
        int hyphenLines = 0;
        boolean descriptionFound = false;

        _description = "";

        if (!energyStrings.isEmpty()) {
            energyStrings.clear();
        }

        while ((line = reader.readLine()) != null) {
            if (!line.startsWith(" Entering Gaussian System")
                    && !gaussianCalc) {
                reader.close();
                throw new IllegalArgumentException("This is not a "
                        + "Gaussian calculation");
            } else {
                gaussianCalc = true;
            }

            if (hyphenLines == 5) {
                _description = line.trim();
                hyphenLines = 0;
                descriptionFound = true;
            }
            if (line.startsWith(" ----") && !descriptionFound) {
                hyphenLines++;
            }
            if (line.startsWith(" # ") && !line.contains("counterpoise=2")) {
                reader.close();
                throw new IllegalArgumentException("This is not a counterpoise"
                        + "calculation");
            }
            if (line.startsWith(" SCF Done:")) {
                Matcher energy = energyExpression.matcher(line);
                if (energy.find()) {
                    energyStrings.add(energy.group());
                }
            }
        }
        reader.close();

        if (energyStrings.size() >= 1) {
            _dimer = Double.parseDouble(energyStrings.get(0));
        }
        if (energyStrings.size() >= 2) {
            _monAdimer = Double.parseDouble(energyStrings.get(1));
        }
        if (energyStrings.size() >= 3) {
            _monBdimer = Double.parseDouble(energyStrings.get(2));
        }
        if (energyStrings.size() >= 4) {
            _monAmonA = Double.parseDouble(energyStrings.get(3));
        }
        if (energyStrings.size() == 5) {
            _monBmonB = Double.parseDouble(energyStrings.get(4));
        }
    }

     /**
     * Sets the interaction energy values and binding constant.
     */
    public void setInteractionEnergies() {
        this._interactHartree = this._dimer
                - (this._monAdimer + this._monBdimer);
        this._interactKjmol = this._interactHartree * 2625.5;
        this._bindingConstant = Math.exp((this._interactKjmol
                * 1000) / (-1 * 8.314 * 298));
    }

    /**
     * Gets the calculation description.
     * @return Calculation description.
     */
    public String getDescription() {
        return this._description;
    }

    /**
     * Gets the energy of the dimer in Hartree atomic units.
     * @return Dimer energy.
     */
    public double getDimer() {
        return this._dimer;
    }

    /**
     * Gets the energy of monomer A in dimer basis in Hartree atomic units.
     * @return Monomer A energy in dimer basis.
     */
    public double getMonomerADimerBasis() {
        return this._monAdimer;
    }

    /**
     * Gets the energy of monomer B in dimer basis in Hartree atomic units.
     * @return Monomer B energy in dimer basis.
     */
    public double getMonomerBDimerBasis() {
        return this._monBdimer;
    }

    /**
     * Gets the energy of monomer A in monomer A basis in Hartree atomic units.
     * @return Monomer A energy in monomer A basis.
     */
    public double getMonomerAMonomerBasis() {
        return this._monAmonA;
    }

    /**
     * Gets the energy of monomer B in monomer B basis in Hartree atomic units.
     * @return Monomer B energy in monomer B basis.
     */
    public double getMonomerBMonomerBasis() {
        return this._monBmonB;
    }

    /**
     * Gets the interaction energy between monomers in Hartree atomic units.
     * @return Interaction energy in Hartree atomic units.
     */
    public double getInteractionEnergyHartrees() {
        return this._interactHartree;
    }

    /**
     * Gets the interaction energy between monomers in kJ/mol.
     * @return Interaction energy in kJ/mol.
     */
    public double getInteractionEnergyKjmol() {
        return this._interactKjmol;
    }

    /**
     * Gets the binding constant between monomers.
     * @return Binding constant.
     */
    public double getBindingConstant() {
        return this._bindingConstant;
    }

    /**
     * Gets the number of energy values obtained from the counterpoise
     * correction calculation.
     * @return Number of energy values.
     */
    public int getEnergyCount() {
        return energyStrings.size();
    }
}
