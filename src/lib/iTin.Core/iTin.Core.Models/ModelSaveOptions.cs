
using System.Text;

using iTin.Core.ComponentModel;

namespace iTin.Core.Models
{
    /// <summary>
    /// Defines file save options. Allows defining if the directory is created automatically if it does not exist, output document without indentation. 
    /// By defaults uses <b>UTF8</b> encoding.
    /// </summary>
    public class ModelSaveOptions
    {
        #region constructor/s

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

        #region public static readonly properties

        /// <summary>
        /// Gets a reference that contains default options for save, by default it creates the directory if it does not exist, uses <see cref="T:System.Text.Encoding.UTF8"/> encoding and not applies indent.
        /// </summary>
        /// <value>
        /// A <see cref="T:iTin.Core.Models.ModelSaveOptions"/> that contains default options for save.
        /// </value>
        public static ModelSaveOptions Default => new();

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets a value that indicates whether the directory should be created if it does not exist.
        /// </summary>
        /// <value>
        /// <b>true</b> if directory should be created; otherwise <b>false</b>.
        /// </value>
        public bool CreateFolderIfNotExist { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the directory should be created if it does not exist.
        /// </summary>
        /// <value>
        /// <b>true</b> if directory should be created; otherwise <b>false</b>.
        /// </value>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the directory should be created if it does not exist.
        /// </summary>
        /// <value>
        /// <b>true</b> if directory should be created; otherwise <b>false</b>.
        /// </value>
        public bool Indent { get; set; }

        #endregion

        #region public methods

        /// <summary>
        /// Convert this options to new <see cref="T:iTin.Core.ComponentModel.SaveOptions"/> instance.
        /// </summary>
        /// <value>
        /// A <see cref="T:iTin.Core.ComponentModel.SaveOptions"/> reference.
        /// </value>
        public SaveOptions ToSaveOptions() => new SaveOptions
        {
            CreateFolderIfNotExist = CreateFolderIfNotExist
        };

        #endregion
    }
}
