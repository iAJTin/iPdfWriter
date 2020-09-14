
namespace iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;

    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using iTextSharp.text.pdf.parser;

    using iTin.Core.Drawing.Helpers;
    using iTin.Core.Models.Design.Enums;

    using Design.Styles;
    using Design.Table;
    using Helpers;
    using Result.Replace;

    using CustomLocationTextExtractionStrategy = LocationTextExtractionStrategy;

    /// <summary>
    /// Specialization of <see cref="TextReplacementBase"/> interface.<br/>
    /// Contains the logic to replace a text with a pdf table.
    /// </summary>
    public class WithTableObject : TextReplacementBase
    {
        #region private constants
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const float DefaultFixedWidth = -float.MinValue;
        #endregion

        #region constructor/s

        #region [public] WithTableObject(): Initializes a new instance of the class
        /// <summary>
        /// Initializes a new instance of the <see cref="WithTableObject"/> class.
        /// </summary>
        public WithTableObject()
        {
            UseTestMode = YesNo.No;
            TableOffset = PointF.Empty;
            Style = PdfTableStyle.Default;
            FixedWidth = DefaultFixedWidth;
            ReplaceOptions = ReplaceTextOptions.Default;
        }
        #endregion

        #endregion

        #region public static readonly properties

        #region [public] static (WithTableObject) Default: Returns a new instance that contains the default values
        /// <summary>
        /// Returns a new instance of <see cref="WithTableObject"/> class that contains the default values.
        /// </summary>
        /// <value>
        /// A <see cref="WithTableObject"/> that contains the default values.
        /// </value>
        public static WithTableObject Default => new WithTableObject();
        #endregion

        #endregion

        #region public properties

        #region [public] (float) FixedWidth: Gets or sets a value that contains the table fixed width to use
        /// <summary>
        /// Gets or sets a value that contains the table fixed width to use. The default value is -<see cref="float.MinValue"/> (No use fixed width).
        /// </summary>
        /// <value>
        /// A <see cref="float"/> object that contains the table fixed width to use.
        /// </value>
        public float FixedWidth { get; set; }
        #endregion

        #region [public] (PdfTable) Table: Gets or sets a value that contains the table to replace the text
        /// <summary>
        /// Gets or sets a value that contains the table to replace the text.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTable"/> object that contains the table to replace the text.
        /// </value>
        public PdfTable Table { get; set; }
        #endregion

        #region [public] (PointF) TableOffset: Gets or sets a reference a point structure which represents the table offset
        /// <summary>
        /// Gets or sets a reference a point structure which represents the table offset. The default is <see cref="PointF.Empty"/>.
        /// </summary>
        /// <value>
        /// A <see cref="PointF"/> object that contains table offset to apply.
        /// </value>
        public PointF TableOffset { get; set; }
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

        #region [public] {new} (PdfTableStyle) Style: Gets or sets a reference to new text style format
        /// <summary>
        /// Gets or sets a reference to new text style format. The default is <see cref="PdfTableStyle.Default"/>.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTableStyle"/> object that contains image offset to apply.
        /// </value>
        public new PdfTableStyle Style { get; set; }
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
                Style = PdfTableStyle.Default;
            }

            return ReplaceImpl(context, input, Text, ReplaceOptions, Table, FixedWidth, TableOffset, Style, UseTestMode);
        }

        #endregion

        #endregion

        #region private static methods

        private static ReplaceResult ReplaceImpl(IInput context, Stream input, string oldText, ReplaceTextOptions options, PdfTable table, float fixedWidth, PointF tableOffset, PdfTableStyle style, YesNo useTestMode)
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
                        var textMatchesFound = strategy.GetExtendedTextLocations(oldText, options).ToList();

                        // MatchesFound contains all text with locations
                        foreach (var match in textMatchesFound)
                        {
                            // Delete tag
                            var bColor = BaseColor.WHITE;
                            cb.SetColorFill(bColor);
                            cb.Rectangle(match.Rect.Left, match.Rect.Bottom, match.Rect.Width, match.Rect.Height);
                            cb.Fill();

                            // Calculates new rectangle
                            var deltaY = CalculatesVerticalDelta(options, match.Rect);
                            var cellHeight = CalculatesCellHeight(match, oldText, strategy, cb, (string[])stringsArray.Clone(), options, deltaY);
                            var r = BuildRectangleByStrategies(match, oldText, strategy, cb, (string[])stringsArray.Clone(), options);

                            // Width strategy to use
                            var safeFixedWidth = fixedWidth;
                            var useFixedWidth = !fixedWidth.Equals(DefaultFixedWidth);
                            if (useFixedWidth)
                            {
                                if (fixedWidth > r.Width)
                                {
                                    safeFixedWidth = r.Width;
                                }
                            }
                            else
                            {
                                safeFixedWidth = r.Width; 
                            }

                            // Creates aligned table by horizontal alignment value (this table contains the user table parameter)
                            var outerBorderTable = new PdfPTable(1)
                            {
                                TotalWidth = safeFixedWidth,
                                HorizontalAlignment = Element.ALIGN_LEFT
                            };

                            var outerCell = PdfHelper.CreateEmptyWithBorderCell(style.Borders);
                            outerCell.MinimumHeight = cellHeight;
                            outerCell.VerticalAlignment = style.Alignment.Vertical.ToVerticalTableAlignment();
                            outerCell.BackgroundColor = new BaseColor(ColorHelper.GetColorFromString(style.Content.Color));

                            //table.Table.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.Table.TotalWidth = safeFixedWidth - (outerCell.EffectivePaddingRight + outerCell.EffectivePaddingLeft) * 2;
                            table.Table.LockedWidth = true; // options.StartStrategy.Equals(StartLocationStrategy.LeftMargin) && options.EndStrategy.Equals(EndLocationStrategy.RightMargin);
                            outerCell.AddElement(table.Table);
                            outerBorderTable.AddCell(outerCell);

                            // Creates strategy table (for shows testmode rectangle)
                            var useTestModeTable = new PdfPTable(1) { TotalWidth = safeFixedWidth  };
                            var useTestCell = PdfHelper.CreateEmptyCell(useTestMode);
                            
                            if (table.Configuration.HeightStrategy == TableHeightStrategy.Exact)
                            {
                                useTestCell.FixedHeight = table.Table.TotalHeight;
                            }

                            useTestCell.AddElement(outerBorderTable);
                            useTestModeTable.AddCell(useTestCell);
                            useTestModeTable.WriteSelectedRows(-1, -1, r.X + tableOffset.X, r.Y - tableOffset.Y - deltaY, cb);

                            cb.Fill();
                        }

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

        private static RectangleF BuildRectangleByStrategies(CustomLocationTextExtractionStrategy.LocationTextResult reference, string text, CustomLocationTextExtractionStrategy strategy, PdfContentByte cb, IReadOnlyList<string> allStrings, ReplaceTextOptions options)
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

            return new RectangleF(x, y, w, h);
        }

        private static float CalculatesVerticalDelta(ReplaceTextOptions options, iTextSharp.text.Rectangle bounds)
        {
            switch (options.VerticalStrategy)
            {
                case VerticalFineStrategy.Middle:
                    return bounds.Height / 2.0f;

                case VerticalFineStrategy.Bottom:
                    return bounds.Height;

                default:
                case VerticalFineStrategy.Top:
                    return 0.0f;
            }
        }

        private static float CalculatesCellHeight(CustomLocationTextExtractionStrategy.LocationTextResult reference, string text, CustomLocationTextExtractionStrategy strategy, PdfContentByte cb, IReadOnlyList<string> allStrings, ReplaceTextOptions options, float deltaY)
        {
            var value = cb.PdfDocument.PageSize.Height - cb.PdfDocument.BottomMargin - cb.PdfDocument.TopMargin - deltaY;

            CustomLocationTextExtractionStrategy.LocationTextResult nextMatchReference = null;

            var index = -1;
            for (var i = 0; i < allStrings.Count; i++)
            {
                string current = allStrings[i].Trim();
                if (current.Equals(text.Trim(), options.Comparison))
                {
                    index = i;
                    break;
                }
            }

            if (index == allStrings.Count)
            {
                return value - (cb.PdfDocument.PageSize.Height - reference.Rect.Bottom);
            }

            if (index == -1)
            {
                return value - (cb.PdfDocument.PageSize.Height - reference.Rect.Bottom);
            }

            var nextElementText = allStrings[index + 1];
            if (!string.IsNullOrEmpty(nextElementText.Trim()))
            {
                var nextMatchReferences = strategy.GetExtendedTextLocations(nextElementText, options);
                nextMatchReference = nextMatchReferences.FirstOrDefault();
            }

            if (nextMatchReference == null)
            {
                return value - (cb.PdfDocument.PageSize.Height - reference.Rect.Bottom);
            }

            // Same line?
            if (nextMatchReference.StartPoint.Y <= reference.StartPoint.Y)
            {
                return value - (cb.PdfDocument.PageSize.Height - reference.Rect.Bottom);
            }

            return value - (cb.PdfDocument.PageSize.Height - nextMatchReference.Rect.Bottom);
        }

        #endregion
    }
}
