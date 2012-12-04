#import <Cocoa/Cocoa.h>
#import "BHEncounter.h"
#import "BHIDataFormatter.h"
#import "BHCsvDataFormatter.h"
#import "BHJsonDataFormatter.h"
#import "BHXmlDataFormatter.h"

/*!
 @copyright (c) Benedict W. Hazel, 2011-2012
 @header XenController Main application controller.
 */

/*!
 @interface XenController
 @abstract Main application controller.
 */
@interface XenController : NSObject {
    /*!
     @var encounter
     @abstract BHEncounter instance
     */
    BHEncounter* encounter;
    
    /*!
     @var alert
     @abstract Displays messages via a sheet.
     */
    NSAlert* alert;
    
    /*!
     @var mainWindow
     @abstract Main application window.
     */
    IBOutlet NSWindow* mainWindow;
    
    /*!
     @var openMenuItem
     @abstract File->Open menu item.
     */
    IBOutlet NSMenuItem* openMenuItem;
    
    /*!
     @var exportMenuItem
     @abstract File->Export menu item.
     */
    IBOutlet NSMenuItem* exportMenuItem;
    
    /*!
     @var openToolbarItem
     @abstract Open toolbar item.
     */
    IBOutlet NSToolbarItem* openToolbarItem;
    
    /*!
     @var exportToolbarItem
     @abstract Export toolbar item.
     */
    IBOutlet NSToolbarItem* exportToolbarItem;
    
    /*!
     @var txtDescription
     @abstract Description text field.
     */
    IBOutlet NSTextField* txtDescription;
    
    /*!
     @var txtDimerDimer
     @abstract Dimer energy in dimer basis text field.
     */
    IBOutlet NSTextField* txtDimerDimer;
    
    /*!
     @var txtMonADimer
     @abstract Monomer A energy in dimer basis text field.
     */
    IBOutlet NSTextField* txtMonADimer;
    
    /*!
     @var txtMonBDimer
     @abstract Monomer B energy in dimer basis text field.
     */
    IBOutlet NSTextField* txtMonBDimer;
    
    /*!
     @var txtMonAMon
     @abstract Monomer A energy in monomer A basis text field.
     */
    IBOutlet NSTextField* txtMonAMon;
    
    /*!
     @var txtMonBMon
     @abstract Monomer B energy in monomer B basis text field.
     */
    IBOutlet NSTextField* txtMonBMon;
    
    /*!
     @var txtInteractionHartree
     @abstract Interaction energy in Hartree atomic units text field.
     */
    IBOutlet NSTextField* txtInteractionHartree;
    
    /*!
     @var txtInteractionKjmol
     @abstract Interaction energy in kJ/mol text field.
     */
    IBOutlet NSTextField* txtInteractionKjmol;
    
    /*!
     @var txtBindingConstant
     @abstract Binding constant text field.
     */
    IBOutlet NSTextField* txtBindingConstant;
    
    /*!
     @var formatsView
     @abstract Output formats view for NSSavePanel when exporting counterpoise correction data.
     */
    IBOutlet NSView* formatsView;
    
    /*!
     @var formatsPopUpButton
     @abstract Pop-up button contaiing formats to export displayed in formatsView view.
     */
    IBOutlet NSPopUpButton* formatsPopUpButton;
}

/*!
 @method actionOpen:
 @abstract Handles the File->Open menu command: opens a file for processing.
 @param sender Source of the event.
 */
-(IBAction)actionOpen:(id)sender;

/*!
 @method actionExport:
 @abstract Handles the File->Export menu command: exports data in the BHIEncounter instance to a file.
 @param sender Source of the event.
 */
-(IBAction)actionExport:(id)sender;

/*!
 @method openFile
 @abstract Opens a file using an NSOpenPanel and sets the UI if successful.
 */
-(void)openFile;

/*!
 @method exportFile
 @abstract Exports a file using an NSSavePanel.
 */
-(void)exportFile;

/*!
 @method setUi
 @abstract Sets the text fields on the Xen window depending on the number of energy values in the BHIEncounter instance.
 */
-(void)setUi;

/*!
 @method resetUi
 @abstract Sets all text fields on the Xen window to empty.	
 */
-(void)resetUi;
@end
