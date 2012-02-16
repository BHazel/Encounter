//
//  XenController.h
//  Xen
//
//  Created by Benedict Hazel on 08/02/2012.
//  Copyright (c) 2012 TransWandsworth. All rights reserved.
//

#import <Cocoa/Cocoa.h>
#import "BHEncounter.h"

@interface XenController : NSObject {
    BHEncounter* encounter;
    NSAlert* alert;
    
    IBOutlet NSWindow* mainWindow;
    IBOutlet NSMenuItem* openMenuItem;
    IBOutlet NSMenuItem* exportMenuItem;
    IBOutlet NSToolbarItem* openToolbarItem;
    IBOutlet NSToolbarItem* exportToolbarItem;
    IBOutlet NSTextField* txtDimerDimer;
    IBOutlet NSTextField* txtMonADimer;
    IBOutlet NSTextField* txtMonBDimer;
    IBOutlet NSTextField* txtMonAMon;
    IBOutlet NSTextField* txtMonBMon;
    IBOutlet NSTextField* txtInteractionHartree;
    IBOutlet NSTextField* txtInteractionKjmol;
    IBOutlet NSTextField* txtBindingConstant;
}
-(IBAction)actionOpen:(id)sender;
-(IBAction)actionExport:(id)sender;
-(void)openFile;
-(void)exportFile;
-(void)setUi;
-(void)resetUi;
@end
