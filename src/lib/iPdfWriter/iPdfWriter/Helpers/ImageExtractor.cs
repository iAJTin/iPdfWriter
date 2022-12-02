
namespace Syntec.Utilities.Pdf.Writer.Helpers
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;

    using iTextSharp.text.pdf;
    using iTextSharp.text.pdf.parser;

    /// <summary>
    /// Contains common helper methods for Portable Document Format (pdf format).
    /// </summary>
    internal class ImageExtractor : IRenderListener
    {
        private int _currentPage = 1;

        private static readonly Collection<byte[]> ImagesAsByteArray = new Collection<byte[]>();

        private static Collection<Image> Images = new Collection<Image>();
        private static Collection<string> ImageNames = new Collection<string>();

        private ImageExtractor()
        {
        }


        public static IEnumerable<Image> ExtractImages(byte[] input)
        {
            var instance = new ImageExtractor();
            using (var reader = new PdfReader(input))
            {
                var parser = new PdfReaderContentParser(reader);
                while (instance._currentPage <= reader.NumberOfPages)
                {
                    parser.ProcessContent(instance._currentPage, instance);
                    instance._currentPage++;
                }
            }

            foreach (var item in ImagesAsByteArray)
            {
                var ms = new MemoryStream(item);
                FIBITMAP dib = FreeImage.LoadFromStream(ms);
                if (dib.IsNull)
                {
                    continue;
                }

                var bmp = FreeImage.GetBitmap(dib);
                Images.Add(bmp);
                FreeImage.UnloadEx(ref dib);
            }

            return Images;
        }

        public void BeginTextBlock()
        {
        }

        public void EndTextBlock()
        {
        }

        public void RenderText(TextRenderInfo renderInfo)
        {
        }

        public void RenderImage(ImageRenderInfo renderInfo)
        {
            var image = renderInfo.GetImage();
            if (renderInfo.GetRef() == null || image == null)
            {
                return;
            }

            ImagesAsByteArray.Add(image.GetImageAsBytes());
            ImageNames.Add($"{renderInfo.GetRef().Number}.{image.GetFileType()}");
        }
    }
}
