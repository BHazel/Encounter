//
//  BHXmlDataFormatter.m
//  Xen
//
//  Created by Benedict Hazel on 22/11/2012.
//  Copyright (c) 2012 TransWandsworth. All rights reserved.
//

#import "BHXmlDataFormatter.h"

@implementation BHXmlDataFormatter
-(void)exportData:(id <BHIEncounter>)encounter output:(NSURL*)url {
    NSMutableString* xml = [[NSMutableString alloc] init];
    [xml appendString:@"<?xml version=\"1.0\" encoding=\"utf-8\"?>"];
    [xml appendString:@"<enc:Counterpoise xmlns:enc=\"http://encounter.codeplex.com\" enc:Description=\""];
    [xml appendString:encounter.description];
    [xml appendString:@"\">"];
    [xml appendString:@"<enc:Basis enc:Type=\"Dimer\">"];
    [xml appendString:@"<enc:Dimer>"];
    if ([encounter getEnergyCount] >= 1) [xml appendString:[NSString stringWithFormat:@"%f", encounter.dimer]];
    [xml appendString:@"</enc:Dimer>"];
    [xml appendString:@"<enc:MonomerA>"];
    if ([encounter getEnergyCount] >= 2) [xml appendString:[NSString stringWithFormat:@"%f", encounter.monomerADimerBasis]];
    [xml appendString:@"</enc:MonomerA>"];
    [xml appendString:@"<enc:MonomerB>"];
    if ([encounter getEnergyCount] >= 3) [xml appendString:[NSString stringWithFormat:@"%f", encounter.monomerBDimerBasis]];
    [xml appendString:@"</enc:MonomerB>"];
    [xml appendString:@"</enc:Basis>"];
    [xml appendString:@"<enc:Basis enc:Type=\"Monomer\">"];
    [xml appendString:@"<enc:MonomerA>"];
    if ([encounter getEnergyCount] >= 4) [xml appendString:[NSString stringWithFormat:@"%f", encounter.monomerAMonomerBasis]];
    [xml appendString:@"</enc:MonomerA>"];
    [xml appendString:@"<enc:MonomerB>"];
    if ([encounter getEnergyCount] == 5) [xml appendString:[NSString stringWithFormat:@"%f", encounter.monomerBMonomerBasis]];
    [xml appendString:@"</enc:MonomerB>"];
    [xml appendString:@"</enc:Basis>"];
    [xml appendString:@"<enc:InteractionEnergy>"];
    [xml appendString:@"<enc:Hartree>"];
    if ([encounter getEnergyCount] >= 3) [xml appendString:[NSString stringWithFormat:@"%f", encounter.interactionEnergyHartrees]];
    [xml appendString:@"</enc:Hartree>"];
    [xml appendString:@"<enc:Kjmol>"];
    if ([encounter getEnergyCount] >= 3) [xml appendString:[NSString stringWithFormat:@"%f", encounter.interactionEnergyKjmol]];
    [xml appendString:@"</enc:Kjmol>"];
    [xml appendString:@"</enc:InteractionEnergy>"];
    [xml appendString:@"<enc:BindingConstant>"];
    if ([encounter getEnergyCount] >= 3) [xml appendString:[NSString stringWithFormat:@"%e", encounter.bindingConstant]];
    [xml appendString:@"</enc:BindingConstant>"];
    [xml appendString:@"</enc:Counterpoise>"];
    [xml writeToURL:url atomically:YES encoding:NSUTF8StringEncoding error:nil];
    [xml release];
}
@end
