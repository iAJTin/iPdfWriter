
namespace iTin.Utilities.Pdf.Design.Styles
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using iTin.Core.Models;

    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlInclude(typeof(PdfTextStyle))]
    [XmlInclude(typeof(PdfTableStyle))]
    [XmlInclude(typeof(PdfImageStyle))]
    [XmlType(Namespace = "http://schemas.iTin.com/pdf/style/v1.0")]
    public partial class PdfBaseStyle : BaseModel<PdfBaseStyle>
    {
    }
}
