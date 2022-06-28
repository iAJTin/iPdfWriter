
using iTin.Core.ComponentModel;
using iTin.Core.Helpers;
using iTin.Logging;

namespace iTin.Core.IO
{
    /// <summary>
    /// Static class than contains extension methods for objects <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.
    /// </summary> 
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Saves this byte array into file.
        /// </summary>
        /// <param name="data">Data to save.</param>
        /// <param name="filename">Destination file.</param>
        /// <returns>
        /// A <see cref="IResult"/> object that contains the operation result
        /// </returns>
        public static IResult SaveToFile(this byte[] data, string filename)
        {
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(ByteArrayExtensions).Assembly.GetName().Name}, v{typeof(ByteArrayExtensions).Assembly.GetName().Version}, Namespace: {typeof(ByteArrayExtensions).Namespace}, Class: {nameof(ByteArrayExtensions)}");
            Logger.Instance.Debug(" Saves this byte array into file");
            Logger.Instance.Debug($" > Signature: ({typeof(IResult)}) SaveToFile(this {typeof(byte[])}, {typeof(string)})");

            SentinelHelper.ArgumentNull(data, nameof(data));
            Logger.Instance.Debug($"   > data: {data.Length} byte(s) [{data[0]} {data[1]} {data[2]} ...]");

            SentinelHelper.IsTrue(string.IsNullOrEmpty(filename));
            Logger.Instance.Debug($"   > filename: {filename}");

            return data.ToMemoryStream().SaveToFile(filename);
        }
    }
}
