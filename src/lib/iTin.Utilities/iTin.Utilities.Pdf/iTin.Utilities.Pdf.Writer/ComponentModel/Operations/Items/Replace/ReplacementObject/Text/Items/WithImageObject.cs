﻿
namespace iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;

    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using iTextSharp.text.pdf.parser;

    using iTin.Core.Drawing;
    using iTin.Core.Drawing.Helpers;
    using iTin.Core.Models.Design.Enums;

    using Design.Styles;
    using Result.Replace;

    using TextSharpPdfImage = iTextSharp.text.Image;
    using CustomLocationTextExtractionStrategy = LocationTextExtractionStrategy;

    /// <summary>
    /// Specialization of <see cref="TextReplacementBase"/> interface.<br/>
    /// Contains the logic to replace a text with an image.
    /// </summary>
    public class WithImageObject : TextReplacementBase
    {
        #region constructor/s

        #region [public] WithImageObject(): Initializes a new instance of the class
        /// <summary>
        /// Initializes a new instance of the <see cref="WithImageObject"/> class.
        /// </summary>
        public WithImageObject()
        {
            UseTestMode = YesNo.No;
            ImageOffset = PointF.Empty;
            Style = PdfImageStyle.Default;
            ReplaceOptions = ReplaceTextOptions.Default;
        }
        #endregion

        #endregion

        #region public static readonly properties

        #region [public] static (WithImageObject) Default: Returns a new instance that contains the default values
        /// <summary>
        /// Returns a new instance of <see cref="WithImageObject"/> class that contains the default values.
        /// </summary>
        /// <value>
        /// A <see cref="WithImageObject"/> that contains the default values.
        /// </value>
        public static WithImageObject Default => new WithImageObject();
        #endregion

        #endregion

        #region public properties

        #region [public] (Design.Image.PdfImage) Image: Gets or sets a reference to pdf image object
        /// <summary>
        /// Gets or sets a reference to pdf image object.
        /// </summary>
        /// <value>
        /// A <see cref="Design.Image.PdfImage"/> object that contains a reference to pdf image object.
        /// </value>
        public Design.Image.PdfImage Image { get; set; }
        #endregion

        #region [public] (PointF) ImageOffset: Gets or sets a reference a point structure which represents the image offset
        /// <summary>
        /// Gets or sets a reference a point structure which represents the image offset. The default is <see cref="PointF.Empty"/>.
        /// Positive values on the y axis move the image down and positive values on the x axis move the image right.
        /// </summary>
        /// <value>
        /// A <see cref="PointF"/> object that contains image offset to apply.
        /// </value>
        public PointF ImageOffset { get; set; }
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

        #region [public] {new} (PdfImageStyle) Style: Gets or sets a reference to new text style format
        /// <summary>
        /// Gets or sets a reference to new text style format. The default is <see cref="PdfImageStyle.Default"/>.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImageStyle"/> object that contains image offset to apply.
        /// </value>
        public new PdfImageStyle Style { get; set; }
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
            if (Image == Design.Image.PdfImage.Null)
            {
                return ReplaceResult.CreateSuccessResult(new ReplaceResultData
                {
                    Context = context,
                    InputStream = input,
                    OutputStream = input
                });
            }

            if (Style == null)
            {
                Style = PdfImageStyle.Default;
            }

            return ReplaceImpl(context, input, Text, ReplaceOptions, Image, ImageOffset, Style, UseTestMode);
        }

        #endregion

        #endregion

        #region private static methods

        private static ReplaceResult ReplaceImpl(IInput context, Stream input, string oldText, ReplaceTextOptions options, Design.Image.PdfImage image, PointF imageOffset, PdfImageStyle style, YesNo useTestMode)
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
                            var r = BuildRectangleByStrategies(match, oldText, image.ScaledHeight, image.ScaledWidth, strategy, cb, (string[])stringsArray.Clone(), options);

                            image.Image.ScaleToFit(r.Width, r.Height);
                            var dX = CalculatesHorizontalDelta(style.Content.Alignment.Horizontal, r, image.Image, imageOffset.X);

                            if (useTestMode == YesNo.Yes)
                            {
                                using (Bitmap emptyImage = BitmapHelper.CreateEmptyBitmap(image.Image.ScaledWidth + 1, image.Image.ScaledHeight + 1, Color.LightGray))
                                using (Graphics g = Graphics.FromImage(emptyImage))
                                using (Canvas canvas = new Canvas(g))
                                {
                                    canvas.DrawBorder(Color.Red);

                                    var testImage = TextSharpPdfImage.GetInstance(emptyImage, ImageFormat.Png);
                                    testImage.SetAbsolutePosition(r.X + dX, -imageOffset.Y + (r.Y - image.Image.ScaledHeight));
                                    cb.AddImage(testImage);
                                }
                            }
                            else
                            {
                                image.Image.SetVisualStyle(style);
                                image.Image.SetAbsolutePosition(r.X + dX, -imageOffset.Y + (r.Y - image.Image.ScaledHeight));
                                cb.AddImage(image.Image);
                            }
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

        private static RectangleF BuildRectangleByStrategies(CustomLocationTextExtractionStrategy.LocationTextResult reference, string text, float imageHeight, float imageWidth, CustomLocationTextExtractionStrategy strategy, PdfContentByte cb, IReadOnlyList<string> allStrings, ReplaceTextOptions options)
        {
            var x = reference.Rect.Left;
            var y = reference.Rect.Top + 1;
            var w = imageWidth;
            var h = imageHeight;

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
                    break;
            }

            return new RectangleF(x, y, w, h);
        }

        private static float CalculatesHorizontalDelta(KnownHorizontalAlignment imageAlignment, RectangleF bounds, TextSharpPdfImage image, float horizontalOffset)
        {
            var deltaX = horizontalOffset;
            switch (imageAlignment)
            {
                case KnownHorizontalAlignment.Center:
                    deltaX += (bounds.Right - bounds.Left - image.ScaledWidth) / 2.0f;
                    break;

                case KnownHorizontalAlignment.Right:
                    deltaX += bounds.Left + (bounds.Right - image.ScaledWidth);
                    break;

                case KnownHorizontalAlignment.Left:
                    break;
            }

            return deltaX;
        }

        #endregion
    }
}
