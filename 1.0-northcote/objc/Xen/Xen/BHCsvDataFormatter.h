#import <Foundation/Foundation.h>
#import "BHIDataFormatter.h"

/*!
 @copyright (c) Benedict W. Hazel, 2011-2012
 @header BHCsvDataFormatter.h Exports counterpoise correction data to CSV format.
 */

/*!
 @interface BHCsvDataFormatter
 @abstract Exports counterpoise correction data to comma-separated values format (CSV).
 */
@interface BHCsvDataFormatter : NSObject <BHIDataFormatter>
/*!
 @method exportData:
 @abstract Exports the counterpoise correction calculation data in comma-separated values format (CSV).
 @param encounter Instance of class implementing BHIEncounter containing the calculation data.
 @param url URL to write formatted data to.
 */
-(void)exportData:(id <BHIEncounter>)encounter output:(NSURL*)url;
@end
