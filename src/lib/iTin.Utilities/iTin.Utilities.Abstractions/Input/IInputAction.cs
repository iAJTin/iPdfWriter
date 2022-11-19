
namespace iTin.Utilities.Writer.Abstractions.Input
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInputAction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IInputAction Insert(IInsert data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IInputAction Replace(IReplace data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IInputAction Set(ISet data);
    }
}
