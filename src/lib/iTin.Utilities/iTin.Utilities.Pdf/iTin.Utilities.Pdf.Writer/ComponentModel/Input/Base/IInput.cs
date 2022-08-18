
using System;
using System.Collections.Generic;
using System.IO;

using iTin.Core.ComponentModel;
using iTin.Core.ComponentModel.Results;

using iTin.Utilities.Pdf.Design.Text;
using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Output;

namespace iTin.Utilities.Pdf.Writer.ComponentModel.Input
{
    /// <summary>
    /// Defines a generic input
    /// </summary>
    public interface IInput
    {
        /// <summary>
        /// Gets or sets a value indicating whether automatic updates for changes.
        /// </summary>
        /// <value>
        /// <b>true</b> if automatic update changes; otherwise, <b>false</b>.
        /// </value>
        bool AutoUpdateChanges { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether delete physical files after merge.
        /// </summary>
        /// <value>
        /// <b>true</b> if delete physical files after merge; otherwise, <b>false</b>.
        /// </value>
        bool DeletePhysicalFilesAfterMerge { get; set; }

        /// <summary>
        /// Gets or sets a value that contains input index.
        /// </summary>
        /// <value>
        /// A <see cref="int"/> that contains input index.
        /// </value>
        int Index { get; set; }

        /// <summary>
        /// Gets or sets the input object.
        /// </summary>
        /// <value>
        /// The input.
        /// </value>
        object Input { get; set; }

        /// <summary>
        /// Gets input type.
        /// </summary>
        /// <value>
        /// An value of enumeration <see cref="KnownInputType"/> indicating type of the input.
        /// </value>
        KnownInputType InputType { get; }

        /// <summary>
        /// Returns a new reference <see cref="OutputResult"/> that complies with what is indicated in its configuration object. By default, an <see cref="IInput"/> will not be zipped.
        /// </summary>
        /// <param name="config">The output result configuration.</param>
        /// <returns>
        /// <para>
        /// A <see cref="OutputResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return value is <see cref="OutputResultData"/>, which contains the operation result
        /// </para>
        /// </returns>
        OutputResult CreateResult(OutputResultConfig config = null);

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
        /// Saves this input into a file.
        /// </summary>
        /// <param name="outputPath">The output path. The use of the <b>~</b> character is allowed to indicate relative paths, and you can also use <b>UNC</b> path.</param>
        /// <param name="options">Save options</param>
        /// <returns>
        /// <para>
        /// A <see cref="BooleanResult"/> which implements the <see cref="IResult"/> interface reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return value is <see cref="bool"/>, which contains the operation result
        /// </para>
        /// </returns>
        IResult SaveToFile(string outputPath, SaveOptions options = null);

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

        /// <summary>
        /// Convert this input into a stream object.
        /// </summary>
        /// <returns>
        /// A <see cref="Stream"/> that represents a input file.
        /// </returns>
        Stream ToStream();
    }
}
