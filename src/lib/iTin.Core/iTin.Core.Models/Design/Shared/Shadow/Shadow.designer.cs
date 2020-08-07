
namespace iTin.Core.Models.Design
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using iTin.Core.Models;

    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://schemas.iTin.com/charting/chart/v1.0")]
    public partial class Shadow : BaseModel<Shadow>
    {
    }
}
