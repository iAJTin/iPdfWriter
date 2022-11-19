
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

using iTin.Core;
using iTin.Core.Drawing;
using iTin.Core.Drawing.Helpers;
using iTin.Core.Models.Design.Enums;

using iTin.Utilities.Pdf.Design.Styles;
using iTin.Utilities.Pdf.Design.Table;

using iTin.Utilities.Pdf.Writer.Helpers;
using iTin.Utilities.Pdf.Writer.Operations.Insert;
using iTin.Utilities.Pdf.Writer.Operations.Replace;
using iTin.Utilities.Pdf.Writer.Operations.Result.Replace;
using iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text;
using iTin.Utilities.Pdf.Writer.SystemTag;

using iTin.Utilities.Writer.Abstractions.Operations.Results;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

using NativeIO = System.IO;

namespace iTin.Utilities.Pdf.Writer.Input
{
    /// <summary>
    /// 
    /// </summary>
    internal static class PdfInputRender
    {
        #region public static methods

        public static ReplaceResult InsertsRender(IPdfInput context)
        {
            var input = context.ToStream();
            var items = PdfInputCache.Cache.GetInserts(context).ToList();

            var outputStream = new NativeIO.MemoryStream();

            try
            {
                using var reader = new PdfReader(input);
                using var stamper = new PdfStamper(reader, outputStream);

                var pages = reader.NumberOfPages;
                for (var page = 1; page <= pages; page++)
                {
                    var currentPage = page;
                    var itemsForPage = items.Where(i => i.Page == currentPage).ToList();
                    var hasItems = itemsForPage.Any();
                    if (!hasItems)
                    {
                        continue;
                    }

                    var cb = stamper.GetOverContent(currentPage);

                    foreach (var item in itemsForPage)
                    {
                        var isInsertImage = item is InsertImage;
                        if (!isInsertImage)
                        {
                            continue;
                        }

                        var target = (InsertImage)item;
                        if (target.Image.Equals(Design.Image.PdfImage.Null))
                        {
                            continue;
                        }

                        if (target.UseTestMode == YesNo.Yes)
                        {
                            using var emptyImage = BitmapHelper.CreateEmptyBitmap(target.Image.Image.ScaledWidth + 1, target.Image.Image.ScaledHeight + 1, Color.LightGray);
                            using var g = Graphics.FromImage(emptyImage);
                            using var canvas = new Canvas(g);
                            canvas.DrawBorder(Color.Red);

                            var testImage = iTextSharp.text.Image.GetInstance(emptyImage, ImageFormat.Png);
                            testImage.SetAbsolutePosition(target.Offset.X, target.Offset.Y);
                            cb.AddImage(testImage);
                        }
                        else
                        {
                            target.Image.Image.SetAbsolutePosition(target.Offset.X, target.Offset.Y);
                            cb.AddImage(target.Image.Image);
                        }
                    }
                }

                stamper.Close();
                reader.Close();

                return ReplaceResult.CreateSuccessResult(new ReplaceResultData
                {
                    Context = context,
                    InputStream = input,
                    OutputStream = new NativeIO.MemoryStream(outputStream.GetBuffer())
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

        public static ReplaceResult SetsRender(IPdfInput context)
        {
            var input = context.ToStream();
            var items = PdfInputCache.Cache.GetSets(context).ToList();

            var outputStream = new NativeIO.MemoryStream();

            try
            {
                using var reader = new PdfReader(input);
                using var stamper = new PdfStamper(reader, outputStream);
                var info = reader.Info;

                foreach (var item in items)
                {
                    var existAuthorKey = info.ContainsKey(item.Key);
                    if (!existAuthorKey)
                    {
                        info.Add(item.Key, item.Value);
                    }
                    else
                    {
                        info[item.Key] = item.Value;
                    }
                }

                stamper.MoreInfo = info;
                stamper.Close();
                reader.Close();

                return ReplaceResult.CreateSuccessResult(new ReplaceResultData
                {
                    Context = context,
                    InputStream = input,
                    OutputStream = new NativeIO.MemoryStream(outputStream.GetBuffer())
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

        public static ReplaceResult TextReplacementsRender(IPdfInput context)
        {
            var input = context.ToStream();
            var items = PdfInputCache.Cache.GetTextReplacements(context).ToList();

            var outputStream = new NativeIO.MemoryStream();

            try
            {
                using var reader = new PdfReader(input);
                using var stamper = new PdfStamper(reader, outputStream);

                var pages = reader.NumberOfPages;
                for (var page = 1; page <= pages; page++)
                {
                    var strategy = new TextStrategy.LocationTextExtractionStrategy();
                    var cb = stamper.GetOverContent(page);

                    // Send some data contained in PdfContentByte, looks like the first is always cero for me and the second 100, 
                    // but i'm not sure if this could change in some cases.
                    strategy.UndercontentCharacterSpacing = cb.CharacterSpacing;
                    strategy.UndercontentHorizontalScaling = cb.HorizontalScaling;

                    // It's not really needed to get the text back, but we have to call this line ALWAYS,
                    // because it triggers the process that will get all chunks from PDF into our strategy Object
                    var allStrings = PdfTextExtractor.GetTextFromPage(reader, page, strategy);
                    var stringsList =
                        allStrings
                            .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                            .Where(entry => !string.IsNullOrEmpty(entry.Trim()))
                            .ToList();

                    foreach (var item in items)
                    {
                        ITextReplacement replacementObject = null;
                        List<TextStrategy.LocationTextExtractionStrategy.LocationTextResult> textMatchesFound;

                        var isSystemTag = item is ReplaceSystemTag;
                        if (!isSystemTag)
                        {
                            replacementObject = ((ReplaceText)item).ReplacementObject;
                            textMatchesFound = strategy.GetExtendedTextLocations(replacementObject.Text, replacementObject.ReplaceOptions).ToList();
                        }
                        else
                        {
                            var replacementTagObject = ((ReplaceSystemTag)item).ReplacementObject;
                            textMatchesFound = strategy.GetExtendedTextLocations(replacementTagObject.Tag.GetDescription(), replacementTagObject.ReplaceOptions).ToList();
                        }

                        // Text matches found contains all text with locations, so do whatever you want with it
                        foreach (var match in textMatchesFound)
                        {
                            // Delete tag
                            var bColor = BaseColor.WHITE;
                            cb.SetColorFill(bColor);
                            cb.Rectangle(match.Rect.Left, match.Rect.Bottom, match.Rect.Width, match.Rect.Height);
                            cb.Fill();

                            var isText = replacementObject is WithTextObject;
                            if (isText)
                            {
                                var current = (WithTextObject)replacementObject;
                                current.Style ??= PdfTextStyle.Default;

                                // Calculates new rectangle
                                var r = BuildRectangleByStrategies(match, replacementObject.Text, strategy, cb, stringsList, replacementObject.ReplaceOptions);

                                // Add table
                                var table = new PdfPTable(1) { TotalWidth = r.Width - replacementObject.Offset.X };
                                table.AddCell(PdfHelper.CreateCell(current.NewText, current.Style, replacementObject.UseTestMode));
                                table.WriteSelectedRows(-1, -1, r.X + replacementObject.Offset.X, r.Y - replacementObject.Offset.Y, cb);

                                cb.Fill();

                                continue;
                            }

                            var isImage = replacementObject is WithImageObject;
                            if (isImage)
                            {
                                var current = (WithImageObject)replacementObject;
                                current.Style ??= PdfImageStyle.Default;

                                // Calculates new rectangle
                                var r = BuildRectangleByStrategies(match, replacementObject.Text, current.Image.ScaledHeight, current.Image.ScaledWidth, strategy, cb, stringsList, replacementObject.ReplaceOptions);

                                current.Image.Image.ScaleToFit(r.Width, r.Height);
                                var dX = CalculatesHorizontalDelta(current.Style.Content.Alignment.Horizontal, r, current.Image.Image, replacementObject.Offset.X);

                                if (replacementObject.UseTestMode == YesNo.Yes)
                                {
                                    using var emptyImage = BitmapHelper.CreateEmptyBitmap(current.Image.Image.ScaledWidth + 1, current.Image.Image.ScaledHeight + 1, Color.LightGray);
                                    using var g = Graphics.FromImage(emptyImage);
                                    using var canvas = new Canvas(g);
                                    canvas.DrawBorder(Color.Red);

                                    var testImage = iTextSharp.text.Image.GetInstance(emptyImage, ImageFormat.Png);
                                    testImage.SetAbsolutePosition(r.X + dX, -replacementObject.Offset.Y + (r.Y - current.Image.Image.ScaledHeight));
                                    cb.AddImage(testImage);
                                }
                                else
                                {
                                    current.Image.Image.SetVisualStyle(current.Style);
                                    current.Image.Image.SetAbsolutePosition(r.X + dX, -replacementObject.Offset.Y + (r.Y - current.Image.Image.ScaledHeight));
                                    cb.AddImage(current.Image.Image);
                                }

                                continue;
                            }

                            var isTable = replacementObject is WithTableObject;
                            if (isTable)
                            {
                                var current = (WithTableObject)replacementObject;

                                // Calculates new rectangle
                                var deltaY = CalculatesVerticalDelta(replacementObject.ReplaceOptions, match.Rect);
                                var cellHeight = CalculatesCellHeight(match, replacementObject.Text, strategy, cb, stringsList, replacementObject.ReplaceOptions, deltaY);
                                var r = BuildRectangleByStrategiesTable(match, replacementObject.Text, strategy, cb, stringsList, replacementObject.ReplaceOptions);

                                // Width strategy to use
                                var safeFixedWidth = current.FixedWidth;
                                var useFixedWidth = !current.FixedWidth.Equals(-float.MinValue);
                                if (useFixedWidth)
                                {
                                    if (current.FixedWidth > r.Width)
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

                                var outerCell = PdfHelper.CreateEmptyWithBorderCell(current.Style.Borders);
                                outerCell.MinimumHeight = cellHeight;
                                outerCell.VerticalAlignment = current.Style.Alignment.Vertical.ToVerticalTableAlignment();
                                outerCell.BackgroundColor = new BaseColor(ColorHelper.GetColorFromString(current.Style.Content.Color));

                                //table.Table.HorizontalAlignment = Element.ALIGN_LEFT;
                                current.Table.Table.TotalWidth = safeFixedWidth - (outerCell.EffectivePaddingRight + outerCell.EffectivePaddingLeft) * 2;
                                current.Table.Table.LockedWidth = true; // options.StartStrategy.Equals(StartLocationStrategy.LeftMargin) && options.EndStrategy.Equals(EndLocationStrategy.RightMargin);
                                outerCell.AddElement(current.Table.Table);
                                outerBorderTable.AddCell(outerCell);

                                // Creates strategy table (for shows testmode rectangle)
                                var useTestModeTable = new PdfPTable(1) { TotalWidth = safeFixedWidth };
                                var useTestCell = PdfHelper.CreateEmptyCell(replacementObject.UseTestMode);

                                if (current.Table.Configuration.HeightStrategy == TableHeightStrategy.Exact)
                                {
                                    useTestCell.FixedHeight = current.Table.Table.TotalHeight;
                                }

                                useTestCell.AddElement(outerBorderTable);
                                useTestModeTable.AddCell(useTestCell);
                                useTestModeTable.WriteSelectedRows(-1, -1, r.X + replacementObject.Offset.X, r.Y - replacementObject.Offset.Y - deltaY, cb);

                                cb.Fill();

                                continue;
                            }

                            if (isSystemTag)
                            {
                                var replacementTagObject = ((ReplaceSystemTag)item).ReplacementObject;

                                var current = (WithSystemTagObject) replacementTagObject;
                                current.Style ??= PdfTextStyle.Default;

                                // Calculates new rectangle
                                var r = BuildRectangleByStrategies(match, current.Tag.GetDescription(), strategy, cb, stringsList, replacementTagObject.ReplaceOptions);

                                // Add table
                                var table = new PdfPTable(1) { TotalWidth = r.Width - current.Offset.X };

                                // New text
                                string newText = string.Empty;
                                switch (current.Tag)
                                {
                                    case SystemTags.PageNumber:
                                        newText = page.ToString();
                                        break;

                                    case SystemTags.TotalPages:
                                        newText = pages.ToString();
                                        break;
                                }

                                table.AddCell(PdfHelper.CreateCell(newText, current.Style, current.UseTestMode));
                                table.WriteSelectedRows(-1, -1, r.X + current.Offset.X, r.Y - current.Offset.Y, cb);
                                cb.Fill();
                            }
                        }

                        cb.Fill();
                        cb.Stroke();
                    }
                }

                stamper.Close();
                reader.Close();

                return ReplaceResult.CreateSuccessResult(new ReplaceResultData
                {
                    Context = context,
                    InputStream = input,
                    OutputStream = new NativeIO.MemoryStream(outputStream.GetBuffer())
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

        #endregion

        #region private static methods

        #region Text

        private static RectangleF BuildRectangleByStrategies(
            TextStrategy.LocationTextExtractionStrategy.LocationTextResult reference, 
            string text,
            TextStrategy.LocationTextExtractionStrategy strategy, 
            PdfContentByte cb, 
            IReadOnlyList<string> allStrings, 
            ReplaceTextOptions options)
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
                    TextStrategy.LocationTextExtractionStrategy.LocationTextResult previousMatchReference = null;

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
                    TextStrategy.LocationTextExtractionStrategy.LocationTextResult nextMatchReference = null;

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

            return new RectangleF(x, y, w, h);
        }
        
        #endregion

        #region Image

        private static RectangleF BuildRectangleByStrategies(
            TextStrategy.LocationTextExtractionStrategy.LocationTextResult reference, 
            string text, 
            float imageHeight, 
            float imageWidth, 
            TextStrategy.LocationTextExtractionStrategy strategy, 
            PdfContentByte cb, 
            IReadOnlyList<string> allStrings, 
            ReplaceTextOptions options)
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
                    TextStrategy.LocationTextExtractionStrategy.LocationTextResult previousMatchReference = null;

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
                    TextStrategy.LocationTextExtractionStrategy.LocationTextResult nextMatchReference = null;

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
        
        private static float CalculatesHorizontalDelta(KnownHorizontalAlignment imageAlignment, RectangleF bounds, iTextSharp.text.Image image, float horizontalOffset)
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

        #region Table

        private static RectangleF BuildRectangleByStrategiesTable(
            TextStrategy.LocationTextExtractionStrategy.LocationTextResult reference, 
            string text, 
            TextStrategy.LocationTextExtractionStrategy strategy, 
            PdfContentByte cb, 
            IReadOnlyList<string> allStrings, 
            ReplaceTextOptions options)
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
                    TextStrategy.LocationTextExtractionStrategy.LocationTextResult previousMatchReference = null;

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
                    TextStrategy.LocationTextExtractionStrategy.LocationTextResult nextMatchReference = null;

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

        private static float CalculatesVerticalDelta(ReplaceTextOptions options, iTextSharp.text.Rectangle bounds) =>
            options.VerticalStrategy switch
            {
                VerticalFineStrategy.Middle => bounds.Height / 2.0f,
                VerticalFineStrategy.Bottom => bounds.Height,
                _ => 0.0f,
            };

        private static float CalculatesCellHeight(
            TextStrategy.LocationTextExtractionStrategy.LocationTextResult reference, 
            string text, TextStrategy.LocationTextExtractionStrategy strategy, 
            PdfContentByte cb, 
            IReadOnlyList<string> allStrings, 
            ReplaceTextOptions options, 
            float deltaY)
        {
            var value = cb.PdfDocument.PageSize.Height - cb.PdfDocument.BottomMargin - cb.PdfDocument.TopMargin - deltaY;

            TextStrategy.LocationTextExtractionStrategy.LocationTextResult nextMatchReference = null;

            var index = -1;
            for (var i = 0; i < allStrings.Count; i++)
            {
                var current = allStrings[i].Trim();
                if (!current.Equals(text.Trim(), options.Comparison))
                {
                    continue;
                }

                index = i;
                break;
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

        #endregion
    }
}
