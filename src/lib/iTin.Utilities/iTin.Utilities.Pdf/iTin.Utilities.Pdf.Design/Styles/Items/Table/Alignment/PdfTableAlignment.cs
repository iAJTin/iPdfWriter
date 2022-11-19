﻿
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Helpers;
using iTin.Core.Models;
using iTin.Core.Models.Design;
using iTin.Core.Models.Design.Enums;

namespace iTin.Utilities.Pdf.Design.Styles
{
    /// <summary>
    /// A Specialization of <see cref="BaseModel{T}"/> class.<br/>
    /// Defines a <b>pdf</b> table alignment.
    /// </summary>
    public partial class PdfTableAlignment : ICombinable<PdfTableAlignment>, ICloneable
    {
        #region private constants

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const KnownVerticalAlignment DefaulVerticalAlignment = KnownVerticalAlignment.Top;

        #endregion

        #region private field members

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private KnownVerticalAlignment _vertical;

        #endregion

        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTableAlignment"/> class.
        /// </summary>
        public PdfTableAlignment()
        {
            Vertical = DefaulVerticalAlignment;
        }

        #endregion

        #region interfaces

        #region ICloneable

        #region private methods

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

        #region public virtual methods

        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        public virtual void Combine(PdfTableAlignment reference)
        {
            if (reference == null)
            {
                return;
            }

            if (Vertical.Equals(DefaulVerticalAlignment))
            {
                Vertical = reference.Vertical;
            }
        }

        #endregion

        #endregion

        #endregion

        #region public static properties

        /// <summary>
        /// Returns a new instance containing default default table alignment settings.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTableAlignment"/> reference containing the default table alignment settings.
        /// </value>
        public static PdfTableAlignment Default => new();

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets vertical table alignment. The default is <see cref="KnownVerticalAlignment.Top"/>.
        /// </summary>
        /// <value>
        /// One of the <see cref="KnownVerticalAlignment"/> values.
        /// </value>
        [XmlAttribute]
        [JsonProperty("vertical")]
        [DefaultValue(DefaulVerticalAlignment)]
        public KnownVerticalAlignment Vertical
        {
            get => _vertical;
            set
            {
                SentinelHelper.IsEnumValid(value);
                _vertical = value;
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
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public override bool IsDefault =>
            base.IsDefault &&
            Vertical.Equals(DefaulVerticalAlignment);

        #endregion

        #region public methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public PdfTableAlignment Clone() => (PdfTableAlignment)MemberwiseClone();

        #endregion

        #region public virtual methods

        /// <summary>
        /// Apply specified options to this alignment.
        /// </summary>
        public void ApplyOptions(PdfTableAlignmentOptions options)
        {
            if (options == null)
            {
                return;
            }

            if (options.IsDefault)
            {
                return;
            }
            
            #region Vertical
            KnownVerticalAlignment? verticalOption = options.Vertical;
            bool verticalHasValue = verticalOption.HasValue;
            if (verticalHasValue)
            {
                Vertical = verticalOption.Value;
            }
            #endregion
        }

        #endregion
    }
}
