
namespace iTin.Core.Models.Design.Charting
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using iTin.Core.Models.Collections;

    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://schemas.iTin.com/style/v1.0")]
    public partial class ChartsCollection : BaseComplexModelCollection<BaseChart, object, string>
    {
    }
}
