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
                if ([encounter getEnergyCount] >= 3) [encounter setInteractionEnergies];
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
    NSArray* extensions = [[NSArray alloc] initWithObjects:@"csv", @"json", @"xml", nil];
    [saveFile setAllowedFileTypes:extensions];
    [saveFile setAccessoryView:formatsView];
    
    [saveFile beginSheetModalForWindow:mainWindow completionHandler:^(NSInteger result) {
        [saveFile close];
        if (result == NSFileHandlingPanelOKButton) {
            id<BHIDataFormatter> formatter;
            @try {
                if ([[formatsPopUpButton titleOfSelectedItem] isEqualToString:@"Comma Separated Values"]) {
                    formatter = [[BHCsvDataFormatter alloc] init];
                }
                else if ([[formatsPopUpButton titleOfSelectedItem] isEqualToString:@"JSON"]) {
                    formatter = [[BHJsonDataFormatter alloc] init];
                }
                else if ([[formatsPopUpButton titleOfSelectedItem] isEqualToString:@"XML"]) {
                    formatter = [[BHXmlDataFormatter alloc] init];
                }
                else {
                    [[NSException exceptionWithName:@"IllegalArgumentException" reason:@"Unknown file type selected" userInfo:nil] raise];
                }
                [formatter exportData:encounter output:[saveFile URL]];
            }
            @catch (NSException *exception) {
                [alert setMessageText:[exception reason]];
                [alert beginSheetModalForWindow:mainWindow modalDelegate:self didEndSelector:nil contextInfo:nil];
            }
            @finally {
                [formatter release];
                [extensions release];
            }
        }
    }];
}

-(void)setUi {
    [txtDescription setStringValue:encounter.description];
    if ([encounter getEnergyCount] >= 1) [txtDimerDimer setDoubleValue:encounter.dimer];
    if ([encounter getEnergyCount] >= 2) [txtMonADimer setDoubleValue:encounter.monomerADimerBasis];
    if ([encounter getEnergyCount] >= 3) {
        [txtMonBDimer setDoubleValue:encounter.monomerBDimerBasis];
        if ([encounter getEnergyCount] >= 4) [txtMonAMon setDoubleValue:encounter.monomerAMonomerBasis];
        if ([encounter getEnergyCount] == 5) [txtMonBMon setDoubleValue:encounter.monomerBMonomerBasis];
        [txtInteractionHartree setDoubleValue:encounter.interactionEnergyHartrees];
        [txtInteractionKjmol setDoubleValue:encounter.interactionEnergyKjmol];
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
