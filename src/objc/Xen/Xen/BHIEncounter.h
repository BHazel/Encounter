#import <Foundation/Foundation.h>

/*!
 @copyright (c) Benedict W. Hazel, 2011-2012
 @header BHIEncounter.h Protocol for classes processing and storing data from counterpoise correction calculations.
 */

/*!
 @protocol BHIEncounter
 @abstract Defines methods implemented by classes processing counterpoise correction calculations.
 */
@protocol BHIEncounter <NSObject>

/*!
 @property description
 @abstract Gets the calculation description.
 */
@property(nonatomic,retain) NSString* description;

/*!
 @property dimer
 @abstract Gets the energy of the dimer in Hartree atomic units.
 */
@property(readonly,assign) double dimer;

/*!
 @property monomerADimerBasis
 @abstract Gets the energy of monomer A in dimer basis in Hartree atomic units.
 */
@property(readonly,assign) double monomerADimerBasis;

/*!
 @property monomerBDimerBasis
 @abstract Gets the energy of monomer B in dimer basis in Hartree atomic units.
 */
@property(readonly,assign) double monomerBDimerBasis;

/*!
 @property monomerAMonomerBasis
 @abstract Gets the energy of monomer A in monomer A basis in Hartree atomic units.
 */
@property(readonly,assign) double monomerAMonomerBasis;

/*!
 @property monomerBMonomerBasis
 @abstract Gets the energy of monomer B in monomer B basis in Hartree atomic units.
 */
@property(readonly,assign) double monomerBMonomerBasis;

/*!
 @property interactionEnergyHartrees
 @abstract Gets the interaction energy between monomers in Hartree atomic units.
 */
@property(readonly,assign) double interactionEnergyHartrees;

/*!
 @property interactionEnergyKjmol
 @abstract Gets the interaction energy between monomers in kJ/mol.
 */
@property(readonly,assign) double interactionEnergyKjmol;

/*!
 @property bindingConstant
 @abstract Gets the binding constant between monomers.
 */
@property(readonly,assign) double bindingConstant;

/*!
 @method getEnergyCount
 @abstract Gets the number of energy values obtained from the counterpoise correction calculation.
 @return Number of energy values.
 */
-(int)getEnergyCount;

/*!
 @method setEnergies:
 @abstract Processes the calculation file and stores energy values.
 @param filename Counterpoise correction calculation file.
 */
-(void)setEnergies:(NSString*)filename;

/*!
 @method setInteractionEnergies
 @abstract Sets the interaction energy values and binding constant.
 */
-(void)setInteractionEnergies;
@end
