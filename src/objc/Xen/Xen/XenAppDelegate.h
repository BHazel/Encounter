//
//  XenAppDelegate.h
//  Xen
//
//  Created by Benedict Hazel on 19/06/2011.
//  Copyright 2011 TransWandsworth. All rights reserved.
//

#import <Cocoa/Cocoa.h>

@interface XenAppDelegate : NSObject <NSApplicationDelegate> {
@private
    NSWindow *window;
}

@property (assign) IBOutlet NSWindow *window;

@end
