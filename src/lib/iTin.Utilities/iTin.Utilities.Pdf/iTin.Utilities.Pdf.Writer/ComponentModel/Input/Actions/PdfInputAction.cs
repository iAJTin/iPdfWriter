
namespace iTin.Utilities.Pdf.Writer.ComponentModel.Input
{
    /// <summary>
    /// 
    /// </summary>
    public class PdfInputAction : IPdfInputAction
    {
        #region constructor/s

        #region [internal] PdfInputAction(PdfInput): Initializes a new instance of the class
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        internal PdfInputAction(PdfInput input)
        {
            Input = input;
        }
        #endregion

        #endregion

        #region private readonly properties

        private PdfInput Input { get; }

        #endregion

        #region public methods

        #region [public] (IPdfInputAction) Insert(IInsert): 
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
        #endregion

        #region [public] (IPdfInputAction) Replace(IReplace): 
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
        #endregion

        #region [public] (IPdfInputAction) Set(IReplace): 
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

        #endregion
    }
}
