//
//  XenController.m
//  Xen
//
//  Created by Benedict Hazel on 08/02/2012.
//  Copyright (c) 2012 TransWandsworth. All rights reserved.
//

#import "XenController.h"

@implementation XenController
-(void)awakeFromNib {
    encounter = [[BHEncounter alloc] init];
    alert = [[NSAlert alloc] init];
    [alert setIcon:[NSImage imageNamed:NSImageNameCaution]];
    [alert addButtonWithTitle:@"OK"];
    [encounter retain];
    [alert retain];
}

-(IBAction)actionOpen:(id)sender {
    [self openFile];
}

-(IBAction)actionExport:(id)sender {
    [self exportFile];
}

-(void)openFile {
    NSOpenPanel* openFile = [NSOpenPanel openPanel];
    [openFile beginSheetModalForWindow:mainWindow completionHandler:^(NSInteger result) {
        [openFile close];
        if (result == NSFileHandlingPanelOKButton) {
            @try {
                [encounter setEnergies:[[openFile URL] absoluteString]];
                [encounter setInteractionEnergies];
            }
            @catch (NSException* exception) {
                [alert setMessageText:[exception reason]];
                [alert beginSheetModalForWindow:mainWindow modalDelegate:self didEndSelector:nil contextInfo:nil];
                if ([encounter energyCount] >= 3) [encounter setInteractionEnergies];
            }
            @finally {
                /*** MODIFY C# and C++ ***/
                [self resetUi];
                [self setUi];
            }
        }
    }];
}

-(void)exportFile {
    NSSavePanel* saveFile = [NSSavePanel savePanel];
    [saveFile beginSheetModalForWindow:mainWindow completionHandler:^(NSInteger result) {
        [saveFile close];
        if (result == NSFileHandlingPanelOKButton) {
            NSString* csvFile;
            @try {
                csvFile = [encounter toCsv];
                [csvFile writeToURL:[saveFile URL] atomically:YES encoding:NSUTF8StringEncoding error:nil];
            }
            @catch (NSException *exception) {
                [alert setMessageText:[exception reason]];
                [alert beginSheetModalForWindow:mainWindow modalDelegate:self didEndSelector:nil contextInfo:nil];
            }
            @finally {
                [csvFile release];
            }
        }
    }];
}

-(void)setUi {
    if ([encounter energyCount] >= 1) [txtDimerDimer setDoubleValue:encounter.dimer];
    if ([encounter energyCount] >= 2) [txtMonADimer setDoubleValue:encounter.monomerADimerBasis];
    if ([encounter energyCount] >= 3) {
        [txtMonBDimer setDoubleValue:encounter.monomerBDimerBasis];
        if ([encounter energyCount] >= 4) [txtMonAMon setDoubleValue:encounter.monomerAMonomerBasis];
        if ([encounter energyCount] == 5) [txtMonBMon setDoubleValue:encounter.monomerBMonomerBasis];
        [txtInteractionHartree setDoubleValue:encounter.interactionEnergyHartrees];
        [txtInteractionKjmol setDoubleValue:encounter.interactionEnergyKJMol];
        [txtBindingConstant setDoubleValue:encounter.bindingConstant];
    }
}

-(void)resetUi {
    [txtDimerDimer setStringValue:@""];
    [txtMonADimer setStringValue:@""];
    [txtMonBDimer setStringValue:@""];
    [txtMonAMon setStringValue:@""];
    [txtMonBMon setStringValue:@""];
    [txtInteractionHartree setStringValue:@""];
    [txtInteractionKjmol setStringValue:@""];
    [txtBindingConstant setStringValue:@""];
}

-(void)dealloc {
    [encounter release];
    [alert release];
    [super dealloc];
}
@end
