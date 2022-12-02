
namespace iPdfWriter.Input
{
    using Operations.Insert;
    using Operations.Replace;
    using Operations.Set;

    /// <summary>
    /// 
    /// </summary>
    public class PdfInputAction : IPdfInputAction
    {
        #region constructor/s

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        internal PdfInputAction(IPdfInput input)
        {
            Input = input;
        }

        #endregion

        #region private readonly properties

        private IPdfInput Input { get; }

        #endregion

        #region public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IPdfInputAction Insert(IInsert data)
        {
            PdfInputCache.Cache.AddInserts(Input, data);

            return new PdfInputAction(Input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IPdfInputAction Replace(IReplace data)
        {
            PdfInputCache.Cache.AddTextReplacement(Input, data);

            return new PdfInputAction(Input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IPdfInputAction Set(ISet data)
        {
            PdfInputCache.Cache.AddSets(Input, data);

            return new PdfInputAction(Input);
        }

        #endregion
    }
}
