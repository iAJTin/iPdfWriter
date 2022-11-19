
using System;
using System.ComponentModel;

using iTin.Core.ComponentModel;

namespace iTin.Utilities.Pdf.Writer.Operations.Set
{
    /// <summary>
    /// A Specialization of <see cref="ISet"/> class.<br/>
    /// </summary>
    public class SetBase : ISet
    {
        /// <summary>
        /// 
        /// </summary>
        protected SetBase()
        {
            Key = GetKeyDescription();
        }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }


        /// <summary>
        /// Returns the value of attribute of type <see cref="T:iTin.Core.ComponentModel.KeyDescriptionAttribute"/> for this enum value. 
        /// If this attribute is not defined returns <strong>null</strong> (<b>Nothing</b> in Visual Basic)
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that contains the value of attribute.
        /// </returns>
        private string GetKeyDescription()
        {
            var type = GetType();
            
            DescriptionAttribute attr = Attribute.GetCustomAttribute(type, typeof(KeyDescriptionAttribute)) as KeyDescriptionAttribute;
            var result = attr?.Description;

            return result;
        }
    }
}
