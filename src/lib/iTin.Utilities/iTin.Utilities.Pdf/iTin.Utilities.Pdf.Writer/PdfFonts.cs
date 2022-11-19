
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;

using iTextSharp.text;
using iTextSharp.text.pdf;

using iTin.Core.ComponentModel;
using iTin.Core.ComponentModel.Results;

using iTin.Hardware.Abstractions.Devices.Graphics.Font;

using iTinIO = iTin.Core.IO;

namespace iTin.Utilities.Pdf.Writer
{
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

        /// <summary>
        /// Returns a new <see cref="StringArrayResult"/> reference which contains the font names that you can in turn use in defining the styles if it has been successfully registered.
        /// </summary>
        /// <param name="fontName">The font name.</param>
        /// <param name="fontFullPath">The target full path to font filename. The use of the <strong>~</strong> character is allowed to indicate relative paths.</param>
        /// <returns>
        /// <para>
        /// A <see cref="StringArrayResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Result</b> property will contain the Result; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return is a string <see cref="Array"/>, which contains the operation result.
        /// </para>
        /// </returns>
        public static StringArrayResult RegisterFont(string fontName, string fontFullPath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (Fonts.ContainsKey(fontName))
            {
                return StringArrayResult.CreateSuccessResult(new[] { fontName });
            }

            var result = TryRegisterFont(fontName, fontFullPath);
            if (!result.Success)
            {
                return result;
            }

            Fonts.Add(fontName, CreateFontFont(fontName));

            return result;

        }

        /// <summary>
        /// Returns a new <see cref="StringArrayResult"/> reference which contains the font names that you can in turn use in defining the styles if it has been successfully registered.
        /// </summary>
        /// <param name="fontNames">The font names.</param>
        /// <param name="fontFullPaths">The target full paths to font filenames. The use of the <strong>~</strong> character is allowed to indicate relative paths.</param>
        /// <returns>
        /// <para>
        /// A <see cref="StringArrayResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Result</b> property will contain the Result; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return is a string <see cref="Array"/>, which contains the operation result.
        /// </para>
        /// </returns>
        public static StringArrayResult RegisterFonts(string[] fontNames, string[] fontFullPaths)
        {
            if (fontNames.Length != fontFullPaths.Length)
            {
                return StringArrayResult.CreateErrorResult("The two arrays must have the same length");
            }

            var installedFontNames = new List<string>();
            var notInstalledFontErrors = new List<IResultError>();

            for (var i = 0; i < fontNames.Length; i++)
            {
                var fontResult = RegisterFont(fontNames[i], fontFullPaths[i]);
                if (!fontResult.Success)
                {
                    notInstalledFontErrors.AddRange(fontResult.Errors);
                    
                    continue;
                }

                installedFontNames.AddRange(fontResult.Result);
            }

            return installedFontNames.Any()
                ? StringArrayResult.CreateSuccessResult((string[])installedFontNames.ToArray().Clone())
                : StringArrayResult.CreateErrorResult((IResultError[])notInstalledFontErrors.ToArray().Clone());
        }

        #endregion

        #region private static members

        private static Font CreateFontFont(string fontName) => FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

        private static bool IsValidFontName(string fontName)
        {
            using var ifc = new InstalledFontCollection();

            return ifc.Families.Any(font => font.Name.Equals(fontName, StringComparison.Ordinal));
        }

        private static StringArrayResult TryRegisterFont(string fontName, string fontFullPath)
        {
            var registerWindowsFontResult = TryRegisterWindowsFont(fontName, fontFullPath);
            var registerPdfFontResult = TryRegisterPdfFont(fontName, fontFullPath);

            if (registerWindowsFontResult.Success && registerPdfFontResult.Success)
            {
                return registerWindowsFontResult;
            }

            var errors = new List<IResultError>();
            errors.AddRange(registerWindowsFontResult.Errors);
            errors.AddRange(registerPdfFontResult.Errors);

            return StringArrayResult.CreateErrorResult((IResultError[]) errors.ToArray().Clone());
        }
 
        private static IResult TryRegisterPdfFont(string fontName, string fontFullPath)
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
            catch (Exception ex)
            {
                return BooleanResult.FromException(ex);
            }
        }

        private static StringArrayResult TryRegisterWindowsFont(string fontName, string fontFullPath)
        {
            try
            {
                var isRegisteredInWindows = IsValidFontName(fontName);
                if (!isRegisteredInWindows)
                {
                    var path = iTinIO.Path.PathResolver(fontFullPath);
                    var count = FontOperations.Instance.AddFontResource(path);
                    if (count == 0)
                    {
                        return StringArrayResult.CreateErrorResult("Can not add font in windows");
                    }
                }
                
                using var ifc = new InstalledFontCollection();
                var installedFonts = ifc.Families.Where(font => font.Name.Contains(fontName)).Select(f=> f.Name);
                    
                return StringArrayResult.CreateSuccessResult((string[]) installedFonts.ToArray().Clone());
            }
            catch (Exception e)
            {
                return StringArrayResult.FromException(e);
            }
        }

        #endregion
    }
}
