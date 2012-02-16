//
//  BHXen.h
//  Xen
//
//  Created by Benedict Hazel on 19/06/2011.
//  Copyright 2011 TransWandsworth. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface BHEncounter : NSObject {
@private
    NSRegularExpression* energyExpression;
    NSMutableArray* energyStrings;
@protected
    double _dimer;
    double _monAdimer;
    double _monBdimer;
    double _monAmonA;
    double _monBmonB;
    double _interactHartree;
    double _interactKjmol;
    double _bindingConstant;
}

@property(readonly,assign) double dimer, monomerADimerBasis, monomerBDimerBasis, monomerAMonomerBasis, monomerBMonomerBasis, interactionEnergyHartrees, interactionEnergyKJMol, bindingConstant;

-(int)energyCount;
-(void)setEnergies:(NSString*)filename;
-(void)setInteractionEnergies;
-(NSString*)toCsv;
@end
