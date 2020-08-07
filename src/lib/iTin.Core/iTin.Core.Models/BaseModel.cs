
namespace iTin.Core.Models
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using iTin.Core;
    using iTin.Core.ComponentModel;
    using iTin.Core.ComponentModel.Results;
    using iTin.Core.Helpers;
    using iTin.Core.IO;

    using NativePath = System.IO.Path;
    using iTinPath = iTin.Core.IO.Path;


    /// <summary>
    /// Base class for model elements.<br/>
    /// Implements functionality to record and read configuration files.
    /// </summary>
    /// <typeparam name="T">Represents a model node type</typeparam>
    [Serializable]
    public class BaseModel<T>
    {
        #region private members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Properties _properties;
        #endregion

        #region public readonly properties

        #region [public] (bool) PropertiesSpecified: Gets a value that tells the serializer if the referenced item is to be included
        /// <summary>
        /// Gets a value that tells the serializer if the referenced item is to be included.
        /// </summary>
        /// <value>
        /// <b>true</b> if the serializer has to include the element; otherwise, <b>false</b>.
        /// </value>
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool PropertiesSpecified => Properties.Any();
        #endregion

        #endregion

        #region public properties

        #region [public] (Properties) Properties: Gets or sets a reference to user-defined property list for this element
        /// <summary>
        /// Gets or sets a reference to user-defined property list for this element.
        /// </summary>
        /// <value>
        /// List of user-defined properties available for this element.
        /// </value>
        [JsonProperty("properties")]
        [XmlArrayItem("Property", typeof(Property), IsNullable = false)]
        public Properties Properties
        {
            get
            {
                if (_properties == null)
                {
                    _properties = new Properties();
                }

                foreach (var property in _properties)
                {
                    property.SetOwner(_properties);
                }

                return _properties;
            }
            set => _properties = value;
        }
        #endregion

        #endregion

        #region public virtual properties

        #region [public] {virtual} (bool) IsDefault: When overridden in a derived class, gets a value indicating whether this instance contains the default
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether this instance contains the default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        [XmlIgnore]
        [JsonIgnore]
        [Browsable(false)]
        public virtual bool IsDefault => Properties.IsDefault;
        #endregion

        #endregion

        #region public static methods

        #region [public] {static} (T) Deserialize(string, KnownFileFormat = KnownFileFormat.Xml): Deserializes the input string into specified model type
        /// <summary>
        /// Deserializes the input string into specified model type. By default, if not specified, it will be used in <b>Xml</b> format.
        /// </summary>
        /// <param name="value">Input string</param>
        /// <param name="format">Input file format</param>
        /// <returns>
        /// A new model reference of type <b>T</b>.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
        public static T Deserialize(string value, KnownFileFormat format = KnownFileFormat.Xml)
        {
            SentinelHelper.ArgumentNull(value, nameof(value));

            return
                format == KnownFileFormat.Xml
                    ? (T)new XmlSerializer(typeof(T)).Deserialize(XmlReader.Create(new StringReader(value)))
                    : JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    });
        }
        #endregion

        #region [public] {static} (T) Deserialize(Stream, KnownFileFormat = KnownFileFormat.Xml): Deserializes the input stream into specified model type
        /// <summary>
        /// Deserializes the input stream into specified model type. By default, if not specified, it will be used in <b>Xml</b> format.
        /// </summary>
        /// <param name="stream">Input stream</param>
        /// <param name="format">Input file format</param>
        /// <returns>
        /// A new model reference of type <b>T</b>.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
        public static T Deserialize(Stream stream, KnownFileFormat format = KnownFileFormat.Xml)
        {
            SentinelHelper.ArgumentNull(stream, nameof(stream));

            return Deserialize(stream.AsString(), format);
        }
        #endregion

        #region [public] {static} (T) LoadFromFile(string, KnownFileFormat = KnownFileFormat.Xml): Returns a reference from a file in xml or json format
        /// <summary>
        /// Returns a reference from a file in <b>Xml</b> or <b>Json</b> format. By default, if it is not specified, it is understood that you are trying to obtain the reference from an <b>Xml</b> file.
        /// The use of the <b>~</b> character is allowed to indicate relative paths.
        /// </summary>
        /// <param name="fileName">Absolute or relative input file path</param>
        /// <param name="format">Input file format</param>
        /// <returns>
        /// A new reference that constains the model.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
        public static T LoadFromFile(string fileName, KnownFileFormat format = KnownFileFormat.Xml)
        {
            SentinelHelper.ArgumentNull(fileName, nameof(fileName));
            SentinelHelper.IsEnumValid(format);

            FileStream file = null;

            try
            {
                var parsedFilenamePath = iTinPath.PathResolver(fileName);
                var fileInfo = new FileInfo(parsedFilenamePath);
                bool existFile = fileInfo.Exists;
                if (!existFile)
                {
                    return default;
                }

                string value;
                file = new FileStream(parsedFilenamePath, FileMode.Open, FileAccess.Read);
                using (var reader = new StreamReader(file))
                {
                    file = null;
                    value = reader.ReadToEnd();
                }

                return Deserialize(value, format);
            }
            catch (InvalidOperationException ex)
            {
                var modelErrorMessage = new StringBuilder();
                modelErrorMessage.AppendLine(ex.Message);
                var inner = ex.InnerException;
                while (true)
                {
                    if (inner == null)
                    {
                        break;
                    }

                    modelErrorMessage.AppendLine(inner.Message);
                    inner = inner.InnerException;
                }

                throw new XmlSchemaValidationException(modelErrorMessage.ToString());
            }

            finally
            {
                file?.Dispose();
            }
        }
        #endregion

        #region [public] {static} (T) LoadFromUri(Uri, KnownFileFormat = KnownFileFormat.Xml): Returns a reference from a uri in xml or json format
        /// <summary>
        /// Returns a reference from a uri in <b>Xml</b> or <b>Json</b> format.
        /// </summary>
        /// <param name="pathUri">File path</param>
        /// <param name="format">Input file format</param>
        /// <returns>
        /// A new reference that constains the model.
        /// </returns>
        public static T LoadFromUri(Uri pathUri, KnownFileFormat format = KnownFileFormat.Xml)
        {
            SentinelHelper.ArgumentNull(pathUri, nameof(pathUri));

            return LoadFromFile(pathUri.LocalPath, format);
        }
        #endregion

        #endregion

        #region public virtual methods

        #region [public] {virtual} (IResult) SaveToFile(string, KnownFileFormat = KnownFileFormat.Xml, SaveOptions = null): Save this model in a xml or json file
        /// <summary>
        /// Save this model in a <b>Xml</b> or <b>Json</b> file. By default, if not specified, it will be saved in <b>Xml</b> format.
        /// You can indicate whether to automatically create the destination path if it does not exist. By default it will try to create the destination path.
        /// The use of the <b>~</b> character is allowed to indicate relative paths.
        /// </summary>
        /// <param name="fileName">Destination file path. Absolute or relative (~) paths are allowed</param>
        /// <param name="format">Output file format</param>
        /// <param name="options">Output model save options</param>
        /// <returns>
        /// <para>
        /// A <see cref="BooleanResult"/> which implements the <see cref="IResult"/> interface reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return value is <see cref="bool"/>, which contains the operation result
        /// </para>
        /// </returns>
        public virtual IResult SaveToFile(string fileName, KnownFileFormat format = KnownFileFormat.Xml, ModelSaveOptions options = null)
        {
            SentinelHelper.ArgumentNull(fileName, nameof(fileName));
            SentinelHelper.IsEnumValid(format);

            try
            {
                var safeOptions = options;
                if (options == null)
                {
                    safeOptions = ModelSaveOptions.Default;
                }

                var candidateFullPath = iTinPath.PathResolver(fileName);
                var filenameWithoutExtension = NativePath.GetFileNameWithoutExtension(candidateFullPath);
                var directoryName = NativePath.GetDirectoryName(candidateFullPath);
                var safeFullFilenamePath = $"{directoryName}{NativePath.DirectorySeparatorChar}{filenameWithoutExtension}.{(format == KnownFileFormat.Xml ? KnownFileFormat.Xml.ToString().ToLowerInvariant() : KnownFileFormat.Json.ToString().ToLowerInvariant())}";

                var rawModel = Serialize(safeOptions, format);

                return
                    rawModel
                        .AsStream(safeOptions.Encoding)
                        .SaveToFile(safeFullFilenamePath, safeOptions.ToSaveOptions());
            }
            catch (Exception ex)
            {
                return BooleanResult.FromException(ex);
            }
        }
        #endregion

        #region [public] {virtual} (string) Serialize(ModelSaveOptions, KnownFileFormat = KnownFileFormat.Xml): Returns a string that contains current model serialized in a xml or json format
        /// <summary>
        /// Returns a <see cref="string"/> that contains current model serialized in a <b>Xml</b> or <b>Json</b> format. By default, if not specified, it will be saved in <b>Xml</b> format.
        /// </summary>
        /// <param name="options">Output model save options</param>
        /// <param name="format">Output file format</param>
        /// <returns>
        /// A <see cref="string"/> that contains serialized model.
        /// </returns>
        public virtual string Serialize(ModelSaveOptions options, KnownFileFormat format = KnownFileFormat.Xml)
        {
            SentinelHelper.IsEnumValid(format);

            string value;
            string result = string.Empty;

            switch (format)
            {
                case KnownFileFormat.Xml:
                    MemoryStream stream = null;

                    try
                    {
                        stream = new MemoryStream();

                        var serializer = new XmlSerializer(typeof(T));
                        using (var writer = new StreamWriter(stream))
                        using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = options.Indent }))
                        {
                            serializer.Serialize(xmlWriter, this);

                            stream.Seek(0, SeekOrigin.Begin);
                            using (var streamReader = new StreamReader(stream))
                            {
                                stream = null;
                                value = streamReader.ReadToEnd();
                            }
                        }

                        result = value;
                    }
                    finally
                    {
                        stream?.Dispose();
                    }
                    break;

                case KnownFileFormat.Json:
                    value =
                        JsonConvert.SerializeObject(
                            this,
                            options.Indent
                                ? Newtonsoft.Json.Formatting.Indented
                                : Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.All, 
                                DefaultValueHandling = DefaultValueHandling.Ignore
                            });

                    result = value;
                    break;
            }

            return result;
        }
        #endregion

        #endregion

        #region public overrides methods

        #region [public] {override} (string) ToString(): Returns a string that represents the current object
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the current object.
        /// </returns>
        public override string ToString() => !IsDefault ? "Modified" : "Default";
        #endregion

        #endregion
    }
}
