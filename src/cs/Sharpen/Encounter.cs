using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace BWHazel.Sharpen
{
    public class Encounter
    {
        private Regex energyExpression;
        double _dimer;
        double _monAdimer;
        double _monBdimer;
        double _monAmonA;
        double _monBmonB;
        double _interactHartree;
        double _interactKjmol;
        double _bindingConstant;
        
        public List<String> energyStrings;

        public int EnergyCount
        {
            get { return energyStrings.Count; }
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

        public double InteractionEnergyKJMol
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
            while ((line = reader.ReadLine()) != null)
            {
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
            if (energyStrings.Count > 5) throw new ApplicationException("Incomplete dataset found, but interaction energy can be calculated");
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
            if (energyStrings.Count >= 3) csv.Append(this.InteractionEnergyKJMol.ToString());
            csv.Append("\nBINDING CONSTANT");
            csv.Append("\n/1,");
            if (energyStrings.Count >= 3) csv.Append(this.BindingConstant.ToString());
            return csv.ToString();
        }
    }
}
