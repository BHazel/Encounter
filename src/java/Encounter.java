import java.io.*;
import java.util.*;
import java.util.regex.*;

public class Encounter {
    private Pattern energyExpression;
    double _dimer;
    double _monAdimer;
    double _monBdimer;
    double _monAmonA;
    double _monBmonB;
    double _interactHartree;
    double _interactKjmol;
    double _bindingConstant;
    private ArrayList<String> energyStrings;
    
    public Encounter()
    {
        energyExpression = Pattern.compile("-\\d+\\.\\d+");
        energyStrings = new ArrayList<String>();
    }

    public int energyCount()
    {
        return energyStrings.size();
    }

    public void setEnergies(String filename) throws IllegalArgumentException, IOException
    {
        BufferedReader reader = new BufferedReader(new FileReader(filename));
        String line = null;
        boolean gaussianCalc = false;

        if (energyStrings.size() != 0) energyStrings.clear();

        while ((line = reader.readLine()) != null)
        {
            if (!line.startsWith(" Entering Gaussian System") && !gaussianCalc)
            {
                reader.close();
                throw new IllegalArgumentException("This is not a Gaussian calculation");
            }
            else gaussianCalc = true;

            if (line.startsWith(" # ") && !line.contains("counterpoise=2"))
            {
                reader.close();
                throw new IllegalArgumentException("This is not a counterpoise calculation");
            }
            if (line.startsWith(" SCF Done:"))
            {
                Matcher energy = energyExpression.matcher(line);
                if (energy.find())
                    energyStrings.add(energy.group());
            }
        }
        reader.close();

        if (energyStrings.size() >= 1) _dimer = Double.parseDouble(energyStrings.get(0));
        if (energyStrings.size() >= 2) _monAdimer = Double.parseDouble(energyStrings.get(1));
        if (energyStrings.size() >= 3) _monBdimer = Double.parseDouble(energyStrings.get(2));
        if (energyStrings.size() >= 4) _monAmonA = Double.parseDouble(energyStrings.get(3));
        if (energyStrings.size() == 5) _monBmonB = Double.parseDouble(energyStrings.get(4));
    }

    public void setInteractionEnergies()
    {
        this._interactHartree = this._dimer - (this._monAdimer + this._monBdimer);
        this._interactKjmol = this._interactHartree * 2625.5;
        this._bindingConstant = Math.exp((this._interactKjmol * 1000) / (-1 * 8.314 * 298));
    }

    public String toCsv()
    {
        StringBuilder csv = new StringBuilder();
        csv.append("DIMER BASIS /au");
        csv.append("\nDimer,");
        if (energyStrings.size() >= 1) csv.append(this.getDimer());
        csv.append("\nMonomer A,");
        if (energyStrings.size() >= 2) csv.append(this.getMonomerADimerBasis());
        csv.append("\nMonomer B,");
        if (energyStrings.size() >= 3) csv.append(this.getMonomerBDimerBasis());
        csv.append("\nMONOMER BASIS /au");
        csv.append("\nMonomer A,");
        if (energyStrings.size() >= 4) csv.append(this.getMonomerAMonomerBasis());
        csv.append("\nMonomer B,");
        if (energyStrings.size() == 5) csv.append(this.getMonomerBMonomerBasis());
        csv.append("\nINTERACTION ENERGY");
        csv.append("\n/au,");
        if (energyStrings.size() >= 3) csv.append(this.getInteractionHartree());
        csv.append("\n/kJ/mol,");
        if (energyStrings.size() >= 3) csv.append(this.getInteractionKjmol());
        csv.append("\nBINDING CONSTANT");
        csv.append("\n/1,");
        if (energyStrings.size() >= 3) csv.append(this.getBindingConstant());
        return csv.toString();
    }

    double getDimer()
    {
        return this._dimer;
    }

    double getMonomerADimerBasis()
    {
        return this._monAdimer;
    }

    double getMonomerBDimerBasis()
    {
        return this._monBdimer;
    }

    double getMonomerAMonomerBasis()
    {
        return this._monAmonA;
    }

    double getMonomerBMonomerBasis()
    {
        return this._monBmonB;
    }

    double getInteractionHartree()
    {
        return this._interactHartree;
    }

    double getInteractionKjmol()
    {
        return this._interactKjmol;
    }

    double getBindingConstant()
    {
        return this._bindingConstant;
    }
}
