
namespace iTin.Core.Models.Design.Charting
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using iTin.Core.Models;

    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://schemas.iTin.com/chart/v1.0")]
    public partial class BaseGenericChart : BaseModel<BaseGenericChart>
    {
    }
}
