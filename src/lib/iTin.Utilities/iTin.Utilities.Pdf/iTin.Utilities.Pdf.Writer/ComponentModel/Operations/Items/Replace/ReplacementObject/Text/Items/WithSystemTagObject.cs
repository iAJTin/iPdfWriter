﻿
namespace iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;

    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using iTextSharp.text.pdf.parser;

    using iTin.Core;
    using iTin.Core.Models.Design.Enums;

    using Design.Styles;
    using Helpers;
    using Result.Replace;

    using NativeRectangle = System.Drawing.RectangleF;
    using CustomLocationTextExtractionStrategy = LocationTextExtractionStrategy;

    /// <summary>
    /// Specialization of <see cref="SystemTagTextReplacementBase"/> interface.<br/>
    /// Contains the logic to replace a text with a <see cref="SystemTags"/>.
    /// </summary>
    internal class WithSystemTagObject : SystemTagTextReplacementBase
    {
        #region constructor/s

        #region [public] WithSystemTagObject(): Initializes a new instance of the class
        /// <summary>
        /// Initializes a new instance of the <see cref="WithSystemTagObject"/> class.
        /// </summary>
        public WithSystemTagObject()
        {
            Tag = SystemTags.None;
            UseTestMode = YesNo.No;
            TextOffset = PointF.Empty;
            Style = PdfTextStyle.Default;
            ReplaceOptions = ReplaceTextOptions.Default;
        }
        #endregion

        #endregion

        #region public static readonly properties

        #region [public] static (WithSystemTagObject) Default: Returns a new instance that contains the default values
        /// <summary>
        /// Returns a new instance of <see cref="WithSystemTagObject"/> class that contains the default values.
        /// </summary>
        /// <value>
        /// A <see cref="WithSystemTagObject"/> that contains the default values.
        /// </value>
        public static WithSystemTagObject Default => new WithSystemTagObject();
        #endregion

        #endregion

        #region public properties

        #region [public] (PointF) TextOffset: Gets or sets a reference a point structure which represents the new text offset
        /// <summary>
        /// Gets or sets a reference a point structure which represents the new text offset. The default is <see cref="PointF.Empty"/>.
        /// </summary>
        /// <value>
        /// A <see cref="PointF"/> object that contains new text offset to apply.
        /// </value>
        public PointF TextOffset { get; set; }
        #endregion

        #region [public] (YesNo) UseTestMode: Gets or sets a value that indicates whether the elements to be inserted are shown with a red border that identifies their position and size in order to validate that they are correct
        /// <summary>
        /// Gets or sets a value that indicates whether the elements to be inserted are shown with a red border that identifies their position and size in order to validate that they are correct. The default value is <see cref="YesNo.No"/>.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if works in mode test; otherwise <see cref="YesNo.No"/>.
        /// </value>
        public YesNo UseTestMode { get; set; }
        #endregion

        #endregion

        #region public new properties

        #region [public] {new} (PdfTextStyle) Style: Gets or sets a reference to new text style format
        /// <summary>
        /// Gets or sets a reference to new text style format. The default is <see cref="PdfTextStyle.Default"/>.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTextStyle"/> object that contains image offset to apply.
        /// </value>
        public new PdfTextStyle Style { get; set; }
        #endregion

        #endregion

        #region protected override methods

        #region [protected] {override} (ReplaceResult) ReplaceImpl(Stream, IInput): Implementation to execute when replace action
        /// <summary>
        /// Implementation to execute when replace action.
        /// </summary>
        /// <param name="input">stream input</param>
        /// <param name="context">Input context</param>
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
        protected override ReplaceResult ReplaceImpl(Stream input, IInput context)
        {
            if (Style == null)
            {
                Style = PdfTextStyle.Default;
            }

            return ReplaceImpl(context, input, Tag, ReplaceOptions, TextOffset, Style, UseTestMode);
        }

        #endregion

        #endregion

        #region private static methods

        private static ReplaceResult ReplaceImpl(IInput context, Stream input, SystemTags tag, ReplaceTextOptions options, PointF offset, PdfTextStyle style, YesNo useTestMode)
        {
            var outputStream = new MemoryStream();

            try
            {
                using (var reader = new PdfReader(input))
                using (var stamper = new PdfStamper(reader, outputStream))
                {
                    var pages = reader.NumberOfPages;
                    for (var page = 1; page <= pages; page++)
                    {
                        var strategy = new CustomLocationTextExtractionStrategy();
                        var cb = stamper.GetOverContent(page);

                        // Send some data contained in PdfContentByte, looks like the first is always cero for me and the second 100, 
                        // but i'm not sure if this could change in some cases.
                        strategy.UndercontentCharacterSpacing = cb.CharacterSpacing;
                        strategy.UndercontentHorizontalScaling = cb.HorizontalScaling;

                        // It's not really needed to get the text back, but we have to call this line ALWAYS,
                        // because it triggers the process that will get all chunks from PDF into our strategy Object
                        var allStrings = PdfTextExtractor.GetTextFromPage(reader, page, strategy);
                        var stringsArray = allStrings.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                        // The real getter process starts in the following line
                        var textMatchesFound = strategy.GetExtendedTextLocations(tag.GetDescription(), options).ToList();

                        // Text matches found contains all text with locations, so do whatever you want with it
                        foreach (var match in textMatchesFound)
                        {
                            // Delete tag
                            var bColor = BaseColor.WHITE;
                            cb.SetColorFill(bColor);
                            cb.Rectangle(match.Rect.Left, match.Rect.Bottom, match.Rect.Width, match.Rect.Height);
                            cb.Fill();

                            // Calculates new rectangle
                            var r = BuildRectangleByStrategies(match, tag.GetDescription(), strategy, cb, (string[])stringsArray.Clone(), options);

                            // Add table
                            var table = new PdfPTable(1) { TotalWidth = r.Width - offset.X };

                            // New text
                            string newText = string.Empty;
                            switch (tag)
                            {
                                case SystemTags.PageNumber:
                                    newText = page.ToString();
                                    break;

                                case SystemTags.TotalPages:
                                    newText = pages.ToString();
                                    break;
                            }

                            table.AddCell(PdfHelper.CreateCell(newText, style, useTestMode));
                            table.WriteSelectedRows(-1, -1, r.X + offset.X, r.Y - offset.Y, cb);
                            cb.Fill();
                        }

                        cb.Fill();
                        cb.Stroke();
                    }

                    stamper.Close();
                    reader.Close();
                }

                return ReplaceResult.CreateSuccessResult(new ReplaceResultData
                {
                    Context = context,
                    InputStream = input,
                    OutputStream = new MemoryStream(outputStream.GetBuffer())
                });
            }
            catch (Exception ex)
            {
                return ReplaceResult.FromException(
                    ex,
                    new ReplaceResultData
                    {
                        Context = context,
                        InputStream = input,
                        OutputStream = input
                    });
            }
        }

        private static NativeRectangle BuildRectangleByStrategies(CustomLocationTextExtractionStrategy.LocationTextResult reference, string text, CustomLocationTextExtractionStrategy strategy, PdfContentByte cb, IReadOnlyList<string> allStrings, ReplaceTextOptions options)
        {
            var x = reference.Rect.Left;
            var y = reference.Rect.Top;
            var w = reference.Rect.Width;
            var h = reference.Rect.Height;

            switch (options.StartStrategy)
            {
                case StartLocationStrategy.LeftMargin:
                    x = cb.PdfDocument.LeftMargin;
                    break;

                case StartLocationStrategy.PreviousElement:
                    CustomLocationTextExtractionStrategy.LocationTextResult previousMatchReference = null;

                    var index = -1;
                    for (var i = 0; i < allStrings.Count; i++)
                    {
                        string current = allStrings[i].Trim();
                        if (current.Equals(text.Trim(), options.Comparison))
                        {
                            index = i;
                            break;
                        }

                        //if (current.Contains(text.Trim()))
                        //{
                        //    index = i;
                        //    break;
                        //}
                    }

                    if (index == 0)
                    {
                        break;
                    }

                    var previousElementText = allStrings[index - 1];
                    if (!string.IsNullOrEmpty(previousElementText))
                    {
                        var nextMatchReferences = strategy.GetExtendedTextLocations(previousElementText, options);
                        previousMatchReference = nextMatchReferences.FirstOrDefault();
                    }

                    if (previousMatchReference == null)
                    {
                        break;
                    }

                    // Same line?
                    if (previousMatchReference.EndPoint.X >= reference.StartPoint.X)
                    {
                        break;
                    }

                    w = (reference.EndPoint.X + reference.TextChunk.CharSpaceWidth) - previousMatchReference.EndPoint.X;
                    break;
            }

            switch (options.EndStrategy)
            {
                case EndLocationStrategy.RightMargin:
                    w = cb.PdfDocument.PageSize.Width - (x + cb.PdfDocument.RightMargin);
                    break;

                case EndLocationStrategy.NextElement:
                    CustomLocationTextExtractionStrategy.LocationTextResult nextMatchReference = null;

                    var index = -1;
                    string nextElementText = string.Empty;
                    for (var i = 0; i < allStrings.Count; i++)
                    {
                        string currentLine = allStrings[i].Trim();
                        string[] currentElementsInSameLine = currentLine.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        int totalElementsInLine = currentElementsInSameLine.Length;
                        foreach (var currentLineElement in currentElementsInSameLine)
                        {
                            if (!currentLineElement.Equals(text.Trim(), options.Comparison))
                            {
                                continue;
                            }

                            index = i;
                            if (totalElementsInLine > 1)
                            {
                                nextElementText = currentElementsInSameLine[1];
                            }

                            break;
                        }

                        if (index != -1)
                        {
                            break;
                        }
                    }

                    if (index == allStrings.Count)
                    {
                        break;
                    }

                    if (!string.IsNullOrEmpty(nextElementText))
                    {
                        var nextMatchReferences = strategy.GetExtendedTextLocations(nextElementText.Trim(), options);
                        nextMatchReference = nextMatchReferences.FirstOrDefault();
                    }

                    if (nextMatchReference == null)
                    {
                        w = cb.PdfDocument.PageSize.Width - (x + cb.PdfDocument.RightMargin);
                        break;
                    }

                    // Same line?
                    if (nextMatchReference.StartPoint.X <= reference.EndPoint.X)
                    {
                        w = cb.PdfDocument.PageSize.Width - (x + cb.PdfDocument.RightMargin);
                        break;
                    }

                    // calculates new width
                    if (options.StartStrategy == StartLocationStrategy.LeftMargin)
                    {
                        w = nextMatchReference.StartPoint.X - (reference.StartPoint.X + reference.TextChunk.CharSpaceWidth) + (reference.StartPoint.X - cb.PdfDocument.LeftMargin);
                    }
                    else
                    {
                        w = nextMatchReference.StartPoint.X - (reference.StartPoint.X + reference.TextChunk.CharSpaceWidth);
                    }
                    break;
            }

            switch (options.VerticalStrategy)
            {
                case VerticalFineStrategy.Middle:
                    y -= reference.Rect.Height / 2.0f;
                    break;

                case VerticalFineStrategy.Bottom:
                    y -= reference.Rect.Height;
                    break;

                default:
                case VerticalFineStrategy.Top:
                    y += reference.TextChunk.CurFontSize / 4;
                    break;
            }

            return new NativeRectangle(x, y, w, h);
        }

        #endregion
    }
}
