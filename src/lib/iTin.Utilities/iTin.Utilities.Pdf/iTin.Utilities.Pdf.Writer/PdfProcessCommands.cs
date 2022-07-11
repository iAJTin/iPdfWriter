
namespace iTin.Utilities.Pdf.Writer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    using iTin.Core;
    using iTin.Core.ComponentModel;
    using iTin.Core.ComponentModel.Results;
    using iTin.Core.Helpers;
    using iTin.Core.IO;

    using ComponentModel;
    using ComponentModel.Result.Insert;
    using ComponentModel.Result.Output;
    using ComponentModel.Result.Replace;
    using ComponentModel.Result.Set;

    using Design.Text;

    using iTextSharp.text;

    using NativePdf = iTextSharp.text.pdf;
    using NativePdfParser = iTextSharp.text.pdf.parser;

    using NativeIO = System.IO;
    using iTinIO = iTin.Core.IO;

    public interface IProcessCommand
    {
        void Process();
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class PdfProcessCommands : IProcessCommand
    {
        //IReplace 

        public void Process()
        {

        }
    }
}
