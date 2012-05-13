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
    NSString* _description;
    double _dimer;
    double _monAdimer;
    double _monBdimer;
    double _monAmonA;
    double _monBmonB;
    double _interactHartree;
    double _interactKjmol;
    double _bindingConstant;
}

@property(readonly,assign) double dimer, monomerADimerBasis, monomerBDimerBasis, monomerAMonomerBasis, monomerBMonomerBasis, interactionEnergyHartrees, interactionEnergyKjmol, bindingConstant;
@property(nonatomic,retain) NSString* description;

-(int)energyCount;
-(void)setEnergies:(NSString*)filename;
-(void)setInteractionEnergies;
-(NSString*)toCsv;
-(NSString*)toJson;
-(NSString*)toXml;
@end
