// <copyright file="IEncounter.cs" company="Benedict W. Hazel">
//      Benedict W. Hazel, 2011-2012
// </copyright>
// <author>Benedict W. Hazel</author>
// <summary>
//      IEncounter: Interface implemented by classes processing counterpoise correction calculations.
// </summary>

namespace BWHazel.Sharpen
{
    /// <summary>
    /// Defines methods and properties implemented by classes processing counterpoise correction calculations.
    /// </summary>
    public interface IEncounter
    {
        /// <summary>Gets the calculation description.</summary>
        string Description { get; }

        /// <summary>Gets the energy of the dimer in Hartree atomic units.</summary>
        double Dimer { get; }

        /// <summary>Gets the energy of monomer A in dimer basis in Hartree atomic units.</summary>
        double MonomerADimerBasis { get; }

        /// <summary>Gets the energy of monomer B in dimer basis in Hartree atomic units.</summary>
        double MonomerBDimerBasis { get; }

        /// <summary>Gets the energy of monomer A in monomer A basis in Hartree atomic units.</summary>
        double MonomerAMonomerBasis { get; }

        /// <summary>Gets the energy of monomer B in monomer B basis in Hartree atomic units.</summary>
        double MonomerBMonomerBasis { get; }

        /// <summary>Gets the interaction energy between monomers in Hartree atomic units.</summary>
        double InteractionEnergyHartrees { get; }

        /// <summary>Gets the interaction energy between monomers in kJ/mol.</summary>
        double InteractionEnergyKjmol { get; }

        /// <summary>Gets the binding constant between monomers.</summary>
        double BindingConstant { get; }

        /// <summary>Gets the number of energy values obtained from the counterpoise correction calculation.</summary>
        int EnergyCount { get; }

        /// <summary>
        /// Processes the calculation file and stores energy values.
        /// </summary>
        /// <param name="filename">Counterpoise correction calculation file.</param>
        void SetEnergies(string filename);

        /// <summary>
        /// Sets the interaction energy values and binding constant.
        /// </summary>
        void SetInteractionEnergies();
    }
}
