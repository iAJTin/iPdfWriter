
using System;

namespace iTin.Utilities.Pdf.Writer.Operations.Replace
{
    /// <summary>
    /// Represents replace text options
    /// </summary>
    public class ReplaceTextOptions
    {
        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceTextOptions"/> class.
        /// </summary>
        public ReplaceTextOptions()
        {
            EndStrategy = EndLocationStrategy.Default;
            StartStrategy = StartLocationStrategy.Default;
            Comparison = StringComparison.OrdinalIgnoreCase;
            VerticalStrategy = VerticalFineStrategy.Top;
        }

        #endregion

        #region public static properties

        /// <summary>
        /// Gets a default replace text options, according to the margins.
        /// </summary>
        /// <value>
        /// A <see cref="ReplaceTextOptions"/> instance that contains the default value of replace text options.
        /// </value>
        public static ReplaceTextOptions AccordingToMargins => new() { StartStrategy = StartLocationStrategy.LeftMargin, EndStrategy = EndLocationStrategy.RightMargin };

        /// <summary>
        /// Gets a default replace text options, includes <b>Comparison</b> as <b>OrdinalIgnoreCase</b> value and strategies position values as <b>Default</b>.
        /// </summary>
        /// <value>
        /// A <see cref="ReplaceTextOptions"/> instance that contains the default value of replace text options.
        /// </value>
        public static ReplaceTextOptions Default => new();

        /// <summary>
        /// Gets a default replace text options, sets strategy as left margin position to next element.
        /// </summary>
        /// <value>
        /// A <see cref="ReplaceTextOptions"/> instance that contains the default value of replace text options.
        /// </value>
        public static ReplaceTextOptions FromLeftMarginToNextElement => new() { StartStrategy = StartLocationStrategy.LeftMargin, EndStrategy = EndLocationStrategy.NextElement };

        /// <summary>
        /// Gets a default replace text options, sets strategy as current position to next element.
        /// </summary>
        /// <value>
        /// A <see cref="ReplaceTextOptions"/> instance that contains the default value of replace text options.
        /// </value>
        public static ReplaceTextOptions FromPositionToNextElement => new() { EndStrategy = EndLocationStrategy.NextElement };

        /// <summary>
        /// Gets a default replace text options, sets strategy as current position to right margin.
        /// </summary>
        /// <value>
        /// A <see cref="ReplaceTextOptions"/> instance that contains the default value of replace text options.
        /// </value>
        public static ReplaceTextOptions FromPositionToRightMargin => new() { EndStrategy = EndLocationStrategy.RightMargin };

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets a value that represents how to compare strings. The default is <see cref="StringComparison.OrdinalIgnoreCase"/>.
        /// </summary>
        /// <value>
        /// One of the enumeration values <see cref="StringComparison"/>.
        /// </value>
        public StringComparison Comparison { get; set; }

        /// <summary>
        /// Gets or sets a value that represents the strategy to follow for the endpoint of the text element to be inserted. The default value is <see cref="EndLocationStrategy.Default"/>.
        /// </summary>
        /// <value>
        /// One of the enumeration values <see cref="EndLocationStrategy"/>.
        /// </value>
        public EndLocationStrategy EndStrategy { get; set; }

        /// <summary>
        /// Gets or sets a value that represents the strategy to follow for the startpoint of the text element to be inserted. The default value is <see cref="StartLocationStrategy.Default"/>.
        /// </summary>
        /// <value>
        /// One of the enumeration values <see cref="StartLocationStrategy"/>.
        /// </value>
        public StartLocationStrategy StartStrategy { get; set; }

        /// <summary>
        /// Gets or sets a value that represents the vertical replacement strategy to follow for the new element to be inserted. The default value is <see cref="VerticalFineStrategy.Top"/>.
        /// </summary>
        /// <value>
        /// One of the enumeration values <see cref="VerticalFineStrategy"/>.
        /// </value>
        public VerticalFineStrategy VerticalStrategy { get; set; }

        #endregion

        #region public override methods

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public override string ToString() =>
            $"Comparison={Comparison}, Stragegy=({StartStrategy}, {EndStrategy}), Vertical={VerticalStrategy}";

        #endregion
    }
}
