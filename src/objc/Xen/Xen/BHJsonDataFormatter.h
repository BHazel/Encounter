#import <Foundation/Foundation.h>
#import "BHIDataFormatter.h"

/*!
 @copyright (c) Benedict W. Hazel, 2011-2012
 @header BHJsonDataFormatter.h Exports counterpoise correction data to JSON format.
 */

/*!
 @interface BHJsonDataFormatter
 @abstract Exports counterpoise correction data to JavaScript object notation format (JSON).
 */
@interface BHJsonDataFormatter : NSObject <BHIDataFormatter>
/*!
 @method exportData:
 @abstract Exports the counterpoise correction calculation data in JavaScript object notation format (JSON).
 @param encounter Instance of class implementing BHIEncounter containing the calculation data.
 @param url URL to write formatted data to.
 */
-(void)exportData:(id <BHIEncounter>)encounter output:(NSURL*)url;
@end
