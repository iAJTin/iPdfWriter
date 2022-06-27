
namespace iPdfWriter.Code
{
    using iTin.Core.ComponentModel;

    using iTin.Logging.ComponentModel;

    using iTin.Core.Models;
    using iTin.Core.Models.Design.Enums;
    using iTin.Core.Models.Design.Styling;

    using iTin.Utilities.Pdf.Design.Styles;

    /// <summary>
    /// Shows the use of how serialize and deserialize text, image and table styles.
    /// </summary>
    internal static class Sample12
    {
        // Generates document
        public static void Generate(ILogger logger)
        {
            #region image style

            logger.Info("   > Working with image styles");

            var imageStyle = new PdfImageStyle
            {
                Name = "ImageStyle",
                Borders =
                {
                    new BaseBorder {Color = "Red", Show = YesNo.Yes, Position = KnownBorderPosition.Right},
                    new BaseBorder {Color = "Yellow", Show = YesNo.Yes, Position = KnownBorderPosition.Top}
                },
                Content =
                {
                    Color = "Blue",
                    Alignment =
                    {
                        Horizontal = KnownHorizontalAlignment.Right
                    },
                    Properties = new Properties
                    {
                        new Property {Name = "p001", Value = "v001"},
                        new Property {Name = "p002", Value = "v002"}
                    }
                }
            };

            // Save image style to disk
            var imageStyleAsXmlResult = imageStyle.SaveToFile("~/Output/Sample12/ImageStyle");
            if (!imageStyleAsXmlResult.Success)
            {
                logger.Info("     > Error while saving image style as xml to disk");
                logger.Info($"      > Error: {imageStyleAsXmlResult.Errors.AsMessages().ToStringBuilder()}");
            }
            else
            {
                logger.Info("     > Saved image style as xml to disk correctly");
                logger.Info("       > Path: ~/Output/Sample12/ImageStyle.xml");
            }

            var imageStyleAsJsonResult = imageStyle.SaveToFile("~/Output/Sample12/ImageStyle", KnownFileFormat.Json);
            if (!imageStyleAsJsonResult.Success)
            {
                logger.Info("     > Error while saving image style as json to disk");
                logger.Info($"      > Error: {imageStyleAsJsonResult.Errors.AsMessages().ToStringBuilder()}");
            }
            else
            {
                logger.Info("     > Saved image style as json to disk correctly");
                logger.Info("       > Path: ~/Output/Sample12/ImageStyle.json");
            }

            // New image style instances from disk
            var imageStyleFromXml = PdfImageStyle.LoadFromFile("~/Output/Sample12/ImageStyle.xml");
            logger.Info(imageStyleFromXml == null
                ? "     > Error while loading image style from xml file"
                : "     > Image style loaded correctly from xml '~/Output/Sample12/ImageStyle.xml' file");

            var imageStyleFromJson = PdfImageStyle.LoadFromFile("~/Output/Sample12/ImageStyle.json", KnownFileFormat.Json);
            logger.Info(imageStyleFromJson == null
                ? "     > Error while loading image style from json file"
                : "     > Image style loaded correctly from json '~/Output/Sample12/ImageStyle.json' file");

            #endregion

            #region text style

            logger.Info("");
            logger.Info("   > Working with text styles");

            var textStyle = new PdfTextStyle
            {
                Name = "NormalStyle",
                Font =
                {
                    Bold = YesNo.Yes,
                    Italic = YesNo.Yes,
                    Color = "Yellow",
                    Underline = YesNo.No
                },
                Borders =
                {
                    new BaseBorder {Color = "Red", Show = YesNo.Yes, Position = KnownBorderPosition.Right},
                    new BaseBorder {Color = "Yellow", Show = YesNo.Yes, Position = KnownBorderPosition.Top}
                },
                Content =
                {
                    Color = "Blue",
                    Alignment =
                    {
                        Vertical = KnownVerticalAlignment.Top,
                        Horizontal = KnownHorizontalAlignment.Right
                    },
                    Properties = new Properties
                    {
                        new Property {Name = "p001", Value = "v001"},
                        new Property {Name = "p002", Value = "v002"}
                    }
                }
            };

            // Save text style to disk
            var textStyleAsXmlResult = textStyle.SaveToFile("~/Output/Sample12/TextStyle");
            if (!textStyleAsXmlResult.Success)
            {
                logger.Info("     > Error while saving text style as xml to disk");
                logger.Info($"      > Error: {textStyleAsXmlResult.Errors.AsMessages().ToStringBuilder()}");
            }
            else
            {
                logger.Info("     > Saved text style as xml to disk correctly");
                logger.Info("       > Path: ~/Output/Sample12/TextStyle.xml");
            }

            var textStyleAsJsonResult = textStyle.SaveToFile("~/Output/Sample12/TextStyle", KnownFileFormat.Json);
            if (!textStyleAsJsonResult.Success)
            {
                logger.Info("     > Error while saving text style as json to disk");
                logger.Info($"      > Error: {textStyleAsJsonResult.Errors.AsMessages().ToStringBuilder()}");
            }
            else
            {
                logger.Info("     > Saved text style as json to disk correctly");
                logger.Info("       > Path: ~/Output/Sample12/TextStyle.json");
            }

            // New text style instances from disk
            var textStyleFromXml = PdfTextStyle.LoadFromFile("~/Output/Sample12/TextStyle.xml");
            logger.Info(textStyleFromXml == null
                ? "     > Error while loading text style from xml file"
                : "     > Text style loaded correctly from xml '~/Output/Sample12/TextStyle.xml' file");

            var textStyleFromJson = PdfTextStyle.LoadFromFile("~/Output/Sample12/TextStyle.json", KnownFileFormat.Json);
            logger.Info(textStyleFromJson == null
                ? "     > Error while loading text style from json file"
                : "     > Text style loaded correctly from json '~/Output/Sample12/TextStyle.json' file");

            #endregion

            #region table style

            logger.Info("");
            logger.Info("   > Working with table styles");

            var tableStyle = new PdfTableStyle
            {
                Name = "NormalStyle",
                Alignment =
                {
                    Vertical = KnownVerticalAlignment.Top
                },
                Borders =
                {
                    new BaseBorder {Color = "Red", Show = YesNo.Yes, Position = KnownBorderPosition.Right},
                    new BaseBorder {Color = "Yellow", Show = YesNo.Yes, Position = KnownBorderPosition.Top}
                },
                Content =
                {
                    Color = "Blue",
                    Show = YesNo.Yes,
                    Properties = new Properties
                    {
                        new Property {Name = "p001", Value = "v001"},
                        new Property {Name = "p002", Value = "v002"}
                    }
                }
            };

            // Save table style to disk
            var tableStyleAsXmlResult = tableStyle.SaveToFile("~/Output/Sample12/TableStyle");
            if (!tableStyleAsXmlResult.Success)
            {
                logger.Info("     > Error while saving table style as xml to disk");
                logger.Info($"      > Error: {tableStyleAsXmlResult.Errors.AsMessages().ToStringBuilder()}");
            }
            else
            {
                logger.Info("     > Saved table style as xml to disk correctly");
                logger.Info("       > Path: ~/Output/Sample12/TableStyle.xml");
            }

            var tableStyleAsJsonResult = tableStyle.SaveToFile("~/Output/Sample12/TableStyle", KnownFileFormat.Json);
            if (!tableStyleAsJsonResult.Success)
            {
                logger.Info("     > Error while saving table style as json to disk");
                logger.Info($"      > Error: {tableStyleAsJsonResult.Errors.AsMessages().ToStringBuilder()}");
            }
            else
            {
                logger.Info("     > Saved table style as json to disk correctly");
                logger.Info("       > Path: ~/Output/Sample12/TableStyle.json");
            }

            // New table style instances from disk
            var tableStyleFromXml = PdfTableStyle.LoadFromFile("~/Output/Sample12/TableStyle.xml");
            logger.Info(tableStyleFromXml == null
                ? "     > Error while loading table style from xml file"
                : "     > Table style loaded correctly from xml '~/Output/Sample12/TableStyle.xml' file");

            var tableStyleFromJson = PdfTableStyle.LoadFromFile("~/Output/Sample12/TableStyle.json", KnownFileFormat.Json);
            logger.Info(tableStyleFromJson == null
                ? "     > Error while loading table style from json file"
                : "     > Table style loaded correctly from json '~/Output/Sample12/TableStyle.json' file");

            #endregion
        }
    }
}
