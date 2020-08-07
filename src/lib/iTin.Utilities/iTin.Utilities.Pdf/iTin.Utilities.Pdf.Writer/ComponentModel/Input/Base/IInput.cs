
namespace iTin.Utilities.Pdf.Writer.ComponentModel
{
    using System.IO;

    using iTin.Core.ComponentModel;
    using iTin.Core.ComponentModel.Results;

    using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Insert;
    using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Output;
    using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Replace;

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
        /// Try to insert an element in a input.
        /// </summary>
        /// <param name="data">Reference to insertable object information</param>
        /// <returns>
        /// <para>
        /// A <see cref="InsertResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return value is <see cref="InsertResultData"/>, which contains the operation result
        /// </para>
        /// </returns>
        InsertResult Insert(IInsert data);

        /// <summary>
        /// Try to replace an element in a input.
        /// </summary>
        /// <param name="data">Reference to replacement object information</param>
        /// <returns>
        /// <para>
        /// A <see cref="ReplaceResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return value is <see cref="ReplaceResultData"/>, which contains the operation result
        /// </para>
        /// </returns>
        ReplaceResult Replace(IReplace data);

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
        /// Convert this input into a stream object.
        /// </summary>
        /// <returns>
        /// A <see cref="Stream"/> that represents a input file.
        /// </returns>
        Stream ToStream();
    }
}
