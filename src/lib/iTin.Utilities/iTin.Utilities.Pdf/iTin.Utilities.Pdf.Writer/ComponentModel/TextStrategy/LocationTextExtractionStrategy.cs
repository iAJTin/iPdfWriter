
using System.Drawing;

namespace iTin.Utilities.Pdf.Writer.ComponentModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using iTextSharp.text.pdf.parser;

    /// <inheritdoc />
    /// <summary>
    /// Class LocationTextExtractionStrategy.
    /// </summary>
    /// <seealso cref="ITextExtractionStrategy"/>
    public class LocationTextExtractionStrategy : ITextExtractionStrategy
    {      
        #region private readonly members
        private readonly SortedList<string, DocumentFont> _thisPdfDocFonts;
        private readonly List<TextChunk> _locationalResult = new();
        #endregion

        #region constructor/s

        #region [public] LocationTextExtractionStrategy(): Initializes a new instance of the class
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationTextExtractionStrategy"/> class.
        /// </summary>
        public LocationTextExtractionStrategy()
        {
            UndercontentCharacterSpacing = 0;
            UndercontentHorizontalScaling = 0;
            _thisPdfDocFonts = new SortedList<string, DocumentFont>();
        }
        #endregion

        #endregion

        #region public properties

        #region [public] (float) UndercontentCharacterSpacing: Gets or sets a value of undercontent character spacing
        /// <summary>
        /// Gets or sets a value of undercontent character spacing.
        /// </summary>
        /// <value>
        /// A <see cref="float"/> that represents undercontent character spacing.
        /// </value>
        public float UndercontentCharacterSpacing { get; set; }
        #endregion

        #region [public] (float) UndercontentHorizontalScaling: Gets or sets a value of undercontent horizontal scaling
        /// <summary>
        /// Gets or sets a value of undercontent horizontal scaling.
        /// </summary>
        /// <value>
        /// A <see cref="float"/> that represents undercontent horizontal scaling.
        /// </value>
        public float UndercontentHorizontalScaling { get; set; }
        #endregion

        #region [public] (int) Color: Gets or sets a color to use
        /// <summary>
        /// Gets or sets a color to use.
        /// </summary>
        /// <value>
        /// A <see cref="int"/> that represents a color.
        /// </value>
        public int Color { get; set; }
        #endregion

        #endregion

        #region public methods

        #region [public] (void) BeginTextBlock(): Begin text block
        /// <summary>
        /// Begin text block
        /// </summary>
        public void BeginTextBlock()
        {
        }
        #endregion

        #region [public] (void) BeginTextBlock(): End Text Block
        /// <summary>
        /// End Text Block
        /// </summary>
        public void EndTextBlock()
        {
        }
        #endregion

        #region [public] (string) GetResultantText(): Get Resultant Text
        /// <summary>
        /// Get Resultant Text
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that contains result
        /// </returns>
        public string GetResultantText()
        {
            TextChunk lastChunk = null;
            var sb = new StringBuilder();

            _locationalResult.Sort();
            foreach (var chunk in _locationalResult)
            {
                if (lastChunk == null)
                {
                    sb.Append(chunk.Text);
                }
                else
                {
                    if (chunk.SameLine(lastChunk))
                    {
                        var dist = chunk.DistanceFromEndOf(lastChunk);
                        if (dist < -chunk.CharSpaceWidth)
                        {
                            sb.Append(' ');
                        }
                        else if (dist > chunk.CharSpaceWidth / 2.0F && !StartsWithSpace(chunk.Text) && !EndsWithSpace(lastChunk.Text))
                        {
                            sb.Append(' ');
                        }

                        sb.Append(chunk.Text);
                    }
                    else
                    {
                        sb.Append(Environment.NewLine);
                        sb.Append(chunk.Text);
                    }
                }

                lastChunk = chunk;
            }

            return sb.ToString();
        }
        #endregion

        #region [public] (IEnumerable<Rectangle>) GetTextLocations(): Returns a collection of LocationTextResult objects with search results
        /// <summary>
        /// Returns a collection of <see cref="LocationTextResult"/> objects with search results.
        /// </summary>
        /// <param name="searchString">Search string</param>
        /// <param name="options">Replace options</param>
        /// <returns>
        /// Collection of <see cref="LocationTextResult"/> objects with search results.
        /// </returns>
        public IEnumerable<Rectangle> GetTextLocations(string searchString, ReplaceTextOptions options)
        {
            var end = false;
            var start = false;
            var sb = new StringBuilder();
            var thisLineChunks = new List<TextChunk>();
            var foundMatches = new List<Rectangle>();

            var fakeLocation = new TextChunk("__FAKE__EOF__", new Vector(0,0, 0), new Vector(0,0, 0), 0.0f);
            _locationalResult.Add(fakeLocation);

            foreach (var chunk in _locationalResult)
            {                
                if (thisLineChunks.Count > 0 && !chunk.SameLine(thisLineChunks.Last()))
                {
                    var line = sb.ToString();
                    if (line.IndexOf(searchString, options.Comparison) > -1)
                    {
                        // Check how many times the Search String is present in this line:
                        var count = 0;
                        var pos = line.IndexOf(searchString, 0, options.Comparison);
                        while (pos > -1)
                        {
                            count += 1;
                            if (pos + searchString.Length > line.Length)
                            {
                                break;
                            }

                            pos += searchString.Length;
                            pos = line.IndexOf(searchString, pos, options.Comparison);
                        }

                        // Process each match found in this Text line:
                        var curPos = 0;
                        for (var i = 1; i <= count; i++)
                        {
                            var fromChar = line.IndexOf(searchString, curPos, options.Comparison);
                            curPos = fromChar;
                            var toChar = fromChar + searchString.Length - 1;
                            string currentText = null;
                            string textInUsedChunks = null;
                            TextChunk firstChunk = null;
                            TextChunk lastChunk = null;

                            // Get first and last Chunks corresponding to this match found, from all Chunks in this line
                            foreach (var chk in thisLineChunks)
                            {
                                currentText += chk.Text;

                                // Check if we entered the part where we had found a matching String then get this Chunk (First Chunk)
                                if (!start && currentText.Length - 1 >= fromChar)
                                {
                                    firstChunk = chk;
                                    start = true;
                                }

                                // Keep getting Text from Chunks while we are in the part where the matching String had been found
                                if (start && !end)
                                {
                                    textInUsedChunks += chk.Text;
                                }

                                // If we get out the matching String part then get this Chunk (last Chunk)
                                if (!end && currentText.Length - 1 >= toChar)
                                {
                                    lastChunk = chk;
                                    end = true;
                                }

                                // If we already have first and last Chunks enclosing the Text where our String pSearchString has been found 
                                // then it's time to get the rectangle, GetRectangleFromText Function below this Function, there we extract the pSearchString locations
                                if (start && end)
                                {                                    
                                    foundMatches.Add(GetRectangleFromText(firstChunk, lastChunk, searchString, textInUsedChunks, fromChar, toChar, options.Comparison));
                                    curPos += searchString.Length;
                                    start = false;
                                    end = false;
                                    break;                                    
                                }
                            }
                        }
                    }

                    sb.Clear();
                    thisLineChunks.Clear();
                }

                thisLineChunks.Add(chunk);
                sb.Append(chunk.Text);
            }

            return foundMatches;
        }
        #endregion

        #region [public] (IEnumerable<LocationTextResult>) GetExtendedTextLocations(): Returns a collection of LocationTextResult objects with search results
        /// <summary>
        /// Returns a collection of <see cref="LocationTextResult"/> objects with search results.
        /// </summary>
        /// <param name="searchString">Search string</param>
        /// <param name="options">Replace options</param>
        /// <returns>
        /// Collection of <see cref="LocationTextResult"/> objects with search results.
        /// </returns>
        public IEnumerable<LocationTextResult> GetExtendedTextLocations(string searchString, ReplaceTextOptions options)
        {
            var end = false;
            var start = false;
            var sb = new StringBuilder();
            var thisLineChunks = new List<TextChunk>();
            var foundMatches = new List<LocationTextResult>();

            var fakeLocation = new TextChunk("__FAKE__EOF__", new Vector(0, 0, 0), new Vector(0, 0, 0), 0.0f);
            _locationalResult.Add(fakeLocation);

            foreach (var chunk in _locationalResult)
            {
                if (thisLineChunks.Count > 0 && !chunk.SameLine(thisLineChunks.Last()))
                {
                    var line = sb.ToString();
                    if (line.IndexOf(searchString, options.Comparison) > -1)
                    {
                        // Check how many times the Search String is present in this line:
                        var count = 0;
                        var pos = line.IndexOf(searchString, 0, options.Comparison);
                        while (pos > -1)
                        {
                            count += 1;
                            if (pos + searchString.Length > line.Length)
                            {
                                break;
                            }

                            pos += searchString.Length;
                            pos = line.IndexOf(searchString, pos, options.Comparison);
                        }

                        // Process each match found in this Text line:
                        var curPos = 0;
                        for (var i = 1; i <= count; i++)
                        {
                            var fromChar = line.IndexOf(searchString, curPos, options.Comparison);

                            curPos = fromChar;
                            var toChar = fromChar + searchString.Length - 1;

                            TextChunk lastChunk = null;
                            TextChunk firstChunk = null;
                            string currentText = null;
                            string textInUsedChunks = null;

                            // Get first and last Chunks corresponding to this match found, from all Chunks in this line
                            foreach (var chk in thisLineChunks)
                            {
                                currentText += chk.Text;

                                // Check if we entered the part where we had found a matching String then get this Chunk (First Chunk)
                                if (!start && currentText.Length - 1 >= fromChar)
                                {
                                    firstChunk = chk;
                                    start = true;
                                }

                                // Keep getting Text from Chunks while we are in the part where the matching String had been found
                                if (start && !end)
                                {
                                    textInUsedChunks += chk.Text;
                                }

                                // If we get out the matching String part then get this Chunk (last Chunk)
                                if (!end && currentText.Length - 1 >= toChar)
                                {
                                    lastChunk = chk;
                                    end = true;
                                }

                                // If we already have first and last Chunks enclosing the Text where our String pSearchString has been found 
                                // then it's time to get the rectangle, GetRectangleFromText Function below this Function, there we extract the pSearchString locations
                                if (start && end)
                                {
                                    foundMatches.Add(
                                        new LocationTextResult
                                        {
                                            TextChunk = chk,  
                                            Rect = GetRectangleFromText(firstChunk, lastChunk, searchString, textInUsedChunks, fromChar, toChar, options.Comparison),
                                            StartPoint = new System.Drawing.PointF(firstChunk.PosLeft, firstChunk.PosTop),
                                            EndPoint = new System.Drawing.PointF(lastChunk.PosRight, lastChunk.PosBottom),
                                            //CharWidth = GetStringWidth(" ", lastChunk.CurFontSize, lastChunk.CharSpaceWidth, _thisPdfDocFonts.Values.ElementAt(lastChunk.FontIndex))
                                        });
                                    curPos += searchString.Length;
                                    start = false;
                                    end = false;
                                    break;
                                }
                            }
                        }
                    }

                    sb.Clear();
                    thisLineChunks.Clear();
                }

                thisLineChunks.Add(chunk);
                sb.Append(chunk.Text);
            }

            return foundMatches;
        }
        #endregion

        #region [public] (void) RenderImage(): Render image
        /// <summary>
        /// Render text
        /// </summary>
        /// <param name="renderInfo">render image information</param>
        public void RenderImage(ImageRenderInfo renderInfo)
        {
        }
        #endregion

        #region [public] (void) RenderText(TextRenderInfo): Render text
        /// <summary>
        /// Render text.
        /// </summary>
        /// <param name="renderInfo">render text information</param>
        public void RenderText(TextRenderInfo renderInfo)
        {
            var segment = renderInfo.GetBaseline();
            var location = new TextChunk(renderInfo.GetText(), segment.GetStartPoint(), segment.GetEndPoint(), renderInfo.GetSingleSpaceWidth())
            {
                PosLeft = renderInfo.GetDescentLine().GetStartPoint()[Vector.I1],
                PosRight = renderInfo.GetAscentLine().GetEndPoint()[Vector.I1],
                PosBottom = renderInfo.GetDescentLine().GetStartPoint()[Vector.I2],
                PosTop = renderInfo.GetAscentLine().GetEndPoint()[Vector.I2],
                FillColor = renderInfo.GetFillColor(),
                StrokeColor = renderInfo.GetStrokeColor(),
                Font = renderInfo.GetFont()
            };

            // Chunk Font Size: (Height)
            location.CurFontSize = location.PosTop - segment.GetStartPoint()[Vector.I2];

            // Use Font name  and Size as Key in the SortedList
            var strKey = renderInfo.GetFont().PostscriptFontName + location.CurFontSize;

            // Add this font to ThisPdfDocFonts SortedList if it's not already present
            if (!_thisPdfDocFonts.ContainsKey(strKey))
            {
                _thisPdfDocFonts.Add(strKey, renderInfo.GetFont());
            }

            // Store the SortedList index in this Chunk, so we can get it later
            location.FontIndex = _thisPdfDocFonts.IndexOfKey(strKey);

            _locationalResult.Add(location);
        }
        #endregion

        #endregion

        #region private static methods

        #region [private] {static} (bool) EndsWithSpace(string): Determines if string ends with space character
        /// <summary>
        /// Determines if string ends with space character
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>
        /// <b>true</b> if the string ends with a space character, <b>false</b> if the string is empty or ends with a non-space character.
        /// </returns>
        private static bool EndsWithSpace(string str)
        {
            if (str.Length == 0)
            {
                return false;
            }

            return str[str.Length - 1] == ' ';
        }
        #endregion

        #region [private] {static} (bool) StartsWithSpace(string): Determines if string starts with space character
        /// <summary>
        /// Determines if string starts with space character
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>
        /// <b>true</b> if the string starts with a space character, <b>false</b> if the string is empty or starts with a non-space character.
        /// </returns>
        private static bool StartsWithSpace(string str)
        {
            if (str.Length == 0)
            {
                return false;
            }

            return str[0] == ' ';
        }
        #endregion

        #endregion

        #region private methods

        private Rectangle GetRectangleFromText(TextChunk firstChunk, TextChunk lastChunk, string searchString, string textInChunks, int fromChar, int toChar, StringComparison strComparison)
        {
            // There are cases where Chunk contains extra text at begining and end, we don't want this text locations, we need to extract the pSearchString location inside
            // for these cases we need to crop this String (left and Right), and measure this excedent at left and right, at this point we don't have any direct way to make a
            // Transformation from text space points to User Space units, the matrix for making this transformation is not accesible from here, so for these special cases when
            // the String needs to be cropped (Left/Right) We'll interpolate between the width from Text in Chunk (we have this value in User Space units), then i'll measure Text corresponding
            // to the same String but in Text Space units, finally from the relation betweeenthese 2 values I get the TransformationValue I need to use for all cases

            // Text Width in User Space Units
            var lineRealWidth = lastChunk.PosRight - firstChunk.PosLeft;

            // Text Width in Text Units
            var lineTextWidth = 
                GetStringWidth(
                    textInChunks, 
                    lastChunk.CurFontSize, 
                    lastChunk.CharSpaceWidth,
                    _thisPdfDocFonts.Values.ElementAt(lastChunk.FontIndex));

            //TransformationValue value for Interpolation
            var transformationValue = lineRealWidth / lineTextWidth;

            // In the worst case, we'll need to crop left and right:
            var start = textInChunks.IndexOf(searchString, strComparison);
            var end = start + searchString.Length - 1;

            var left =
                start == 0 
                    ? null 
                    : textInChunks.Substring(0, start);

            var right = 
                end == textInChunks.Length - 1
                    ? null
                    : textInChunks.Substring(end + 1, textInChunks.Length - end - 1);

            // Measure cropped MediaTypeNames.Text at left:
            var leftWidth = 0.0f;
            if (start > 0)
            {
                leftWidth = 
                    GetStringWidth(
                        left, 
                        lastChunk.CurFontSize, 
                        lastChunk.CharSpaceWidth,
                        _thisPdfDocFonts.Values.ElementAt(lastChunk.FontIndex));
                leftWidth *= transformationValue;
            }

            // Measure cropped MediaTypeNames.Text at right:
            var rightWidth = 0.0f;
            if (end < textInChunks.Length - 1)
            {
                rightWidth = 
                    GetStringWidth(
                        right, 
                        lastChunk.CurFontSize, 
                        lastChunk.CharSpaceWidth, 
                        _thisPdfDocFonts.Values.ElementAt(lastChunk.FontIndex));
                rightWidth *= transformationValue;
            }

            // LeftWidth is the text width at left we need to exclude, 
            // FirstChunk.distParallelStart is the distance to left margin, both together will give us this LeftOffset
            var leftOffset = firstChunk.DistParallelStart + leftWidth;

            // RightWidth is the text width at right we need to exclude, 
            // FirstChunk.distParallelEnd is the distance to right margin, we substract RightWidth from distParallelEnd to get RightOffset
            var rightOffset = lastChunk.DistParallelEnd - rightWidth;

            //Return this Rectangle
            return new Rectangle(leftOffset, firstChunk.PosBottom, rightOffset, firstChunk.PosTop);
        }

        private float GetStringWidth(string str, float curFontSize, float singleSpaceWidth, BaseFont pFont)
        {
            var totalWidth = 0.0f;

            var chars = str.ToCharArray();
            foreach (var c in chars)
            {
                var w = pFont.GetWidth(c) / 1000.0f;
                totalWidth += (w * curFontSize + UndercontentCharacterSpacing) * UndercontentHorizontalScaling / 100;
            }

            return totalWidth;
        }

        #endregion


        #region nested classes

        #region [public] (class) LocationTextResult: Represents a text chunk location rectangle
        /// <summary>
        /// Represents a text chunk location rectangle.
        /// </summary>
        public class LocationTextResult
        {
            /// <summary>
            /// Gets or sets textchunk
            /// </summary>
            /// <value>
            /// A <see cref="TextChunk"/> reference.
            /// </value>
            public TextChunk TextChunk { get; set; }

            /// <summary>
            /// Gets or sets a textchunk rectangle
            /// </summary>
            /// <value>
            /// A <see cref="Rectangle"/> reference.
            /// </value>
            public Rectangle Rect { get; set; }

            /// <summary>
            /// Gets or sets the end text position
            /// </summary>
            /// <value>
            /// A <see cref="PointF"/> reference.
            /// </value>
            public PointF EndPoint { get; set; }

            /// <summary>
            /// Gets or sets the start text position
            /// </summary>
            /// <value>
            /// A <see cref="PointF"/> reference.
            /// </value>
            public PointF StartPoint { get; set; }
        }
        #endregion

        #region [public] (class) TextChunk: Represents a chunk of text, it's orientation, and location relative to the orientation vector
        /// <inheritdoc/>
        /// <summary>
        /// Represents a chunk of text, it's orientation, and location relative to the orientation vector.
        /// </summary>
        public class TextChunk : IComparable<TextChunk>
        {
            #region public members

            /// <summary>
            /// Defines adjust for determine the same line method.
            /// </summary>
            public int SameLineEpsilon = 5;

            /// <summary>
            /// The width of a single space character in the font of the chunk
            /// </summary>
            public float CharSpaceWidth;

            /// <summary>
            /// Distance of the end of the chunk parallel to the orientation unit vector (i.e. the X position in an unrotated coordinate system) 
            /// </summary>
            public float DistParallelEnd;

            /// <summary>
            /// Distance of the start of the chunk parallel to the orientation unit vector (i.e. the X position in an unrotated coordinate system) 
            /// </summary>
            public float DistParallelStart;

            /// <summary>
            /// Perpendicular distance to the orientation unit vector (i.e. the Y position in an unrotated coordinate system)
            /// we round to the nearest integer to handle the fuzziness of comparing floats 
            /// </summary>
            public int DistPerpendicular;

            /// <summary>
            /// The ending location of the chunk 
            /// </summary>
            public Vector EndLocation;

            /// <summary>
            /// The orientation as a scalar for quick sorting 
            /// </summary>
            public int OrientationMagnitude;

            /// <summary>
            /// Unit vector in the orientation of the chunk 
            /// </summary>
            public Vector OrientationVector;

            /// <summary>
            /// The starting location of the chunk 
            /// </summary>
            public Vector StartLocation;

            /// <summary>
            /// The text of the chunk 
            /// </summary>
            public string Text;

            #endregion

            #region constructor/s

            #region [public] TextChunk(string, Vector, Vector, float): Initializes a new instance of the class
            /// <summary>
            /// Initializes a new instance of the <see cref="TextChunk"/> class.
            /// </summary>
            /// <param name="str">The string.</param>
            /// <param name="startLocation">The start location.</param>
            /// <param name="endLocation">The end location.</param>
            /// <param name="charSpaceWidth">Width of the character space.</param>
            public TextChunk(string str, Vector startLocation, Vector endLocation, float charSpaceWidth)
            {
                Text = str;
                StartLocation = startLocation;
                EndLocation = endLocation;
                CharSpaceWidth = charSpaceWidth;

                var oVector = endLocation.Subtract(startLocation);
                if (oVector.Length == 0.0f)
                {
                    oVector = new Vector(1, 0, 0);
                }

                OrientationVector = oVector.Normalize();
                OrientationMagnitude = (int) Math.Truncate(Math.Atan2(OrientationVector[Vector.I2], OrientationVector[Vector.I1]) * 1000);

                var origin = new Vector(0, 0, 1);
                DistPerpendicular = (int) startLocation.Subtract(origin).Cross(OrientationVector)[Vector.I3];

                DistParallelStart = OrientationVector.Dot(startLocation);
                DistParallelEnd = OrientationVector.Dot(endLocation);
            }
            #endregion

            #endregion

            #region public properties

            /// <summary>
            /// Pos left
            /// </summary>
            public float PosLeft { get; set; }

            /// <summary>
            /// Pos right
            /// </summary>
            public float PosRight { get; set; }

            /// <summary>
            /// Pos Top
            /// </summary>
            public float PosTop { get; set; }

            /// <summary>
            /// Pos bottom
            /// </summary>
            public float PosBottom { get; set; }

            /// <summary>
            /// Current font size
            /// </summary>
            public float CurFontSize { get; set; }

            /// <summary>
            /// Font index
            /// </summary>
            public int FontIndex { get; set; }

            /// <summary>
            /// Fill color
            /// </summary>
            public BaseColor FillColor { get; set; }

            /// <summary>
            /// Stroke Color
            /// </summary>
            public BaseColor StrokeColor { get; set; }

            /// <summary>
            /// Font
            /// </summary>
            public BaseFont Font { get; set; }

            #endregion

            #region public methods

            #region [public] (int) CompareTo(TextChunk): Compare the current object with another object of the same type
            /// <inheritdoc />
            /// <summary>
            /// Compara el objeto actual con otro objeto del mismo tipo.
            /// </summary>
            /// <param name="other">Objeto que se va a comparar con este objeto.</param>
            /// <returns>
            /// Un valor que indica el orden relativo de los objetos que se están comparando.El valor devuelto tiene los siguientes significados
            /// Menor que cero Este objeto es menor que el parámetro <paramref name="other"/>.
            /// Cero Este objeto es igual que <paramref name="other"/>.
            /// Mayor que cero Este objeto es mayor que <paramref name="other"/>.
            /// </returns>
            public int CompareTo(TextChunk other)
            {
                int rslt = CompareInts(OrientationMagnitude, other.OrientationMagnitude);
                if (rslt != 0)
                {
                    return rslt;
                }

                rslt = CompareInts(DistPerpendicular, other.DistPerpendicular);
                if (rslt != 0)
                {
                    return rslt;
                }

                // Note: it's never safe to check floating point numbers for equality, and if two chunks
                // are truly right on top of each other, which one comes first or second just doesn't matter
                // so we arbitrarily choose this way.
                rslt = DistParallelStart < other.DistParallelStart ? -1 : 1;

                return rslt;
            }
            #endregion

            #region [public] (float) DistanceFromEndOf(TextChunk):  Computes the distance between the end of 'other' and the beginning of this chunk in the direction of this chunk's orientation vector
            /// <summary>
            /// Computes the distance between the end of 'other' and the beginning of this chunk
            /// in the direction of this chunk's orientation vector. Note that it's a bad idea
            /// to call this for chunks that aren't on the same line and orientation, but we don't
            /// explicitly check for that condition for performance reasons.
            /// </summary>
            /// <param name="other">The other chunk.</param>
            /// <returns>
            /// The number of spaces between the end of 'other' and the beginning of this chunk.
            /// </returns>
            public float DistanceFromEndOf(TextChunk other)
            {
                return DistParallelStart - other.DistParallelEnd;
            }
            #endregion

            #region [public] (bool) SameLine(TextChunk): Compares two TextChunk instances
            /// <summary>
            /// Compares two instances.
            /// </summary>
            /// <param name="other">The location to compare.</param>
            /// <returns>
            /// <b>true</b> if is this location is on the the same line as the other, <b>false</b> otherwise.
            /// </returns>
            public bool SameLine(TextChunk other)
            {
                if (OrientationMagnitude != other.OrientationMagnitude)
                {
                    return false;
                }

                //if (DistPerpendicular < (other.DistPerpendicular - SameLineEpsilon) && DistPerpendicular > (other.DistPerpendicular + SameLineEpsilon))
                //{
                //    return false;
                //}

                if (DistPerpendicular != other.DistPerpendicular)
                {
                    return false;
                }

                return true;
            }
            #endregion

            #endregion

            #region private static methods

            #region [private] {static} (int) CompareInts(int, int): Compares two integer values
            /// <summary>
            /// Compares two integer values.
            /// </summary>
            /// <param name="int1">Left value</param>
            /// <param name="int2">Right value</param>
            /// <returns>
            /// Comparison of the two integers.
            /// </returns>
            private static int CompareInts(int int1, int int2) => int1.CompareTo(int2);
            #endregion

            #endregion
        }
        #endregion

        #endregion
    }
}
