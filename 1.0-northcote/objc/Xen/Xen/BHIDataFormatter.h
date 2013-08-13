#import <Foundation/Foundation.h>
#import "BHIEncounter.h"

/*!
 @copyright (c) Benedict W. Hazel, 2011-2012
 @header Protocol implemented by data exporting classes.
 */

/*!
 @protocol BHIDataFormatter
 @abstract Protocol implemented by data exporting classes.
 */
@protocol BHIDataFormatter <NSObject>
/*!
 @method exportData::
 @abstract Exports the counterpoise correction calculation data in the format supported by the class.
 @param encounter Instance of class implementing BHIEncounter containing the calculation data.
 @param url URL to write formatted data to.
 */
-(void)exportData:(id <BHIEncounter>)encounter output:(NSURL*)url;
@end
