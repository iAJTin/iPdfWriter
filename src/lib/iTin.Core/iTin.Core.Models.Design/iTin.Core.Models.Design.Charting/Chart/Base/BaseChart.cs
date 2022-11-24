
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

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseChart"/> class.
        /// </summary>
        protected BaseChart()
        {
            Show = DefaultShow;
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
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        public ICharts Owner { get; private set; }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the name of the chart.
        /// </summary>
        /// <value>
        /// The name of the chart.
        /// </value>
        [XmlAttribute]
        public string Name { get; set; }

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

        #region public override properties

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

        #region ICombinable

        #region explicit

        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        void ICombinable<IChart>.Combine(IChart reference) => Combine((BaseChart)reference);

        #endregion

        #endregion

        #endregion

        #region public static properties

        /// <summary>
        /// Gets a default chart.
        /// </summary>
        /// <value>
        /// A default chart.
        /// </value>
        public static BaseChart Default => new();

        #endregion

        #region public static methods

        /// <summary>
        /// Returns a random graph name.
        /// </summary>
        /// <returns>
        /// A new graph name.
        /// </returns>
        public static string GenerateRandomChartName() => 
            Path.ChangeExtension(IO.File.GetUniqueTempRandomFile().Segments.Last(), string.Empty)
                .Replace(".", string.Empty);

        #endregion

        #region public methods

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

        #region public virtual methods

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
