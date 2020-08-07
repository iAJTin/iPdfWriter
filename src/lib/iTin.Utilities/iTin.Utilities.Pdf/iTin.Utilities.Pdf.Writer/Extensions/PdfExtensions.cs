
namespace iTin.Utilities.Pdf.Writer
{
    using System.Collections.ObjectModel;

    using iTextSharp.text;
    using iTextSharp.text.pdf;

    using iTin.Core.Helpers;
    using iTin.Core.Models.Design;
    using iTin.Core.Models.Design.Enums;
    using iTin.Core.Models.Design.Styling;

    using iTin.Logging;

    using Design.Styles;

    /// <summary>
    /// Static class than contains common extension methods for objects of the namespace <see cref="N:iTextSharp.text.pdf"/>.
    /// </summary>
    static class PdfExtensions
    {
        #region public static methods

        #region [public] {static} (PdfPTable) AddCells(this PdfPTable, Collection<PdfPCell>): Adds the cells
        /// <summary>
        /// Adds the cells.
        /// </summary>
        /// <param name="table">The Table.</param>
        /// <param name="cells">Cells to adds.</param>
        /// <returns>
        /// <see cref="iTextSharp.text.pdf.PdfPTable" /> which contains the range cells.
        /// </returns>
        public static PdfPTable AddCells(this PdfPTable table, Collection<PdfPCell> cells)
        {
            Logger.Instance.Debug("External Call");
            Logger.Instance.Info("  Adds the cells");
            Logger.Instance.Info("  > Library: iTin.Utilities.Pdf");
            Logger.Instance.Info("  > Class: PdfExtensions");
            Logger.Instance.Info("  > Method: AddCells(this PdfPTable, Collection<PdfPCell>)");
            Logger.Instance.Info("  > Output: PdfPTable");

            SentinelHelper.ArgumentNull(table, nameof(table));
            SentinelHelper.ArgumentNull(cells, nameof(cells));

            foreach (var cell in cells)
            {
                table.AddCell(cell);
            }

            return table;
        }
        #endregion

        #region [public] {static} (PdfPCell) SetAlignmentVisualStyle(this PdfPCell, PdfTextContentAlignment): Sets visual text alignment for specified cell
        /// <summary>
        /// Sets visual text alignment for specified cell.
        /// </summary>
        /// <param name="cell">Cell which receives visual style.</param>
        /// <param name="alignment">Text alignment to apply.</param>
        public static void SetAlignmentVisualStyle(this PdfPCell cell, PdfTextContentAlignment alignment)
        {
            Logger.Instance.Debug("External Call");
            Logger.Instance.Info("  Sets visual text alignment for specified cell");
            Logger.Instance.Info("  > Library: iTin.Utilities.Pdf");
            Logger.Instance.Info("  > Class: PdfExtensions");
            Logger.Instance.Info("  > Method: SetAlignmentVisualStyle(this PdfPCell, ContentAlignmentModel)");
            Logger.Instance.Info("  > Output: void");

            SentinelHelper.ArgumentNull(cell, nameof(cell));
            SentinelHelper.ArgumentNull(alignment, nameof(alignment));

            cell.VerticalAlignment = alignment.Vertical.ToElementVerticalAlignment();
            cell.HorizontalAlignment = alignment.Horizontal.ToElementHorizontalAlignment();

            switch (alignment.Vertical)
            {
                case KnownVerticalAlignment.Top:
                    cell.UseAscender = true;
                    cell.UseDescender = true;
                    break;

                case KnownVerticalAlignment.Center:
                    cell.UseDescender = true;
                    break;

                case KnownVerticalAlignment.Bottom:
                    break;
            }
        }
        #endregion

        #region [public] {static} (PdfPCell) SetBordersVisualStyle(this PdfPCell, BordersCollection): Sets visual border style for specified cell
        /// <summary>
        /// Sets visual border style for specified cell.
        /// </summary>
        /// <param name="cell">Cell which receives visual style.</param>
        /// <param name="borders">Border collection definition to apply.</param>
        /// <returns>
        /// A <see cref="PdfPCell"/> object which contains specified visual style.
        /// </returns>
        public static PdfPCell SetBordersVisualStyle(this PdfPCell cell, BordersCollection borders)
        {
            cell.BorderWidthLeft = 0.0f;
            BaseBorder leftBorder = borders.GetBy(KnownBorderPosition.Left);
            if (leftBorder != null)
            {
                if (leftBorder.Show.AsBoolean())
                {
                    cell.BorderWidthLeft = leftBorder.Width;
                    cell.BorderColorLeft = new BaseColor(leftBorder.GetColor());
                }
            }

            cell.BorderWidthTop = 0.0f;
            BaseBorder topBorder = borders.GetBy(KnownBorderPosition.Top);
            if (topBorder != null)
            {
                if (topBorder.Show.AsBoolean())
                {
                    cell.BorderWidthTop = topBorder.Width;
                    cell.BorderColorTop = new BaseColor(topBorder.GetColor());
                }
            }

            cell.BorderWidthRight = 0.0f;
            BaseBorder rightBorder = borders.GetBy(KnownBorderPosition.Right);
            if (rightBorder != null)
            {
                if (rightBorder.Show.AsBoolean())
                {
                    cell.BorderWidthRight = rightBorder.Width;
                    cell.BorderColorRight = new BaseColor(rightBorder.GetColor());
                }
            }

            cell.BorderWidthBottom = 0.0f;
            BaseBorder bottomBorder = borders.GetBy(KnownBorderPosition.Bottom);
            if (bottomBorder != null)
            {
                if (bottomBorder.Show.AsBoolean())
                {
                    cell.BorderWidthBottom = bottomBorder.Width;
                    cell.BorderColorBottom = new BaseColor(bottomBorder.GetColor());
                }
            }

            return cell;
        }
        #endregion

        #region [public] {static} (Image) SetVisualStyle(this Image, PdfImageStyle): Sets visual border style for specified iTextSharp image.
        /// <summary>
        /// Sets visual border style for specified <see cref="Image"/>.
        /// </summary>
        /// <param name="image">Horizontal alignment.</param>
        /// <param name="style">Style to apply.</param>
        /// <returns>
        /// A <see cref="int"/> value that represents the alignment.
        /// </returns>
        public static Image SetVisualStyle(this Image image, PdfImageStyle style)
        {
            if (!string.IsNullOrEmpty(style.Content.Color))
            {
                image.BackgroundColor = new BaseColor(style.Content.GetColor());
            }

            BordersCollection borders = style.Borders;
            image.BorderWidthLeft = 0.0f;
            BaseBorder leftBorder = borders.GetBy(KnownBorderPosition.Left);
            if (leftBorder != null)
            {
                if (leftBorder.Show.AsBoolean())
                {
                    image.BorderWidthLeft = leftBorder.Width;
                    image.BorderColorLeft = new BaseColor(leftBorder.GetColor());
                }
            }

            image.BorderWidthTop = 0.0f;
            BaseBorder topBorder = borders.GetBy(KnownBorderPosition.Top);
            if (topBorder != null)
            {
                if (topBorder.Show.AsBoolean())
                {
                    image.BorderWidthTop = topBorder.Width;
                    image.BorderColorTop = new BaseColor(topBorder.GetColor());
                }
            }

            image.BorderWidthRight = 0.0f;
            BaseBorder rightBorder = borders.GetBy(KnownBorderPosition.Right);
            if (rightBorder != null)
            {
                if (rightBorder.Show.AsBoolean())
                {
                    image.BorderWidthRight = rightBorder.Width;
                    image.BorderColorRight = new BaseColor(rightBorder.GetColor());
                }
            }

            image.BorderWidthBottom = 0.0f;
            BaseBorder bottomBorder = borders.GetBy(KnownBorderPosition.Bottom);
            if (bottomBorder != null)
            {
                if (bottomBorder.Show.AsBoolean())
                {
                    image.BorderWidthBottom = bottomBorder.Width;
                    image.BorderColorBottom = new BaseColor(bottomBorder.GetColor());
                }
            }

            return image;
        }
        #endregion

        #region [public] {static} (PdfPCell) SetVisualStyle(this PdfPCell, PdfTextStyle): Sets visual style for specified cell
        /// <summary>
        /// Sets visual style for specified cell.
        /// </summary>
        /// <param name="cell">Cell which receives visual style.</param>
        /// <param name="style">Style to apply.</param>
        /// <param name="useTestMode">Indicates if draw a border in testmode.</param>
        /// <returns>
        /// A <see cref="PdfPCell"/> object which contains specified visual style.
        /// </returns>
        public static PdfPCell SetVisualStyle(this PdfPCell cell, PdfTextStyle style, YesNo useTestMode = YesNo.No)
        {
            Logger.Instance.Debug("External Call");
            Logger.Instance.Info("  Sets visual style for specified cell");
            Logger.Instance.Info("  > Library: iTin.Utilities.Pdf");
            Logger.Instance.Info("  > Class: PdfExtensions");
            Logger.Instance.Info("  > Method: SetVisualStyle(this PdfPCell, StyleModel)");
            Logger.Instance.Info("  > Output: PdfPCell");

            SentinelHelper.ArgumentNull(cell, nameof(cell));
            SentinelHelper.ArgumentNull(style, nameof(style));

            cell.BackgroundColor = new BaseColor(style.Content.GetColor());
            cell.SetAlignmentVisualStyle(style.Content.Alignment);

            if (useTestMode.AsBoolean())
            {
                cell.SetBordersVisualStyle(
                    new BordersCollection
                    {
                        new BaseBorder {Color = "Red", Position = KnownBorderPosition.Left, Show = YesNo.Yes},
                        new BaseBorder {Color = "Red", Position = KnownBorderPosition.Top, Show = YesNo.Yes},
                        new BaseBorder {Color = "Red", Position = KnownBorderPosition.Right, Show = YesNo.Yes},
                        new BaseBorder {Color = "Red", Position = KnownBorderPosition.Bottom, Show = YesNo.Yes}
                    });
            }
            else
            {
                cell.SetBordersVisualStyle(style.Borders);
            }

            return cell;
        }
        #endregion

        #region [public] {static} (int) ToHorizontalTableAlignment(this KnownHorizontalAlignment): Converts one of the enumeration values KnownHorizontalAlignment to the proper alignment value for iTextSharp
        /// <summary>
        /// Converts one of the enumeration values <see cref="KnownHorizontalAlignment"/> to the proper alignment value for <b>iTextSharp</b>.
        /// </summary>
        /// <param name="horizontalAlignment">Horizontal alignment.</param>
        /// <returns>
        /// A <see cref="int"/> value that represents the alignment.
        /// </returns>
        public static int ToHorizontalTableAlignment(this KnownHorizontalAlignment horizontalAlignment)
        {
            switch (horizontalAlignment)
            {
                case KnownHorizontalAlignment.Center:
                    return Element.ALIGN_CENTER;
                    
                case KnownHorizontalAlignment.Right:
                    return Element.ALIGN_RIGHT;

                default:
                case KnownHorizontalAlignment.Left:
                    return Element.ALIGN_LEFT;
            }
        }
        #endregion

        #region [public] {static} (int) ToVerticalTableAlignment(this KnownVerticalAlignment): Converts one of the enumeration values KnownVerticalAlignment to the proper vertical alignment value for iTextSharp
        /// <summary>
        /// Converts one of the enumeration values <see cref="KnownVerticalAlignment"/> to the proper vertical alignment value for <b>iTextSharp</b>.
        /// </summary>
        /// <param name="verticalAlignment">Vertical alignment.</param>
        /// <returns>
        /// A <see cref="int"/> value that represents the vertical alignment.
        /// </returns>
        public static int ToVerticalTableAlignment(this KnownVerticalAlignment verticalAlignment)
        {
            switch (verticalAlignment)
            {
                case KnownVerticalAlignment.Bottom:
                    return Element.ALIGN_BOTTOM;

                case KnownVerticalAlignment.Center:
                    return Element.ALIGN_MIDDLE;

                default:
                case KnownVerticalAlignment.Top:
                    return Element.ALIGN_TOP;
            }
        }
        #endregion

        #endregion

        #region private static methods

        #region [private] {static} (int) ToElementHorizontalAlignment(this KnownHorizontalAlignment): Converts the enumerated type KnownHorizontalAlignment to the appropriate value for the specified alignment
        /// <summary>
        /// Converts the enumerated type <see cref="KnownHorizontalAlignment"/> to the appropriate value for the specified alignment.
        /// </summary>
        /// <param name="alignment">Alignment to convert.</param>
        /// <returns>
        /// A <see cref="int"/> value that represents appropriate the value for the specified alignment.
        /// </returns>
        private static int ToElementHorizontalAlignment(this KnownHorizontalAlignment alignment)
        {
            int pdfAlignment = Element.ALIGN_LEFT;
            switch (alignment)
            {
                case KnownHorizontalAlignment.Center:
                    pdfAlignment = Element.ALIGN_CENTER;
                    break;

                case KnownHorizontalAlignment.Right:
                    pdfAlignment = Element.ALIGN_RIGHT;
                    break;
            }

            return pdfAlignment;
        }
        #endregion

        #region [private] {static} (int) ToElementVerticalAlignment(this KnownVerticalAlignment): Converts the enumerated type KnownVerticalAlignment to the appropriate value for the specified alignment
        /// <summary>
        /// Converts the enumerated type <see cref="KnownVerticalAlignment"/> to the appropriate value for the specified alignment.
        /// </summary>
        /// <param name="alignment">Alignment to convert.</param>
        /// <returns>
        /// A <see cref="int"/> value that represents appropriate the value for the specified alignment.
        /// </returns>
        private static int ToElementVerticalAlignment(this KnownVerticalAlignment alignment)
        {
            int pdfAlignment = Element.ALIGN_MIDDLE;
            switch (alignment)
            {
                case KnownVerticalAlignment.Bottom:
                    pdfAlignment = Element.ALIGN_BOTTOM;
                    break;

                case KnownVerticalAlignment.Top:
                    pdfAlignment = Element.ALIGN_TOP;
                    break;
            }

            return pdfAlignment;
        }
        #endregion

        #endregion
    }
}
