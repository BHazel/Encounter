/**
 * Copyright: (c) Benedict W. Hazel, 2011-2012
 * JsonDataFormatter: Class to export counterpoise correction data to XML
 * format.
 */

package uk.co.bwhazel.jcounterpoise.dataformatters;

import java.io.IOException;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import uk.co.bwhazel.jcounterpoise.IEncounter;

/**
 * Exports counterpoise correction data to extensible markup language format
 * (XML).
 * @author Benedict Hazel
 */
public class XmlDataFormatter implements IDataFormatter {

    /**
     * Exports the counterpoise correction calculation data in extensible markup
     * language format (XML).
     * @param encounter Instance of class implementing IEncounter containing the
     * calculation data.
     * @param stream Stream to write formatted data into.
     * @throws IOException Exception thrown if an error occurs while exporting
     * data to XML format.
     */
    public void exportData(IEncounter encounter, OutputStream stream)
            throws IOException {
        OutputStreamWriter xml = new OutputStreamWriter(stream);
        xml.write("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        xml.write("<enc:Counterpoise xmlns:enc="
                + "\"http://encounter.codeplex.com\" enc:Description=\"");
        xml.write(encounter.getDescription().concat("\">"));
        xml.write("<enc:Basis enc:Type=\"Dimer\">");
        xml.write("<enc:Dimer>");
        if (encounter.getEnergyCount() >= 1) {
            xml.write(String.valueOf(encounter.getDimer()));
        }
        xml.write("</enc:Dimer>");
        xml.write("<enc:MonomerA>");
        if (encounter.getEnergyCount() >= 2) {
            xml.write(String.valueOf(encounter.getMonomerADimerBasis()));
        }
        xml.write("</enc:MonomerA>");
        xml.write("<enc:MonomerB>");
        if (encounter.getEnergyCount() >= 3) {
            xml.write(String.valueOf(encounter.getMonomerBDimerBasis()));
        }
        xml.write("</enc:MonomerB>");
        xml.write("</enc:Basis>");
        xml.write("<enc:Basis enc:Type=\"Monomer\">");
        xml.write("<enc:MonomerA>");
        if (encounter.getEnergyCount() >= 4) {
            xml.write(String.valueOf(encounter.getMonomerAMonomerBasis()));
        }
        xml.write("</enc:MonomerA>");
        xml.write("<enc:MonomerB>");
        if (encounter.getEnergyCount() == 5) {
            xml.write(String.valueOf(encounter.getMonomerBMonomerBasis()));
        }
        xml.write("</enc:MonomerB>");
        xml.write("</enc:Basis>");
        xml.write("<enc:InteractionEnergy>");
        xml.write("<enc:Hartree>");
        if (encounter.getEnergyCount() >= 3) {
            xml.write(String.valueOf(encounter.getInteractionEnergyHartrees()));
        }
        xml.write("</enc:Hartree>");
        xml.write("<enc:Kjmol>");
        if (encounter.getEnergyCount() >= 3) {
            xml.write(String.valueOf(encounter.getInteractionEnergyKjmol()));
        }
        xml.write("</enc:Kjmol>");
        xml.write("</enc:InteractionEnergy>");
        xml.write("<enc:BindingConstant>");
        if (encounter.getEnergyCount() >= 3) {
            xml.write(String.valueOf(encounter.getBindingConstant()));
        }
        xml.write("</enc:BindingConstant>");
        xml.write("</enc:Counterpoise>");
        xml.close();
    }
}
