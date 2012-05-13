using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace BWHazel.Sharpen
{
    public class Encounter
    {
        private Regex energyExpression;
        string _description;
        double _dimer;
        double _monAdimer;
        double _monBdimer;
        double _monAmonA;
        double _monBmonB;
        double _interactHartree;
        double _interactKjmol;
        double _bindingConstant;
        private List<String> energyStrings;

        public int EnergyCount
        {
            get { return energyStrings.Count; }
        }

        public string Description
        {
            get { return _description; }
        }

        public double Dimer
        {
            get { return _dimer; }
        }

        public double MonomerADimerBasis
        {
            get { return _monAdimer; }
        }

        public double MonomerBDimerBasis
        {
            get { return _monBdimer; }
        }

        public double MonomerAMonomerBasis
        {
            get { return _monAmonA; }
        }

        public double MonomerBMonomerBasis
        {
            get { return _monBmonB; }
        }

        public double InteractionEnergyHartrees
        {
            get { return _interactHartree; }
        }

        public double InteractionEnergyKjmol
        {
            get { return _interactKjmol; }
        }

        public double BindingConstant
        {
            get { return _bindingConstant; }
        }

        public Encounter()
        {
            energyExpression = new Regex("-*\\d+\\.\\d+", RegexOptions.IgnoreCase);
            energyStrings = new List<String>();
        }

        public void SetEnergies(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            string line = null;
            bool gaussianCalc = false;
            int hyphenLines = 0;
            bool descriptionFound = false;

            _description = "";
            if (energyStrings.Count != 0) energyStrings.Clear();

            while ((line = reader.ReadLine()) != null)
            {
                if (!line.StartsWith(" Entering Gaussian System") && !gaussianCalc)
                {
                    reader.Close();
                    throw new ArgumentException("This is not a Gaussian calculation");
                }
                else gaussianCalc = true;

                if (hyphenLines == 5)
                {
                    _description = line.Trim();
                    hyphenLines = 0;
                    descriptionFound = true;
                }
                if (line.StartsWith(" ----") && !descriptionFound) hyphenLines++;
                if (line.StartsWith(" # ") && !line.Contains("counterpoise=2"))
                {
                    reader.Close();
                    throw new ArgumentException("This is not a counterpoise calculation");
                }
                if (line.StartsWith(" SCF Done:"))
                {
                    // Requires Checking!
                    Match energy = energyExpression.Match(line);
                    energyStrings.Add(energy.ToString());
                }
            }
            reader.Close();

            if (energyStrings.Count == 0) throw new ApplicationException("No energy values found");
            if (energyStrings.Count >= 1) _dimer = Double.Parse(energyStrings[0]);
            if (energyStrings.Count >= 2) _monAdimer = Double.Parse(energyStrings[1]);
            if (energyStrings.Count < 3) throw new ApplicationException("Incomplete dataset found, from which interaction energy cannot be calculated");
            
            if (energyStrings.Count >= 3) _monBdimer = Double.Parse(energyStrings[2]);
            if (energyStrings.Count >= 4) _monAmonA = Double.Parse(energyStrings[3]);
            if (energyStrings.Count < 5) throw new ApplicationException("Incomplete dataset found, but interaction energy can be calculated");
            if (energyStrings.Count == 5) _monBmonB = Double.Parse(energyStrings[4]);
        }

        public void SetInteractionEnergies()
        {
            _interactHartree = _dimer - (_monAdimer + _monBdimer);
            _interactKjmol = _interactHartree * 2625.5;
            _bindingConstant = Math.Exp((_interactKjmol * 1000) / (-1 * 8.314 * 298));
        }

        public string ToCsv()
        {
            StringBuilder csv = new StringBuilder();
            csv.Append(this.Description + "\n");
            csv.Append("DIMER BASIS /au");
            csv.Append("\nDimer,");
            if (energyStrings.Count >= 1) csv.Append(this.Dimer.ToString());
            csv.Append("\nMonomer A,");
            if (energyStrings.Count >= 2) csv.Append(this.MonomerADimerBasis.ToString());
            csv.Append("\nMonomer B,");
            if (energyStrings.Count >= 3) csv.Append(this.MonomerBDimerBasis.ToString());
            csv.Append("\nMONOMER BASIS /au");
            csv.Append("\nMonomer A,");
            if (energyStrings.Count >= 4) csv.Append(this.MonomerAMonomerBasis.ToString());
            csv.Append("\nMonomer B,");
            if (energyStrings.Count == 5) csv.Append(this.MonomerBMonomerBasis.ToString());
            csv.Append("\nINTERACTION ENERGY");
            csv.Append("\n/au,");
            if (energyStrings.Count >= 3) csv.Append(this.InteractionEnergyHartrees.ToString());
            csv.Append("\n/kJ/mol,");
            if (energyStrings.Count >= 3) csv.Append(this.InteractionEnergyKjmol.ToString());
            csv.Append("\nBINDING CONSTANT");
            csv.Append("\n/1,");
            if (energyStrings.Count >= 3) csv.Append(this.BindingConstant.ToString());
            return csv.ToString();
        }

        public string ToJson()
        {
            StringBuilder json = new StringBuilder();
            json.Append("{");
            json.Append("\n\t\"Description\" : \"" + this.Description + "\",");
            json.Append("\n\t\"Basis\" : [");
            json.Append("\n\t\t{ \"Type\" : \"Dimer\", \"Dimer\" : \"");
            if (energyStrings.Count >= 1) json.Append(this.Dimer.ToString());
            json.Append("\", \"MonomerA\" : \"");
            if (energyStrings.Count >= 2) json.Append(this.MonomerADimerBasis.ToString());
            json.Append("\", \"MonomerB\" : \"");
            if (energyStrings.Count >= 3) json.Append(this.MonomerBDimerBasis.ToString());
            json.Append("\" },");
            json.Append("\n\t\t{ \"Type\" : \"Monomer\", \"MonomerA\" : \"");
            if (energyStrings.Count >= 4) json.Append(this.MonomerAMonomerBasis.ToString());
            json.Append("\", \"MonomerB\" : \"");
            if (energyStrings.Count == 5) json.Append(this.MonomerBMonomerBasis.ToString());
            json.Append("\" }");
            json.Append("\n\t],");
            json.Append("\n\t\"InteractionEnergy\" : {");
            json.Append("\n\t\t\"Hartree\" : \"");
            if (energyStrings.Count >= 3) json.Append(this.InteractionEnergyHartrees.ToString());
            json.Append("\",");
            json.Append("\n\t\t\"Kjmol\" : \"");
            if (energyStrings.Count >= 3) json.Append(this.InteractionEnergyKjmol.ToString());
            json.Append("\"");
            json.Append("\n\t},");
            json.Append("\n\t\"BindingConstant\" : \"");
            if (energyStrings.Count >= 3) json.Append(this.BindingConstant.ToString());
            json.Append("\"");
            json.Append("\n}");
            return json.ToString();
        }

        public string ToXml()
        {
            string xmlns = @"http://encounter.codeplex.com";
            StringBuilder xml = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(xml);
            writer.WriteStartDocument();
            writer.WriteStartElement("enc", "Counterpoise", xmlns);
            writer.WriteStartAttribute("enc", "Description", xmlns);
            writer.WriteValue(this.Description);
            writer.WriteEndAttribute();
            writer.WriteStartElement("enc", "Basis", xmlns);
            writer.WriteStartAttribute("enc", "Type", xmlns);
            writer.WriteValue("Dimer");
            writer.WriteEndAttribute();
            writer.WriteStartElement("enc", "Dimer", xmlns);
            if (energyStrings.Count >= 1) writer.WriteValue(this.Dimer.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("enc", "MonomerA", xmlns);
            if (energyStrings.Count >= 2) writer.WriteValue(this.MonomerADimerBasis.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("enc", "MonomerB", xmlns);
            if (energyStrings.Count >= 3) writer.WriteValue(this.MonomerBDimerBasis.ToString());
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteStartElement("enc", "Basis", xmlns);
            writer.WriteStartAttribute("enc", "Type", xmlns);
            writer.WriteValue("Monomer");
            writer.WriteEndAttribute();
            writer.WriteStartElement("enc", "MonomerA", xmlns);
            if (energyStrings.Count >= 4) writer.WriteValue(this.MonomerAMonomerBasis.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("enc", "MonomerB", xmlns);
            if (energyStrings.Count == 5) writer.WriteValue(this.MonomerBMonomerBasis.ToString());
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteStartElement("enc", "InteractionEnergy", xmlns);
            writer.WriteStartElement("enc", "Hartree", xmlns);
            if (energyStrings.Count >= 3) writer.WriteValue(this.InteractionEnergyHartrees.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("enc", "Kjmol", xmlns);
            if (energyStrings.Count >= 3) writer.WriteValue(this.InteractionEnergyKjmol.ToString());
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteStartElement("enc", "BindingConstant", xmlns);
            if (energyStrings.Count >= 3) writer.WriteValue(this.BindingConstant.ToString());
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Close();
            return xml.ToString();
        }
    }
}
