
namespace iTin.Core.Drawing
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using Core.Helpers;

    using ComponentModel;

    /// <summary>
    /// Static class than contains extension methods for objects of type <see cref="RectangleF"/>.
    /// </summary> 
    public static class RectangleFExtensions
    {
        #region [public] {static} (bool) AreValid(this IEnumerable<RectangleF>): Check if the properties Width and Height of all elements have values greater than zero
        /// <summary>
        /// Check if the properties <see cref="RectangleF.Width"/> and <see cref="RectangleF.Height"/> of all elements have values greater than zero.
        /// </summary>
        /// <param name="rectangles">Lista de <see cref="RectangleF"/> a comprobar.</param>
        /// <returns>
        /// <b>true</b> if all <see cref="RectangleF.Width"/> and <see cref="RectangleF.Height"/> properties values are greater than zero; otherwise, <b>false</b>.
        /// </returns>
        public static bool AreValid(this IEnumerable<RectangleF> rectangles)
        {
            bool ok = true;

            foreach (var r in rectangles)
            {
                var isValid = r.IsValid();
                if (!isValid)
                {
                    ok = false;
                    break;
                }
            }

            return ok;
        }
        #endregion

        #region [public] {static} (RectangleF) DeflateInOne(this RectangleF): Returns a new RectangleF structure decreased by one unit
        /// <summary>
        /// Returns a new <see cref="RectangleF"/> structure decreased by one unit.
        /// </summary>
        /// <param name="rect">Structure <see cref="RectangleF"/> to decrease.</param>
        /// <returns>
        /// A new <see cref="RectangleF"/> structure result of decreasing this structure by one unit.
        /// </returns>
        public static RectangleF DeflateInOne(this RectangleF rect)
        {
            rect.Inflate(-1, -1);
            return rect;
        }
        #endregion

        #region [public] {static} (RectangleF[]) DeflateInOne(this IEnumerable<RectangleF>): Returns a zero-dimensional unidimensional matrix containing the deflated RectangleF elements in a unit
        /// <summary>
        /// Returns a zero-dimensional unidimensional matrix containing the deflated <see cref="RectangleF"/> elements in a unit.
        /// </summary>
        /// <param name="rects">The rects.</param>
        /// <returns>
        /// Structure matrix <see cref="RectangleF"/>
        /// </returns>
        public static RectangleF[] DeflateInOne(this IEnumerable<RectangleF> rects)
        {
            return rects.Select(r => r.DeflateInOne()).ToArray();
        }
        #endregion

        #region [public] {static} (void) DrawCenterLine(this RectangleF Graphics, Color, DashStyleEx, int): Draw a centered line inside a rectangle with a specified color and style
        /// <summary>
        /// Draw a centered line inside a rectangle with a specified color and style.
        /// </summary>
        /// <param name="rect"><see cref="RectangleF"/> structure that represents the source rectangle.</param>
        /// <param name="graphics">A <see cref="Graphics"/> object used to draw.</param>
        /// <param name="color">Line color.</param>
        /// <param name="style">A value of the enumeration <see cref="DashStyleEx"/> that represents the style of the line</param>
        /// <param name="width">Thickness of the line</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="graphics"/> is <b>null</b>.</exception>
        public static void DrawCenterLine(this RectangleF rect, Graphics graphics, Color color, DashStyleEx style, int width)
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

        #region [public] {static} (RectangleF) Flip(this RectangleF, FlipMode): Returns a new rectangle structure flipped in the specified direction
        /// <summary>
        /// Returns a new <see cref="RectangleF"/>structure  flipped in the specified direction.
        /// </summary>
        /// <param name="rect">A <see cref="RectangleF"/> structure that represents the source rectangle to be flipped.</param>
        /// <param name="flipMode">One of the values in the enumeration <see cref="FlipMode"/> that represents the type of flip.</param>
        /// <returns>
        /// A new <see cref="RectangleF"/> structure flipped in the specified direction.
        /// </returns>
        public static RectangleF Flip(this RectangleF rect, FlipMode flipMode)
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

        #region [public] {static} (RectangleF) InflateInOne(this RectangleF): Returns a new RectangleF structure increased by one unit
        /// <summary>
        /// Returns a new <see cref="RectangleF"/> structure increased by one unit.
        /// </summary>
        /// <param name="rect">A <see cref= "RectangleF"/> structure to increase</param>
        /// <returns>
        /// A new <see cref="RectangleF"/> structure result of increasing this structure by one unit.
        /// </returns>
        public static RectangleF InflateInOne(this RectangleF rect)
        {
            rect.Inflate(1, 1);
            return rect;
        }
        #endregion

        #region [public] {static} (bool) IsValid(this RectangleF): Check if the Width and Height properties of this Rectangle structure have values greater than zero
        /// <summary>
        /// Check if the <see cref="RectangleF.Width"/> and <see cref="RectangleF.Height"/> properties of this <see cref="RectangleF"/> structure have values greater than zero.
        /// </summary>
        /// <param name="rect">Estructura <see cref="RectangleF"/> que representa el rectángulo origen.</param>
        /// <returns>
        /// <b>true</b> if <see cref="RectangleF.Width"/> and <see cref="RectangleF.Height"/> properties values are greater than zero; otherwise, <b>false</b>.
        /// </returns>
        public static bool IsValid(this RectangleF rect) => rect.Width > 0 && rect.Height > 0;
        #endregion

        #region [public] {static} (RectangleF) ModifyByLTRB(this RectangleF, int, int, int, int): Modify the specified rectangle with the indicated values
        /// <summary>
        /// Modify the specified rectangle with the indicated values.
        /// </summary>
        /// <param name="rect">Rectangle to modify</param>
        /// <param name="left">Left value</param>
        /// <param name="top">Top value</param>
        /// <param name="right">Right value</param>
        /// <param name="bottom">Bottom value</param>
        /// <returns>
        /// A <see cref="RectangleF"/> modified.
        /// </returns>
        public static RectangleF ModifyByLTRB(this RectangleF rect, int left, int top, int right, int bottom) => ModifyByLTRB(rect, left, top, right, bottom, Orientation.Top);
        #endregion

        #region [public] {static} (RectangleF) ModifyByLTRB(this RectangleF, int, int, int, int, Orientation): Modify the specified rectangle with the indicated values and its orientation
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
        /// A <see cref="RectangleF"/> modified.
        /// </returns>
        public static RectangleF ModifyByLTRB(this RectangleF rect, int left, int top, int right, int bottom, Orientation orientation)
        {
            RectangleF r = rect;

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

        #region [public] {static} (RectangleF) MoveAndAlignWith(this RectangleF, Rectangle, ContentAlignment): Returns a rectanglef structure aligned with the specified structure
        /// <summary>
        /// Returns a <see cref="RectangleF"/> structure aligned with the specified structure.
        /// </summary>
        /// <param name="rect">A <see cref="RectangleF"/> structure to align.</param>
        /// <param name="reference">A <see cref="RectangleF"/> structure reference.</param>
        /// <param name="alignment">One of the values in the enumeration <see cref="ContentAlignment"/> that represents the type of alignment to apply.</param>
        /// <returns>
        /// A new <see cref="RectangleF"/> structure aligned with the reference rectangle.
        /// </returns>
        public static RectangleF MoveAndAlignWith(this RectangleF rect, RectangleF reference, ContentAlignment alignment)
        {
            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                    rect.Offset(reference.X - rect.X, reference.Y - rect.Y - rect.Height);
                    break;

                case ContentAlignment.TopCenter:
                    rect.Offset(reference.X - rect.X + (reference.Width - rect.Width) / 2, reference.Y - rect.Y - rect.Height);
                    break;

                case ContentAlignment.TopRight:
                    rect.Offset(reference.X - rect.X + reference.Width - rect.Width, reference.Y - rect.Y - rect.Height);
                    break;

                case ContentAlignment.MiddleRight:
                    rect.Offset(reference.X - rect.X + reference.Width, reference.Y - rect.Y + (reference.Height - rect.Height) / 2);
                    break;

                case ContentAlignment.BottomRight:
                    rect.Offset(reference.X - rect.X + reference.Width - rect.Width, reference.Y + reference.Height - rect.Y);
                    break;

                case ContentAlignment.BottomCenter:
                    rect.Offset(reference.X - rect.X + (reference.Width - rect.Width) / 2, reference.Y + reference.Height - rect.Y);
                    break;

                case ContentAlignment.BottomLeft:
                    rect.Offset(reference.X - rect.X, reference.Y + reference.Height - rect.Y);
                    break;

                case ContentAlignment.MiddleLeft:
                    rect.Offset(reference.X - rect.X - rect.Width, reference.Y - rect.Y + (reference.Height - rect.Height) / 2);
                    break;

                case ContentAlignment.MiddleCenter:
                    rect.Offset(reference.X - rect.X + (reference.Width - rect.Width) / 2, reference.Y - rect.Y + (reference.Height - rect.Height) / 2);
                    break;
            }

            return rect;
        }
        #endregion

        #region [public] {static} (RectangleF) Rotate(this RectangleF, Orientation): Returns a new RectangleF structure rotated in the specified direction
        /// <summary>
        /// Returns a new <see cref="RectangleF"/> structure rotated in the specified direction.
        /// </summary>
        /// <param name="rect"><see cref="RectangleF"/> structure that represents the source rectangle.</param>
        /// <param name="orientation">One of the values in the enumeration <see cref="Orientation"/> that represents the type of orientation.</param>
        /// <returns>
        /// A new <see cref="RectangleF"/> structure rotated in the specified direction.
        /// </returns>
        public static RectangleF Rotate(this RectangleF rect, Orientation orientation)
        {
            RectangleF r = rect;

            switch (orientation)
            {
                case Orientation.Top:
                case Orientation.Bottom:
                    break;

                case Orientation.Left:
                case Orientation.Right:
                    r = new RectangleF(rect.X, rect.Y, rect.Height, rect.Width);
                    break;
            }

            return r;
        }
        #endregion

        #region [public] {static} (RectangleF) Scale(this RectangleF, float, float): Returns a new RectangleF structure scaled in the specified horizontal and vertical proportions
        /// <summary>
        /// Returns a new <see cref="RectangleF"/> structure scaled in the specified horizontal and vertical proportions.
        /// </summary>
        /// <param name="rect">A <see cref="RectangleF"/> structure that represents the origin rectangle to scale.</param>
        /// <param name="xScale">Scale factor on the horizontal axis.</param>
        /// <param name="yScale">Scale factor on the vertical axis.</param>
        /// <returns>
        /// A new <see cref="RectangleF"/> structure which represents the scaled origin rectangle in the specified proportions.
        /// </returns>
        public static RectangleF Scale(this RectangleF rect, float xScale, float yScale)
        {
            return new RectangleF(
                rect.X,
                rect.Y,
                rect.Width * xScale,
                rect.Height * yScale);
        }
        #endregion

        #region [public] {static} (RectangleF) Split(this RectangleF, Orientation): Returns a one-dimensional zero-based matrix that contains two equal RectangleF elements
        /// <summary>
        /// Returns a one-dimensional zero-based matrix that contains two equal <see cref="RectangleF"/> elements.
        /// </summary>
        /// <param name="rect"><see cref="Rectangle"/> structure that represents the source rectangle.</param>
        /// <param name="orientation">One of the values in the enumeration <see cref="Orientation" /> that represents the type of orientation.</param>
        /// <returns>
        /// A new matrix of <see cref="RectangleF"/> structures equals.
        /// If the diemsiones of the source rectangle are not valid, <b>Split</b> returns an array of a single element that contains the source rectangle.
        /// </returns>
        public static RectangleF[] Split(this RectangleF rect, Orientation orientation) => rect.Split(orientation, 0.5f);
        #endregion

        #region [public] {static} (RectangleF) Split(this RectangleF, Orientation, float): Returns a one-dimensional zero-based matrix that contains two RectangleF elements
        /// <summary>
        /// Returns a one-dimensional zero-based matrix that contains two <see cref="RectangleF"/> elements.
        /// </summary>
        /// <param name="rect"><see cref="Rectangle"/> structure that represents the source rectangle.</param>
        /// <param name="orientation">One of the values in the enumeration <see cref="Orientation"/> that represents the type of orientation.</param>
        /// <param name="lenghtPercent">Value in percentage of the length of the first rectangle.</param>
        /// <returns>
        /// A new matrix of <see cref="RectangleF"/> structures.
        /// If the diemsiones of the source rectangle are not valid, <b>Split</b> returns an array of a single element that contains the source rectangle.
        /// </returns>
        /// <remarks>
        /// The first element of the matrix is always the result of applying the criteria of orientation and length.
        /// </remarks>
        public static RectangleF[] Split(this RectangleF rect, Orientation orientation, float lenghtPercent)
        {
            int length = orientation.IsVerticalOrientation()
                ? (int) (rect.Height * lenghtPercent)
                : (int) (rect.Width * lenghtPercent);

            RectangleF restRect = RectangleF.Empty;
            RectangleF lengthRect = RectangleF.Empty;
            List<RectangleF> rectangles = new List<RectangleF>();
            switch (orientation)
            {
                case Orientation.Top:
                    lengthRect = new RectangleF(rect.X, rect.Y, rect.Width, length);
                    restRect = new RectangleF(rect.X, lengthRect.Bottom + 1, rect.Width, rect.Height - lengthRect.Height - 1);
                    break;

                case Orientation.Right:
                    lengthRect = new RectangleF(rect.Right - length, rect.Y, length, rect.Height);
                    restRect = new RectangleF(rect.X, lengthRect.Y, rect.Width - lengthRect.Width - 1, lengthRect.Height);
                    break;

                case Orientation.Bottom:
                    restRect = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height - length - 1);
                    lengthRect = new RectangleF(rect.X, restRect.Bottom + 1, rect.Width, length);
                    break;

                case Orientation.Left:
                    lengthRect = new RectangleF(rect.X, rect.Y, length, rect.Height);
                    restRect = new RectangleF(lengthRect.Right + 1, rect.Y, rect.Width - lengthRect.Width - 1, rect.Height);
                    break;
            }

            rectangles.Add(lengthRect);
            rectangles.Add(restRect);
            return rectangles.ToArray();
        }
        #endregion
    }
}
