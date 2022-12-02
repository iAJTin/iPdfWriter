
using System.Drawing;

using iTin.Core.Models.Design.Enums;
using iTin.Utilities.Pdf.Design.Image;

namespace iPdfWriter.Operations.Insert
{
    /// <summary>
    /// A Specialization of <see cref="InsertImageBase"/> class.<br/>
    /// Allows insert an image object.
    /// </summary>
    public class InsertImage : InsertImageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsertImage"/> class.
        /// </summary>
        public InsertImage()
        {
            Page = 1;
            Image = PdfImage.Null;
            Offset = PointF.Empty;
            UseTestMode = YesNo.No;
        }
    }
}
