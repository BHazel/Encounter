In quantum molecular modeling, atomic orbitals are represented using Basis Functions which, when combined as a linear combination, provide a description of the molecular system.  The smallest number of functions to describe the system is equal to that which will just account for all the electrons within it.  The larger the number of basis functions, the more accurate the description.  An infinite number would give complete accuracy.  Application of an infinite number of functions is not possible, therefore a limited number are used thus leading to an incompleteness of the description.

When determining the interaction energy between two molecules in a system a problem arises.  For a two molecule system, for example, a simple subtraction as shown in Equation 1 is not possible.

![Equation 1](eqn1.png)

**Equation 1:** Simple interaction energy calculation where A and B correspond to the two separate molecules in the dimer

This is due to an effect known as the Basis Set Superposition Error (BSSE).  Basis functions from one component, being in close proximity to the other, can provide and compensate for the incompleteness of the other and vice-versa.  This leads to a “mixing” of the functions, or superposition. This leads to an overall lowering of the energy of the dimer, therefore an overestimation of the interaction energy.