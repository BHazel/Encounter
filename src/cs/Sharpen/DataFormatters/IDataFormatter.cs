// <copyright file="IDataFormatter.cs" company="Benedict W. Hazel">
//      Benedict W. Hazel, 2011-2012
// </copyright>
// <author>Benedict W. Hazel</author>
// <summary>
//      IDataFormatter: Interface implemented by data exporting classes.
// </summary>

using System.IO;

namespace BWHazel.Sharpen.DataFormatters
{
    /// <summary>
    /// Defines methods implemented by data exporting classes.
    /// </summary>
    public interface IDataFormatter
    {
        /// <summary>
        /// Exports the counterpoise correction calculation data in the format supported by the class.
        /// </summary>
        /// <param name="encounter">Instance of class implementing <see cref="IEncounter"/> containing the calculation data.</param>
        /// <param name="stream">Stream to write formatted data into.</param>
        void ExportData(IEncounter encounter, Stream stream);
    }
}
