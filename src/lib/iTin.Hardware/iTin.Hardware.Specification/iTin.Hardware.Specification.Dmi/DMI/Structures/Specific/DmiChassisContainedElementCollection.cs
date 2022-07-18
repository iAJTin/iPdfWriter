﻿
namespace iTin.Hardware.Specification.Dmi
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Smbios;

    /// <inheritdoc/>
    /// <summary>
    /// Represents a collection of objects <see cref="ChassisContainedElement"/>.
    /// </summary>
    public sealed class DmiChassisContainedElementCollection : ReadOnlyCollection<DmiChassisContainedElement>
    {
        #region constructor/s

        #region [internal] DmiChassisContainedElementCollection(ChassisContainedElementCollection): Initialize a new instance of the class
        /// <inheritdoc/>
        /// <summary>
        /// Initialize a new instance of the class <see cref="DmiChassisContainedElementCollection"/>.
        /// </summary>
        /// <param name="elements">Item list.</param>
        internal DmiChassisContainedElementCollection(ChassisContainedElementCollection elements) : base(AsDmiCollectionFrom(elements).ToList())
        {
        }
        #endregion

        #endregion

        #region public override methods

        #region [public] {override} (string) ToString(): Returns a class String that represents the current object
        /// <summary>
        /// Returns a class <see cref="string"/> that represents the current object.
        /// </summary>
        /// <returns>
        /// Object <see cref="string"/> that represents the current <see cref="ChassisContainedElementCollection"/> class.
        /// </returns>
        /// <remarks>
        /// This method returns a string that includes the number of available elements.
        /// </remarks>                                    
        public override string ToString() => $"Elements = {Items.Count}";
        #endregion

        #endregion              

        #region private static methods

        private static IEnumerable<DmiChassisContainedElement> AsDmiCollectionFrom(ChassisContainedElementCollection elements)
        {
            var items = new Collection<DmiChassisContainedElement>();

            foreach (var element in elements)
            {
                items.Add(new DmiChassisContainedElement(element.Properties));
            }

            return items;
        }

        #endregion
    }
}
