
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using iTin.Core.Models.Design.Styling;

namespace iTin.Utilities.Pdf.Design.Styles
{
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://schemas.iTin.com/pdf/style/v1.0")]
    public partial class PdfImageContentAlignment : BaseContentAlignment
    {
    }
}
