
using iTin.Core.Models.Design.Enums;

using iTin.Utilities.Pdf.Design.Styles;

namespace iPdfWriter.WebAppCore.ComponentModel.Helpers
{
    internal static class StylesHelper
    {
        internal static class Sample01
        {
            internal static readonly Dictionary<string, PdfImageStyle> ImagesStylesTable = new()
            {
                {
                    "Center",
                    new PdfImageStyle
                    {
                        Content =
                        {
                            Alignment =
                            {
                                Horizontal = KnownHorizontalAlignment.Center
                            }
                        }
                    }
                },
                {
                    "Default",
                    new PdfImageStyle
                    {
                        Content =
                        {
                            Alignment =
                            {
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                }
            };

            internal static readonly Dictionary<string, PdfTextStyle> TextStylesTable = new()
            {
                {
                    "ReportTitle",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Arial",
                            Size = 28.0f,
                            Bold = YesNo.Yes,
                            Italic = YesNo.Yes,
                            Color = "Blue"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Center
                            }
                        }
                    }
                }
            };
        }
    }
}
