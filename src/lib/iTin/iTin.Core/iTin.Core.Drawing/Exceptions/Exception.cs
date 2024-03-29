﻿
using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace iTin.Core.Drawing.Exceptions
{
    /// <summary>
    /// Class Exception.
    /// </summary>
    /// <seealso cref="System.Exception"/>
    [Serializable]
    internal class Exception : System.Exception
    {
        internal static object Lock = new();
        internal static ResourceManager InnerGeneralExceptionResourceManager;

        #region constructor/s
        /// <summary>
        /// Initializes a new instance of the <see cref="Exception"/> class.
        /// </summary>
        public Exception()
        {
            ResourceKey = "UNKNOWN_ERROR";
            ResourceFile = "iTin.Core.Drawing.Localization.Exceptions.Exception";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Exception"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public Exception(string code)
        {
            ResourceKey = code;
            ResourceFile = "iTin.Core.Drawing.Localization.Exceptions.Exception";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Exception"/> class.
        /// </summary>
        /// <param name="resourceFile">The resource file.</param>
        /// <param name="resourceKey">The resource key.</param>
        public Exception(string resourceFile, string resourceKey)
        {
            ResourceKey = resourceKey;
            ResourceFile = resourceFile;
        }
        #endregion

        #region public properties
        /// <summary>
        /// Gets or sets the resource key.
        /// </summary>
        /// <value>
        /// The resource key.
        /// </value>
        public string ResourceKey { get; set; }

        /// <summary>
        /// Gets or sets the resource file.
        /// </summary>
        /// <value>The resource file.</value>
        public string ResourceFile { get; set; }
        #endregion

        #region public override properties
        /// <summary>
        /// Obtiene un mensaje que describe la excepción actual.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public override string Message => GetMessage();

        #endregion

        #region internal static properties
        internal static ResourceManager GeneralExceptionResourceManager
        {
            get
            {
                if (InnerGeneralExceptionResourceManager != null)
                {
                    return InnerGeneralExceptionResourceManager;
                }

                lock (Lock)
                {
                    InnerGeneralExceptionResourceManager = new ResourceManager(Type.GetType("iTin.Core.Drawing.Localization.Exceptions.Exception")!);
                }

                return InnerGeneralExceptionResourceManager;
            }
        }

        #endregion

        #region public virtual methods

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <returns>
        /// System.String.
        /// </returns>
        public virtual string GetMessage() => GetMessage(CultureInfo.CurrentUICulture.Name);

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <returns>
        /// System.String.
        /// </returns>
        public virtual string GetMessage(string language) => GetMessage(language, ResourceFile, ResourceKey);

        #endregion

        #region internal static methods

        internal static string GetMessage(string type, string code) => GetMessage(CultureInfo.CurrentUICulture.Name, type, code);

        internal static string GetMessage(string language, string resourceFile, string resourceKey)
        {
            Type resourceType = Type.GetType(resourceFile);
            if (resourceType == null)
            {
                return GetUnknowErrorMessage(language);
            }

            ResourceManager resourceManager = new ResourceManager(resourceType);
            string message = resourceManager.GetString(resourceKey, CultureInfo.GetCultureInfo(language));

            return !string.IsNullOrWhiteSpace(message) 
                ? message 
                : GetUnknowErrorMessage(language);
        }

        internal static string GetUnknowErrorMessage(string language)
        {
            if (Thread.CurrentThread.CurrentUICulture.Name == language)
            {
                return Localization.Exceptions.Exception.UNKNOWN_ERROR;
            }

            if (GeneralExceptionResourceManager == null)
            {
                return Localization.Exceptions.Exception.UNKNOWN_ERROR;
            }

            string message = GeneralExceptionResourceManager.GetString("UNKNOWN_ERROR", CultureInfo.GetCultureInfo(language));

            return !string.IsNullOrWhiteSpace(message)
                ? message
                : Localization.Exceptions.Exception.UNKNOWN_ERROR;
        }
        #endregion
    }
}
