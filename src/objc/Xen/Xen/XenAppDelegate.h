#import <Cocoa/Cocoa.h>

/*!
 @copyright (c) Benedict W. Hazel, 2011-2012
 @header XenAppDelegate.h Main application delegate.
 */

/*!
 @interface XenAppDelegate
 @abstract Main application delegate.
 */
@interface XenAppDelegate : NSObject <NSApplicationDelegate> {
@private
    /*!
     @var window
     @abstract Main application window.
     */
    NSWindow *window;
}

/*!
 @property window
 @abstract Gets or sets the main application window.
 */
@property (assign) IBOutlet NSWindow *window;

@end
