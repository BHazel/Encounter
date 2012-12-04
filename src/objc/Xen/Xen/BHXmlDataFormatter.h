#import <Foundation/Foundation.h>
#import "BHIDataFormatter.h"

/*!
 @copyright (c) Benedict W. Hazel, 2011-2012
 @header BHXmlDataFormatter.h Exports counterpoise correction data to XML format.
 */

/*!
 @interface BHXmlDataFormatter
 @abstract Exports counterpoise correction data to extensible markup language format (XML).
 */
@interface BHXmlDataFormatter : NSObject <BHIDataFormatter>
/*!
 @method exportData:
 @abstract Exports the counterpoise correction calculation data in extensible markup language format (XML).
 @param encounter Instance of class implementing BHIEncounter containing the calculation data.
 @param url URL to write formatted data to.
 */
-(void)exportData:(id <BHIEncounter>)encounter output:(NSURL*)url;
@end
