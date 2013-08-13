//
//  JsonDataFormatter.m
//  Xen
//
//  Created by Benedict Hazel on 22/11/2012.
//  Copyright (c) 2012 TransWandsworth. All rights reserved.
//

#import "BHJsonDataFormatter.h"

@implementation BHJsonDataFormatter
-(void)exportData:(id <BHIEncounter>)encounter output:(NSURL*)url {
    NSMutableString* json = [[NSMutableString alloc] init];
    [json appendString:@"{"];
    [json appendString:@"\n\t\"Description\" : \""];
    [json appendString:encounter.description];
    [json appendString:@"\","];
    [json appendString:@"\n\t\"Basis\" : ["];
    [json appendString:@"\n\t\t{ \"Type\" : \"Dimer\", \"Dimer\" : \""];
    if ([encounter getEnergyCount] >= 1) [json appendString:[NSString stringWithFormat:@"%f", encounter.dimer]];
    [json appendString:@"\", \"MonomerA\" : \""];
    if ([encounter getEnergyCount] >= 2) [json appendString:[NSString stringWithFormat:@"%f", encounter.monomerADimerBasis]];
    [json appendString:@"\", \"MonomerB\" : \""];
    if ([encounter getEnergyCount] >= 3) [json appendString:[NSString stringWithFormat:@"%f", encounter.monomerBDimerBasis]];
    [json appendString:@"\" },"];
    [json appendString:@"\n\t\t{ \"Type\" : \"Monomer\", \"MonomerA\" : \""];
    if ([encounter getEnergyCount] >= 4) [json appendString:[NSString stringWithFormat:@"%f", encounter.monomerAMonomerBasis]];
    [json appendString:@"\", \"MonomerB\" : \""];
    if ([encounter getEnergyCount] == 5) [json appendString:[NSString stringWithFormat:@"%f", encounter.monomerBMonomerBasis]];
    [json appendString:@"\" }"];
    [json appendString:@"\n\t],"];
    [json appendString:@"\n\t\"InteractionEnergy\" : {"];
    [json appendString:@"\n\t\t\"Hartree\" : \""];
    if ([encounter getEnergyCount] >= 3) [json appendString:[NSString stringWithFormat:@"%f", encounter.interactionEnergyHartrees]];
    [json appendString:@"\","];
    [json appendString:@"\n\t\t\"Kjmol\" : \""];
    if ([encounter getEnergyCount] >= 3) [json appendString:[NSString stringWithFormat:@"%f", encounter.interactionEnergyKjmol]];
    [json appendString:@"\""];
    [json appendString:@"\n\t},"];
    [json appendString:@"\n\t\"BindingConstant\" : \""];
    if ([encounter getEnergyCount] >= 3) [json appendString:[NSString stringWithFormat:@"%e", encounter.bindingConstant]];
    [json appendString:@"\""];
    [json appendString:@"\n}"];
    [json writeToURL:url atomically:YES encoding:NSUTF8StringEncoding error:nil];
    [json release];
}
@end
