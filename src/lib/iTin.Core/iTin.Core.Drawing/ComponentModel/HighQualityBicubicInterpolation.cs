
namespace iTin.Core.Drawing.ComponentModel
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    using Core.Helpers;

    /// <summary>
    /// Set the interpolation mode to <see cref="InterpolationMode.HighQualityBicubic"/>.
    /// </summary>
    public class HighQualityBicubicInterpolation : IDisposable
    {
        #region private readonly members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Graphics _graphics;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly InterpolationMode _interpolationModePrev;
        #endregion

        #region private members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _isDisposed;
        #endregion

        #region constructor/s

        #region [public] HighQualityBicubicInterpolation(Graphics): Initialize a new instance of the class
        /// <summary>
        /// Initialize a new instance of the <see cref="HighQualityBicubicInterpolation"/> class by setting the interpolation mode to the value <see cref="InterpolationMode.HighQualityBicubic"/>.
        /// </summary>
        /// <param name="graphics">Objeto <see cref="Graphics"/> utilizado para dibujar.</param>
        public HighQualityBicubicInterpolation(Graphics graphics)
        {
            SentinelHelper.ArgumentNull(graphics, nameof(graphics));

            _graphics = graphics;
            _interpolationModePrev = _graphics.InterpolationMode;
            _graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        }
        #endregion

        #endregion

        #region finalizer

        #region [~] HighQualityBicubicInterpolation(): Finalizes an instance of this class
        /// <summary>
        /// Finalizes an instance of the <see cref="HighQualityBicubicInterpolation"/> class. Clean only unmanaged resources.
        /// </summary>
        ~HighQualityBicubicInterpolation()
        {
            Dispose(false);
        }
        #endregion

        #endregion

        #region interfaces

        #region IDisposable

        #region public methods

        #region [public] (void) Dispose(): Free managed resources
        /// <summary>
        /// Free managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #endregion

        #endregion

        #endregion

        #region protected virtual methods

        #region [protected] {virtual} (void) Dispose(bool): Cleans managed and unmanaged resources
        /// <summary>
        /// Cleans managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// If it is <b>true</b>, the method is invoked directly or indirectly from the user code.
        /// If it is <b>false</b>, the method is called the finalizer and only unmanaged resources are finalized.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            // free managed resources
            if (disposing)
            {
                // Restore the previous interpolation mode.
                _graphics.InterpolationMode = _interpolationModePrev;
            }

            // free native resources.
            // Nothing to do

            _isDisposed = true;
        }
        #endregion

        #endregion
    }
}
