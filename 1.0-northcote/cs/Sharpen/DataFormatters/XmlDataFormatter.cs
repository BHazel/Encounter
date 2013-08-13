// <copyright file="XmlDataFormatter.cs" company="Benedict W. Hazel">
//      Benedict W. Hazel, 2011-2012
// </copyright>
// <author>Benedict W. Hazel</author>
// <summary>
//      XmlDataFormatter: Class to export counterpoise correction data to XML format.
// </summary>

using System.IO;
using System.Xml;

namespace BWHazel.Sharpen.DataFormatters
{
    /// <summary>
    /// Exports counterpoise correction data to XML format.
    /// </summary>
    public class XmlDataFormatter : IDataFormatter
    {
        /// <summary>
        /// Exports the counterpoise correction calculation data in XML format.
        /// </summary>
        /// <param name="encounter">Instance of class implementing <see cref="IEncounter"/> containing the calculation data.</param>
        /// <param name="stream">Stream to write XML formatted data into.</param>
        public void ExportData(IEncounter encounter, Stream stream)
        {
            StreamWriter xml = new StreamWriter(stream);
            string xmlns = @"http://encounter.codeplex.com";
            XmlWriter writer = XmlWriter.Create(xml);
            writer.WriteStartDocument();
            writer.WriteStartElement("enc", "Counterpoise", xmlns);
            writer.WriteStartAttribute("enc", "Description", xmlns);
            writer.WriteValue(encounter.Description);
            writer.WriteEndAttribute();
            writer.WriteStartElement("enc", "Basis", xmlns);
            writer.WriteStartAttribute("enc", "Type", xmlns);
            writer.WriteValue("Dimer");
            writer.WriteEndAttribute();
            writer.WriteStartElement("enc", "Dimer", xmlns);
            if (encounter.EnergyCount >= 1)
            {
                writer.WriteValue(encounter.Dimer.ToString());
            }

            writer.WriteEndElement();
            writer.WriteStartElement("enc", "MonomerA", xmlns);
            if (encounter.EnergyCount >= 2)
            {
                writer.WriteValue(encounter.MonomerADimerBasis.ToString());
            }

            writer.WriteEndElement();
            writer.WriteStartElement("enc", "MonomerB", xmlns);
            if (encounter.EnergyCount >= 3)
            {
                writer.WriteValue(encounter.MonomerBDimerBasis.ToString());
            }

            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteStartElement("enc", "Basis", xmlns);
            writer.WriteStartAttribute("enc", "Type", xmlns);
            writer.WriteValue("Monomer");
            writer.WriteEndAttribute();
            writer.WriteStartElement("enc", "MonomerA", xmlns);
            if (encounter.EnergyCount >= 4)
            {
                writer.WriteValue(encounter.MonomerAMonomerBasis.ToString());
            }

            writer.WriteEndElement();
            writer.WriteStartElement("enc", "MonomerB", xmlns);
            if (encounter.EnergyCount == 5)
            {
                writer.WriteValue(encounter.MonomerBMonomerBasis.ToString());
            }

            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteStartElement("enc", "InteractionEnergy", xmlns);
            writer.WriteStartElement("enc", "Hartree", xmlns);
            if (encounter.EnergyCount >= 3)
            {
                writer.WriteValue(encounter.InteractionEnergyHartrees.ToString());
            }

            writer.WriteEndElement();
            writer.WriteStartElement("enc", "Kjmol", xmlns);
            if (encounter.EnergyCount >= 3)
            {
                writer.WriteValue(encounter.InteractionEnergyKjmol.ToString());
            }

            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteStartElement("enc", "BindingConstant", xmlns);
            if (encounter.EnergyCount >= 3)
            {
                writer.WriteValue(encounter.BindingConstant.ToString());
            }

            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Close();
            xml.Close();
        }
    }
}
