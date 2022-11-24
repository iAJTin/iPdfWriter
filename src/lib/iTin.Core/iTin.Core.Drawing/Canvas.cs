
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

using iTin.Core.Drawing.ComponentModel;
using iTin.Core.Drawing.Helpers;
using iTin.Core.Helpers;

namespace iTin.Core.Drawing
{
    /// <summary>
    /// Encapsulates a drawing surface with orientation support.
    /// </summary>
    public class Canvas : IDisposable
    {
        #region private readonly members

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Rectangle _rect;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int _translateX;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int _translateY;
              
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly float _rotation;

        #endregion

        #region private members

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _isDisposed;

        #endregion

        #region constructor/s

        /// <summary>
        /// Initialize a new instance of the <see cref="Canvas"/> class.
        /// </summary>
        /// <param name="graphics">Surface <see cref="T:System.Drawing.Graphics"/> on which to draw.</param>
        public Canvas(Graphics graphics) : this(graphics, Orientation.Top)
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Canvas"/> class by setting the orientation.
        /// </summary>
        /// <param name="graphics">Surface <see cref="T:System.Drawing.Graphics"/> on which to draw.</param>
        /// <param name="orientation">One of the values in the enumeration <see cref="ComponentModel.Orientation"/> that represents the orientation.</param>
        public Canvas(Graphics graphics, Orientation orientation) : this(graphics, SentinelHelper.PassThroughNonNull(graphics).VisibleClipBounds, orientation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Canvas"/> class by setting the drawing region in the specified rectangle
        /// </summary>
        /// <param name="graphics">Surface <see cref="T:System.Drawing.Graphics"/> on which to draw.</param>
        /// <param name="rect">A <see cref="RectangleF"/> structure that represents the drawing region.</param>
        public Canvas(Graphics graphics, RectangleF rect) : this(graphics, Rectangle.Ceiling(rect), Orientation.Top)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Canvas"/> class by setting the orientation and drawing region in the specified rectangle.
        /// </summary>
        /// <param name="graphics">Surface <see cref="T:System.Drawing.Graphics"/> on which to draw.</param>
        /// <param name="rect">A <see cref="RectangleF"/> structure that represents the drawing region.</param>
        /// <param name="orientation">One of the values in the enumeration <see cref="ComponentModel.Orientation"/> that represents the orientation.</param>
        public Canvas(Graphics graphics, RectangleF rect, Orientation orientation) : this(graphics, Rectangle.Ceiling(rect), orientation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Canvas"/> class by setting the orientation and drawing region in the specified rectangle.
        /// </summary>
        /// <param name="graphics">Surface <see cref="T:System.Drawing.Graphics"/> on which to draw.</param>
        /// <param name="rect">A <see cref="RectangleF"/> structure that represents the drawing region.</param>
        /// <param name="orientation">One of the values in the enumeration <see cref="ComponentModel.Orientation"/> that represents the orientation.</param>
        private Canvas(Graphics graphics, Rectangle rect, Orientation orientation)
        {
            var safeGraphics = SentinelHelper.PassThroughNonNull(graphics);

            var isValidRect = rect.IsValid();
            if (!isValidRect)
            {
                return;
            }

            Orientation = orientation;

            _rect = rect;
            _rotation = 0f;
            _translateX = 0;
            _translateY = 0;

            Graphics = safeGraphics;
            switch (orientation)
            {
                case Orientation.Left:
                    _rect = new Rectangle(rect.X, rect.Y, rect.Height, rect.Width);

                    // Translate back from a quater left turn to the original place 
                    _translateX = _rect.X - _rect.Y - 1;
                    _translateY = _rect.X + _rect.Y + _rect.Width;
                    _rotation = 270;
                    break;

                case Orientation.Right:
                    // Invert the dimensions of the rectangle for drawing upwards
                    _rect = new Rectangle(rect.X, rect.Y, rect.Height, rect.Width);

                    // Translate back from a quater right turn to the original place 
                    _translateX = _rect.X + _rect.Y + _rect.Height + 1;
                    _translateY = -(_rect.X - _rect.Y);
                    _rotation = 90f;
                    break;

                case Orientation.Bottom:
                    // Translate to opposite side of origin, so the rotate can then bring it back to original position but mirror image
                    _translateX = _rect.X * 2 + _rect.Width;
                    _translateY = _rect.Y * 2 + _rect.Height;
                    _rotation = 180f;
                    break;
            }

            // Apply the transforms if we have any to apply
            if (_translateX != 0 || _translateY != 0)
            {
                safeGraphics.TranslateTransform(_translateX, _translateY);
            }

            if (_rotation != 0.0f)
            {
                safeGraphics.RotateTransform(_rotation);
            }
        }

        #endregion

        #region finalizer

        /// <summary>
        /// Finalizes an instance of the <see cref="Canvas"/> class. Clean only unmanaged resources.
        /// </summary>
        ~Canvas()
        {
            Dispose(false);
        }

        #endregion

        #region interfaces

        #region IDisposable

        #region public methods

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

        #region public properties

        /// <summary>
        /// Gets a reference to the drawing surface adapted to the indicated orientation.
        /// </summary>
        /// <value>
        /// Surface <see cref="T: System.Drawing.Graphics" /> adapted an orientation in which to draw.
        /// </value>
        public Graphics Graphics { get; }

        /// <summary>
        /// Gets a value that represents the orientation of this drawing surface.
        /// </summary>
        /// <value>
        /// One of the values in the enumeration <see cref = "T:iTin.Core.Drawing.Orientation" /> that represents the orientation.
        /// </value>
        public Orientation Orientation { get; }

        /// <summary>
        /// Gets a <see cref="T:System.Drawing.Rectangle"/> structure that represents the drawing region.
        /// </summary>
        /// <value>
        /// A <see cref="T:System.Drawing.Rectangle" /> structure that represents the drawing region.
        /// </value>
        public Rectangle Rectangle => _rect;

        #endregion

        #region public methods

        /// <summary>
        /// Draw the perimeter of a rectangle with the specified brush.
        /// </summary>
        /// <param name="brush">A <see cref="Brush"/> that represents the brush to be used.</param>
        public void DrawBorder(Brush brush) => 
            DrawBorder(brush, new Rectangle(_rect.Location, new Size(_rect.Width - 1, _rect.Bottom - 1)));

        /// <summary>
        /// Draw the perimeter of a rectangle with the specified brush.
        /// </summary>
        /// <param name="brush">A <see cref="Brush"/> reference that represents the brush to be used.</param>
        /// <param name="rect">A <see cref="RectangleF"/> structure that represents the rectangle in which to paint.</param>
        public void DrawBorder(Brush brush, RectangleF rect) =>
            DrawBorder(brush, rect, SmoothingModeEx.HighQuality);

        /// <summary>
        /// Draw the perimeter of a rectangle with the specified brush.
        /// </summary>
        /// <param name="brush">A <see cref="Brush"/> reference that represents the brush to be used.</param>
        /// <param name="rect">A <see cref="RectangleF"/> structure that represents the rectangle in which to paint.</param>
        /// <param name="quality">Representation quality to use in the Graphics object.</param>
        public void DrawBorder(Brush brush, RectangleF rect, SmoothingModeEx quality)
        {
            using (new Smoothing(Graphics, quality))
            using (var p = new Pen(brush))
            {
                Graphics.DrawRectangle(p, Rectangle.Ceiling(rect.DeflateInOne())); //);Rectangle.Ceiling(rect));
            }
        }

        /// <summary>
        /// Draw the perimeter of a rectangle using a solid color.
        /// </summary>
        /// <param name="color">A <see cref="Color"/> structure that represents the color of the rectangle.</param>
        public void DrawBorder(Color color) => 
            DrawBorder(color, new Rectangle(_rect.Location, new Size(_rect.Width - 1, _rect.Bottom - 1)));

        /// <summary>
        /// Draw the perimeter of a rectangle using a solid color.
        /// </summary>
        /// <param name="color">A <see cref="Color"/> structure that represents the color of the rectangle.</param>
        /// <param name="rect">A <see cref="RectangleF"/> structure that represents the rectangle in which to paint.</param>
        public void DrawBorder(Color color, RectangleF rect)
        {
            SolidBrush tempBrush = null;

            try
            {
                tempBrush = new SolidBrush(color);
                SolidBrush brush = (SolidBrush) tempBrush.Clone();
                DrawBorder(brush, rect);
            }
            finally
            {
                tempBrush?.Dispose();
            }
        }

        /// <summary>
        /// Draw the perimeter of the figure with the default color and quality.
        /// </summary>
        /// <param name="pen">A <see cref="Pen"/> reference that represents the pen to be used.</param>
        /// <param name="shape">A <see cref="GraphicsPath"/> reference that represents the shape of the destination in which to paint.</param>
        /// <remarks>
        /// The rendering quality used to draw is <see cref="SmoothingModeEx.HighQuality" />.
        /// </remarks>
        public void DrawBorder(Pen pen, GraphicsPath shape) => 
            DrawBorder(pen, shape, SmoothingModeEx.HighQuality);

        /// <summary>
        /// Draw the perimeter of the figure with the specified color and quality.
        /// </summary>
        /// <param name="pen">A <see cref="Pen"/> reference that represents the pen to be used.</param>
        /// <param name="shape">A <see cref="GraphicsPath"/> reference that represents the shape of the destination in which to paint.</param>
        /// <param name="quality">Representation quality to use in the Graphics object.</param>
        public void DrawBorder(Pen pen, GraphicsPath shape, SmoothingModeEx quality)
        {
            using (new Smoothing(Graphics, quality))
            {
                Graphics.DrawPath(pen, shape);
            }
        }

        /// <summary>
        /// Draw an image at the specified position.
        /// </summary>
        /// <param name="image">An <see cref="Image"/> reference that represents the image to be drawn.</param>
        /// <param name="alignment">One of the values of <see cref="ContentAlignment"/> that represents the type of alignment.</param>
        public void DrawImage(Image image, ContentAlignment alignment) => 
            DrawImage(image, _rect, alignment, string.Empty, EffectType.None);

        /// <summary>
        /// Draw an image at the specified position.
        /// </summary>
        /// <param name="image">An <see cref="Image"/> reference that represents the image to be drawn.</param>
        /// <param name="alignment">One of the values of <see cref="ContentAlignment"/> that represents the type of alignment.</param>
        /// <param name="effect">One of the values of <see cref="EffectType"/> that represents the type of effect to apply.</param>
        public void DrawImage(Image image, ContentAlignment alignment, EffectType effect) => 
            DrawImage(image, _rect, alignment, string.Empty, effect);

        /// <summary>
        /// Draw an image at the specified position.
        /// </summary>
        /// <param name="image">An <see cref="Image"/> reference that represents the image to be drawn.</param>
        /// <param name="alignment">One of the values of <see cref="ContentAlignment"/> that represents the type of alignment.</param>
        /// <param name="borderColor">Defines the color of the image border.</param>
        public void DrawImage(Image image, ContentAlignment alignment, string borderColor) => 
            DrawImage(image, _rect, alignment, borderColor, EffectType.None);

        /// <summary>
        /// Draw an image by applying an effect at the specified position.
        /// </summary>
        /// <param name="image">An <see cref="Image"/> reference that represents the image to be drawn.</param>
        /// <param name="alignment">One of the values of <see cref="ContentAlignment"/> that represents the type of alignment.</param>
        /// <param name="borderColor">Defines the color of the image border.</param>
        /// <param name="effect">One of the values of <see cref="EffectType"/> that represents the type of effect to apply.</param>
        public void DrawImage(Image image, ContentAlignment alignment, string borderColor, EffectType effect) => 
            DrawImage(image, _rect, alignment, borderColor, effect);

        /// <summary>
        /// Draw an image by applying an effect at the specified position.
        /// </summary>
        /// <param name="image">An <see cref="Image"/> reference that represents the image to be drawn.</param>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle in which to paint.</param>
        /// <param name="alignment">One of the values of <see cref="ContentAlignment"/> that represents the type of alignment.</param>
        /// <param name="borderColor">Defines the color of the image border.</param>
        /// <param name="effect">One of the values of <see cref="EffectType"/> that represents the type of effect to apply.</param>
        public void DrawImage(Image image, Rectangle rect, ContentAlignment alignment, string borderColor, EffectType effect)
        {
            using (Brush brush = image.ToBrush(rect, alignment.ToImageStyle(), effect, Orientation))
            {
                Fill(brush, rect);
            }

            if (string.IsNullOrEmpty(borderColor))
            {
                return;
            }

            var rectangles = AlignInside(rect, Size.Empty, ContentAlignment.TopLeft, image.Size, alignment, 0);
            var imageRectangle = rectangles[1];
            DrawBorder(ColorHelper.GetColorFromString(borderColor), imageRectangle);
        }

        /// <summary>
        /// Draw a text string with the brush, quality, format and font specified.
        /// </summary>
        /// <param name="text">A <see cref="String"/> that represents the text to be drawn.</param>
        /// <param name="brush">An object <see cref="Brush"/> that represents the definition of available brushes.</param>
        /// <param name="font">A <see cref="Font"/> where the text will be displayed.</param>
        /// <param name="format">An object <see cref="StringFormat"/> that represents the format of the text.</param>
        /// <param name="quality">One of the values in the enumeration <see cref="TextRenderingHint"/> indicating the rendering quality.</param>
        public void DrawString(string text, Brush brush, Font font, StringFormat format, TextRenderingHint quality) => 
            DrawString(text, _rect, brush, font, format, quality);

        /// <summary>
        /// Draw a text string with the brush, quality, format and font specified.
        /// </summary>
        /// <param name="text">A <see cref="String"/> that represents the text to be drawn.</param>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the rectangle in which to paint.</param>
        /// <param name="brush">An object <see cref="Brush"/> that represents the definition of available brushes.</param>
        /// <param name="font">A <see cref="Font"/> where the text will be displayed.</param>
        /// <param name="format">An object <see cref="StringFormat"/> that represents the format of the text.</param>
        /// <param name="quality">One of the values in the enumeration <see cref="TextRenderingHint"/> indicating the rendering quality.</param>
        public void DrawString(string text, Rectangle rect, Brush brush, Font font, StringFormat format, TextRenderingHint quality)
        {
            using (new TextRendering(Graphics, quality))
            {
                Rectangle textRectangle = RectangleToClient(text, font, format);
                Brush textRectangleBrush = BrushToClient(brush, textRectangle);
                Graphics.DrawString(text, font, textRectangleBrush, rect, format);
            }
        }

        /// <summary>
        /// Draw a text string with the brush, quality, format and font specified.
        /// </summary>
        /// <param name="text">A <see cref="String"/> that represents the text to be drawn.</param>
        /// <param name="brush">An object <see cref="Brush"/> that represents the definition of available brushes.</param>
        /// <param name="font">A <see cref="Font"/> where the text will be displayed.</param>
        /// <param name="point"><see cref="PointF"/> structure that specifies the upper left corner of the drawn text.</param>
        public void DrawString(string text, Brush brush, Font font, PointF point) => 
            DrawString(text, brush, font, point, TextRenderingHint.SystemDefault);

        /// <summary>
        /// Draw a text string with the brush, quality, format and font specified.
        /// </summary>
        /// <param name="text">A <see cref="String"/> that represents the text to be drawn.</param>
        /// <param name="brush">An object <see cref="Brush"/> that represents the definition of available brushes.</param>
        /// <param name="font">A <see cref="Font"/> where the text will be displayed.</param>
        /// <param name="point"><see cref="PointF"/> structure that specifies the upper left corner of the drawn text.</param>
        /// <param name="quality">One of the values in the enumeration <see cref="TextRenderingHint"/> indicating the rendering quality.</param>
        public void DrawString(string text, Brush brush, Font font, PointF point, TextRenderingHint quality) =>
            DrawString(text, brush, font, point, StringFormat.GenericDefault, quality);

        /// <summary>
        /// Draw a text string with the brush, quality, format and font specified.
        /// </summary>
        /// <param name="text">A <see cref="String"/> that represents the text to be drawn.</param>
        /// <param name="brush">An object <see cref="Brush"/> that represents the definition of available brushes.</param>
        /// <param name="font">A <see cref="Font"/> where the text will be displayed.</param>
        /// <param name="point"><see cref="PointF"/> structure that specifies the upper left corner of the drawn text.</param>
        /// <param name="format">An object <see cref="StringFormat"/> that represents the format of the text.</param>
        /// <param name="quality">One of the values in the enumeration <see cref="TextRenderingHint"/> indicating the rendering quality.</param>
        public void DrawString(string text, Brush brush, Font font, PointF point, StringFormat format, TextRenderingHint quality)
        {
            using (new TextRendering(Graphics, quality))
            {
                Graphics.DrawString(text, font, brush, point, format);
            }
        }

        /// <summary>
        /// Fill the inside of the specified rectangle.
        /// </summary>
        /// <param name="brush">A <see cref="Brush"/> reference that determines the characteristics of the padding.</param>
        public void Fill(Brush brush) => 
            Fill(brush, _rect);

        /// <summary>
        /// Fill the inside of a rectangle specified by a <see cref="RectangleF"/> structure.
        /// </summary>
        /// <param name="brush">A <see cref="Brush"/> reference that determines the characteristics of the padding.</param>
        /// <param name="rect">A <see cref="RectangleF"/> structure that represents the rectangle to be filled. </param>
        public void Fill(Brush brush, RectangleF rect) => 
            Fill(brush, rect, SmoothingModeEx.HighQuality);

        /// <summary>
        /// Fill the inside of a rectangle specified by a <see cref="RectangleF"/> structure.
        /// </summary>
        /// <param name="brush">A <see cref="Brush"/> reference that determines the characteristics of the padding.</param>
        /// <param name="rect">A <see cref="RectangleF"/> structure that represents the rectangle to be filled. </param>
        /// <param name="quality">Representation quality to use in the Graphics object.</param>
        public void Fill(Brush brush, RectangleF rect, SmoothingModeEx quality)
        {
            using (new Smoothing(Graphics, quality))
            {
                Graphics.FillRectangle(brush, rect);
            }
        }

        /// <summary>
        /// Fill the specified figure using the specified brush.
        /// </summary>
        /// <param name="brush">A <see cref="Brush"/> reference that determines the characteristics of the padding.</param>
        /// <param name="shape">A <see cref="GraphicsPath"/> reference that represents the shape of the destination in which to paint.</param>
        public void Fill(Brush brush, GraphicsPath shape) => 
            Fill(brush, shape, SmoothingModeEx.HighQuality);

        /// <summary>
        /// Fill the inside of a rectangle specified by a <see cref="RectangleF"/> structure.
        /// </summary>
        /// <param name="brush">A <see cref="Brush"/> reference that determines the characteristics of the padding.</param>
        /// <param name="shape">A <see cref="GraphicsPath"/> reference that represents the shape of the destination in which to paint.</param>
        /// <param name="quality">Representation quality to use in the Graphics object.</param>
        public void Fill(Brush brush, GraphicsPath shape, SmoothingModeEx quality)
        {
            using (new Smoothing(Graphics, quality))
            {
                Graphics.FillPath(brush, shape);
            }
        }

        /// <summary>
        /// Fill the inside of a rectangle specified by a <see cref="RectangleF"/> structure with a solid color.
        /// </summary>
        /// <param name="color">A <see cref="Color"/> structure that represents the fill color.</param>
        public void Fill(Color color) => 
            Fill(color, _rect);

        /// <summary>
        /// Fill the inside of a rectangle specified by a <see cref="RectangleF"/> structure with a solid color.
        /// </summary>
        /// <param name="color">A <see cref="Color"/> structure that represents the fill color.</param>
        /// <param name="rect">A <see cref="RectangleF"/> structure that represents the rectangle to be filled.</param>
        public void Fill(Color color, RectangleF rect)
        {
            SolidBrush tempBrush = null;

            try
            {
                tempBrush = new SolidBrush(color);
                SolidBrush brush = (SolidBrush)tempBrush.Clone();
                Fill(brush, rect);
            }
            finally
            {
                tempBrush?.Dispose();
            }
        }

        /// <summary>
        /// Fill in the specified figure using a solid color.
        /// </summary>
        /// <param name="color">A <see cref="Color"/> structure that represents the fill color.</param>
        /// <param name="shape">A <see cref="GraphicsPath"/> reference that represents the shape of the destination in which to paint.</param>
        public void Fill(Color color, GraphicsPath shape)
        {
            SolidBrush tempBrush = null;

            try
            {
                tempBrush = new SolidBrush(color);
                SolidBrush brush = (SolidBrush)tempBrush.Clone();
                Fill(brush, shape);
            }
            finally
            {
                tempBrush?.Dispose();
            }
        }

        /// <summary>
        /// Calculate the size and location of the text, at the customer's coordinates.
        /// </summary>
        /// <param name="text">A <see cref="String"/> that represents the text to draw.</param>
        /// <param name="font">Font of the text that shows the element.</param>
        /// <param name="format">A <see cref="StringFormat"/> que representa el formato del texto a dibujar.</param>
        /// <returns>
        /// Size and location of the rectangle of the text with respect to the client rectangle.
        /// </returns>
        /// <exception cref="ArgumentNullException">The format value is <b>null</b>.</exception>
        public Rectangle RectangleToClient(string text, Font font, StringFormat format)
        {
            Font safeFont = SentinelHelper.PassThroughNonNull(font);
            StringFormat safeFormat = SentinelHelper.PassThroughNonNull(format);

            if (string.IsNullOrEmpty(text))
            {
                return Rectangle.Empty;
            }

            Size prefixSize = Graphics.MeasureString("&", safeFont).ToSize();
            Size textSize = Graphics.MeasureString(text, safeFont).ToSize();

            textSize.Width += prefixSize.Width >> 2;
            if (safeFormat.HotkeyPrefix != HotkeyPrefix.None)
            {
                textSize.Width -= prefixSize.Width;
            }

            Rectangle textRectangle = new Rectangle(_rect.Location, textSize);
            switch (safeFormat.LineAlignment)
            {
                case StringAlignment.Near:
                    switch (safeFormat.Alignment)
                    {
                        case StringAlignment.Center:
                            textRectangle.Offset((_rect.Width - textSize.Width) >> 1, 0);
                            break;

                        case StringAlignment.Far:
                            textRectangle.Offset(_rect.Right - textSize.Width, 0);
                            break;
                    }
                    break;

                case StringAlignment.Center:
                    switch (safeFormat.Alignment)
                    {
                        case StringAlignment.Near:
                            textRectangle.Offset(0, (_rect.Height - textSize.Height) >> 1);
                            break;

                        case StringAlignment.Center:
                            textRectangle.Offset((_rect.Width - textSize.Width) >> 1, (_rect.Height - textSize.Height) >> 1);
                            break;

                        case StringAlignment.Far:
                            textRectangle.Offset(_rect.Right - textSize.Width, (_rect.Height - textSize.Height) >> 1);
                            break;
                    }
                    break;

                case StringAlignment.Far:
                    switch (safeFormat.Alignment)
                    {
                        case StringAlignment.Near:
                            textRectangle.Offset(0, _rect.Height - textSize.Height);
                            break;

                        case StringAlignment.Center:
                            textRectangle.Offset((_rect.Width - textSize.Width) >> 1, _rect.Height - textSize.Height);
                            break;

                        case StringAlignment.Far:
                            textRectangle.Offset(_rect.Right - textSize.Width, _rect.Height - textSize.Height);
                            break;
                    }
                    break;
            }

            return textRectangle;
        }
    
        /// <summary>
        /// Calculate the size and location of the text, at the customer's coordinates.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="text">The text.</param>
        /// <param name="textAlignment">The text alignment.</param>
        /// <param name="image">The image.</param>
        /// <param name="imageAlignment">The image alignment.</param>
        /// <param name="gap">The gap.</param>
        /// <returns>
        /// Size and location of the text rectangle with respect to the client rectangle.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// El valor de format es <strong>null</strong>.
        /// </exception>
        public static Rectangle[] AlignInside(Rectangle rect, Size text, ContentAlignment textAlignment, Size image, ContentAlignment imageAlignment, int gap)
        {
            Rectangle auxRect;
            Rectangle textClip = rect;
            Rectangle imageClip = rect;

            switch (imageAlignment)
            {
                #region Alignment: TopLeft
                case ContentAlignment.TopLeft:
                    switch (textAlignment)
                    {
                        #region Text: TopLeft
                        case ContentAlignment.TopLeft:
                            auxRect = new Rectangle(image.Width, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.TopLeft, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.TopLeft, rect);
                            break;
                        #endregion

                        #region Text: TopCenter
                        case ContentAlignment.TopCenter:
                            auxRect = new Rectangle(image.Width, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.TopCenter, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.TopLeft, rect);
                            break;
                        #endregion

                        #region Text: TopRight
                        case ContentAlignment.TopRight:
                            imageClip = image.AlignInside(ContentAlignment.TopLeft, rect);
                            auxRect = new Rectangle(image.Width, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.TopRight, auxRect);
                            break;
                        #endregion

                        #region Text: MiddleLeft
                        case ContentAlignment.MiddleLeft:
                            auxRect = new Rectangle(0, image.Height, rect.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleLeft, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.TopLeft, rect);
                            break;
                        #endregion

                        #region Text: MiddleCenter
                        case ContentAlignment.MiddleCenter:
                            auxRect = new Rectangle(image.Width, image.Height, rect.Width - image.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleCenter, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.TopLeft, rect);
                            break;
                        #endregion

                        #region Text: MiddleRight
                        case ContentAlignment.MiddleRight:
                            auxRect = new Rectangle(image.Width, image.Height, rect.Width - image.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.TopLeft, rect);
                            break;
                        #endregion

                        #region Text: BottomLeft
                        case ContentAlignment.BottomLeft:
                            auxRect = new Rectangle(0, image.Height, rect.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.BottomLeft, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.TopLeft, rect);
                            break;
                        #endregion

                        #region Text: BottomCenter
                        case ContentAlignment.BottomCenter:
                            auxRect = new Rectangle(image.Width, image.Height, rect.Width - image.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.BottomCenter, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.TopLeft, rect);
                            break;
                        #endregion

                        #region Text: BottomRight
                        case ContentAlignment.BottomRight:
                            auxRect = new Rectangle(image.Width, image.Height, rect.Width - image.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.BottomRight, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.TopLeft, rect);
                            break;
                        #endregion
                    }
                    break;
                #endregion

                #region Alignment: TopCenter
                case ContentAlignment.TopCenter:
                    switch (textAlignment)
                    {
                        #region Text: TopLeft
                        case ContentAlignment.TopLeft:
                            textClip = text.AlignInside(ContentAlignment.TopLeft, rect);
                            auxRect = new Rectangle(text.Width, 0, rect.Width - text.Width, rect.Height);
                            imageClip = image.AlignInside(ContentAlignment.TopCenter, auxRect);
                            break;
                        #endregion

                        #region Text: TopCenter
                        case ContentAlignment.TopCenter:
                            auxRect = new Rectangle(0, 0, text.Width + image.Width, rect.Height).AlignInside(ContentAlignment.TopCenter, rect);
                            textClip = text.AlignInside(ContentAlignment.TopRight, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.TopLeft, auxRect);
                            break;
                        #endregion

                        #region Text: TopRight
                        case ContentAlignment.TopRight:
                            textClip = text.AlignInside(ContentAlignment.TopRight, rect);
                            auxRect = new Rectangle(0, 0, rect.Width - text.Width, rect.Height);
                            imageClip = image.AlignInside(ContentAlignment.TopCenter, auxRect);
                            break;
                        #endregion

                        #region Text: MiddleLeft
                        case ContentAlignment.MiddleLeft:
                            auxRect = new Rectangle(0, image.Height, rect.Width - image.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleLeft, auxRect);
                            auxRect = new Rectangle(text.Width, 0, rect.Width - text.Width, rect.Height);
                            imageClip = image.AlignInside(ContentAlignment.TopCenter, auxRect);
                            break;
                        #endregion

                        #region Text: MiddleCenter
                        case ContentAlignment.MiddleCenter:
                            auxRect = new Rectangle(0, image.Height, rect.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleCenter, auxRect);
                            auxRect = new Rectangle(0, 0, rect.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.TopCenter, auxRect);
                            break;
                        #endregion

                        #region Text: MiddleRight
                        case ContentAlignment.MiddleRight:
                            auxRect = new Rectangle(0, image.Height, rect.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            auxRect = new Rectangle(0, 0, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.TopCenter, auxRect);
                            break;
                        #endregion

                        #region Text: BottomLeft
                        case ContentAlignment.BottomLeft:
                            auxRect = new Rectangle(0, image.Height, rect.Width - image.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.BottomLeft, auxRect);
                            auxRect = new Rectangle(text.Width, 0, rect.Width - text.Width, rect.Height);
                            imageClip = image.AlignInside(ContentAlignment.TopCenter, auxRect);
                            break;
                        #endregion

                        #region Text: BottomCenter
                        case ContentAlignment.BottomCenter:
                            auxRect = new Rectangle(0, image.Height, rect.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.BottomCenter, auxRect);
                            auxRect = new Rectangle(0, 0, rect.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.TopCenter, auxRect);
                            break;
                        #endregion

                        #region Text: BottomRight
                        case ContentAlignment.BottomRight:
                            auxRect = new Rectangle(0, image.Height, rect.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.BottomRight, auxRect);
                            auxRect = new Rectangle(0, 0, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.TopCenter, auxRect);
                            break;
                        #endregion
                    }
                    break;
                #endregion

                #region Alignment: TopRight
                case ContentAlignment.TopRight:
                    switch (textAlignment)
                    {
                        #region Text: TopLeft
                        case ContentAlignment.TopLeft:
                            textClip = text.AlignInside(ContentAlignment.TopLeft, rect);
                            imageClip = image.AlignInside(ContentAlignment.TopRight, rect);
                            break;
                        #endregion

                        #region Text: TopLeft
                        case ContentAlignment.TopCenter:
                            auxRect = new Rectangle(0, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.TopCenter, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.TopRight, rect);
                            break;
                        #endregion

                        #region Text: TopRight
                        case ContentAlignment.TopRight:
                            auxRect = new Rectangle(0, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.TopRight, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.TopRight, rect);
                            break;
                        #endregion

                        #region Text: MiddleLeft
                        case ContentAlignment.MiddleLeft:
                            auxRect = new Rectangle(0, image.Height, rect.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleLeft, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.TopRight, rect);
                            break;
                        #endregion

                        #region Text: MiddleCenter
                        case ContentAlignment.MiddleCenter:
                            auxRect = new Rectangle(0, image.Height, rect.Width - image.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleCenter, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.TopRight, rect);
                            break;
                        #endregion

                        #region Text: MiddleRight
                        case ContentAlignment.MiddleRight:
                            auxRect = new Rectangle(0, image.Height, rect.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.TopRight, rect);
                            break;
                        #endregion

                        #region Text: BottomLeft
                        case ContentAlignment.BottomLeft:
                            auxRect = new Rectangle(0, image.Height, rect.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.BottomLeft, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.TopRight, rect);
                            break;
                        #endregion

                        #region Text: BottomCenter
                        case ContentAlignment.BottomCenter:
                            auxRect = new Rectangle(0, image.Height, rect.Width - image.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.BottomCenter, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.TopRight, rect);
                            break;
                        #endregion

                        #region Text: BottomRight
                        case ContentAlignment.BottomRight:
                            auxRect = new Rectangle(0, image.Height, rect.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.BottomRight, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.TopRight, rect);
                            break;
                        #endregion
                    }
                    break;
                #endregion

                #region Alignment: MiddleLeft
                case ContentAlignment.MiddleLeft:
                    switch (textAlignment)
                    {
                        #region Text: TopLeft
                        case ContentAlignment.TopLeft:
                            textClip = text.AlignInside(ContentAlignment.TopLeft, rect);
                            auxRect = new Rectangle(0, text.Height, rect.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleLeft, auxRect);
                            break;
                        #endregion

                        #region Text: TopCenter
                        case ContentAlignment.TopCenter:
                            auxRect = new Rectangle(image.Width, 0, rect.Width - image.Width, text.Height);
                            textClip = text.AlignInside(ContentAlignment.TopCenter, auxRect);
                            auxRect = new Rectangle(0, text.Height, rect.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleLeft, auxRect);
                            break;
                        #endregion

                        #region Text: TopRight
                        case ContentAlignment.TopRight:
                            auxRect = new Rectangle(image.Width, 0, rect.Width - image.Width, text.Height);
                            textClip = text.AlignInside(ContentAlignment.TopRight, auxRect);
                            auxRect = new Rectangle(0, text.Height, rect.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleLeft, auxRect);
                            break;
                        #endregion

                        #region Text: MiddleLeft
                        case ContentAlignment.MiddleLeft:
                            auxRect = new Rectangle(image.Width, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleLeft, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.MiddleLeft, rect);
                            break;
                        #endregion

                        #region Text: MiddleCenter
                        case ContentAlignment.MiddleCenter:
                            auxRect = new Rectangle(image.Width, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleCenter, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.MiddleLeft, rect);
                            break;
                        #endregion

                        #region Text: MiddleRight
                        case ContentAlignment.MiddleRight:
                            auxRect = new Rectangle(image.Width, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.MiddleLeft, rect);
                            break;
                        #endregion

                        #region Text: BottomLeft
                        case ContentAlignment.BottomLeft:
                            textClip = text.AlignInside(ContentAlignment.BottomLeft, rect);
                            auxRect = new Rectangle(0, 0, rect.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleLeft, auxRect);
                            break;
                        #endregion

                        #region Text: BottomCenter
                        case ContentAlignment.BottomCenter:
                            auxRect = new Rectangle(image.Width, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.BottomCenter, auxRect);
                            auxRect = new Rectangle(0, 0, rect.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleLeft, auxRect);
                            break;
                        #endregion

                        #region Text: BottomRight
                        case ContentAlignment.BottomRight:
                            textClip = text.AlignInside(ContentAlignment.BottomRight, rect);
                            auxRect = new Rectangle(0, 0, rect.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleLeft, auxRect);
                            break;
                        #endregion
                    }
                    break;
                #endregion

                #region Alignment: MiddleCenter
                case ContentAlignment.MiddleCenter:
                    switch (textAlignment)
                    {
                        #region Text: TopLeft
                        case ContentAlignment.TopLeft:
                            textClip = text.AlignInside(ContentAlignment.TopLeft, rect);
                            auxRect = new Rectangle(text.Width, text.Height, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleCenter, auxRect);
                            break;
                        #endregion

                        #region Text: TopCenter
                        case ContentAlignment.TopCenter:
                            textClip = text.AlignInside(ContentAlignment.TopCenter, rect);
                            auxRect = new Rectangle(0, text.Height, rect.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleCenter, auxRect);
                            break;
                        #endregion

                        #region Text: TopRight
                        case ContentAlignment.TopRight:
                            textClip = text.AlignInside(ContentAlignment.TopRight, rect);
                            auxRect = new Rectangle(0, text.Height, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleCenter, auxRect);
                            break;
                        #endregion

                        #region Text: MiddleLeft
                        case ContentAlignment.MiddleLeft:
                            textClip = text.AlignInside(ContentAlignment.MiddleLeft, rect);
                            auxRect = new Rectangle(text.Width, 0, rect.Width - text.Width, rect.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleCenter, auxRect);
                            break;
                        #endregion

                        #region Text: MiddleCenter
                        case ContentAlignment.MiddleCenter:
                            auxRect = new Rectangle(0, 0, image.Width + text.Width, rect.Height).AlignInside(ContentAlignment.MiddleCenter, rect);
                            imageClip = image.AlignInside(ContentAlignment.MiddleLeft, auxRect);
                            textClip = text.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            break;
                        #endregion

                        #region Text: MiddleRight
                        case ContentAlignment.MiddleRight:
                            textClip = text.AlignInside(ContentAlignment.MiddleRight, rect);
                            auxRect = new Rectangle(0, 0, rect.Width - text.Width, rect.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleCenter, auxRect);
                            break;
                        #endregion

                        #region Text: BottomLeft
                        case ContentAlignment.BottomLeft:
                            textClip = text.AlignInside(ContentAlignment.BottomLeft, rect);
                            auxRect = new Rectangle(text.Width, 0, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleCenter, auxRect);
                            break;
                        #endregion

                        #region Text: BottomCenter
                        case ContentAlignment.BottomCenter:
                            textClip = text.AlignInside(ContentAlignment.BottomCenter, rect);
                            auxRect = new Rectangle(0, 0, rect.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleCenter, auxRect);
                            break;
                        #endregion

                        #region Text: BottomRight
                        case ContentAlignment.BottomRight:
                            textClip = text.AlignInside(ContentAlignment.BottomRight, rect);
                            auxRect = new Rectangle(0, 0, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleCenter, auxRect);
                            break;
                        #endregion
                    }
                    break;
                #endregion

                #region Alignment: MiddleRight
                case ContentAlignment.MiddleRight:
                    switch (textAlignment)
                    {
                        #region Text: TopLeft
                        case ContentAlignment.TopLeft:
                            textClip = text.AlignInside(ContentAlignment.TopLeft, rect);
                            auxRect = new Rectangle(text.Width, text.Height, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            break;
                        #endregion

                        #region Text: TopCenter
                        case ContentAlignment.TopCenter:
                            auxRect = new Rectangle(0, 0, rect.Width - image.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.TopCenter, auxRect);
                            auxRect = new Rectangle(0, text.Height, rect.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            break;
                        #endregion

                        #region Text: TopRight
                        case ContentAlignment.TopRight:
                            textClip = text.AlignInside(ContentAlignment.TopRight, rect);
                            auxRect = new Rectangle(0, text.Height, rect.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            break;
                        #endregion

                        #region Text: MiddleLeft
                        case ContentAlignment.MiddleLeft:
                            textClip = text.AlignInside(ContentAlignment.MiddleLeft, rect);
                            auxRect = new Rectangle(text.Width, 0, rect.Width - text.Width, rect.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            break;
                        #endregion

                        #region Text: MiddleCenter
                        case ContentAlignment.MiddleCenter:
                            auxRect = new Rectangle(0, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleCenter, auxRect);
                            auxRect = new Rectangle(text.Width, 0, rect.Width - text.Width, rect.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            break;
                        #endregion

                        #region Text: MiddleRight
                        case ContentAlignment.MiddleRight:
                            auxRect = new Rectangle(0, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            auxRect = new Rectangle(text.Width, 0, rect.Width - text.Width, rect.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            break;
                        #endregion

                        #region Text: BottomLeft
                        case ContentAlignment.BottomLeft:
                            textClip = text.AlignInside(ContentAlignment.BottomLeft, rect);
                            auxRect = new Rectangle(text.Width, 0, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            break;
                        #endregion

                        #region Text: BottomCenter
                        case ContentAlignment.BottomCenter:
                            auxRect = new Rectangle(0, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.BottomCenter, auxRect);
                            auxRect = new Rectangle(text.Width, 0, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            break;
                        #endregion

                        #region Text: BottomRight
                        case ContentAlignment.BottomRight:
                            textClip = text.AlignInside(ContentAlignment.BottomRight, rect);
                            auxRect = new Rectangle(text.Width, 0, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            break;
                        #endregion
                    }
                    break;
                #endregion

                #region Alignment: BottomLeft
                case ContentAlignment.BottomLeft:
                    switch (textAlignment)
                    {
                        #region Text: TopLeft
                        case ContentAlignment.TopLeft:
                            textClip = text.AlignInside(ContentAlignment.TopLeft, rect);
                            imageClip = image.AlignInside(ContentAlignment.BottomLeft, rect);
                            break;
                        #endregion

                        #region Text: TopCenter
                        case ContentAlignment.TopCenter:
                            auxRect = new Rectangle(image.Width, 0, rect.Width - image.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.TopCenter, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.BottomLeft, rect);
                            break;
                        #endregion

                        #region Text: TopRight
                        case ContentAlignment.TopRight:
                            auxRect = new Rectangle(image.Width, 0, rect.Width - image.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.TopRight, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.BottomLeft, rect);
                            break;
                        #endregion

                        #region Text: MiddleLeft
                        case ContentAlignment.MiddleLeft:
                            auxRect = new Rectangle(0, 0, rect.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleLeft, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.BottomLeft, rect);
                            break;
                        #endregion

                        #region Text: MiddleCenter
                        case ContentAlignment.MiddleCenter:
                            auxRect = new Rectangle(image.Width, 0, rect.Width - image.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleCenter, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.BottomLeft, rect);
                            break;
                        #endregion

                        #region Text: MiddleRight
                        case ContentAlignment.MiddleRight:
                            auxRect = new Rectangle(image.Width, 0, rect.Width - image.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.BottomLeft, rect);
                            break;
                        #endregion

                        #region Text: BottomLeft
                        case ContentAlignment.BottomLeft:
                            auxRect = new Rectangle(image.Width, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.BottomLeft, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.BottomLeft, rect);
                            break;
                        #endregion

                        #region Text: BottomCenter
                        case ContentAlignment.BottomCenter:
                            auxRect = new Rectangle(image.Width, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.BottomCenter, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.BottomLeft, rect);
                            break;
                        #endregion

                        #region Text: BottomRight
                        case ContentAlignment.BottomRight:
                            auxRect = new Rectangle(image.Width, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.BottomRight, auxRect);
                            imageClip = image.AlignInside(ContentAlignment.BottomLeft, rect);
                            break;
                        #endregion
                    }
                    break;
                #endregion

                #region Alignment: BottomCenter
                case ContentAlignment.BottomCenter:
                    switch (textAlignment)
                    {
                        #region Text: TopLeft
                        case ContentAlignment.TopLeft:
                            textClip = text.AlignInside(ContentAlignment.TopLeft, rect);
                            auxRect = new Rectangle(text.Width, text.Height, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.BottomCenter, auxRect);
                            break;
                        #endregion

                        #region Text: TopCenter
                        case ContentAlignment.TopCenter:
                            textClip = text.AlignInside(ContentAlignment.TopCenter, rect);
                            imageClip = image.AlignInside(ContentAlignment.BottomCenter, rect);
                            break;
                        #endregion

                        #region Text: TopRight
                        case ContentAlignment.TopRight:
                            textClip = text.AlignInside(ContentAlignment.TopRight, rect);
                            auxRect = new Rectangle(0, text.Height, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.BottomCenter, auxRect);
                            break;
                        #endregion

                        #region Text: MiddleLeft
                        case ContentAlignment.MiddleLeft:
                            auxRect = new Rectangle(0, 0, rect.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleLeft, auxRect);
                            auxRect = new Rectangle(text.Width, 0, rect.Width - text.Width, rect.Height);
                            imageClip = image.AlignInside(ContentAlignment.BottomCenter, auxRect);
                            break;
                        #endregion

                        #region Text: MiddleCenter
                        case ContentAlignment.MiddleCenter:
                            auxRect = new Rectangle(0, 0, rect.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleCenter, auxRect);
                            auxRect = new Rectangle(0, text.Height, rect.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.BottomCenter, auxRect);
                            break;
                        #endregion

                        #region Text: MiddleRight
                        case ContentAlignment.MiddleRight:
                            auxRect = new Rectangle(0, 0, rect.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            auxRect = new Rectangle(0, text.Height, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.BottomCenter, auxRect);
                            break;
                        #endregion

                        #region Text: BottomLeft
                        case ContentAlignment.BottomLeft:
                            textClip = text.AlignInside(ContentAlignment.BottomLeft, rect);
                            auxRect = new Rectangle(text.Width, 0, rect.Width - text.Width, rect.Height);
                            imageClip = image.AlignInside(ContentAlignment.BottomCenter, auxRect);
                            break;
                        #endregion

                        #region Text: BottomCenter
                        case ContentAlignment.BottomCenter:
                            auxRect = new Rectangle(0, 0, image.Width + text.Width, rect.Height).AlignInside(ContentAlignment.BottomCenter, rect);
                            imageClip = image.AlignInside(ContentAlignment.BottomLeft, auxRect);
                            textClip = text.AlignInside(ContentAlignment.BottomRight, auxRect);
                            break;
                        #endregion

                        #region Text: BottomRight
                        case ContentAlignment.BottomRight:
                            textClip = text.AlignInside(ContentAlignment.BottomRight, rect);
                            auxRect = new Rectangle(0, 0, rect.Width - text.Width, rect.Height);
                            imageClip = image.AlignInside(ContentAlignment.BottomCenter, auxRect);
                            break;
                        #endregion
                    }
                    break;
                #endregion

                #region Alignment: BottomRight
                case ContentAlignment.BottomRight:
                    switch (textAlignment)
                    {
                        #region Text: TopLeft
                        case ContentAlignment.TopLeft:
                            textClip = text.AlignInside(ContentAlignment.TopLeft, rect);
                            auxRect = new Rectangle(text.Width, text.Height, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.BottomRight, auxRect);
                            break;
                        #endregion

                        #region Text: TopCenter
                        case ContentAlignment.TopCenter:
                            auxRect = new Rectangle(0, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.TopCenter, auxRect);
                            auxRect = new Rectangle(text.Width, text.Height, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.BottomRight, auxRect);
                            break;
                        #endregion

                        #region Text: TopRight
                        case ContentAlignment.TopRight:
                            auxRect = new Rectangle(0, 0, rect.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.TopRight, auxRect);
                            auxRect = new Rectangle(text.Width, text.Height, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.BottomRight, auxRect);
                            break;
                        #endregion

                        #region Text: MiddleLeft
                        case ContentAlignment.MiddleLeft:
                            auxRect = new Rectangle(0, 0, rect.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleLeft, auxRect);
                            auxRect = new Rectangle(text.Width, text.Height, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.BottomRight, auxRect);
                            break;
                        #endregion

                        #region Text: MiddleCenter
                        case ContentAlignment.MiddleCenter:
                            auxRect = new Rectangle(0, 0, rect.Width - image.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleCenter, auxRect);
                            auxRect = new Rectangle(text.Width, text.Height, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.BottomRight, auxRect);
                            break;
                        #endregion

                        #region Text: MiddleRight
                        case ContentAlignment.MiddleRight:
                            auxRect = new Rectangle(0, 0, rect.Width, rect.Height - image.Height);
                            textClip = text.AlignInside(ContentAlignment.MiddleRight, auxRect);
                            auxRect = new Rectangle(text.Width, text.Height, rect.Width - text.Width, rect.Height - text.Height);
                            imageClip = image.AlignInside(ContentAlignment.BottomRight, auxRect);
                            break;
                        #endregion

                        #region Text: BottomLeft
                        case ContentAlignment.BottomLeft:
                            textClip = text.AlignInside(ContentAlignment.BottomLeft, rect);
                            auxRect = new Rectangle(text.Width, 0, rect.Width - text.Width, rect.Height);
                            imageClip = image.AlignInside(ContentAlignment.BottomRight, auxRect);
                            break;
                        #endregion

                        #region Text: BottomCenter
                        case ContentAlignment.BottomCenter:
                            auxRect = new Rectangle(0, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.BottomCenter, auxRect);
                            auxRect = new Rectangle(text.Width, 0, rect.Width - text.Width, rect.Height);
                            imageClip = image.AlignInside(ContentAlignment.BottomRight, auxRect);
                            break;
                        #endregion

                        #region Text: BottomRight
                        case ContentAlignment.BottomRight:
                            auxRect = new Rectangle(0, 0, rect.Width - image.Width, rect.Height);
                            textClip = text.AlignInside(ContentAlignment.BottomRight, auxRect);
                            auxRect = new Rectangle(text.Width, 0, rect.Width - text.Width, rect.Height);
                            imageClip = image.AlignInside(ContentAlignment.BottomRight, auxRect);
                            break;
                        #endregion
                    }
                    break;
                #endregion
            }

            return new[] { textClip, imageClip };
        }

        #endregion

        #region protected virtual methods

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
                // Restaurar las transformadas aplicadas.
                if (_rotation != 0.0f)
                {
                    Graphics.RotateTransform(-_rotation);
                }

                if (_translateX != 0 || _translateY != 0)
                {
                    Graphics.TranslateTransform(-_translateX, -_translateY);
                }

                Graphics?.Dispose();

            }

            // free native resources.
            // Nothing to do

            _isDisposed = true;
        }

        #endregion

        #region private members

        private Brush BrushToClient(Brush brush, Rectangle rect)
        {
            var tempBrush = brush;

            var sx = (float)rect.Width / _rect.Width;
            var sy = (float)rect.Height / _rect.Height;

            switch (brush)
            {
                case SolidBrush sb:
                    tempBrush = sb;
                    break;

                case HatchBrush hb:
                    tempBrush = hb;
                    break;

                case TextureBrush tb:
                    tb.TranslateTransform(rect.X, rect.Y);
                    tempBrush = tb;
                    break;

                case LinearGradientBrush lgb:
                    lgb.ScaleTransform(sx, sy);
                    lgb.TranslateTransform(rect.X, rect.Y);
                    tempBrush = lgb;
                    break;

                default:
                    if (brush is PathGradientBrush pgb)
                    {
                        pgb.ScaleTransform(sx, sy);
                        pgb.TranslateTransform(rect.X, rect.Y);
                        tempBrush = pgb;
                    }
                    break;
            }

            return tempBrush;
        }

        #endregion
    }
}
