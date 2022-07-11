
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

using iTin.Core.Helpers;

namespace iTin.Core.Drawing
{
    /// <summary>
    /// Define graphic cropping by using a figure, a rectangle or a region.
    /// </summary>
    public class Clipping : IDisposable
    {
        #region private readonly members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Region _newRegion;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Graphics _graphics;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Region _previousRegion;
        #endregion

        #region private members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _isDisposed;
        #endregion

        #region constructor

        #region [public] Clipping(Canvas, GraphicsPath): Initialize a new instance of the class by setting the figure to draw on
        /// <summary>
        /// Initialize a new instance of the <see cref="Clipping"/> class by setting the figure to draw on.
        /// </summary>
        /// <param name="canvas">Surface <see cref="Canvas"/> oriented where you are going to draw.</param>
        /// <param name="path">A <see cref="GraphicsPath"/> which represents the destination figure in which to paint.</param>
        public Clipping(Canvas canvas, GraphicsPath path) : this(canvas, path, false)
        {
        }
        #endregion

        #region [public] Clipping(Canvas, Rectangle): Initialize a new instance of the class by setting the clipping rectangle
        /// <summary>
        /// Initialize a new instance of the <see cref="Clipping"/> class by setting the clipping rectangle.
        /// </summary>
        /// <param name="canvas">Surface <see cref="Canvas"/> oriented where you are going to draw.</param>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle of clipping.</param>
        public Clipping(Canvas canvas, Rectangle rect) : this(canvas, rect, false)
        {
        }
        #endregion

        #region [public] Clipping(Canvas, Region): Initialize a new instance of the class by setting the clipping region
        /// <summary>
        /// Initialize a new instance of the <see cref="Clipping"/> class by setting the clipping region.
        /// </summary>
        /// <param name="canvas">Surface <see cref="Canvas"/> oriented where you are going to draw.</param>
        /// <param name="region">A <see cref="Region"/> which represents the region of clipping.</param>
        public Clipping(Canvas canvas, Region region) : this(canvas, region, false)
        {
        }
        #endregion

        #region [public] Clipping(Canvas, GraphicsPath, bool): Initializes a new instance of the class by setting the figure on which to draw and if excluded from clipping
        /// <summary>
        /// Initializes a new instance of the <see cref="Clipping"/> class by setting the figure on which to draw and if excluded from clipping.
        /// </summary>
        /// <param name="canvas">Surface <see cref="Canvas"/> oriented where you are going to draw.</param>
        /// <param name="path">A <see cref="GraphicsPath"/> which represents the destination figure in which to paint.</param>
        /// <param name="exclude"><b>true</b> if the figure is excluded in the clipping; otherwise <b>false</b>.</param>
        public Clipping(Canvas canvas, GraphicsPath path, bool exclude) : this(SentinelHelper.PassThroughNonNull(canvas).Graphics, path, exclude)
        {
        }
        #endregion

        #region [public] Clipping(Canvas, Rectangle, bool): Initialize a new instance of the class by setting the clipping rectangle and if it is excluded
        /// <summary>
        /// Initialize a new instance of the <see cref="Clipping"/> class by setting the clipping rectangle and if it is excluded.
        /// </summary>
        /// <param name="canvas">Surface <see cref="Canvas"/> oriented where you are going to draw.</param>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle of clipping.</param>
        /// <param name="exclude"><b>true</b> if the figure is excluded in the clipping; otherwise <b>false</b>.</param>
        public Clipping(Canvas canvas, Rectangle rect, bool exclude) : this(SentinelHelper.PassThroughNonNull(canvas).Graphics, rect, exclude)
        {
        }
        #endregion

        #region [public] Clipping(Canvas, Region, bool): Initializes a new instance of the class by setting the clipping region and if it is excluded from clipping
        /// <summary>
        /// Initializes a new instance of the <see cref="Clipping"/> class by setting the clipping region and if it is excluded from clipping.
        /// </summary>
        /// <param name="canvas">Surface <see cref="Canvas"/> oriented where you are going to draw.</param>
        /// <param name="region">A <see cref="Region"/> which represents the region of clipping.</param>
        /// <param name="exclude"><b>true</b> if the figure is excluded in the clipping; otherwise <b>false</b>.</param>
        public Clipping(Canvas canvas, Region region, bool exclude) : this(SentinelHelper.PassThroughNonNull(canvas).Graphics, region, exclude)
        {
        }
        #endregion

        #region [public] Clipping(Graphics, GraphicsPath): Inicializa una nueva instancia de la clase estableciendo la figura en la que dibujar
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Clipping"/> estableciendo la figura en la que dibujar.
        /// </summary>
        /// <param name="graphics">Surface <see cref="Graphics"/> on which to draw.</param>
        /// <param name="path">A <see cref="GraphicsPath"/> which represents the destination figure in which to paint.</param>
        public Clipping(Graphics graphics, GraphicsPath path) : this(graphics, path, false)
        {
        }
        #endregion

        #region [public] Clipping(Graphics, Rectangle): Initialize a new instance of the class by setting the cropping rectangle
        /// <summary>
        /// Initialize a new instance of the <see cref="Clipping"/> class by setting the cropping rectangle.
        /// </summary>
        /// <param name="graphics">Surface <see cref="Graphics"/> on which to draw.</param>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle of clipping.</param>
        public Clipping(Graphics graphics, Rectangle rect) : this(graphics, rect, false)
        {
        }
        #endregion

        #region [public] Clipping(Graphics, Region): Initialize a new instance of the class by setting the clipping region
        /// <summary>
        /// Initialize a new instance of the <see cref="Clipping"/> class by setting the clipping region.
        /// </summary>
        /// <param name="graphics">Surface <see cref="Graphics"/> on which to draw.</param>
        /// <param name="region">A <see cref="Region"/> which represents the region of clipping.</param>
        public Clipping(Graphics graphics, Region region) : this(graphics, region, false)
        {
        }
        #endregion

        #region [public] Clipping(Graphics, GraphicsPath, bool): Initializes a new instance of the class by setting the figure on which to draw and if excluded from clipping
        /// <summary>
        /// Initializes a new instance of the <see cref="Clipping"/> class by setting the figure on which to draw and if excluded from clipping.
        /// </summary>
        /// <param name="graphics">Surface <see cref="Graphics"/> on which to draw.</param>
        /// <param name="path">A <see cref="GraphicsPath"/> which represents the destination figure in which to paint.</param>
        /// <param name="exclude"><b>true</b> if the figure is excluded in the clipping; otherwise <b>false</b>.</param>
        public Clipping(Graphics graphics, GraphicsPath path, bool exclude)
        {
            Graphics safeGraphics = SentinelHelper.PassThroughNonNull(graphics);

            // Guardar la instancia actual.
            _graphics = safeGraphics;

            // Guardar la region de recorte actual.
            _previousRegion = _graphics.Clip;

            // Añadir figura a la región de recorte actual.
            _newRegion = _previousRegion.Clone();

            if (exclude)
            {
                _newRegion.Exclude(path);
            }
            else
            {
                _newRegion.Intersect(path);
            }

            _graphics.Clip = _newRegion;
        }
        #endregion

        #region [public] Clipping(Graphics, Rectangle, bool): Initialize a new instance of the class by setting the clipping rectangle and if it is excluded
        /// <summary>
        /// Initialize a new instance of the <see cref="Clipping"/> class by setting the clipping rectangle and if it is excluded.
        /// </summary>
        /// <param name="graphics">Surface <see cref="Graphics"/> on which to draw.</param>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle of clipping.</param>
        /// <param name="exclude"><b>true</b> if the figure is excluded in the clipping; otherwise <b>false</b>.</param>
        public Clipping(Graphics graphics, Rectangle rect, bool exclude)
        {
            Graphics safeGraphics = SentinelHelper.PassThroughNonNull(graphics);

            // Guardar la instancia actual.
            _graphics = safeGraphics;

            // Guardar la region de recorte actual.
            _previousRegion = _graphics.Clip;

            // Añadir rectángulo de recorte a la región de recorte actual.
            _newRegion = _previousRegion.Clone();

            if (exclude)
            {
                _newRegion.Exclude(rect);
            }
            else
            {
                _newRegion.Intersect(rect);
            }

            _graphics.Clip = _newRegion;
        }
        #endregion

        #region [public] Clipping(Graphics, Region, bool): Initialize a new instance of the class by setting the clipping region and if it is exluded
        /// <summary>
        /// Initialize a new instance of the <see cref="Clipping"/> class by setting the clipping region and if it is exluded.
        /// </summary>
        /// <param name="graphics">Surface <see cref="Graphics"/> on which to draw.</param>
        /// <param name="region">A <see cref="Region"/> which represents the region of clipping.</param>
        /// <param name="exclude"><b>true</b> if the figure is excluded in the clipping; otherwise <b>false</b>.</param>
        public Clipping(Graphics graphics, Region region, bool exclude)
        {
            Graphics safeGraphics = SentinelHelper.PassThroughNonNull(graphics);

            // Guardar la instancia actual.
            _graphics = safeGraphics;

            // Guardar la region de recorte actual.
            _previousRegion = _graphics.Clip;

            // Añadir región de recorte a la región de recorte actual.
            _newRegion = _previousRegion.Clone();

            if (exclude)
            {
                _newRegion.Exclude(region);
            }
            else
            {
                _newRegion.Intersect(region);
            }

            _graphics.Clip = _newRegion;
        }
        #endregion

        #endregion

        #region finalizer

        #region [~] Clipping(): Finalizer
        /// <summary>
        /// Finalizer
        /// </summary>
        ~Clipping()
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

        #region protecetd virtual methods

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
                // Restaurar region original.
                _graphics.Clip = _previousRegion;

                // Liberar recursos.
                _newRegion.Dispose();
                _graphics?.Dispose();
            }

            // free native resources.
            // Nothing to do

            _isDisposed = true;
        }
        #endregion

        #endregion
    }
}
