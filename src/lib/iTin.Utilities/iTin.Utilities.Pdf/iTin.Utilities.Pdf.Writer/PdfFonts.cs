
namespace iTin.Utilities.Pdf.Writer
{
    using System;
    using System.Collections.Generic;

    using iTextSharp.text;
    using iTextSharp.text.pdf;

    using iTin.Core.ComponentModel;
    using iTin.Core.ComponentModel.Results;

    using iTinIO = iTin.Core.IO;

    /// <summary>
    /// Represents 
    /// </summary>
    public static class PdfFonts
    {
        #region public static readonly members
        /// <summary>
        /// 
        /// </summary>
        public static readonly Dictionary<string, Font> Fonts = new();
        #endregion

        #region public static members

        #region [public] {static} (void) RegisterFont(string, string): 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fontName">Font name</param>
        /// <param name="fontFullPath">Full path to font file</param>
        public static void RegisterFont(string fontName, string fontFullPath)
        {
            if (Fonts.ContainsKey(fontName))
            {
                return;
            }

            var result = TryRegisterFont(fontName, fontFullPath);
            if (result.Success)
            {
                Fonts.Add(fontName, CreateFontFont(fontName));
            }
        }
        #endregion

        #region [public] {static} (void) RegisterFonts(string, string): 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fontNames">Font name</param>
        /// <param name="fontFullPaths">Full path to font file</param>
        public static void RegisterFonts(string[] fontNames, string[] fontFullPaths)
        {
            if (fontNames.Length != fontFullPaths.Length)
            {
                return;
            }

            for (var i = 0; i < fontNames.Length; i++)
            {
                RegisterFont(fontNames[i], fontFullPaths[i]);
            }
        }
        #endregion

        #region [public] {static} (void) RegisterDefaultFonts(string, string): 
        /// <summary>
        /// 
        /// </summary>
        public static void RegisterDefaultFonts()
        {
            RegisterFont("Awesome Regular", @"~\Resources\Fonts\otfs\Font Awesome\Font Awesome 5 Free-Regular-400.otf");
            RegisterFont("Awesome Solid", @"~\Resources\Fonts\otfs\Font Awesome\Font Awesome 5 Free-Solid-900.otf");
        }
        #endregion

        #endregion

        #region private static members

        private static IResult TryRegisterFont(string fontName, string fontFullPath)
        {
            try
            {
                var isRegistered = FontFactory.IsRegistered(fontName);
                if (isRegistered)
                {
                    return BooleanResult.SuccessResult;
                }

                var path = iTinIO.Path.PathResolver(fontFullPath);
                FontFactory.Register(path, fontName);

                return BooleanResult.SuccessResult;
            }
            catch(Exception ex)
            {
                return BooleanResult.FromException(ex);
            }
        }

        private static Font CreateFontFont(string fontName) => FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

        #endregion
    }
}
