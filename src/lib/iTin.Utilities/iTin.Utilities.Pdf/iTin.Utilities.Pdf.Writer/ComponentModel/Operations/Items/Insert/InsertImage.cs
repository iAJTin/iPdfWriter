
using System;
using System.Drawing;
using System.IO;

using iTin.Utilities.Pdf.Design.Image;
using iTin.Utilities.Pdf.Design.Styles;

using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Insert;

using TextSharp = iTextSharp.text.pdf;

namespace iTin.Utilities.Pdf.Writer.ComponentModel
{
    /// <summary>
    /// A Specialization of <see cref="InsertBase"/> class.<br/>
    /// Allows insert an image object.
    /// </summary>
    public class InsertImage : InsertBase
    {
        #region constructor/s

        #region [public] InsertImage(): Initializes a new instance of the class
        /// <summary>
        /// Initializes a new instance of the <see cref="InsertImage"/> class.
        /// </summary>
        public InsertImage()
        {
            Page = 1;
            Offset = PointF.Empty;
            Style = PdfImageStyle.Default;
            Image = Design.Image.PdfImage.Null;
        }
        #endregion

        #endregion

        #region public properties

        #region [public] (PdfImage) Image: Gets or sets a reference to pdf image object
        /// <summary>
        /// Gets or sets a reference to pdf image object.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImage"/> object that contains a reference to pdf image object.
        /// </value>
        public PdfImage Image { get; set; }
        #endregion

        #endregion

        #region public new properties

        #region [public] {new} (PdfImageStyle) Style: Gets or sets a reference to image style
        /// <summary>
        /// Gets or sets a reference to image style.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImageStyle"/> reference. 
        /// </value>
        public new PdfImageStyle Style { get; set; }
        #endregion

        #endregion

        #region protected override methods

        #region [protected] {override} (InsertResult) InsertImpl(Stream, IInput): Implementation to execute when insert action
        /// <summary>
        /// Implementation to execute when insert action.
        /// </summary>
        /// <param name="input">stream input</param>
        /// <param name="context">Input context</param>
        /// <returns>
        /// <para>
        /// A <see cref="InsertResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return value is <see cref="InsertResultData"/>, which contains the operation result
        /// </para>
        /// </returns>
        protected override InsertResult InsertImpl(Stream input, IInput context)
        {
            if (Style == null)
            {
                Style = PdfImageStyle.Default;
            }

            if (Image == Design.Image.PdfImage.Null)
            {
                return InsertResult.CreateSuccessResult(new InsertResultData
                {
                    Context = context,
                    InputStream = input,
                    OutputStream = input
                });
            }

            return InsertImpl(context, input, Page, Image, Offset, Style);
        }

        #endregion

        #endregion

        #region private static methods

        private static InsertResult InsertImpl(IInput context, Stream input, int page, PdfImage image, PointF imageOffset, PdfImageStyle style)
        {
            var outputStream = new MemoryStream();

            try
            {
                var reader = new TextSharp.PdfReader(input);
                var stamper = new TextSharp.PdfStamper(reader, outputStream);

                var pages = reader.NumberOfPages;
                for (var pdfPage = 1; pdfPage <= pages; pdfPage++)
                {
                    if (pdfPage != page)
                    {
                        continue;
                    }

                    var cb = stamper.GetOverContent(pdfPage);
                    image.Image.SetAbsolutePosition(imageOffset.X, imageOffset.Y);
                    cb.AddImage(image.Image);
                    break;
                }

                stamper.Close();
                reader.Close();

                return InsertResult.CreateSuccessResult(new InsertResultData
                {
                    Context = context,
                    InputStream = input,
                    OutputStream = new MemoryStream(outputStream.GetBuffer())
                });
            }
            catch (Exception ex)
            {
                return InsertResult.FromException(
                    ex,
                    new InsertResultData
                    {
                        Context = context,
                        InputStream = input,
                        OutputStream = input
                    });
            }
        }

        #endregion
    }
}
