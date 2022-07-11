
using System.Drawing;

using iTin.Core.Models.Design.Enums;

namespace iTin.Utilities.Pdf.Writer
{
    /// <summary>
    /// Static class than contains common extension methods for <see cref="KnownHorizontalAlignment"/> enumeration.
    /// </summary>
    internal static class AlignmentExtensions
    {
        public static ContentAlignment ToContentAlignemnt(this KnownHorizontalAlignment horizontalAlignment)
        {
            ContentAlignment result = ContentAlignment.MiddleCenter;

            switch (horizontalAlignment)
            {
                case KnownHorizontalAlignment.Right:
                    result = ContentAlignment.MiddleRight;
                    break;

                case KnownHorizontalAlignment.Left:
                    result = ContentAlignment.MiddleLeft;
                    break;
            }

            return result;
        }
    }
}
