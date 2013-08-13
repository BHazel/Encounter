//
//  BHCsvDataFormatter.m
//  Xen
//
//  Created by Benedict Hazel on 22/11/2012.
//  Copyright (c) 2012 TransWandsworth. All rights reserved.
//

#import "BHCsvDataFormatter.h"

@implementation BHCsvDataFormatter
-(void)exportData:(id <BHIEncounter>)encounter output:(NSURL*)url {
    NSMutableString* csv = [[NSMutableString alloc] init];
    [csv appendString:[NSString stringWithString:encounter.description]];
    [csv appendString:@"\nDIMER BASIS /au"];
    [csv appendString:@"\nDimer,"];
    if ([encounter getEnergyCount] >= 1) [csv appendString:[NSString stringWithFormat:@"%f", encounter.dimer]];
    [csv appendString:@"\nMonomer A,"];
    if ([encounter getEnergyCount] >= 2) [csv appendString:[NSString stringWithFormat:@"%f", encounter.monomerADimerBasis]];
    [csv appendString:@"\nMonomer B,"];
    if ([encounter getEnergyCount] >= 3) [csv appendString:[NSString stringWithFormat:@"%f", encounter.monomerBDimerBasis]];
    [csv appendString:@"\nMONOMER BASIS /au"];
    [csv appendString:@"\nMonomer A,"];
    if ([encounter getEnergyCount] >= 4) [csv appendString:[NSString stringWithFormat:@"%f", encounter.monomerAMonomerBasis]];
    [csv appendString:@"\nMonomer B,"];
    if ([encounter getEnergyCount] == 5) [csv appendString:[NSString stringWithFormat:@"%f", encounter.monomerBMonomerBasis]];
    [csv appendString:@"\nINTERACTION ENERGY"];
    [csv appendString:@"\n/au,"];
    if ([encounter getEnergyCount] >= 3) [csv appendString:[NSString stringWithFormat:@"%f", encounter.interactionEnergyHartrees]];
    [csv appendString:@"\n/kJ/mol,"];
    if ([encounter getEnergyCount] >= 3) [csv appendString:[NSString stringWithFormat:@"%f", encounter.interactionEnergyKjmol]];
    [csv appendString:@"\nBINDING CONSTANT"];
    [csv appendString:@"\n/1,"];
    if ([encounter getEnergyCount] >= 3) [csv appendString:[NSString stringWithFormat:@"%e", encounter.bindingConstant]];
    [csv writeToURL:url atomically:YES encoding:NSUTF8StringEncoding error:nil];
    [csv release];
}
@end
