
namespace iTin.Core.Models.Design.Charting
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Xml.Serialization;

    using iTin.Core.Helpers;

    using Enums;
    using Helpers;

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

        #region [protected] BaseGenericChart(): Initializes a new instance of this class
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseGenericChart"/> class.
        /// </summary>
        protected BaseGenericChart()
        {
            Show = DefaultShow;
            BackColor = DefaultBackColor;
        }
        #endregion

        #endregion

        #region interfaces

        #region IChart

        #region explicit

        #region (void) IChart.SetOwner(ICharts): Sets the element that owns this
        /// <summary>
        /// Sets the element that owns this <see cref="IChart"/>.
        /// </summary>
        /// <param name="reference">Reference to owner.</param>
        void IChart.SetOwner(ICharts reference) => SetOwner(reference);
        #endregion

        #endregion

        #region public readonly properties

        #region [public] (ICharts) Owner: Gets the element that owns this
        /// <summary>
        /// Gets the element that owns this <see cref="IChart"/>.
        /// </summary>
        /// <value>
        /// The <see cref="ICharts"/> that owns this <see cref="IChart"/>.
        /// </value>
        public ICharts Owner { get; private set; }
        #endregion

        #endregion

        #region public properties

        #region [public] (string) Name: Gets or sets the name of the chart
        /// <summary>
        /// Gets or sets the name of the chart.
        /// </summary>
        /// <value>
        /// The name of the chart.
        /// </value>
        public string Name { get; set; }

        #endregion

        #region [public] (YesNo) Show: Gets or sets a value that determines whether to display the border
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

        #endregion

        #region public override properties

        #region [public] {overide} (bool) IsDefault: Gets a value indicating whether this instance is default
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

        #endregion

        #region IGenericChart

        #region public properties

        #region [public] (string) BackColor: Gets or sets preferred back color for this chart
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

        #endregion

        #region public methods

        #region [public] (Color) GetBackColor(): Gets a reference to the Color structure than represents back color for this chart
        /// <summary>
        /// Gets a reference to the <see cref="Color"/> structure than represents back color for this chart.
        /// </summary>
        /// <returns>
        /// A <see cref="Color"/> structure that represents back color.
        /// </returns> 
        public Color GetBackColor() => ColorHelper.GetColorFromString(BackColor);
        #endregion

        #endregion

        #endregion

        #region ICloneable

        #region explicit

        #region (object) ICloneable.Clone(): Creates a new object that is a copy of the current instance
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

        #endregion

        #region ICombinable<IChart>

        #region explicit

        #region (void) ICombinable<IChart>.Combine(IChart): Creates a new object that is a copy of the current instance
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        void ICombinable<IChart>.Combine(IChart reference) => Combine((BaseGenericChart)reference);
        #endregion

        #endregion

        #endregion

        #region ICombinable<IGenericChart>

        #region explicit

        #region (void) ICombinable<IGenericChart>.Combine(IGenericChart): Combines this instance with reference parameter
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference content</param>
        void ICombinable<IGenericChart>.Combine(IGenericChart reference) => Combine((BaseGenericChart)reference);
        #endregion

        #endregion

        #endregion

        #endregion

        #region public static properties

        #region [public] {static} (BaseGenericChart) Default: Gets a default generic chart
        /// <summary>
        /// Gets a default generic chart.
        /// </summary>
        /// <value>
        /// A default generic chart.
        /// </value>
        public static BaseGenericChart Default => new BaseGenericChart();
        #endregion

        #endregion

        #region public methods

        #region [public] (BaseGenericChart) Clone(): Clones this instance
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

        #endregion

        #region public virtual methods

        #region [public] {virtual} (void) Combine(BaseGenericChart): Combines this instance with reference parameter
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

        #endregion

        #region internal methods

        #region [internal] (void) SetOwner(ICharts): Sets the element that owns this
        /// <summary>
        /// Sets the element that owns this <see cref="IChart"/>.
        /// </summary>
        /// <param name="reference">Reference to owner.</param>
        internal void SetOwner(ICharts reference)
        {
            Owner = reference;
        }
        #endregion

        #endregion
    }
}
