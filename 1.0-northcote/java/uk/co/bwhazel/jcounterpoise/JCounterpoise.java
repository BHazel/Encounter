/**
 * Copyright: (c) Benedict W. Hazel, 2011-2012
 * JCounterpoise: Class containing the main entry point and logic for the
 * program.
 */

package uk.co.bwhazel.jcounterpoise;

import gnu.getopt.Getopt;
import gnu.getopt.LongOpt;
import java.io.Console;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import uk.co.bwhazel.jcounterpoise.dataformatters.CsvDataFormatter;
import uk.co.bwhazel.jcounterpoise.dataformatters.IDataFormatter;
import uk.co.bwhazel.jcounterpoise.dataformatters.JsonDataFormatter;
import uk.co.bwhazel.jcounterpoise.dataformatters.XmlDataFormatter;

/**
 * Main program class.
 * @author Benedict Hazel
 */
public class JCounterpoise {
    /** JCounterpoise title. */
    static final String JC_TITLE = "JCounterpoise (Java Implementation)";

    /** JCounterpoise version. */
    static final String JC_VERSION = "1.3.0.50";

    /** JCounterpoise copyright details. */
    static final String JC_COPYRIGHT = "(c) Benedict W. Hazel, 2011-2012";

    /** Gaussian calculation file. */
    static String calcFile = "";

    /** Exported data file. */
    static String exportFile = "";

    /** Exported file format. */
    static String format = "";

    /** Encounter instance. */
    static Encounter encounter;

    /**
     * The main entry point for the application.
     * @param args Command-lie arguments passed to the application.
     */
    public static void main(String[] args) {
        Console console = System.console();

        // Process Command-Line Arguments
        LongOpt[] options = {
            new LongOpt("format", LongOpt.REQUIRED_ARGUMENT, null, 'f'),
            new LongOpt("output", LongOpt.REQUIRED_ARGUMENT, null, 'o'),
            new LongOpt("version", LongOpt.NO_ARGUMENT, null, 'v')
        };
        Getopt g = new Getopt("JCounterpoise", args, "f:o:v", options);
        g.setOpterr(false);
        int optChar;
        while ((optChar = g.getopt()) != -1) {
            switch (optChar) {
                case 'f':
                    format = g.getOptarg();
                    break;
                case 'o':
                    exportFile = g.getOptarg();
                    break;
                case 'v':
                    about();
                    System.exit(0);
                    break;
                case '?':
                    System.err.println("ERROR: Option -"
                            + (char) g.getOptopt() + " is not valid");
                    System.exit(1);
                    break;
                default:
                    break;
            }
        }
        System.out.println("# " + JC_TITLE + " v." + JC_VERSION + " #");
        if (g.getOptind() == (args.length - 1)) {
            calcFile = args[g.getOptind()];
        } else {
            while (calcFile.length() == 0) {
                calcFile = console.readLine(
                        "Enter counterpoise calculation filename: ");
            }
        }

        encounter = new Encounter();

        try {
            openFile();
        } catch (FileNotFoundException ex) {
            System.err.println("ERROR: File "
                    + calcFile + " could not be found.");
            System.exit(1);
        } catch (IOException ex) {
            System.err.println("ERROR: An input error occurred while reading "
                    + calcFile);
            System.exit(1);
        } catch (IllegalArgumentException ex) {
            System.err.println("ERROR: " + ex.getMessage());
        }

        setUi();

        if (exportFile.length() != 0) {
            try {
                exportFile();
            } catch (IOException ex) {
                System.err.println("ERROR: An output error occurred "
                        + "while writing " + exportFile);
            } catch (IllegalArgumentException ex) {
                System.err.println("ERROR: Unknown file type selected");
            }
        }
    }

    /**
     * Opens a file.
     * @throws IOException Exception thrown if an error occurs while reading
     * the file.
     */
    private static void openFile() throws IOException {
        encounter.setEnergies(calcFile);
        if (encounter.getEnergyCount() == 0) {
            System.err.println("ERROR: No energy values found");
        } else if (encounter.getEnergyCount() < 3) {
            System.err.println("ERROR: Incomplete dataset found,"
                    + "from which interaction energy cannot be calculated");
        } else if (encounter.getEnergyCount() < 5) {
            System.err.println("WARNING: Incomplete dataset found,"
                    + "but interaction energy can be calculated");
            encounter.setInteractionEnergies();
        } else if (encounter.getEnergyCount() == 5) {
            encounter.setInteractionEnergies();
        }
    }

    /**
     * Exports data to a file.
     * @throws IOException Exception thrown if an error occurs while writing
     * the file.
     */
    private static void exportFile()
            throws IOException {
        IDataFormatter formatter;
        if (format.toLowerCase().contentEquals("csv")
                || format.toLowerCase().contentEquals("c")) {
            formatter = new CsvDataFormatter();
        } else if (format.toLowerCase().contentEquals("json")
                || format.toLowerCase().contentEquals("j")) {
            formatter = new JsonDataFormatter();
        } else if (format.toLowerCase().contentEquals("xml")
                || format.toLowerCase().contentEquals("x")) {
            formatter = new XmlDataFormatter();
        } else {
            throw new IllegalArgumentException();
        }
        formatter.exportData(encounter, new FileOutputStream(exportFile));
    }

    /**
     * Sets the UI text depending on the number of energy values in the
     * IEncounter instance.
     */
    private static void setUi() {
        System.out.println(encounter.getDescription() + "\n");
        if (encounter.getEnergyCount() >= 1) {
            System.out.println("DIMER BASIS /au:");
            System.out.println("  Dimer =       "
                    + String.valueOf(encounter.getDimer()));
        }
        if (encounter.getEnergyCount() >= 2) {
            System.out.println("  Monomer A =   "
                    + String.valueOf(encounter.getMonomerADimerBasis()));
        }
        if (encounter.getEnergyCount() >= 3) {
            System.out.println("  Monomer B =   "
                    + String.valueOf(encounter.getMonomerBDimerBasis()));
            if (encounter.getEnergyCount() >= 4) {
                System.out.println("MONOMER BASIS /au:");
                System.out.println("  Monomer A =    "
                        + String.valueOf(encounter.getMonomerAMonomerBasis()));
            }
            if (encounter.getEnergyCount() == 5) {
                System.out.println("  Monomer B =    "
                        + String.valueOf(encounter.getMonomerBMonomerBasis()));
            }
            System.out.println("\nInteraction Energy =");
            System.out.println("  " + String.valueOf(
                    encounter.getInteractionEnergyHartrees()) + " au");
            System.out.println("  " + String.valueOf(
                    encounter.getInteractionEnergyKjmol()) + " kJ/mol");
            System.out.println("\nBinding Constant =");
            System.out.println("  " + String.valueOf(
                    encounter.getBindingConstant()));
        }
    }

    /**
     * Displays information about JCounterpoise.
     */
    private static void about() {
        System.out.println("# " + JC_TITLE + " v." + JC_VERSION + " #");
        System.out.println(JC_COPYRIGHT);
    }
}
