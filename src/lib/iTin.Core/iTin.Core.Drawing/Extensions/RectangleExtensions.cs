
using System.Collections.Generic;
using System.Drawing;

using iTin.Core.Drawing.ComponentModel;
using iTin.Core.Helpers;

namespace iTin.Core.Drawing
{
    /// <summary>
    /// Static class than contains extension methods for objects of type <see cref="Rectangle"/>.
    /// </summary> 
    public static class RectangleExtensions
    {
        #region [public] {static} (Rectangle) AlignInside(this Rectangle, ContentAlignment, Rectangle): Align the structure Rectangle inside the target rectangle
        /// <summary>
        /// Align the structure <see cref="Rectangle"/> inside the target rectangle.
        /// </summary>
        /// <param name="rect">Structure <see cref="Rectangle"/> to be aligned.</param>
        /// <param name="alignment">One of the values in the enumeration <see cref="ContentAlignment"/> that represents the type of alignment.</param>
        /// <param name="destRect">Structure <see cref="Rectangle"/> where it will be aligned.</param>
        /// <returns>
        /// Structure <see cref="Rectangle"/> that represents the destination rectangle that contains the aligned element.
        /// </returns>
        public static Rectangle AlignInside(this Rectangle rect, ContentAlignment alignment, Rectangle destRect)
        {
            bool isValidRect = rect.IsValid();
            if (!isValidRect)
            {
                return Rectangle.Empty;
            }

            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                    rect.Offset(0, 0);
                    break;

                case ContentAlignment.TopCenter:
                    rect.Offset((destRect.Width - rect.Width) >> 1, 0);
                    break;

                case ContentAlignment.TopRight:
                    rect.Offset(destRect.Width - rect.Width, 0);
                    break;

                case ContentAlignment.MiddleLeft:
                    rect.Offset(0, (destRect.Height - rect.Height) >> 1);
                    break;

                case ContentAlignment.MiddleCenter:
                    rect.Offset((destRect.Width - rect.Width) >> 1, (destRect.Height - rect.Height) >> 1);
                    break;

                case ContentAlignment.MiddleRight:
                    rect.Offset(destRect.Width - rect.Width, (destRect.Height - rect.Height) >> 1);
                    break;

                case ContentAlignment.BottomLeft:
                    rect.Offset(0, destRect.Height - rect.Height);
                    break;

                case ContentAlignment.BottomCenter:
                    rect.Offset((destRect.Width - rect.Width) >> 1, destRect.Height - rect.Height);
                    break;

                case ContentAlignment.BottomRight:
                    rect.Offset(destRect.Width - rect.Width, destRect.Height - rect.Height);
                    break;
            }

            return rect;
        }
        #endregion

        #region [public] {static} (Rectangle) AlignOutside(this Rectangle, OutsideAlignment, Rectangle): Align the Rectangle structure on the outside of the destination rectangle
        /// <summary>
        /// Align the <see cref="Rectangle"/> structure on the outside of the destination <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rect">Structure <see cref="Rectangle"/> to be aligned.</param>
        /// <param name="alignment">One of the values in the enumeration <see cref="OutsideAlignment"/> that represents the type of alignment.</param>
        /// <param name="destRect">Structure <see cref="Rectangle"/> where it will be aligned.</param>
        /// <returns>
        /// A <see cref="Rectangle"/> structure that represents the destination rectangle that contains the aligned element.
        /// </returns>
        public static Rectangle AlignOutside(this Rectangle rect, OutsideAlignment alignment, Rectangle destRect)
        {
            switch (alignment)
            {
                case OutsideAlignment.TopLeftCorner:
                    rect.Offset(destRect.X - rect.X - rect.Width, destRect.Y - rect.Y);
                    break;

                case OutsideAlignment.TopLeft:
                    rect.Offset(destRect.X - rect.X, destRect.Y - rect.Y - rect.Height);
                    break;

                case OutsideAlignment.TopCenter:
                    rect.Offset(destRect.X - rect.X + (destRect.Width - rect.Width) >> 1, destRect.Y - rect.Y - rect.Height);
                    break;

                case OutsideAlignment.TopRight:
                    rect.Offset(destRect.X - rect.X + destRect.Width - rect.Width, destRect.Y - rect.Y - rect.Height);
                    break;

                case OutsideAlignment.TopRightCorner:
                    rect.Offset(destRect.X - rect.X + destRect.Width, destRect.Y - rect.Y);
                    break;

                case OutsideAlignment.MiddleRight:
                    rect.Offset(destRect.X - rect.X + destRect.Width, destRect.Y - rect.Y + (destRect.Height - rect.Height) >> 1);
                    break;

                case OutsideAlignment.BottomRightCorner:
                    rect.Offset(destRect.X - rect.X + destRect.Width, destRect.Bottom - rect.Height);
                    break;

                case OutsideAlignment.BottomRight:
                    rect.Offset(destRect.X - rect.X + destRect.Width - rect.Width, destRect.Y + destRect.Height - rect.Y);
                    break;

                case OutsideAlignment.BottomCenter:
                    rect.Offset(destRect.X - rect.X + (destRect.Width - rect.Width) >> 1, destRect.Y + destRect.Height - rect.Y);
                    break;

                case OutsideAlignment.BottomLeft:
                    rect.Offset(destRect.X - rect.X, destRect.Y + destRect.Height - rect.Y);
                    break;

                case OutsideAlignment.BottomLeftCorner:
                    rect.Offset(destRect.X - rect.X - rect.Width, destRect.Bottom - rect.Height);
                    break;

                case OutsideAlignment.MiddleLeft:
                    rect.Offset(destRect.X - rect.X - rect.Width, destRect.Y - rect.Y + (destRect.Height - rect.Height) >> 1);
                    break;
            }

            return rect;
        }
        #endregion

        #region [public] {static} (bool) AreValid(this IEnumerable<Rectangle>): Check if the properties Width and Height of all elements have values greater than zero
        /// <summary>
        /// Check if the properties <see cref="Rectangle.Width" /> and <see cref="Rectangle.Height" /> of all elements have values greater than zero.
        /// </summary>
        /// <param name="rectangles">Lista de <see cref="Rectangle"/> a comprobar.</param>
        /// <returns>
        /// <b>true</b> if all <see cref="Rectangle.Width"/> and <see cref="Rectangle.Height"/> properties values are greater than zero; otherwise, <b>false</b>.
        /// </returns>
        public static bool AreValid(this IEnumerable<Rectangle> rectangles)
        {
            bool ok = true;

            foreach (var r in rectangles)
            {
                var isValid = r.IsValid();
                if (isValid)
                {
                    continue;
                }

                ok = false;
                break;
            }

            return ok;
        }
        #endregion

        #region [public] {static} (Rectangle) DeflateInOne(this Rectangle): Returns a new Rectangle structure decreased by one unit
        /// <summary>
        /// Returns a new <see cref="Rectangle"/> structure decreased by one unit.
        /// </summary>
        /// <param name="rect">Structure <see cref="Rectangle"/> to decrease.</param>
        /// <returns>
        /// A new <see cref="Rectangle"/> structure result of decreasing this structure by one unit.
        /// </returns>
        public static Rectangle DeflateInOne(this Rectangle rect)
        {
            rect.Inflate(-1, -1);
            return rect;
        }
        #endregion

        #region [public] {static} (void) DrawCenterLine(this Rectangle, Graphics, Color, DashStyleEx, int): Draw a centered line inside a rectangle with a specified color and style
        /// <summary>
        /// Draw a centered line inside a rectangle with a specified color and style.
        /// </summary>
        /// <param name="rect"><see cref="Rectangle"/> structure that represents the source rectangle.</param>
        /// <param name="graphics">A <see cref="Graphics"/> object used to draw.</param>
        /// <param name="color">Line color.</param>
        /// <param name="style">A value of the enumeration <see cref="DashStyleEx"/> that represents the style of the line</param>
        /// <param name="width">Thickness of the line</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="graphics"/> is <b>null</b>.</exception>
        public static void DrawCenterLine(this Rectangle rect, Graphics graphics, Color color, DashStyleEx style, int width)
        {
            SentinelHelper.ArgumentNull(graphics, nameof(graphics));

            bool isValid = rect.IsValid();
            if (!isValid)
            {
                return;
            }

            float y = rect.Y + rect.Height / 2;
            using (Pen p = new Pen(color, width))
            {
                p.DashStyle = style.ToDashStyle();
                graphics.DrawLine(p, rect.Left + 1, y, rect.Right - 1, y);
            }
        }
        #endregion

        #region [public] {static} (Rectangle) Flip(this Rectangle, FlipMode): Returns a new rectangle structure flipped in the specified direction
        /// <summary>
        /// Returns a new <see cref="Rectangle"/> structure flipped in the specified direction.
        /// </summary>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the source rectangle to be flipped.</param>
        /// <param name="flipMode">One of the values in the enumeration <see cref="FlipMode"/> that represents the type of flip.</param>
        /// <returns>
        /// A new <see cref="Rectangle"/> structure flipped in the specified direction.
        /// </returns>
        public static Rectangle Flip(this Rectangle rect, FlipMode flipMode)
        {
            switch (flipMode)
            {
                case FlipMode.Top:
                    rect.Offset(0, -rect.Height);
                    break;

                case FlipMode.Right:
                    rect.Offset(rect.Width, 0);
                    break;

                case FlipMode.Bottom:
                    rect.Offset(0, rect.Height);
                    break;

                case FlipMode.Left:
                    rect.Offset(-rect.Width, 0);
                    break;
            }

            return rect;
        }
        #endregion

        #region [public] {static} (Rectangle) InflateInOne(this Rectangle): Returns a new Rectangle structure increased by one unit
        /// <summary>
        /// Returns a new <see cref="Rectangle"/> structure increased by one unit.
        /// </summary>
        /// <param name="rect">A <see cref="Rectangle"/> structure to increase</param>
        /// <returns>
        /// A new <see cref="Rectangle"/> structure result of increasing this structure by one unit.
        /// </returns>
        public static Rectangle InflateInOne(this Rectangle rect)
        {
            rect.Inflate(1, 1);
            return rect;
        }
        #endregion

        #region [public] {static} (bool) IsValid(this Rectangle): Check if the Width and Height properties of this Rectangle structure have values greater than zero
        /// <summary>
        /// Check if the <see cref="Rectangle.Width"/> and <see cref="Rectangle.Height"/> properties of this <see cref="Rectangle"/> structure have values greater than zero.
        /// </summary>
        /// <param name="rect">Estructura <see cref="Rectangle"/> que representa el rectángulo origen.</param>
        /// <returns>
        /// <b>true</b> if <see cref="Rectangle.Width"/> and <see cref="Rectangle.Height"/> properties values are greater than zero; otherwise, <b>false</b>.
        /// </returns>
        public static bool IsValid(this Rectangle rect) => rect.Width > 0 && rect.Height > 0;
        #endregion

        #region [public] {static} (Rectangle) ModifyByLTRB(this Rectangle, int, int, int, int): Modify the specified rectangle with the indicated values
        /// <summary>
        /// Modify the specified rectangle with the indicated values.
        /// </summary>
        /// <param name="rect">Rectangle to modify</param>
        /// <param name="left">Left value</param>
        /// <param name="top">Top value</param>
        /// <param name="right">Right value</param>
        /// <param name="bottom">Bottom value</param>
        /// <returns>
        /// A <see cref="Rectangle"/> modified.
        /// </returns>
        public static Rectangle ModifyByLTRB(this Rectangle rect, int left, int top, int right, int bottom) => ModifyByLTRB(rect, left, top, right, bottom, Orientation.Top);
        #endregion

        #region [public] {static} (Rectangle) ModifyByLTRB(this Rectangle, int, int, int, int, Orientation): Modify the specified rectangle with the indicated values and its orientation
        /// <summary>
        /// Modify the specified rectangle with the indicated values and its orientation.
        /// </summary>
        /// <param name="rect">Rectangle to modify</param>
        /// <param name="left">Left value</param>
        /// <param name="top">Top value</param>
        /// <param name="right">Right value</param>
        /// <param name="bottom">Bottom value</param>
        /// <param name="orientation">Orientation value</param>
        /// <returns>
        /// A <see cref="Rectangle"/> modified.
        /// </returns>
        public static Rectangle ModifyByLTRB(this Rectangle rect, int left, int top, int right, int bottom, Orientation orientation)
        {
            Rectangle r = rect;

            switch (orientation)
            {
                case Orientation.Top:
                    r.X += left;
                    r.Width -= left + right;
                    r.Y += top;
                    r.Height -= top + bottom;
                    break;

                case Orientation.Right:
                    r.X += bottom;
                    r.Width -= top + bottom;
                    r.Y += left;
                    r.Height -= left + right;
                    break;

                case Orientation.Bottom:
                    r.X += left;
                    r.Width -= left + right;
                    r.Y += bottom;
                    r.Height -= top + bottom;
                    break;

                case Orientation.Left:
                    r.X += top;
                    r.Width -= top + bottom;
                    r.Y += right;
                    r.Height -= left + right;
                    break;
            }

            return r;
        }
        #endregion

        #region [public] {static} (RectangleF) MoveAndAlignWith(this Rectangle, Rectangle, ContentAlignment): Returns a rectangle structure aligned with the specified structure
        /// <summary>
        /// Returns a <see cref="T:System.Drawing.Rectangle"/> structure aligned with the specified structure.
        /// </summary>
        /// <param name="rect">A <see cref="Rectangle"/> structure to align.</param>
        /// <param name="reference">A <see cref="Rectangle"/> structure reference.</param>
        /// <param name="alignment">One of the values in the enumeration <see cref="ContentAlignment"/> that represents the type of alignment to apply.</param>
        /// <returns>
        /// A new <see cref="RectangleF"/> structure aligned with the reference rectangle.
        /// </returns>
        public static RectangleF MoveAndAlignWith(this Rectangle rect, Rectangle reference, ContentAlignment alignment)
        {
            RectangleF rf = rect;
            return rf.MoveAndAlignWith(reference, alignment);
        }
        #endregion

        #region [public] {static} (Rectangle) Rotate(this Rectangle, Orientation): Returns a new Rectangle structure rotated in the specified direction
        /// <summary>
        /// Returns a new <see cref="Rectangle"/> structure rotated in the specified direction.
        /// </summary>
        /// <param name="rect"><see cref="Rectangle"/> structure that represents the source rectangle.</param>
        /// <param name="orientation">One of the values in the enumeration <see cref="Orientation"/> that represents the type of orientation.</param>
        /// <returns>
        /// A new <see cref="Rectangle"/> structure rotated in the specified direction.
        /// </returns>
        public static Rectangle Rotate(this Rectangle rect, Orientation orientation)
        {
            Rectangle r = rect;

            switch (orientation)
            {
                case Orientation.Top:
                case Orientation.Bottom:
                    break;

                case Orientation.Left:
                case Orientation.Right:
                    r = new Rectangle(rect.X, rect.Y, rect.Height, rect.Width);
                    break;
            }

            return r;
        }
        #endregion

        #region [public] {static} (RectangleF) Scale(this Rectangle, float, float): Returns a new RectangleF structure scaled in the specified horizontal and vertical proportions
        /// <summary>
        /// Returns a new <see cref="RectangleF"/> structure scaled in the specified horizontal and vertical proportions.
        /// </summary>
        /// <param name="rect">A <see cref="Rectangle"/> structure that represents the origin rectangle to scale.</param>
        /// <param name="xScale">Scale factor on the horizontal axis.</param>
        /// <param name="yScale">Scale factor on the vertical axis.</param>
        /// <returns>
        /// A new <see cref="RectangleF"/> structure which represents the scaled origin rectangle in the specified proportions.
        /// </returns>
        public static RectangleF Scale(this Rectangle rect, float xScale, float yScale)
        {
            RectangleF rf = rect;
            return rf.Scale(xScale, yScale);
        }
        #endregion

        #region [public] {static} (RectangleF) Split(this Rectangle, Orientation): Returns a one-dimensional zero-based matrix that contains two equal RectangleF elements
        /// <summary>
        /// Returns a one-dimensional zero-based matrix that contains two equal <see cref="RectangleF"/> elements.
        /// </summary>
        /// <param name="rect"><see cref="Rectangle"/> structure that represents the source rectangle.</param>
        /// <param name="orientation">One of the values in the enumeration <see cref="Orientation"/> that represents the type of orientation.</param>
        /// <returns>
        /// A new matrix of <see cref="RectangleF"/> structures equals.
        /// If the diemsiones of the source rectangle are not valid, <b>Split</b> returns an array of a single element that contains the source rectangle.
        /// </returns>
        public static RectangleF[] Split(this Rectangle rect, Orientation orientation) => rect.Split(orientation, 0.5f);
        #endregion

        #region [public] {static} (RectangleF) Split(this Rectangle, Orientation, float): Returns a one-dimensional zero-based matrix that contains two RectangleF elements
        /// <summary>
        /// Returns a one-dimensional zero-based matrix that contains two <see cref="RectangleF"/> elements.
        /// </summary>
        /// <param name="rect"><see cref="Rectangle" /> structure that represents the source rectangle.</param>
        /// <param name="orientation">One of the values in the enumeration <see cref="Orientation"/> that represents the type of orientation.</param>
        /// <param name="lenghtPercent">Value in percentage of the length of the first rectangle.</param>
        /// <returns>
        /// A new matrix of <see cref="RectangleF"/> structures.
        /// If the diemsiones of the source rectangle are not valid, <b>Split</b> returns an array of a single element that contains the source rectangle.
        /// </returns>
        /// <remarks>
        /// The first element of the matrix is always the result of applying the criteria of orientation and length.
        /// </remarks>
        public static RectangleF[] Split(this Rectangle rect, Orientation orientation, float lenghtPercent)
        {
            RectangleF rf = rect;
            return rf.Split(orientation, lenghtPercent);
        }
        #endregion
    }
}
