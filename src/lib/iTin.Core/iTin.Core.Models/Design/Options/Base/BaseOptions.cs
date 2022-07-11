
using System;
using System.ComponentModel;

using Newtonsoft.Json;

namespace iTin.Core.Models.Design.Options
{
    /// <summary>
    /// Base class for model option elements. 
    /// </summary>
    [Serializable]
    public class BaseOptions
    {
        #region public virtual properties

        #region [public] {virtual} (bool) IsDefault: When overridden in a derived class, gets a value indicating whether this instance contains the default
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether this instance contains the default.
        /// </summary>
        /// <value>
        /// <strong>true</strong> if this instance contains the default; otherwise, <strong>false</strong>.
        /// </value>
        [JsonIgnore]
        [Browsable(false)]
        public virtual bool IsDefault => true;
        #endregion

        #endregion

        #region public overrides methods

        #region [public] {override} (string) ToString(): Returns a string that represents the current object
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current object.
        /// </returns>
        public override string ToString() => !IsDefault ? "Modified" : "Default";
        #endregion

        #endregion
    }
}
