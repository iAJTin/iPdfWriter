
using System.Collections.Generic;

using iTin.Core.Models.Design.Enums;

using iTin.Utilities.Pdf.Design.Styles;

namespace iPdfWriter.ConsoleApp.ComponentModel.Helpers
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

        internal static class Sample03
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

        internal static class Sample04
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
                    "Header",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Verdana",
                            Size = 8.0f,
                            Bold = YesNo.Yes,
                            Color = "Gray"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
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

        internal static class Sample05
        {
            internal static readonly Dictionary<string, PdfTextStyle> TextStylesTable = new()
            {
                {
                    "Header",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Verdana",
                            Size = 8.0f,
                            Bold = YesNo.No,
                            Color = "Gray"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
                {
                    "PageNumber",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Verdana",
                            Size = 8.0f,
                            Bold = YesNo.No,
                            Color = "Gray"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Right
                            }
                        }
                    }
                },
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

        internal static class Sample06
        {
            internal static readonly Dictionary<string, PdfTextStyle> TextStylesTable = new()
            {
                {
                    "Header",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Verdana",
                            Size = 8.0f,
                            Bold = YesNo.No,
                            Color = "Gray"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
                {
                    "PageNumber",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Verdana",
                            Size = 8.0f,
                            Bold = YesNo.No,
                            Color = "Gray"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Right
                            }
                        }
                    }
                },
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

        internal static class Sample07
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

        internal static class Sample08
        {
            internal static readonly Dictionary<string, PdfTextStyle> TextStylesTable = new()
            {
                {
                    "Header",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Verdana",
                            Size = 8.0f,
                            Bold = YesNo.No,
                            Color = "Gray"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
                {
                    "PageNumber",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Verdana",
                            Size = 8.0f,
                            Bold = YesNo.No,
                            Color = "Gray"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Right
                            }
                        }
                    }
                },
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

        internal static class Sample09
        {
            internal static readonly Dictionary<string, PdfTextStyle> TextStylesTable = new()
            {
                {
                    "Header",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Verdana",
                            Size = 8.0f,
                            Bold = YesNo.No,
                            Color = "Gray"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
                {
                    "PageNumber",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Verdana",
                            Size = 8.0f,
                            Bold = YesNo.No,
                            Color = "Gray"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Right
                            }
                        }
                    }
                },
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

        internal static class Sample10
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

        internal static class Sample13
        {
            internal static readonly Dictionary<string, PdfImageStyle> ImagesStylesTable = new()
            {
                {
                    "Default",
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
                }
            };

            internal static readonly Dictionary<string, PdfTextStyle> TextStylesTable = new()
            {
                {
                    "MainTitle",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Arial",
                            Size = 18.0f,
                            Bold = YesNo.Yes,
                            Italic = YesNo.No,
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
                },
                {
                    "RazonSocial",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Arial",
                            Size = 10.0f,
                            Color = "Green"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Top
                            }
                        }
                    }
                },
                {
                    "PersonaSolicitante",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Arial",
                            Size = 10.0f,
                            Color = "Green"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Top
                            }
                        }
                    }
                },
                {
                    "TelefonoSolicitante",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Arial",
                            Size = 10.0f,
                            Color = "Green"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Top
                            }
                        }
                    }
                },
                {
                    "EmailSolicitante",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Arial",
                            Size = 10.0f,
                            Color = "Green"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Top
                            }
                        }
                    }
                },
                {
                    "FechaInicio",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Arial",
                            Size = 10.0f,
                            Color = "Red",
                            Bold = YesNo.Yes
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Top
                            }
                        }
                    }
                },
                {
                    "FechaAcabado",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Arial",
                            Size = 10.0f,
                            Color = "Red",
                            Bold = YesNo.Yes
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Top
                            }
                        }
                    }
                },
                {
                    "Horario",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Arial",
                            Size = 10.0f,
                            Color = "Red",
                            Bold = YesNo.Yes
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Top
                            }
                        }
                    }
                },
                {
                    "RazonSocialEmpresa",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Arial",
                            Size = 10.0f,
                            Color = "#AC1198"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Top
                            }
                        }
                    }
                },
                {
                    "ContactoEmpresa",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Arial",
                            Size = 10.0f,
                            Color = "#AC1198"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Top
                            }
                        }
                    }
                },
                {
                    "TelefonoEmpresa",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Arial",
                            Size = 10.0f,
                            Color = "#AC1198"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Top
                            }
                        }
                    }
                },
                {
                    "EmailEmpresa",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Arial",
                            Size = 10.0f,
                            Color = "#AC1198"
                        },
                        Content =
                        {
                            Alignment =
                            {
                                Vertical = KnownVerticalAlignment.Top
                            }
                        }
                    }
                }
            };
        }

        internal static class Sample20
        {
            internal static readonly Dictionary<string, PdfImageStyle> ImagesStylesTable = new()
            {
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
        }

        internal static class Sample21
        {
            internal static readonly Dictionary<string, PdfBaseStyle> StylesTable = new()
            {
                {
                    "ReportTitle",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Pacifico",
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
                },
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
        }

        internal static class Sample26
        {
            internal static readonly Dictionary<string, PdfBaseStyle> StylesTable = new()
            {
                {
                    "ReportTitle",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Pacifico",
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
                },
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
        }

        internal static class Sample27
        {
            internal static readonly Dictionary<string, PdfBaseStyle> StylesTable = new()
            {
                {
                    "ReportTitle",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Pacifico",
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
                },
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
        }

        internal static class Sample28
        {
            internal static readonly Dictionary<string, PdfBaseStyle> StylesTable = new()
            {
                {
                    "ReportTitle",
                    new PdfTextStyle
                    {
                        Font =
                        {
                            Name = "Pacifico",
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
                },
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
        }
    }
}
