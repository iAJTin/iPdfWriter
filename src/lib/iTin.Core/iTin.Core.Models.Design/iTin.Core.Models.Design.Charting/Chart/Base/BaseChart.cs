
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Helpers;

using iTin.Core.Models.Design.Enums;

namespace iTin.Core.Models.Design.Charting
{
    /// <summary>
    /// A Specialization of <see cref="IChart"/> interface.<br/>
    /// Defines a generic chart.
    /// </summary>
    public partial class BaseChart : IChart
    {
        #region private constants
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const YesNo DefaultShow = YesNo.Yes;
        #endregion

        #region private members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private YesNo _show;
        #endregion

        #region constructor/s

        #region [protected] BaseChart(): Initializes a new instance of this class
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseChart"/> class.
        /// </summary>
        protected BaseChart()
        {
            Show = DefaultShow;
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
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
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
        [XmlAttribute]
        public string Name { get; set; }

        #endregion

        #region [public] (YesNo) Show: Gets or sets a value that determines whether to display the border
        /// <summary>
        /// Gets or sets a value that determines whether to display the chart. The default is <see cref="YesNo.Yes"/>.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if display the chart; otherwise, <see cref="YesNo.No"/>.
        /// </value>
        [XmlAttribute]
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
            Show.Equals(DefaultShow);
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

        #region ICombinable

        #region explicit

        #region (object) ICombinable<IChart>.Combine(IChart): Creates a new object that is a copy of the current instance
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        void ICombinable<IChart>.Combine(IChart reference) => Combine((BaseChart)reference);
        #endregion

        #endregion

        #endregion

        #endregion

        #region public static properties

        #region [public] {static} (BaseChart) Default: Gets a default chart
        /// <summary>
        /// Gets a default chart.
        /// </summary>
        /// <value>
        /// A default chart.
        /// </value>
        public static BaseChart Default => new BaseChart();
        #endregion

        #endregion

        #region public static methods

        #region [public] {static} (string) GenerateRandomChartName(): Returns a random graph name
        /// <summary>
        /// Returns a random graph name.
        /// </summary>
        /// <returns>
        /// A new graph name.
        /// </returns>
        public static string GenerateRandomChartName() 
            => Path.ChangeExtension(IO.File.GetUniqueTempRandomFile().Segments.Last(), string.Empty).Replace(".", string.Empty);
        #endregion

        #endregion

        #region public methods

        #region [public] (BaseChart) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public BaseChart Clone()
        {
            var cloned = (BaseChart)MemberwiseClone();
            //styleCloned.Borders = Borders.Clone();
            //styleCloned.Content = Content.Clone();
            cloned.Properties = Properties.Clone();

            return cloned;
        }
        #endregion

        #endregion

        #region public virtual methods

        #region [public] {virtual} (void) Combine(BaseChart): Combines this instance with reference parameter
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        public virtual void Combine(BaseChart reference)
        {
            if (reference == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(Name))
            {
                throw new NullReferenceException("Primero asignar un nombre al gráfico antes de combinar");
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
