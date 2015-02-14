For a successful calculation of the interaction energy and binding constant the following requirements should be met by the calculation file:

* The calculation file must be output from Gaussian.
* It must be an Energy calculation, i.e. not an Optimisation as this would produce too many energy values.
* It must also use the Counterpoise Correction method for a 2-member system.

A successful calculation would yield 5 energy values corresponding to:

* The dimer
* Both monomers in dimer basis
* Each monomer in its own basis

Errors when Loading Files
-------------------------

If the calculation does not meet the above requirements an error is more than likely to occur.  The internal error-handling in Encounter can recognise the following errors:

* **Non-counterpoise calculation:** if the calculation does not use the Counterpoise Correction an error will be thrown and the file will not be read.
  * This is currently not available in JCounterpoise.
* **Fewer than 3 energy values:** any values found will be displayed but no attempt is made at calculating the interaction energy and binding constant.
* **3 or 4 energy values:** an error message is displayed but the interaction energy and binding constant will not be calculated.