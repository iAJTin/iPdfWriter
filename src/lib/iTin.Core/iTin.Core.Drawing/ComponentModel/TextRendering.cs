
namespace iTin.Core.Drawing.ComponentModel
{
    using System;
    using System.Drawing;
    using System.Drawing.Text;

    using Core.Helpers;

    /// <summary>
    /// Sets the quality of text rendering indicating one of the enumeration values <see cref="TextRenderingHint"/>.
    /// </summary>
    public class TextRendering : IDisposable
    {
        private readonly Graphics _graphics;
        private readonly TextRenderingHint _quality;

        /// <summary>
        /// Initialize a new instance of the class <see cref="TextRendering"/> establishing the smoothing quality of the lines.
        /// </summary>
        /// <param name="graphics">A <see cref="Graphics" /> reference used to draw.</param>
        /// <param name="quality">One of the values in the enumeration <see cref="TextRenderingHint" /> that represents the rendering quality of the text.</param>
        public TextRendering(Graphics graphics, TextRenderingHint quality)
        {
            SentinelHelper.ArgumentNull(graphics, nameof(graphics));

            _graphics = graphics;
            _quality = quality;
            _graphics.TextRenderingHint = quality;
        }

        /// <summary>
        /// Clean resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Cleans managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// If it is <b>true</b>, the method is invoked directly or indirectly from the user code.
        /// If it is <b>false</b>, the method is called the finalizer and only unmanaged resources are finalized.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // Delete managed resources
            if (disposing)
            {
            }

            // Restore the original rendering quality of the text
            _graphics.TextRenderingHint = _quality;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="TextRendering"/> class.
        /// </summary>
        ~TextRendering()
        {
            Dispose(false);
        }
    }
}
