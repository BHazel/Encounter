// Xen 1.0.0.1 - move to UI code

#import "BHEncounter.h"

@implementation BHEncounter
@synthesize description=_description, dimer=_dimer, monomerADimerBasis=_monAdimer, monomerBDimerBasis=_monBdimer, monomerAMonomerBasis=_monAmonA, monomerBMonomerBasis=_monBmonB, interactionEnergyHartrees=_interactHartree, interactionEnergyKjmol=_interactKjmol, bindingConstant=_bindingConstant;

- (id)init {
    if (self == [super init]) {
        energyExpression = [NSRegularExpression regularExpressionWithPattern:@"-*\\d+\\.\\d+" options:0 error:nil];
        energyStrings = [[NSMutableArray alloc] init];
        [_description retain];
        [energyExpression retain];
        [energyStrings retain];
    }
    return self;
}

-(void)setEnergies:(NSString*)filename {
    // Load calcualtion file and clear energy array
    NSString* text = [[NSString alloc] initWithContentsOfURL:[NSURL URLWithString:filename] encoding:NSUTF8StringEncoding error:nil];
    NSArray* textLines = [text componentsSeparatedByString:@"\n"];
    NSString* line = nil;
    BOOL gaussianCalc = NO;
    int hyphenLines = 0;
    BOOL descriptionFound = NO;
    
    _description = [NSString stringWithString:@""];
    if ([energyStrings count] != 0) [energyStrings removeAllObjects];
    
    for (int i = 0; i < [textLines count]; i++) {
        line = [NSString stringWithString:[textLines objectAtIndex:i]];
        
        // Check for Gaussian calculation
        if ([line rangeOfString:@" Entering Gaussian System"].location == NSNotFound && !gaussianCalc) {
            [[NSException exceptionWithName:@"ArgumentException" reason:@"This is not a Gaussian calculation" userInfo:nil] raise];
        }
        else gaussianCalc = YES;
        
        // Store calculation description
        if (hyphenLines == 5) {
            _description = [NSString stringWithString:[line stringByTrimmingCharactersInSet:[NSCharacterSet whitespaceAndNewlineCharacterSet]]];
            hyphenLines = 0;
            descriptionFound = YES;
        }
        if ([line rangeOfString:@" ----"].location != NSNotFound && !descriptionFound) hyphenLines++;
        
        // Check for counterpoise calculation
        if ([line rangeOfString:@" #"].location != NSNotFound && [line rangeOfString:@"counterpoise=2"].location == NSNotFound) {
            [[NSException exceptionWithName:@"ArgumentException" reason:@"This is not a counterpoise calculation" userInfo:nil] raise];
        }
        
        // Store SCF Done Values
        if ([line rangeOfString:@" SCF Done:"].location != NSNotFound) {
            NSRange energy = [energyExpression rangeOfFirstMatchInString:line options:0 range:NSMakeRange(0, [line length])];
            [energyStrings addObject:[line substringWithRange:energy]];
        }
    }
    [text release];
    
    // Assign Values
    if ([energyStrings count] == 0) [[NSException exceptionWithName:@"ApplicationException" reason:@"No energy values found" userInfo:nil] raise];
    if ([energyStrings count] >= 1) _dimer = [[energyStrings objectAtIndex:0] doubleValue];
    if ([energyStrings count] >= 2) _monAdimer = [[energyStrings objectAtIndex:1] doubleValue];
    if ([energyStrings count] < 3) [[NSException exceptionWithName:@"ApplicationException" reason:@"Incomplete dataset found, from which interaction energy cannot be calculated" userInfo:nil] raise];
    if ([energyStrings count] >= 3) _monBdimer = [[energyStrings objectAtIndex:2] doubleValue];
    if ([energyStrings count] >= 4) _monAmonA = [[energyStrings objectAtIndex:3] doubleValue];
    if ([energyStrings count] < 5) [[NSException exceptionWithName:@"ApplicationException" reason:@"Incomplete dataset found, but interaction energy can be calculated" userInfo:nil] raise];
    if ([energyStrings count] == 5) _monBmonB = [[energyStrings objectAtIndex:4] doubleValue];
}

-(void)setInteractionEnergies {
    _interactHartree = _dimer - (_monAdimer + _monBdimer);
    _interactKjmol = _interactHartree * 2625.5;
    _bindingConstant = exp((_interactKjmol * 1000) / (-1 * 8.314 * 298));
}

-(NSString*)toCsv {
    NSMutableString* csv = [[NSMutableString alloc] init];
    [csv appendString:[NSString stringWithString:self.description]];
    [csv appendString:@"\nDIMER BASIS /au"];
    [csv appendString:@"\nDimer,"];
    if ([energyStrings count] >= 1) [csv appendString:[NSString stringWithFormat:@"%f", self.dimer]];
    [csv appendString:@"\nMonomer A,"];
    if ([energyStrings count] >= 2) [csv appendString:[NSString stringWithFormat:@"%f", self.monomerADimerBasis]];
    [csv appendString:@"\nMonomer B,"];
    if ([energyStrings count] >= 3) [csv appendString:[NSString stringWithFormat:@"%f", self.monomerBDimerBasis]];
    [csv appendString:@"\nMONOMER BASIS /au"];
    [csv appendString:@"\nMonomer A,"];
    if ([energyStrings count] >= 4) [csv appendString:[NSString stringWithFormat:@"%f", self.monomerAMonomerBasis]];
    [csv appendString:@"\nMonomer B,"];
    if ([energyStrings count] == 5) [csv appendString:[NSString stringWithFormat:@"%f", self.monomerBMonomerBasis]];
    [csv appendString:@"\nINTERACTION ENERGY"];
    [csv appendString:@"\n/au,"];
    if ([energyStrings count] >= 3) [csv appendString:[NSString stringWithFormat:@"%f", self.interactionEnergyHartrees]];
    [csv appendString:@"\n/kJ/mol,"];
    if ([energyStrings count] >= 3) [csv appendString:[NSString stringWithFormat:@"%f", self.interactionEnergyKjmol]];
    [csv appendString:@"\nBINDING CONSTANT"];
    [csv appendString:@"\n/1,"];
    if ([energyStrings count] >= 3) [csv appendString:[NSString stringWithFormat:@"%e", self.bindingConstant]];
    return csv;
}

-(NSString*)toJson {
    NSMutableString* json = [[NSMutableString alloc] init];
    [json appendString:@"{"];
    [json appendString:@"\n\t\"Description\" : \""];
    [json appendString:self.description];
    [json appendString:@"\","];
    [json appendString:@"\n\t\"Basis\" : ["];
    [json appendString:@"\n\t\t{ \"Type\" : \"Dimer\", \"Dimer\" : \""];
    if ([energyStrings count] >= 1) [json appendString:[NSString stringWithFormat:@"%f", self.dimer]];
    [json appendString:@"\", \"MonomerA\" : \""];
    if ([energyStrings count] >= 2) [json appendString:[NSString stringWithFormat:@"%f", self.monomerADimerBasis]];
    [json appendString:@"\", \"MonomerB\" : \""];
    if ([energyStrings count] >= 3) [json appendString:[NSString stringWithFormat:@"%f", self.monomerBDimerBasis]];
    [json appendString:@"\" },"];
    [json appendString:@"\n\t\t{ \"Type\" : \"Monomer\", \"MonomerA\" : \""];
    if ([energyStrings count] >= 4) [json appendString:[NSString stringWithFormat:@"%f", self.monomerAMonomerBasis]];
    [json appendString:@"\", \"MonomerB\" : \""];
    if ([energyStrings count] == 5) [json appendString:[NSString stringWithFormat:@"%f", self.monomerBMonomerBasis]];
    [json appendString:@"\" }"];
    [json appendString:@"\n\t],"];
    [json appendString:@"\n\t\"InteractionEnergy\" : {"];
    [json appendString:@"\n\t\t\"Hartree\" : \""];
    if ([energyStrings count] >= 3) [json appendString:[NSString stringWithFormat:@"%f", self.interactionEnergyHartrees]];
    [json appendString:@"\","];
    [json appendString:@"\n\t\t\"Kjmol\" : \""];
    if ([energyStrings count] >= 3) [json appendString:[NSString stringWithFormat:@"%f", self.interactionEnergyKjmol]];
    [json appendString:@"\""];
    [json appendString:@"\n\t},"];
    [json appendString:@"\n\t\"BindingConstant\" : \""];
    if ([energyStrings count] >= 3) [json appendString:[NSString stringWithFormat:@"%e", self.bindingConstant]];
    [json appendString:@"\""];
    [json appendString:@"\n}"];
    return json;
}

-(NSString*)toXml {
    NSMutableString* xml = [[NSMutableString alloc] init];
    [xml appendString:@"<?xml version=\"1.0\" encoding=\"utf-8\"?>"];
    [xml appendString:@"<enc:Counterpoise xmlns:enc=\"http://encounter.codeplex.com\" enc:Description=\""];
    [xml appendString:self.description];
    [xml appendString:@"\">"];
    [xml appendString:@"<enc:Basis enc:Type=\"Dimer\">"];
    [xml appendString:@"<enc:Dimer>"];
    if ([energyStrings count] >= 1) [xml appendString:[NSString stringWithFormat:@"%f", self.dimer]];
    [xml appendString:@"</enc:Dimer>"];
    [xml appendString:@"<enc:MonomerA>"];
    if ([energyStrings count] >= 2) [xml appendString:[NSString stringWithFormat:@"%f", self.monomerADimerBasis]];
    [xml appendString:@"</enc:MonomerA>"];
    [xml appendString:@"<enc:MonomerB>"];
    if ([energyStrings count] >= 3) [xml appendString:[NSString stringWithFormat:@"%f", self.monomerBDimerBasis]];
    [xml appendString:@"</enc:MonomerB>"];
    [xml appendString:@"</enc:Basis>"];
    [xml appendString:@"<enc:Basis enc:Type=\"Monomer\">"];
    [xml appendString:@"<enc:MonomerA>"];
    if ([energyStrings count] >= 4) [xml appendString:[NSString stringWithFormat:@"%f", self.monomerAMonomerBasis]];
    [xml appendString:@"</enc:MonomerA>"];
    [xml appendString:@"<enc:MonomerB>"];
    if ([energyStrings count] == 5) [xml appendString:[NSString stringWithFormat:@"%f", self.monomerBMonomerBasis]];
    [xml appendString:@"</enc:MonomerB>"];
    [xml appendString:@"</enc:Basis>"];
    [xml appendString:@"<enc:InteractionEnergy>"];
    [xml appendString:@"<enc:Hartree>"];
    if ([energyStrings count] >= 3) [xml appendString:[NSString stringWithFormat:@"%f", self.interactionEnergyHartrees]];
    [xml appendString:@"</enc:Hartree>"];
    [xml appendString:@"<enc:Kjmol>"];
    if ([energyStrings count] >= 3) [xml appendString:[NSString stringWithFormat:@"%f", self.interactionEnergyKjmol]];
    [xml appendString:@"</enc:Kjmol>"];
    [xml appendString:@"</enc:InteractionEnergy>"];
    [xml appendString:@"<enc:BindingConstant>"];
    if ([energyStrings count] >= 3) [xml appendString:[NSString stringWithFormat:@"%e", self.bindingConstant]];
    [xml appendString:@"</enc:BindingConstant>"];
    [xml appendString:@"</enc:Counterpoise>"];    
    return xml;
}

-(int)energyCount {
    return (int)[energyStrings count];
}

- (void)dealloc {
    [_description release];
    [energyExpression release];
    [energyStrings release];
    [super dealloc];
}

@end
