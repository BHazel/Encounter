// <copyright file="CsvDataFormatter.cs" company="Benedict W. Hazel">
//      Benedict W. Hazel, 2011-2012
// </copyright>
// <author>Benedict W. Hazel</author>
// <summary>
//      CsvDataFormatter: Class to export counterpoise correction data to CSV format.
// </summary>

using System.IO;

namespace BWHazel.Sharpen.DataFormatters
{
    /// <summary>
    /// Exports counterpoise correction data to comma-separated values format (CSV)
    /// </summary>
    public class CsvDataFormatter : IDataFormatter
    {
        /// <summary>
        /// Exports the counterpoise correction calculation data in comma-separated values format (CSV)
        /// </summary>
        /// <param name="encounter">Instance of class implementing <see cref="IEncounter"/> containing the calculation data.</param>
        /// <param name="stream">Stream to write CSV formatted data into.</param>
        public void ExportData(IEncounter encounter, Stream stream)
        {
            StreamWriter csv = new StreamWriter(stream);
            csv.Write(encounter.Description + "\n");
            csv.Write("DIMER BASIS /au");
            csv.Write("\nDimer,");
            if (encounter.EnergyCount >= 1)
            {
                csv.Write(encounter.Dimer.ToString());
            }

            csv.Write("\nMonomer A,");
            if (encounter.EnergyCount >= 2)
            {
                csv.Write(encounter.MonomerADimerBasis.ToString());
            }

            csv.Write("\nMonomer B,");
            if (encounter.EnergyCount >= 3)
            {
                csv.Write(encounter.MonomerBDimerBasis.ToString());
            }

            csv.Write("\nMONOMER BASIS /au");
            csv.Write("\nMonomer A,");
            if (encounter.EnergyCount >= 4)
            {
                csv.Write(encounter.MonomerAMonomerBasis.ToString());
            }

            csv.Write("\nMonomer B,");
            if (encounter.EnergyCount == 5)
            {
                csv.Write(encounter.MonomerBMonomerBasis.ToString());
            }

            csv.Write("\nINTERACTION ENERGY");
            csv.Write("\n/au,");
            if (encounter.EnergyCount >= 3)
            {
                csv.Write(encounter.InteractionEnergyHartrees.ToString());
            }

            csv.Write("\n/kJ/mol,");
            if (encounter.EnergyCount >= 3)
            {
                csv.Write(encounter.InteractionEnergyKjmol.ToString());
            }

            csv.Write("\nBINDING CONSTANT");
            csv.Write("\n/1,");
            if (encounter.EnergyCount >= 3)
            {
                csv.Write(encounter.BindingConstant.ToString());
            }

            csv.Close();
        }
    }
}
