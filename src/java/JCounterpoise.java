import java.io.*;
import gnu.getopt.*;

public class JCounterpoise
{
    static final String JC_TITLE = "JCounterpoise (Java Implementation)";
    static final String JC_VERSION = "1.1.0.37";
    static final String JC_COPYRIGHT = "(c) Benedict W. Hazel, 2011-2012";
    static String calcFile = "";
    static String exportFile = "";
    static Encounter encounter;

    public static void main(String[] args) {
        Console console = System.console();

        // Process Command-Line Arguments
        LongOpt[] options = {
            new LongOpt("open", LongOpt.REQUIRED_ARGUMENT, null, 'o'),
            new LongOpt("version", LongOpt.NO_ARGUMENT, null, 'v')
        };
        Getopt g = new Getopt("JCounterpoise", args, "o:v", options);
        g.setOpterr(false);
        int optChar;
        while ((optChar = g.getopt()) != -1) {
            switch (optChar) {
                case 'o':
                    exportFile = g.getOptarg();
                    break;
                case 'v':
                    about();
                    System.exit(0);
                    break;
                case '?':
                    System.err.println("ERROR: Option -" + (char)g.getOptopt() + " is not valid");
                    System.exit(1);
                    break;
                default:
                    break;
            }
        }
        System.out.println("# " + JC_TITLE + " v." + JC_VERSION + " #");
        if (g.getOptind() == (args.length - 1)) calcFile = args[g.getOptind()];
        else {
            while (calcFile.length() == 0) {
                calcFile = console.readLine("Enter counterpoise calculation filename: ");
            }
        }

        encounter = new Encounter();

        try {
            openFile();
        }
        catch (FileNotFoundException ex) {
            System.err.println("ERROR: File " + calcFile + " could not be found.");
            System.exit(1);
        }
        catch (IOException ex) {
            System.err.println("ERROR: An input error occurred while reading " + calcFile);
            System.exit(1);
        }
        catch (IllegalArgumentException ex) {
            System.err.println(ex.getMessage());
        }

        setUi();

        if (exportFile.length() != 0) {
            try {
                exportFile();
            }
            catch (IOException ex) {
                System.err.println("ERROR: An output error occurred while writing " + exportFile);
            }
        }
    }

    private static void openFile() throws IOException {
        encounter.setEnergies(calcFile);
        if (encounter.energyStrings.size() < 3) {
            System.err.println("ERROR: Incomplete dataset found, from which interaction energy cannot be calculated");
        }
        else if (encounter.energyStrings.size() < 5) {
            System.err.println("WARNING: Incomplete dataset found, but interaction energy can be calculated");
            encounter.setInteractionEnergies();
        }
        else if (encounter.energyStrings.size() == 5) {
            encounter.setInteractionEnergies();
        }
    }

    private static void exportFile() throws IOException {
        BufferedWriter writer = new BufferedWriter(new FileWriter(exportFile));
        try {
            writer.write(encounter.toCsv());
        }
        finally {
            writer.close();
        }
    }

    private static void setUi() {
        if (encounter.energyStrings.size() >= 1) {
            System.out.println("DIMER BASIS:");
            System.out.println("  Dimer =       " + String.valueOf(encounter.getDimer()));
        }
        if (encounter.energyStrings.size() >= 2) {
            System.out.println("  Monomer A =   " + String.valueOf(encounter.getMonomerADimerBasis()));
        }
        if (encounter.energyStrings.size() >= 3) {
            System.out.println("  Monomer B =   " + String.valueOf(encounter.getMonomerBDimerBasis()));
            if (encounter.energyStrings.size() >= 4) {
                System.out.println("MONOMER BASIS:");
                System.out.println("  Monomer A =    " + String.valueOf(encounter.getMonomerAMonomerBasis()));
            }
            if (encounter.energyStrings.size() == 5) {
                System.out.println("  Monomer B =    " + String.valueOf(encounter.getMonomerBMonomerBasis()));
            }
            System.out.println("\nInteraction Energy =");
            System.out.println("  " + String.valueOf(encounter.getInteractionHartree()) + " au");
            System.out.println("  " + String.valueOf(encounter.getInteractionKjmol()) + " kJ/mol");
            System.out.println("\nBinding Constant =");
            System.out.println("  " + String.valueOf(encounter.getBindingConstant()));
        }
    }

    private static void about() {
        System.out.println("# " + JC_TITLE + " v." + JC_VERSION + " #");
        System.out.println(JC_COPYRIGHT);
    }
}
