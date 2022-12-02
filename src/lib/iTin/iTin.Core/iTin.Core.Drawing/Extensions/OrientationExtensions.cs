
using iTin.Core.Drawing.ComponentModel;

namespace iTin.Core.Drawing
{
    /// <summary>
    /// Static class than contains extension methods for type <see cref="Orientation"/>.
    /// </summary> 
    public static class OrientationExtensions
    {
        /// <summary>
        /// Returns a value that indicates whether the orientation is vertical.
        /// </summary>
        /// <param name="orientation">Orientation to check</param>
        /// <returns>
        /// <b>true</b> if orientation is vertical; otherwise, <b>false</b>.
        /// </returns>
        public static bool IsVerticalOrientation(this Orientation orientation) => orientation == Orientation.Top || orientation == Orientation.Bottom;

        /// <summary>
        /// Gets the angle of rotation in degrees for the specified orientation.
        /// </summary>
        /// <param name="orientation">One of the values in the enumeration <see cref="Orientation"/> that represents the orientation of the brush.</param>
        /// <returns>
        /// Angle of rotation in degrees for the specified orientation.
        /// </returns>
        public static float ToAngle(this Orientation orientation)
        {
            var angle = 90.0f;

            switch (orientation)
            {
                case Orientation.Left:
                    angle = 0.0f;
                    break;

                case Orientation.Bottom:
                    angle = 270.0f;
                    break;

                case Orientation.Right:
                    angle = 180.0f;
                    break;
            }

            return angle;
        }
    }
}
