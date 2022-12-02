
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using iTin.Core;

namespace iPdfWriter.Input
{
    using Operations.Insert;
    using Operations.Replace;
    using Operations.Set;

    /// <summary>
    /// 
    /// </summary>
    public class PdfInputCache
    {
        #region private static readonly members

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static IDictionary<IPdfInput, IEnumerable<ISet>> SetsCacheDictionary { get; } = new Dictionary<IPdfInput, IEnumerable<ISet>>();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static IDictionary<IPdfInput, IEnumerable<IInsert>> InsertsCacheDictionary { get; } = new Dictionary<IPdfInput, IEnumerable<IInsert>>();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static IDictionary<IPdfInput, IEnumerable<IReplace>> TextReplacementsCacheDictionary { get; } = new Dictionary<IPdfInput, IEnumerable<IReplace>>();

        #endregion

        #region public static readonly properties

        /// <summary>
        /// Gets a reference to the available <see cref="PdfInput"/> cache.
        /// </summary>
        /// <value>
        /// A unique <see cref="PdfInputCache"/> reference that handles <see cref="PdfInput"/> items.
        /// </value>
        /// <remarks>
        /// Static members are 'eagerly initialized', that is, immediately when class is loaded for the first time.
        /// .NET guarantees thread safety for static initialization.
        /// </remarks>
        public static PdfInputCache Cache { get; } = new();

        #endregion

        #region public methods

        /// <summary>
        /// Add a collection of <see cref="IInsert"/> items for specified <see cref="PdfInput"/> key.
        /// </summary>
        /// <param name="key">Target <see cref="PdfInput"/>.</param>
        /// <param name="data">Target <see cref="IInsert"/> item.</param>
        public void AddInserts(IPdfInput key, IInsert data)
        {
            var existKey = InsertsCacheDictionary.ContainsKey(key);
            if (!existKey)
            {
                InsertsCacheDictionary.Add(key, data.Yield());
            }
            else
            {
                var currentKey = InsertsCacheDictionary[key];
                var alreadyExistValue = currentKey.Contains(data);
                if (!alreadyExistValue)
                {
                    var items = InsertsCacheDictionary[key].ToList();
                    items.Add(data);
                    InsertsCacheDictionary[key] = items;
                }
            }
        }

        /// <summary>
        /// Add a collection of <see cref="IReplace"/> items for specified <see cref="PdfInput"/> key.
        /// </summary>
        /// <param name="key">Target <see cref="PdfInput"/>.</param>
        /// <param name="data">Target <see cref="ISet"/> item.</param>
        public void AddSets(IPdfInput key, ISet data)
        {
            var existKey = SetsCacheDictionary.ContainsKey(key);
            if (!existKey)
            {
                SetsCacheDictionary.Add(key, data.Yield());
            }
            else
            {
                var currentKey = SetsCacheDictionary[key];
                var alreadyExistValue = currentKey.Contains(data);
                if (!alreadyExistValue)
                {
                    var items = SetsCacheDictionary[key].ToList();
                    items.Add(data);
                    SetsCacheDictionary[key] = items;
                }
            }
        }

        /// <summary>
        /// Add a collection of <see cref="IReplace"/> items for specified <see cref="PdfInput"/> key.
        /// </summary>
        /// <param name="key">Target <see cref="PdfInput"/>.</param>
        /// <param name="data">Target <see cref="IReplace"/> item.</param>
        public void AddTextReplacement(IPdfInput key, IReplace data)
        {
            var existKey = TextReplacementsCacheDictionary.ContainsKey(key);
            if (!existKey)
            {
                TextReplacementsCacheDictionary.Add(key, data.Yield());
            }
            else 
            {
                var currentKey = TextReplacementsCacheDictionary[key];
                var alreadyExistValue = currentKey.Contains(data);
                if (!alreadyExistValue)
                {
                    var items = TextReplacementsCacheDictionary[key].ToList();
                    items.Add(data);
                    TextReplacementsCacheDictionary[key] = items;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Target <see cref="IPdfInput"/>.</param>
        /// <returns>
        /// The collection of available <see cref="IInsert"/> items.
        /// </returns>
        public bool AnyInserts(IPdfInput key) => InsertsCacheDictionary.ContainsKey(key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Target <see cref="IPdfInput"/>.</param>
        /// <returns>
        /// The collection of available <see cref="IReplace"/> items.
        /// </returns>
        public bool AnySets(IPdfInput key) => SetsCacheDictionary.ContainsKey(key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Target <see cref="IPdfInput"/>.</param>
        /// <returns>
        /// The collection of available <see cref="IReplace"/> items.
        /// </returns>
        public bool AnyTextReplacements(IPdfInput key) => TextReplacementsCacheDictionary.ContainsKey(key);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Target <see cref="IPdfInput"/>.</param>
        /// <returns>
        /// The collection of available <see cref="IInsert"/> items.
        /// </returns>
        public IEnumerable<IInsert> GetInserts(IPdfInput key) => InsertsCacheDictionary[key];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Target <see cref="IPdfInput"/>.</param>
        /// <returns>
        /// The collection of available <see cref="ISet"/> items.
        /// </returns>
        public IEnumerable<ISet> GetSets(IPdfInput key) => SetsCacheDictionary[key];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Target <see cref="IPdfInput"/>.</param>
        /// <returns>
        /// The collection of available <see cref="IReplace"/> items.
        /// </returns>
        public IEnumerable<IReplace> GetTextReplacements(IPdfInput key) => TextReplacementsCacheDictionary[key];

        #endregion
    }
}
