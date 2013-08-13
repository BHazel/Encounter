/**
 * Copyright: (c) Benedict W. Hazel, 2011-2012
 * JsonDataFormatter: Class to export counterpoise correction data to JSON
 * format.
 */

package uk.co.bwhazel.jcounterpoise.dataformatters;

import java.io.IOException;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import uk.co.bwhazel.jcounterpoise.IEncounter;

/**
 * Exports counterpoise correction data to JavaScript object notation
 * format (JSON).
 * @author Benedict Hazel
 */
public class JsonDataFormatter implements IDataFormatter {

    /**
     * Exports the counterpoise correction calculation data in JavaScript object
     * notation format (JSON).
     * @param encounter Instance of class implementing IEncounter containing the
     * calculation data.
     * @param stream Stream to write formatted data into.
     * @throws IOException Exception thrown if an error occurs while exporting
     * data to JSON format.
     */
    public void exportData(IEncounter encounter, OutputStream stream)
            throws IOException {
        OutputStreamWriter json = new OutputStreamWriter(stream);
        json.write("{");
        json.write("\n\t\"Description\" : \"");
        json.write((encounter.getDescription().concat("\",")));
        json.write("\n\t\"Basis\" : [");
        json.write("\n\t\t{ \"Type\" : \"Dimer\", \"Dimer\" : \"");
        if (encounter.getEnergyCount() >= 1) {
            json.write(String.valueOf(encounter.getDimer()));
        }
        json.write("\", \"MonomerA\" : \"");
        if (encounter.getEnergyCount() >= 2) {
            json.write(String.valueOf(encounter.getMonomerADimerBasis()));
        }
        json.write("\", \"MonomerB\" : \"");
        if (encounter.getEnergyCount() >= 3) {
            json.write(String.valueOf(encounter.getMonomerBDimerBasis()));
        }
        json.write("\" },");
        json.write("\n\t\t{ \"Type\" : \"Monomer\", \"MonomerA\" : \"");
        if (encounter.getEnergyCount() >= 4) {
            json.write(String.valueOf(encounter.getMonomerAMonomerBasis()));
        }
        json.write("\", \"MonomerB\" : \"");
        if (encounter.getEnergyCount() == 5) {
            json.write(String.valueOf(encounter.getMonomerBMonomerBasis()));
        }
        json.write("\" }");
        json.write("\n\t],");
        json.write("\n\t\"InteractionEnergy\" : {");
        json.write("\n\t\t\"Hartree\" : \"");
        if (encounter.getEnergyCount() >= 3) {
            json.write(String.valueOf(
                    encounter.getInteractionEnergyHartrees()));
        }
        json.write("\",");
        json.write("\n\t\t\"Kjmol\" : \"");
        if (encounter.getEnergyCount() >= 3) {
            json.write(String.valueOf(encounter.getInteractionEnergyKjmol()));
        }
        json.write("\"");
        json.write("\n\t},");
        json.write("\n\t\"BindingConstant\" : \"");
        if (encounter.getEnergyCount() >= 3) {
            json.write(String.valueOf(encounter.getBindingConstant()));
        }
        json.write("\"");
        json.write("\n}");
        json.close();
    }
}
