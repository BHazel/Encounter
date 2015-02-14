JCounterpoise runs from the command-line and is controlled by flags.  If an error occurs when opening a file an error message will describe what has happened.

Using the Bash Script
---------------------

If you built the bash script you can use a simplified short-hand to run JCounterpoise:

    jcp calc.log -o calc.csv -f csv

This opens calculation file _calc.log_ and exports its data in CSV to _calc.csv_.  Supported values for the `-f` flag are `csv`, `json` and `xml`.  Both the `-f` and `-o` flags are optional.

A long-flag variant can also be used:

    jcp calc.log --output=csv --format=csv

An additional flag `-v`/`--version` returns the JCounterpoise version info.

Using the Java Runtime Directly
-------------------------------

All the instructions for using the Bash script apply except swap `jcp` for `java JCounterpoise`:

    java JCounterpoise calc.log -o calc.csv -f csv