#import <Foundation/Foundation.h>
#import "BHIEncounter.h"

/*!
 @copyright (c) Benedict W. Hazel, 2011-2012
 @header BHEncounter.h Class to process and store counterpoise correction calculation data.
 */

/*!
 @interface BHEncounter
 @abstract Class to process and store counterpoise correction calculation data.
 */
@interface BHEncounter : NSObject <BHIEncounter> {
@private
    /*!
     @var energyExpression
     @abstract Regular expression to detect energy values in calculation file.
     */
    NSRegularExpression* energyExpression;
    
    /*!
     @var energyStrings
     @abstract Variable to store energy values extracted from the calculation file.
     */
    NSMutableArray* energyStrings;
    
    /*!
     @var _description
     @abstract Calculation description.
     */
    NSString* _description;
    
    /*!
     @var _dimer
     @abstract Dimer energy.
     */
    double _dimer;
    
    /*!
     @var _monAdimer
     @abstract Monomer A energy in dimer basis.
     */
    double _monAdimer;
    
    /*!
     @var _monBdimer
     @abstract Monomer B energy in dimer basis.
     */
    double _monBdimer;
    
    /*!
     @var _monAmonA
     @abstract Monomer A energy in Monomer A basis.
     */
    double _monAmonA;
    
    /*!
     @var _monBmonB
     @abstract Monomer B energy in Monomer B basis.
     */
    double _monBmonB;
    
    /*!
     @var _interactHartree
     @abstract Interaction energy in Hartree atomic units.
     */
    double _interactHartree;
    
    /*!
     @var _interactKjmol
     @abstract Interaction energy in kJ/mol.
     */
    double _interactKjmol;
    
    /*!
     @var _bindingConstant
     @abstract Binding constant.
     */
    double _bindingConstant;
}

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
