// Xen 1.0.0.1 - move to UI code

#import "BHEncounter.h"

@implementation BHEncounter
@synthesize dimer=_dimer, monomerADimerBasis=_monAdimer, monomerBDimerBasis=_monBdimer, monomerAMonomerBasis=_monAmonA, monomerBMonomerBasis=_monBmonB, interactionEnergyHartrees=_interactHartree, interactionEnergyKJMol=_interactKjmol, bindingConstant=_bindingConstant;

- (id)init {
    if (self == [super init]) {
        energyExpression = [NSRegularExpression regularExpressionWithPattern:@"-*\\d+\\.\\d+" options:0 error:nil];
        energyStrings = [[NSMutableArray alloc] init];
        [energyExpression retain];
        [energyStrings retain];
    }
    return self;
}

-(void)setEnergies:(NSString*)filename {
    // Load calcualtion file and clear energy array
    NSString* text = [[NSString alloc] initWithContentsOfURL:[NSURL URLWithString:filename] encoding:NSUTF8StringEncoding error:nil];
    // *** CHECK C#, C++, and JAVA AND ADD IF NECESSARY ***
    if ([energyStrings count] != 0) [energyStrings removeAllObjects];
    
    // Check for Gaussian calculation
    if ([text rangeOfString:@" Entering Gaussian System"].location == NSNotFound) {
        [[NSException exceptionWithName:@"ArgumentException" reason:@"This is not a Gaussian calculation" userInfo:nil] raise];
    }
    
    // Check for counterpoise calculation
    if ([text rangeOfString:@"counterpoise=2"].location == NSNotFound) {
        [[NSException exceptionWithName:@"ArgumentException" reason:@"This is not a counterpoise calculation" userInfo:nil] raise];
    }
    
    // Store SCF Done Values
    NSRegularExpression* scfExpression = [NSRegularExpression regularExpressionWithPattern:@"SCF Done:" options:0 error:nil];
    NSArray* scfMatches = [scfExpression matchesInString:text options:0 range:NSMakeRange(0, [text length])];
    for (NSTextCheckingResult* match in scfMatches) {
        NSRange energyRange = [energyExpression rangeOfFirstMatchInString:text options:0 range:NSMakeRange([match range].location, (NSUInteger)66)];
        [energyStrings addObject:[text substringWithRange:energyRange]];
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
    [csv appendString:@"DIMER BASIS /au"];
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
    if ([energyStrings count] >= 3) [csv appendString:[NSString stringWithFormat:@"%f", self.interactionEnergyKJMol]];
    [csv appendString:@"\nBINDING CONSTANT"];
    [csv appendString:@"\n/1,"];
    if ([energyStrings count] >= 3) [csv appendString:[NSString stringWithFormat:@"%f", self.bindingConstant]];
    return csv;
}

-(int)energyCount {
    return (int)[energyStrings count];
}

- (void)dealloc {
    [energyExpression release];
    [energyStrings release];
    [super dealloc];
}

@end
