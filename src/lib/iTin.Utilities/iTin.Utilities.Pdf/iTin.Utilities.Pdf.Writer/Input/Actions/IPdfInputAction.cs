
using iTin.Utilities.Pdf.Writer.Operations.Insert;
using iTin.Utilities.Pdf.Writer.Operations.Replace;
using iTin.Utilities.Pdf.Writer.Operations.Set;

namespace iTin.Utilities.Pdf.Writer.Input
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPdfInputAction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IPdfInputAction Insert(IInsert data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IPdfInputAction Replace(IReplace data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IPdfInputAction Set(ISet data);
    }
}
