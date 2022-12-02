
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Xml.Serialization;

using iTin.Core.Helpers;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Helpers;

namespace iTin.Core.Models.Design.Charting
{
    /// <summary>
    /// A Specialization of <see cref="IGenericChart"/> interface.<br/>
    /// Defines a generic chart.
    /// </summary>
    public partial class BaseGenericChart : IGenericChart
    {
        #region private constants

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string DefaultBackColor = "White";

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const YesNo DefaultShow = YesNo.Yes;

        #endregion

        #region private members

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private YesNo _show;

        #endregion

        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseGenericChart"/> class.
        /// </summary>
        protected BaseGenericChart()
        {
            Show = DefaultShow;
            BackColor = DefaultBackColor;
        }

        #endregion

        #region interfaces

        #region IChart

        #region explicit

        /// <summary>
        /// Sets the element that owns this <see cref="IChart"/>.
        /// </summary>
        /// <param name="reference">Reference to owner.</param>
        void IChart.SetOwner(ICharts reference) => SetOwner(reference);

        #endregion

        #region public readonly properties

        /// <summary>
        /// Gets the element that owns this <see cref="IChart"/>.
        /// </summary>
        /// <value>
        /// The <see cref="ICharts"/> that owns this <see cref="IChart"/>.
        /// </value>
        public ICharts Owner { get; private set; }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the name of the chart.
        /// </summary>
        /// <value>
        /// The name of the chart.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value that determines whether to display the border. The default is <see cref="YesNo.Yes"/>.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if display the border; otherwise, <see cref="YesNo.No"/>.
        /// </value>
        [DefaultValue(DefaultShow)]
        public YesNo Show
        {
            get => _show;
            set
            {
                SentinelHelper.IsEnumValid(value);
                _show = value;
            }
        }

        #endregion

        #region public override properties

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        public override bool IsDefault =>
            base.IsDefault &&
            Show.Equals(DefaultShow) &&
            BackColor.Equals(DefaultBackColor);

        #endregion

        #endregion

        #region IGenericChart

        #region public properties

        /// <summary>
        /// Gets or sets preferred back color for this chart. The default is <b>White</b>.
        /// </summary>
        /// <value>
        /// Preferred back color. 
        /// </value>
        [XmlAttribute]
        [DefaultValue(DefaultBackColor)]
        public string BackColor { get; set; }

        #endregion

        #region public methods

        /// <summary>
        /// Gets a reference to the <see cref="Color"/> structure than represents back color for this chart.
        /// </summary>
        /// <returns>
        /// A <see cref="Color"/> structure that represents back color.
        /// </returns> 
        public Color GetBackColor() => ColorHelper.GetColorFromString(BackColor);

        #endregion

        #endregion

        #region ICloneable

        #region explicit

        /// <inheritdoc />
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        object ICloneable.Clone() => Clone();

        #endregion

        #endregion

        #region ICombinable<IChart>

        #region explicit

        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        void ICombinable<IChart>.Combine(IChart reference) => Combine((BaseGenericChart)reference);

        #endregion

        #endregion

        #region ICombinable<IGenericChart>

        #region explicit

        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference content</param>
        void ICombinable<IGenericChart>.Combine(IGenericChart reference) => Combine((BaseGenericChart)reference);

        #endregion

        #endregion

        #endregion

        #region public static properties

        /// <summary>
        /// Gets a default generic chart.
        /// </summary>
        /// <value>
        /// A default generic chart.
        /// </value>
        public static BaseGenericChart Default => new();

        #endregion

        #region public methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public BaseGenericChart Clone()
        {
            var cloned = (BaseGenericChart) MemberwiseClone();
            //styleCloned.Borders = Borders.Clone();
            //styleCloned.Content = Content.Clone();
            cloned.Properties = Properties.Clone();

            return cloned;
        }

        #endregion

        #region public virtual methods

        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        public virtual void Combine(BaseGenericChart reference)
        {
            if (reference == null)
            {
                return;
            }

            if (BackColor.Equals(DefaultBackColor))
            {
                BackColor = reference.BackColor;
            }

            //Borders.Combine(reference.Borders);
            //Content.Combine(reference.Content);
        }

        #endregion

        #region internal methods

        /// <summary>
        /// Sets the element that owns this <see cref="IChart"/>.
        /// </summary>
        /// <param name="reference">Reference to owner.</param>
        internal void SetOwner(ICharts reference)
        {
            Owner = reference;
        }

        #endregion
    }
}
