using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models
{
    public class BarcodeModel
    {
        public const int defaultWidthPx = 200;
        public const int defaultHeightPx = 200;

        private const ZXing.BarcodeFormat DEFAULT_BARCODE_FORMAT = ZXing.BarcodeFormat.QR_CODE;

        public ZXing.BarcodeFormat BarcodeFormat { get; protected set; }
        public System.Drawing.Imaging.ImageFormat ImageFormat { get; protected set; }
        public int WidthPx { get; protected set; }
        public int HeightPx { get; protected set; }

        public string BarcodeImageSvg { get; protected set; }
        public System.Drawing.Bitmap BarcodeImageBmp { get; protected set; }
        public byte[] BarcodeImageEncoded { get; protected set; }

        public string BarcodeContent { get; protected set; }

        public BarcodeModel(
            string barcodeContent,
            System.Drawing.Imaging.ImageFormat imageFormat,
            ZXing.BarcodeFormat barcodeFormat = DEFAULT_BARCODE_FORMAT,
            int widthPx = defaultWidthPx,
            int heightPx = defaultHeightPx)
        {
            ImageFormat = imageFormat;
            BarcodeContent = barcodeContent;
            BarcodeFormat = barcodeFormat;
            WidthPx = widthPx;
            HeightPx = heightPx;

            EncodeBarcodeSvg();
            EncodeBarcodeBmp();

        }

        protected virtual void EncodeBarcodeSvg()
        {
            var encodingOptions = new ZXing.Common.EncodingOptions
            {
                Width = WidthPx,
                Height = HeightPx
            };

            var svgWriter = new ZXing.BarcodeWriterSvg()
            {
                Options = encodingOptions,
                Format = BarcodeFormat
            };

            var svgImage = svgWriter.Write(BarcodeContent);

            BarcodeImageSvg = svgImage.ToString();

        }

        protected virtual void EncodeBarcodeBmp()
        {
            var encodingOptions = new ZXing.Common.EncodingOptions
            {
                Width = WidthPx,
                Height = HeightPx
            };
            ZXing.Rendering.BitmapRenderer renderer = new ZXing.Rendering.BitmapRenderer()
            {
                Foreground = System.Drawing.Color.Black,
                Background = System.Drawing.Color.White
            };
            var w = new ZXing.BarcodeWriter()
            {
                Format = BarcodeFormat,
                Options = encodingOptions,
                Renderer = renderer
            };

            BarcodeImageBmp = w.Write(BarcodeContent);

            using (var imageStream = new System.IO.MemoryStream())
            {
                BarcodeImageBmp.Save(imageStream, ImageFormat);
                BarcodeImageEncoded = imageStream.ToArray();
            }
        }
    }
}