// <copyright file="JsonDataFormatter.cs" company="Benedict W. Hazel">
//      Benedict W. Hazel, 2011-2012
// </copyright>
// <author>Benedict W. Hazel</author>
// <summary>
//      JsonDataFormatter: Class to export counterpoise correction data to JSON format.
// </summary>

using System.IO;

namespace BWHazel.Sharpen.DataFormatters
{
    /// <summary>
    /// Exports counterpoise correction data to JavaScript object notation format (JSON).
    /// </summary>
    public class JsonDataFormatter : IDataFormatter
    {
        /// <summary>
        /// Exports the counterpoise correction calculation data in JavaScript object notation format (JSON)
        /// </summary>
        /// <param name="encounter">Instance of class implementing <see cref="IEncounter"/> containing the calculation data.</param>
        /// <param name="stream">Stream to write JSON formatted data into.</param>
        public void ExportData(IEncounter encounter, Stream stream)
        {
            StreamWriter json = new StreamWriter(stream);
            json.Write("{");
            json.Write("\n\t\"Description\" : \"" + encounter.Description + "\",");
            json.Write("\n\t\"Basis\" : [");
            json.Write("\n\t\t{ \"Type\" : \"Dimer\", \"Dimer\" : \"");
            if (encounter.EnergyCount >= 1)
            {
                json.Write(encounter.Dimer.ToString());
            }

            json.Write("\", \"MonomerA\" : \"");
            if (encounter.EnergyCount >= 2)
            {
                json.Write(encounter.MonomerADimerBasis.ToString());
            }

            json.Write("\", \"MonomerB\" : \"");
            if (encounter.EnergyCount >= 3)
            {
                json.Write(encounter.MonomerBDimerBasis.ToString());
            }

            json.Write("\" },");
            json.Write("\n\t\t{ \"Type\" : \"Monomer\", \"MonomerA\" : \"");
            if (encounter.EnergyCount >= 4)
            {
                json.Write(encounter.MonomerAMonomerBasis.ToString());
            }

            json.Write("\", \"MonomerB\" : \"");
            if (encounter.EnergyCount == 5)
            {
                json.Write(encounter.MonomerBMonomerBasis.ToString());
            }

            json.Write("\" }");
            json.Write("\n\t],");
            json.Write("\n\t\"InteractionEnergy\" : {");
            json.Write("\n\t\t\"Hartree\" : \"");
            if (encounter.EnergyCount >= 3)
            {
                json.Write(encounter.InteractionEnergyHartrees.ToString());
            }

            json.Write("\",");
            json.Write("\n\t\t\"Kjmol\" : \"");
            if (encounter.EnergyCount >= 3)
            {
                json.Write(encounter.InteractionEnergyKjmol.ToString());
            }

            json.Write("\"");
            json.Write("\n\t},");
            json.Write("\n\t\"BindingConstant\" : \"");
            if (encounter.EnergyCount >= 3)
            {
                json.Write(encounter.BindingConstant.ToString());
            }

            json.Write("\"");
            json.Write("\n}");
            json.Close();
        }
    }
}
