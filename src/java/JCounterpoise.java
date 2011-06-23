import java.io.*;
import java.util.*;
import java.util.regex.*;

public class JCounterpoise
{

	static String filename = "";
	static String version = "1.0.5.25";

	public static void main(String[] args)
	{
		Console console = System.console();
		ArrayList<Double> energies = new ArrayList<Double>();
		Pattern scfDone = Pattern.compile("-\\d+\\.\\d+");
		Double interactionEnergyHartrees;
		Double interactionEnergyKJMol;
		Double bindingConstant;
		BufferedReader calcFile = null;
		
		// Welcome message
		System.out.println("\n# JCounterpoise v" + version + " #");

		// Check for filename in command-line arguments, otherwise ask
		if (args.length < 1)
		{
			System.out.println("");
			while (filename.length() == 0)
			{
				filename = console.readLine("Enter counterpoise calculation filename: ");
			}
		}
		else
		{
			filename = args[0];
		}

		// Extract completed SCF cycles
		try
		{
			calcFile = new BufferedReader(new FileReader(filename));
			String currentLine;
			while ((currentLine = calcFile.readLine()) != null)
			{
				if (currentLine.startsWith(" SCF Done"))
				{
					Matcher matcher = scfDone.matcher(currentLine);
					if (matcher.find())
						energies.add(Double.parseDouble(matcher.group()));
				}
			}
			System.out.println("");

			// Print warning messages if incomplete dataset
			if (energies.size() == 0)
			{
				System.err.println("ERROR: No energy values found\n");
				System.exit(1);
			}
			else if (energies.size() < 3)
			{
				System.err.println("ERROR: Insufficient data to calculate interaction energy\n");
			}
			else if (energies.size() < 5)
			{
				System.err.println("WARNING: Incomplete dataset, but interaction energy can be calculated\n");
			}
			else if (energies.size() > 5)
			{
				System.err.println("ERROR: This is not a single-point calculation\n");
				System.exit(1);
			}

			// Print energy values to the console
			System.out.println("Energies /au:\n");
			if (energies.size() >= 1)
			{
				System.out.println("DIMER BASIS");
				System.out.println("  Dimer =       " + energies.get(0));
			}
			if (energies.size() >= 2)
			{
				System.out.println("  Monomer A =   " + energies.get(1));
			}
			if (energies.size() >= 3)
			{
				System.out.println("  Monomer B =   " + energies.get(2));
			}
			if (energies.size() >= 4)
			{
				System.out.println("MONOMER BASIS");
				System.out.println("  Monomer A =   " + energies.get(3));
			}
			if (energies.size() == 5)
			{
				System.out.println("  Monomer B =   " + energies.get(4));
			}

			// Calculate interaction energy and binding constant
			if (energies.size() >= 3)
			{
				interactionEnergyHartrees = energies.get(0) - (energies.get(1) + energies.get(2));
				interactionEnergyKJMol = interactionEnergyHartrees * 2625.5;
				bindingConstant = Math.exp((interactionEnergyKJMol * 1000)/(-1 * 8.314 * 298));

				System.out.println("\nInteraction Energy =\n");
				System.out.println("  " + interactionEnergyHartrees.toString() + " au");
				System.out.println("  " + interactionEnergyKJMol.toString() + " kJ/mol");
				System.out.println("\nBinding Constant = \n");
				System.out.println("  " + bindingConstant.toString());
			}
		}
		catch (FileNotFoundException ex)
		{
			System.err.println("\nERROR: File \"" + filename + "\" not found");
		}
		catch (IOException ex)
		{
			System.err.println("\nERROR: An input error has occurred while reading " + filename);
		}
		catch (Exception ex)
		{
			System.err.println("\n** An error has occurred: " + ex.toString());
			System.err.println("** " + ex.getMessage());
		}
		finally
		{
			try
			{
				calcFile.close();
			}
			catch (IOException ex)
			{
				System.err.println("\nERROR: BufferedReader object cannot be closed");
			}
		}
		System.out.println("");
	}
}
