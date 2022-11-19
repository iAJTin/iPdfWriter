
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

using iTin.AspNet.Web.ComponentModel;
using iTin.Core;
using iTin.Core.Helpers;

using iTinIO = iTin.Core.IO;

namespace iTin.AspNet.Web
{
    /// <summary>
    /// Static class than contains extension methods for objects of type <see cref="T:System.IO.Stream" />.
    /// </summary> 
    public static class StreamExtensions
    {
        /// <summary>
        /// Downloads the specified data with the name specified in <paramref name="fileName"/>.
        /// </summary>
        /// <param name="target">Data to download.</param>
        /// <param name="fileName">Name of the file to download.</param>
        /// <param name="response">The response to use.</param>
        public static void Download(this Stream target, string fileName, HttpResponse response)
        {
            SentinelHelper.ArgumentNull(target, nameof(target));
            SentinelHelper.ArgumentNull(fileName, nameof(fileName));
            SentinelHelper.IsFalse(iTinIO.File.IsValidFileName(fileName), "Filename does not have a valid name");

            HttpResponseEx info = new()
            {
                ContentType = HttpResponseEx.GetMimeMapping(Path.GetExtension(fileName).Replace(".", string.Empty)),
                ContentDisposition = string.Format(CultureInfo.InvariantCulture, "attachment; filename={0}", fileName)
            };

            target.DownloadImpl(response, info);
        }

        /// <summary>
        /// Downloads the specified data with the name specified in <paramref name="fileName"/> asynchronously.
        /// </summary>
        /// <param name="target">Data to download.</param>
        /// <param name="fileName">Name of the file to download.</param>
        /// <param name="response">The response to use.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public static async Task DownloadAsync(this Stream target, string fileName, HttpResponse response, CancellationToken cancellationToken = default)
        {
            SentinelHelper.ArgumentNull(target, nameof(target));
            SentinelHelper.ArgumentNull(fileName, nameof(fileName));
            SentinelHelper.IsFalse(iTinIO.File.IsValidFileName(fileName), "Filename does not have a valid name");

            HttpResponseEx info = new()
            {
                ContentType = HttpResponseEx.GetMimeMapping(Path.GetExtension(fileName).Replace(".", string.Empty)),
                ContentDisposition = string.Format(CultureInfo.InvariantCulture, "attachment; filename={0}", fileName)
            };

            await target.DownloadImplAsync(response, info, cancellationToken);
        }


        /// <summary>
        /// Downloads the specified file in <paramref name="responseInfo"/>.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="response">The response.</param>
        /// <param name="responseInfo">The response information.</param>
        private static void DownloadImpl(this Stream target, HttpResponse response, HttpResponseEx responseInfo)
        {
            response.Clear();
            response.ContentType = responseInfo.ContentType;
            response.AddHeader("content-disposition", responseInfo.ContentDisposition);
            response.BinaryWrite(target.AsByteArray());
            response.Flush();
            response.End();
        }

        /// <summary>
        /// Downloads the specified file in <paramref name="responseInfo"/> asynchronously.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="response">The response.</param>
        /// <param name="responseInfo">The response information.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        private static async Task DownloadImplAsync(this Stream target, HttpResponse response, HttpResponseEx responseInfo, CancellationToken cancellationToken = default)
        {
            response.Clear();
            response.ContentType = responseInfo.ContentType;
            response.AddHeader("content-disposition", responseInfo.ContentDisposition);
            response.BinaryWrite(await target.AsByteArrayAsync(cancellationToken));
            await response.FlushAsync();
            response.End();
        }
    }
}
