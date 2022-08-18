
using System.Collections.Generic;
using System.Linq;

using iTin.Core;

namespace iTin.Utilities.Pdf.Writer.ComponentModel.Input
{
    /// <summary>
    /// 
    /// </summary>
    public class PdfInputCache
    {
        #region private static readonly members

        private static IDictionary<IInput, IEnumerable<ISet>> SetsCacheDictionary { get; } = new Dictionary<IInput, IEnumerable<ISet>>();

        private static IDictionary<IInput, IEnumerable<IInsert>> InsertsCacheDictionary { get; } = new Dictionary<IInput, IEnumerable<IInsert>>();
        
        private static IDictionary<IInput, IEnumerable<IReplace>> TextReplacementsCacheDictionary { get; } = new Dictionary<IInput, IEnumerable<IReplace>>();

        #endregion

        #region public static readonly properties

        #region [public] {static} (PdfInputCache) Cache: Gets an unique reference to the available pdfinput cache
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

        #endregion

        #region public methods

        #region [public] (void) AddInserts(IInput, IInsert): Add a collection of IInsert items for specified PdfInput key
        /// <summary>
        /// Add a collection of <see cref="IInsert"/> items for specified <see cref="PdfInput"/> key.
        /// </summary>
        /// <param name="key">Target <see cref="PdfInput"/>.</param>
        /// <param name="data">Target <see cref="IInsert"/> item.</param>
        public void AddInserts(IInput key, IInsert data)
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

        #endregion

        #region [public] (void) AddSets(IInput, ISet): Add a collection of ISet items for specified PdfInput key
        /// <summary>
        /// Add a collection of <see cref="IReplace"/> items for specified <see cref="PdfInput"/> key.
        /// </summary>
        /// <param name="key">Target <see cref="PdfInput"/>.</param>
        /// <param name="data">Target <see cref="ISet"/> item.</param>
        public void AddSets(IInput key, ISet data)
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

        #endregion

        #region [public] (void) AddTextReplacement(IInput, IReplace): Add a collection of IReplace items for specified PdfInput key
        /// <summary>
        /// Add a collection of <see cref="IReplace"/> items for specified <see cref="PdfInput"/> key.
        /// </summary>
        /// <param name="key">Target <see cref="PdfInput"/>.</param>
        /// <param name="data">Target <see cref="IReplace"/> item.</param>
        public void AddTextReplacement(IInput key, IReplace data)
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

        #endregion


        #region [public] (bool) ExistInsertInput(IInput): 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Target <see cref="IInput"/>.</param>
        /// <returns>
        /// The collection of available <see cref="IInsert"/> items.
        /// </returns>
        public bool ExistInsertInput(IInput key) => InsertsCacheDictionary.ContainsKey(key);
        #endregion

        #region [public] (bool) ExistSetInput(IInput): 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Target <see cref="IInput"/>.</param>
        /// <returns>
        /// The collection of available <see cref="IReplace"/> items.
        /// </returns>
        public bool ExistSetInput(IInput key) => SetsCacheDictionary.ContainsKey(key);
        #endregion

        #region [public] (bool) ExistTextReplacementInput(IInput): 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Target <see cref="IInput"/>.</param>
        /// <returns>
        /// The collection of available <see cref="IReplace"/> items.
        /// </returns>
        public bool ExistTextReplacementInput(IInput key) => TextReplacementsCacheDictionary.ContainsKey(key);
        #endregion


        #region [public] (IEnumerable<IInsert>) GetSets(IInput): 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Target <see cref="IInput"/>.</param>
        /// <returns>
        /// The collection of available <see cref="IInsert"/> items.
        /// </returns>
        public IEnumerable<IInsert> GetInserts(IInput key) => InsertsCacheDictionary[key];
        #endregion

        #region [public] (IEnumerable<ISet>) GetSets(IInput): 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Target <see cref="IInput"/>.</param>
        /// <returns>
        /// The collection of available <see cref="ISet"/> items.
        /// </returns>
        public IEnumerable<ISet> GetSets(IInput key) => SetsCacheDictionary[key];
        #endregion

        #region [public] (IEnumerable<IReplace>) GetTextReplacements(IInput): 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Target <see cref="IInput"/>.</param>
        /// <returns>
        /// The collection of available <see cref="IReplace"/> items.
        /// </returns>
        public IEnumerable<IReplace> GetTextReplacements(IInput key) => TextReplacementsCacheDictionary[key];
        #endregion

        #endregion
    }
}
