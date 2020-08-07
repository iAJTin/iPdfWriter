
namespace iTin.Core.Models.Design.Styling
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using iTin.Core.Models.Collections;
    using iTin.Core.Models.Design.Enums;

    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://schemas.iTin.com/style/v1.0")]
    public partial class BordersCollection : BaseComplexModelCollection<BaseBorder, object, KnownBorderPosition>
    {
    }
}
