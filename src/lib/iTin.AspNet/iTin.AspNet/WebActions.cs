
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using iTin.Core;
using iTin.Core.ComponentModel;
using iTin.Core.ComponentModel.Results;

namespace iTin.AspNet
{
    /// <summary>
    /// Defines a common actions.
    /// </summary>
    public static class WebActions
    {
        /// <summary>
        /// Try to download synchronously the file associated with the specified path and save it to disk. Check the value of the <b>Success</b> property to check if the operation was successful.
        /// </summary>
        /// <param name="uri">Target url</param>
        /// <param name="fullFilenamePath">Full file path where the downloaded file is saved.</param>
        /// <returns>
        /// Result of the action
        /// </returns>
        public static IResult DownloadFile(Uri uri, string fullFilenamePath)
        {
            var uriIsAccesibleResult = uri.IsAccessible();
            if (!uriIsAccesibleResult.Success)
            {
                return BooleanResult.CreateErrorResult("Specified uri is not accesible");
            }

            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(uri, fullFilenamePath);
                }

                return BooleanResult.SuccessResult;
            }
            catch (Exception e)
            {
                return BooleanResult.FromException(e);
            }
        }

        /// <summary>
        /// Returns a <see cref="StreamResult"/> with the contents of the remote url synchronously. If it cannot download the content it returns <b>Stream.Null</b>.
        /// </summary>
        /// <param name="uri">Target url</param>
        /// <returns>
        /// A <see cref="StreamResult"/> that contains uri content.
        /// </returns>
        public static StreamResult DownloadStream(Uri uri)
        {
            var uriIsAccesibleResult = uri.IsAccessible();
            if (!uriIsAccesibleResult.Success)
            {
                return StreamResult.CreateErrorResult("Specified uri is not accesible");
            }

            try
            {
                StreamResult result;

                using (var client = new HttpClient())
                using (var response = client.GetAsync(uri).GetAwaiter().GetResult())
                using (var remoteStream = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult())
                {
                    result = StreamResult.CreateSuccessResult(remoteStream.Clone());
                }

                return result;
            }
            catch (Exception e)
            {
                return StreamResult.FromException(e);
            }
        }


        /// <summary>
        /// Try to download asynchronously the file associated with the specified path and save it to disk. Check the value of the <b>Success</b> property to check if the operation was successful.
        /// </summary>
        /// <param name="uri">Target url</param>
        /// <param name="fullFilenamePath">Full file path where the downloaded file is saved.</param>
        /// <returns>
        /// Result of the action
        /// </returns>
        public static async Task<IResult> DownloadFileAsync(Uri uri, string fullFilenamePath)
        {
            var uriIsAccesibleResult = await uri.IsAccessibleAsync();
            if (!uriIsAccesibleResult.Success)
            {
                return BooleanResult.CreateErrorResult("Specified uri is not accesible");
            }

            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(uri, fullFilenamePath);
                }

                return BooleanResult.SuccessResult;
            }
            catch (Exception e)
            {
                return BooleanResult.FromException(e);
            }
        }

        /// <summary>
        /// Returns a <see cref="StreamResult"/> with the contents of the remote url asynchronously. If it cannot download the content it returns <b>Stream.Null</b>.
        /// </summary>
        /// <param name="uri">Target url</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A <see cref="StreamResult"/> that contains uri content.
        /// </returns>
        public static async Task<StreamResult> DownloadStreamAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            var uriIsAccesibleResult = await uri.IsAccessibleAsync();
            if (!uriIsAccesibleResult.Success)
            {
                return StreamResult.CreateErrorResult("Specified uri is not accesible");
            }

            try
            {
                StreamResult result;

                using (var client = new HttpClient())
                using (var response = await client.GetAsync(uri, cancellationToken))
                using (var remoteStream = await response.Content.ReadAsStreamAsync())
                {
                    result = StreamResult.CreateSuccessResult(remoteStream.Clone());
                }

                return result;
            }
            catch (Exception e)
            {
                return StreamResult.FromException(e);
            }
        }
    }
}
