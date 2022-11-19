
using System;
using System.Collections.Generic;

using iTin.Utilities.Pdf.Design.Text;

using iTin.Utilities.Pdf.Writer.ComponentModel;
using iTin.Utilities.Pdf.Writer.Operations.Insert;
using iTin.Utilities.Pdf.Writer.Operations.Replace;
using iTin.Utilities.Pdf.Writer.Operations.Set;

using iTin.Utilities.Writer.Abstractions.Input;

namespace iTin.Utilities.Pdf.Writer.Input
{
    /// <summary>
    /// Defines a generic input
    /// </summary>
    public interface IPdfInput : IInput
    {
        /// <summary>
        /// Gets input type.
        /// </summary>
        /// <value>
        /// An value of enumeration <see cref="KnownInputType"/> indicating type of the input.
        /// </value>
        new KnownInputType InputType { get; }

        /// <summary>
        /// Create a new <see cref="PdfInput"/> containing the selected pages.
        /// </summary>
        /// <param name="from">Start page</param>
        /// <param name="to">End page. If is <see langword="null"/> the last page will be used</param>
        /// <returns>
        /// A new instance of <see cref="PdfInput"/> containing a document containing the specified pages.
        /// </returns>
        PdfInput ExtractPages(int from, int? to = null);

        /// <summary>
        /// Returns total pages of this <see cref="PdfInput"/>.
        /// </summary>
        /// <returns>
        /// Total pages of this <see cref="PdfInput"/>.
        /// </returns>
        int NumberOfPages();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IPdfInputAction Insert(IInsert data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IPdfInputAction Replace(IReplace data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IPdfInputAction Set(ISet data);

        /// <summary>
        /// Gets the lines of text for this <see cref="PdfInput"/>, filtered values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If document has no pages</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> is <see langword="null"/></exception>
        IEnumerable<PdfTextLine> TextLines(Func<PdfTextLine, bool> predicate);

        /// <summary>
        /// Gets the lines of text for this <see cref="PdfInput"/>, optionally you can set both the start and end pages and a value indicating whether blank lines are included in the result.
        /// </summary>
        /// <param name="fromPage">Defines start page. If a value is not set, it will default to 1</param>
        /// <param name="toPage">Defines end page. If a value is not set, it will default to total document pages</param>
        /// <param name="removeEmptyLines">Indicates whether blank lines are included in the result. By default they are not included</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If document has no pages</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="fromPage"/> is less than one or is greater than the total number of pages of the document</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="toPage"/> is less than one or is greater than the total number of pages of the document</exception>
        IEnumerable<PdfTextLine> TextLines(int? fromPage = null, int? toPage = null, bool removeEmptyLines = true);
    }
}
