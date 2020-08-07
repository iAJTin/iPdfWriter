
namespace iTin.Core.Models
{
    using System.Text;

    using iTin.Core.ComponentModel;

    /// <summary>
    /// Defines file save options. Allows defining if the directory is created automatically if it does not exist, output document without indentation. 
    /// By defaults uses <b>UTF8</b> encoding.
    /// </summary>
    public class ModelSaveOptions
    {
        #region constructor/s

        #region [public] ModelSaveOptions(): Initializes a new instance of the class
        /// <summary>
        /// Initializes a new instance of the <see cref="T:iTin.Core.Models.ModelSaveOptions" /> class.
        /// </summary>
        public ModelSaveOptions()
        {
            Indent = false;
            Encoding = Encoding.UTF8;
            CreateFolderIfNotExist = true;
        }
        #endregion

        #endregion

        #region public static readonly properties

        #region [public] {static} (ModelSaveOptions) Default: Gets a reference that contains default options for save, by default it creates the directory if it does not exist
        /// <summary>
        /// Gets a reference that contains default options for save, by default it creates the directory if it does not exist.
        /// </summary>
        /// <value>
        /// A <see cref="T:iTin.Core.Models.ModelSaveOptions"/> that contains default options for save.
        /// </value>
        public static ModelSaveOptions Default => new ModelSaveOptions();
        #endregion

        #endregion

        #region public properties

        #region [public] (bool) CreateFolderIfNotExist: Gets or sets a value that indicates whether the directory should be created if it does not exist
        /// <summary>
        /// Gets or sets a value that indicates whether the directory should be created if it does not exist.
        /// </summary>
        /// <value>
        /// <b>true</b> if directory should be created; otherwise <b>false</b>.
        /// </value>
        public bool CreateFolderIfNotExist { get; set; }
        #endregion

        #region [public] (Encoding) Encoding: Gets or sets a value that indicates whether the directory should be created if it does not exist
        /// <summary>
        /// Gets or sets a value that indicates whether the directory should be created if it does not exist.
        /// </summary>
        /// <value>
        /// <b>true</b> if directory should be created; otherwise <b>false</b>.
        /// </value>
        public Encoding Encoding { get; set; }
        #endregion

        #region [public] (bool) Indent: Gets or sets a value that indicates whether the directory should be created if it does not exist
        /// <summary>
        /// Gets or sets a value that indicates whether the directory should be created if it does not exist.
        /// </summary>
        /// <value>
        /// <b>true</b> if directory should be created; otherwise <b>false</b>.
        /// </value>
        public bool Indent { get; set; }
        #endregion

        #endregion

        #region public methods

        #region [public] (SaveOptions) ToSaveOptions(): Converts this options to new SaveOptions
        /// <summary>
        /// Convert this options to new <see cref="T:iTin.Core.ComponentModel.SaveOptions"/> instance.
        /// </summary>
        /// <value>
        /// A <see cref="T:iTin.Core.ComponentModel.SaveOptions"/> reference.
        /// </value>
        public SaveOptions ToSaveOptions() => new SaveOptions { CreateFolderIfNotExist = CreateFolderIfNotExist};
        #endregion

        #endregion
    }
}
