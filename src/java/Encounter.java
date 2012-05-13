import java.io.*;
import java.util.*;
import java.util.regex.*;

public class Encounter {
    private Pattern energyExpression;
    String _description;
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
        int hyphenLines = 0;
        boolean descriptionFound = false;

        _description = "";
        if (energyStrings.size() != 0) energyStrings.clear();

        while ((line = reader.readLine()) != null)
        {
            if (!line.startsWith(" Entering Gaussian System") && !gaussianCalc)
            {
                reader.close();
                throw new IllegalArgumentException("This is not a Gaussian calculation");
            }
            else gaussianCalc = true;

            if (hyphenLines == 5)
            {
                _description = line.trim();
                hyphenLines = 0;
                descriptionFound = true;
            }
            if (line.startsWith(" ----") && !descriptionFound) hyphenLines++;
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
        csv.append(this.getDescription().concat("\n"));
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
        if (energyStrings.size() >= 3) csv.append(this.getInteractionEnergyHartrees());
        csv.append("\n/kJ/mol,");
        if (energyStrings.size() >= 3) csv.append(this.getInteractionEnergyKjmol());
        csv.append("\nBINDING CONSTANT");
        csv.append("\n/1,");
        if (energyStrings.size() >= 3) csv.append(this.getBindingConstant());
        return csv.toString();
    }

    public String toJson()
    {
        StringBuilder json = new StringBuilder();
        json.append("{");
        json.append("\n\t\"Description\" : \"");
        json.append((this.getDescription().concat("\",")));
        json.append("\n\t\"Basis\" : [");
        json.append("\n\t\t{ \"Type\" : \"Dimer\", \"Dimer\" : \"");
        if (energyStrings.size() >= 1) json.append(this.getDimer());
        json.append("\", \"MonomerA\" : \"");
        if (energyStrings.size() >= 2) json.append(this.getMonomerADimerBasis());
        json.append("\", \"MonomerB\" : \"");
        if (energyStrings.size() >= 3) json.append(this.getMonomerBDimerBasis());
        json.append("\" },");
        json.append("\n\t\t{ \"Type\" : \"Monomer\", \"MonomerA\" : \"");
        if (energyStrings.size() >= 4) json.append(this.getMonomerAMonomerBasis());
        json.append("\", \"MonomerB\" : \"");
        if (energyStrings.size() == 5) json.append(this.getMonomerBMonomerBasis());
        json.append("\" }");
        json.append("\n\t],");
        json.append("\n\t\"InteractionEnergy\" : {");
        json.append("\n\t\t\"Hartree\" : \"");
        if (energyStrings.size() >= 3) json.append(this.getInteractionEnergyHartrees());
        json.append("\",");
        json.append("\n\t\t\"Kjmol\" : \"");
        if (energyStrings.size() >= 3) json.append(this.getInteractionEnergyKjmol());
        json.append("\"");
        json.append("\n\t},");
        json.append("\n\t\"BindingConstant\" : \"");
        if (energyStrings.size() >= 3) json.append(this.getBindingConstant());
        json.append("\"");
        json.append("\n}");
        return json.toString();
    }

    public String toXml()
    {
        StringBuilder xml = new StringBuilder();
        xml.append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        xml.append("<enc:Counterpoise xmlns:enc=\"http://encounter.codeplex.com\" enc:Description=\"");
        xml.append(this.getDescription().concat("\">"));
        xml.append("<enc:Basis enc:Type=\"Dimer\">");
        xml.append("<enc:Dimer>");
        if (energyStrings.size() >= 1) xml.append(this.getDimer());
        xml.append("</enc:Dimer>");
        xml.append("<enc:MonomerA>");
        if (energyStrings.size() >= 2) xml.append(this.getMonomerADimerBasis());
        xml.append("</enc:MonomerA>");
        xml.append("<enc:MonomerB>");
        if (energyStrings.size() >= 3) xml.append(this.getMonomerBDimerBasis());
        xml.append("</enc:MonomerB>");
        xml.append("</enc:Basis>");
        xml.append("<enc:Basis enc:Type=\"Monomer\">");
        xml.append("<enc:MonomerA>");
        if (energyStrings.size() >= 4) xml.append(this.getMonomerAMonomerBasis());
        xml.append("</enc:MonomerA>");
        xml.append("<enc:MonomerB>");
        if (energyStrings.size() == 5) xml.append(this.getMonomerBMonomerBasis());
        xml.append("</enc:MonomerB>");
        xml.append("</enc:Basis>");
        xml.append("<enc:InteractionEnergy>");
        xml.append("<enc:Hartree>");
        if (energyStrings.size() >= 3) xml.append(this.getInteractionEnergyHartrees());
        xml.append("</enc:Hartree>");
        xml.append("<enc:Kjmol>");
        if (energyStrings.size() >= 3) xml.append(this.getInteractionEnergyKjmol());
        xml.append("</enc:Kjmol>");
        xml.append("</enc:InteractionEnergy>");
        xml.append("<enc:BindingConstant>");
        if (energyStrings.size() >= 3) xml.append(this.getBindingConstant());
        xml.append("</enc:BindingConstant>");
        xml.append("</enc:Counterpoise>");
        return xml.toString();
    }

    String getDescription()
    {
        return this._description;
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

    double getInteractionEnergyHartrees()
    {
        return this._interactHartree;
    }

    double getInteractionEnergyKjmol()
    {
        return this._interactKjmol;
    }

    double getBindingConstant()
    {
        return this._bindingConstant;
    }
}
