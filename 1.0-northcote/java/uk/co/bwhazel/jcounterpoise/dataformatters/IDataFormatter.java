/**
 * Copyright: (c) Benedict W. Hazel, 2011-2012
 * IDataFormatter: Interface implemented by data exporting classes.
 */

package uk.co.bwhazel.jcounterpoise.dataformatters;

import java.io.IOException;
import java.io.OutputStream;
import uk.co.bwhazel.jcounterpoise.IEncounter;

/**
 * Defines methods implemented by data exporting classes.
 * @author Benedict
 */
public interface IDataFormatter {
    /**
     * Exports the counterpoise correction calculation data in the format
     * supported by the class.
     * @param encounter Instance of class implementing IEncounter containing the
     * calculation data.
     * @param stream Stream to write formatted data into.
     * @throws IOException Exception thrown if an error occurs while exporting
     * data.
     */
    void exportData(IEncounter encounter, OutputStream stream)
            throws IOException;
}
