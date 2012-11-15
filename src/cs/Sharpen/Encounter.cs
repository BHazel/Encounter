// <copyright file="Encounter.cs" company="Benedict W. Hazel">
//      Benedict W. Hazel, 2011-2012
// </copyright>
// <author>Benedict W. Hazel</author>
// <summary>
//      Encounter: Class to process and store counterpoise correction calculation data.
// </summary>

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace BWHazel.Sharpen
{
    /// <summary>
    /// Processes and stores counterpoise correction calculation data.
    /// </summary>
    public class Encounter : IEncounter
    {
        /// <summary>Calculation description.</summary>
        private string _description;

        /// <summary>Dimer energy.</summary>
        private double _dimer;

        /// <summary>Monomer A energy in dimer basis.</summary>
        private double _monAdimer;

        /// <summary>Monomer B energy in dimer basis.</summary>
        private double _monBdimer;

        /// <summary>Monomer A energy in Monomer A basis.</summary>
        private double _monAmonA;

        /// <summary>Monomer B energy in Monomer B basis.</summary>
        private double _monBmonB;

        /// <summary>Interaction energy in Hartree atomic units.</summary>
        private double _interactHartree;

        /// <summary>Interaction energy in kJ/mol.</summary>
        private double _interactKjmol;

        /// <summary>Binding constant.</summary>
        private double _bindingConstant;

        /// <summary>Regular expression to detect energy values in calculation file.</summary>
        private Regex energyExpression;

        /// <summary>Variable to store energy values extracted from the calc    ulation file.</summary>
        private List<string> energyStrings;

        /// <summary>
        /// Initialises a new instance of the <see cref="Encounter"/> class.
        /// </summary>
        public Encounter()
        {
            this.energyExpression = new Regex("-*\\d+\\.\\d+", RegexOptions.IgnoreCase);
            this.energyStrings = new List<string>();
        }

        /// <summary>
        /// Gets the number of energy values obtained from the counterpoise correction calculation.
        /// </summary>
        public int EnergyCount
        {
            get { return this.energyStrings.Count; }
        }

        /// <summary>
        /// Gets the calculation description.
        /// </summary>
        public string Description
        {
            get { return this._description; }
        }

        /// <summary>
        /// Gets the energy of the dimer in Hartree atomic units.
        /// </summary>
        public double Dimer
        {
            get { return this._dimer; }
        }

        /// <summary>
        /// Gets the energy of monomer A in dimer basis in Hartree atomic units.
        /// </summary>
        public double MonomerADimerBasis
        {
            get { return this._monAdimer; }
        }

        /// <summary>
        /// Gets the energy of monomer B in dimer basis in Hartree atomic units.
        /// </summary>
        public double MonomerBDimerBasis
        {
            get { return this._monBdimer; }
        }

        /// <summary>
        /// Gets the energy of monomer A in monomer A basis in Hartree atomic units.
        /// </summary>
        public double MonomerAMonomerBasis
        {
            get { return this._monAmonA; }
        }

        /// <summary>
        /// Gets the energy of monomer B in monomer B basis in Hartree atomic units.
        /// </summary>
        public double MonomerBMonomerBasis
        {
            get { return this._monBmonB; }
        }

        /// <summary>
        /// Gets the interaction energy between monomers in Hartree atomic units.
        /// </summary>
        public double InteractionEnergyHartrees
        {
            get { return this._interactHartree; }
        }

        /// <summary>
        /// Gets the interaction energy between monomers in kJ/mol.
        /// </summary>
        public double InteractionEnergyKjmol
        {
            get { return this._interactKjmol; }
        }

        /// <summary>
        /// Gets the binding constant between monomers.
        /// </summary>
        public double BindingConstant
        {
            get { return this._bindingConstant; }
        }

        /// <summary>
        /// Processes the calculation file and stores energy values.
        /// </summary>
        /// <param name="filename">Counterpoise correction calculation file.</param>
        public void SetEnergies(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            string line = null;
            bool gaussianCalc = false;
            int hyphenLines = 0;
            bool descriptionFound = false;

            this._description = string.Empty;
            if (this.energyStrings.Count != 0)
            {
                this.energyStrings.Clear();
            }

            while ((line = reader.ReadLine()) != null)
            {
                if (!line.StartsWith(" Entering Gaussian System") && !gaussianCalc)
                {
                    reader.Close();
                    throw new ArgumentException("This is not a Gaussian calculation");
                }
                else
                {
                    gaussianCalc = true;
                }

                if (hyphenLines == 5)
                {
                    this._description = line.Trim();
                    hyphenLines = 0;
                    descriptionFound = true;
                }

                if (line.StartsWith(" ----") && !descriptionFound)
                {
                    hyphenLines++;
                }

                if (line.StartsWith(" # ") && !line.Contains("counterpoise=2"))
                {
                    reader.Close();
                    throw new ArgumentException("This is not a counterpoise calculation");
                }

                if (line.StartsWith(" SCF Done:"))
                {
                    // Requires Checking!
                    Match energy = this.energyExpression.Match(line);
                    this.energyStrings.Add(energy.ToString());
                }
            }

            reader.Close();

            if (this.energyStrings.Count == 0)
            {
                throw new ApplicationException("No energy values found");
            }

            if (this.energyStrings.Count >= 1)
            {
                this._dimer = double.Parse(this.energyStrings[0]);
            }

            if (this.energyStrings.Count >= 2)
            {
                this._monAdimer = double.Parse(this.energyStrings[1]);
            }

            if (this.energyStrings.Count < 3)
            {
                throw new ApplicationException("Incomplete dataset found, from which interaction energy cannot be calculated");
            }

            if (this.energyStrings.Count >= 3)
            {
                this._monBdimer = double.Parse(this.energyStrings[2]);
            }

            if (this.energyStrings.Count >= 4)
            {
                this._monAmonA = double.Parse(this.energyStrings[3]);
            }

            if (this.energyStrings.Count < 5)
            {
                throw new ApplicationException("Incomplete dataset found, but interaction energy can be calculated");
            }

            if (this.energyStrings.Count == 5)
            {
                this._monBmonB = double.Parse(this.energyStrings[4]);
            }
        }

        /// <summary>
        /// Sets the interaction energy values and binding constant.
        /// </summary>
        public void SetInteractionEnergies()
        {
            this._interactHartree = this._dimer - (this._monAdimer + this._monBdimer);
            this._interactKjmol = this._interactHartree * 2625.5;
            this._bindingConstant = Math.Exp((this._interactKjmol * 1000) / (-1 * 8.314 * 298));
        }
    }
}
