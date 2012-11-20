/**
 * Copyright: (c) Benedict W. Hazel, 2011-2012
 * CsvDataFormatter: Class to export counterpoise correction data to CSV format.
 */

package uk.co.bwhazel.jcounterpoise.dataformatters;

import java.io.IOException;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import uk.co.bwhazel.jcounterpoise.IEncounter;

/**
 * Exports counterpoise correction data to comma-separated values format (CSV).
 * @author Benedict Hazel
 */
public class CsvDataFormatter implements IDataFormatter {

    /**
     * Exports the counterpoise correction calculation data in comma-separated
     * values format (CSV).
     * @param encounter Instance of class implementing IEncounter containing the
     * calculation data.
     * @param stream Stream to write formatted data into.
     * @throws IOException Exception thrown if an error occurs while exporting
     * data to CSV format.
     */
    public void exportData(IEncounter encounter, OutputStream stream)
            throws IOException {
        OutputStreamWriter csv = new OutputStreamWriter(stream);
        csv.write(encounter.getDescription().concat("\n"));
        csv.write("DIMER BASIS /au");
        csv.write("\nDimer,");
        if (encounter.getEnergyCount() >= 1) {
            csv.write(String.valueOf(encounter.getDimer()));
        }
        csv.write("\nMonomer A,");
        if (encounter.getEnergyCount() >= 2) {
            csv.write(String.valueOf(encounter.getMonomerADimerBasis()));
        }
        csv.write("\nMonomer B,");
        if (encounter.getEnergyCount() >= 3) {
            csv.write(String.valueOf(encounter.getMonomerBDimerBasis()));
        }
        csv.write("\nMONOMER BASIS /au");
        csv.write("\nMonomer A,");
        if (encounter.getEnergyCount() >= 4) {
            csv.write(String.valueOf(encounter.getMonomerAMonomerBasis()));
        }
        csv.write("\nMonomer B,");
        if (encounter.getEnergyCount() == 5) {
            csv.write(String.valueOf(encounter.getMonomerBMonomerBasis()));
        }
        csv.write("\nINTERACTION ENERGY");
        csv.write("\n/au,");
        if (encounter.getEnergyCount() >= 3) {
            csv.write(String.valueOf(encounter.getInteractionEnergyHartrees()));
        }
        csv.write("\n/kJ/mol,");
        if (encounter.getEnergyCount() >= 3) {
            csv.write(String.valueOf(encounter.getInteractionEnergyKjmol()));
        }
        csv.write("\nBINDING CONSTANT");
        csv.write("\n/1,");
        if (encounter.getEnergyCount() >= 3) {
            csv.write(String.valueOf(encounter.getBindingConstant()));
        }
        csv.close();
    }
}
