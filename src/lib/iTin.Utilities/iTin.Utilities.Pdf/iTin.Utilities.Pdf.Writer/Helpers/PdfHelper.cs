
using iTextSharp.text;
using iTextSharp.text.pdf;

using iTin.Core.Models.Design;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Styling;

using iTin.Utilities.Pdf.Design.Styles;

namespace iTin.Utilities.Pdf.Writer.Helpers
{
    /// <summary>
    /// Contains common helper methods for Portable Document Format (pdf format).
    /// </summary>
    static class PdfHelper
    {
        /// <summary>
        /// Creates a new cell with the visual style defined in the model.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="style">The style.</param>
        /// <param name="useTestMode">Indicates if draw a border in testmode.</param>
        /// <returns>
        /// A new <see cref="PdfPCell"/> with visual style defined in the model.
        /// </returns>
        public static PdfPCell CreateCell(string text, PdfTextStyle style, YesNo useTestMode = YesNo.No)
        {
            var phrase = new Phrase { Font = CreateFont(style.Font) };
            phrase.Add(text);

            return new PdfPCell(phrase).SetVisualStyle(style, useTestMode);
        }

        /// <summary>
        /// Creates a new empty cell.
        /// </summary>
        /// <param name="useTestMode">Indicates if draw a border in testmode.</param>
        /// <param name="borders">The borders color, it allows defining the background color so as not to draw a border when we do not use test mode.</param>
        /// <returns>
        /// A new <see cref="PdfPCell"/> with visual style defined in the model.
        /// </returns>
        public static PdfPCell CreateEmptyCell(YesNo useTestMode = YesNo.No, BordersCollection borders = null)
        {
            if (useTestMode.AsBoolean())
            {
                return CreateEmptyWithBorderCell(BordersCollection.FromKnownColor(KnownBorderColor.Red));
            }

            return
                borders == null
                    ? CreateEmptyWithoutBorderCell()
                    : CreateEmptyWithBorderCell(borders);
        }

        /// <summary>
        /// Creates a new empty cell with border(s).
        /// </summary>
        /// <param name="borders">Borders definition.</param>
        /// <returns>
        /// A new <see cref="PdfPCell"/> with visual style defined in the model.
        /// </returns>
        public static PdfPCell CreateEmptyWithBorderCell(BordersCollection borders) => new PdfPCell().SetBordersVisualStyle(borders);

        /// <summary>
        /// Creates a new empty cell without border(s).
        /// </summary>
        /// <returns>
        /// A new <see cref="PdfPCell"/> with visual style defined in the model.
        /// </returns>
        public static PdfPCell CreateEmptyWithoutBorderCell() => new PdfPCell {BorderWidth = 0.0f};

        /// <summary>
        /// Creates a new font from model.
        /// </summary>
        /// <param name="font">Font to create.</param>
        /// <returns>
        /// A new <see cref="Font"/> that contains specified font from model.
        /// </returns>
        public static Font CreateFont(FontModel font)
        {
            font ??= FontModel.DefaultFont;

            int registeredFonts = FontFactory.RegisteredFonts.Count;
            if (registeredFonts <= 14)
            {
                FontFactory.RegisterDirectories();
            }

            int style = Font.NORMAL;
            if (font.Italic.AsBoolean())
            {
                style = Font.ITALIC;
            }

            if (font.Bold.AsBoolean())
            {
                style |= Font.BOLD;
            }

            if (font.Underline.AsBoolean())
            {
                style |= Font.UNDERLINE;
            }

            Font validFont;
            var fontName = font.Name;
            var isFontRegistered = FontFactory.IsRegistered(fontName);
            if (isFontRegistered)
            {
                validFont = FontFactory.GetFont(
                    fontName,
                    BaseFont.IDENTITY_H,
                    BaseFont.EMBEDDED,
                    font.Size,
                    style,
                    new BaseColor(font.GetColor()));
            }
            else
            {
                var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, FontFactory.DefaultEncoding, BaseFont.EMBEDDED);
                validFont = new Font(baseFont, font.Size, style, new BaseColor(font.GetColor()));
            }

            return validFont;
        }

        /// <summary>
        /// Creates a new font which is default font from model.
        /// </summary>
        /// <returns>
        /// A new <see cref="Font"/> that contains default font from model.
        /// </returns>
        public static Font DefaultFont() => CreateFont(FontModel.DefaultFont);
    }
}
