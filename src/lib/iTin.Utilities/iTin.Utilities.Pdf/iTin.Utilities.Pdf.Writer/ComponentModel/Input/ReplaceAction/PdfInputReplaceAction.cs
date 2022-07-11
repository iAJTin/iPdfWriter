
namespace iTin.Utilities.Pdf.Writer.ComponentModel.Input
{
    /// <summary>
    /// 
    /// </summary>
    public class PdfInputReplaceAction
    {
        #region constructor/s

        #region [public] InputReplaceAction(PdfInput): Initializes a new instance of the class
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        internal PdfInputReplaceAction(PdfInput input)
        {
            Input = input;
        }
        #endregion

        #endregion

        #region private readonly properties

        private PdfInput Input { get; }

        #endregion

        #region public methods

        #region [public] (InputReplaceAction) Replace(IReplace): 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public PdfInputReplaceAction Replace(IReplace data)
        {
            PdfInputCache.Cache.AddTextReplacement(Input, data);

            return new PdfInputReplaceAction(Input);
        }
        #endregion

        #endregion
    }
}
