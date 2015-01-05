using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using SitecoreMobile.Common;



namespace SitecoreMobile.Models.ViewRenderingModels
{
    public class BarcodeControllerViewModel : Sitecore.Mvc.Presentation.RenderingModel
    {
        public const int defaultWidthPx = 200;
        public const int defaultHeightPx = 200;

        public const string defaultBarcodeFormat = "QR_CODE";
        public const string defaultImageFormat = "PNG";

        public BarcodeModel BarcodeModel { get; protected set; }

        public string BarcodeControllerName { get; protected set; }
        public string BarcodeControllerAction { get; protected set; }

        public HtmlString EmbeddedBarcodeImage { get; protected set; }

        public HtmlString SvgBarcodeImage { get; protected set; }


        public string BarcodeFormatParameter { get; protected set; }
        public string ImageFormatParameter { get; protected set; }

        public BarcodeControllerViewModel()
            : base()
        {
        }

        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);

            bool pageEditEnabled = (Sitecore.Context.Site.DisplayMode == Sitecore.Sites.DisplayMode.Edit);

            bool hiddenEditingField = rendering.Parameters[MobileFieldNames.StandardViewRenderingParameters.HiddenEditingField] == "1" ? true : false;
            if (hiddenEditingField && !pageEditEnabled)
            {
                return;
            }
            InitializeBeaconViewRendering();
        }

        private void InitializeBeaconViewRendering()
        {

            BarcodeControllerName = Rendering.Parameters[MobileFieldNames.BarcodeControllerViewRenderingParameters.BarcodeControllerName];

            BarcodeControllerAction = Rendering.Parameters[MobileFieldNames.BarcodeControllerViewRenderingParameters.BarcodeControllerAction];

            var linkFieldNameParameter = Rendering.Parameters[MobileFieldNames.BarcodeViewRenderingParameters.LinkFieldName] ?? null;
            var generalLinkParameter = Rendering.Parameters[MobileFieldNames.BarcodeViewRenderingParameters.GeneralLink] ?? null;
            BarcodeFormatParameter = Rendering.Parameters[MobileFieldNames.BarcodeViewRenderingParameters.BarcodeFormat] ?? defaultBarcodeFormat;
            ImageFormatParameter = Rendering.Parameters[MobileFieldNames.BarcodeViewRenderingParameters.ImageFormat] ?? defaultImageFormat;

            int w = defaultWidthPx;
            int h = defaultHeightPx;
            int.TryParse(Rendering.Parameters[MobileFieldNames.BarcodeViewRenderingParameters.WidthPx], out w);
            int.TryParse(Rendering.Parameters[MobileFieldNames.BarcodeViewRenderingParameters.HeightPx], out h);

            ZXing.BarcodeFormat barcodeFormat = ZXing.BarcodeFormat.QR_CODE;
            ImageFormat imageFormat = ImageFormat.Png;

            barcodeFormat = (ZXing.BarcodeFormat)Enum.Parse(typeof(ZXing.BarcodeFormat), BarcodeFormatParameter.Trim());

            switch (ImageFormatParameter.ToLower())
            {
                case "svg":
                    break;
                case "bmp":
                    imageFormat = ImageFormat.Bmp;
                    break;
                case "emf":
                    imageFormat = ImageFormat.Emf;
                    break;
                case "gif":
                    imageFormat = ImageFormat.Gif;
                    break;
                case "icon":
                    imageFormat = ImageFormat.Icon;
                    break;
                case "jpeg":
                case "jpg":
                    imageFormat = ImageFormat.Jpeg;
                    break;
                case "png":
                    imageFormat = ImageFormat.Png;
                    break;
                case "tiff":
                    imageFormat = ImageFormat.Tiff;
                    break;
                case "wmf":
                    imageFormat = ImageFormat.Wmf;
                    break;
                default:
                    throw new ArgumentException("Image format isn't supported", ImageFormatParameter.ToLower());
            }


            var buttonLink = (string)null;

            if (!string.IsNullOrEmpty(BarcodeControllerName) &&
                !string.IsNullOrEmpty(BarcodeControllerAction))
            {
                var r = new Sitecore.Mvc.Controllers.ControllerRunner(
                    BarcodeControllerName,
                    BarcodeControllerAction);
                buttonLink = r.Execute();

                // formFieldControllerValue = Html.Partial(Model.FormFieldValueControllerAction, Model.FormFieldValueControllerName);

            }

            if (string.IsNullOrEmpty(buttonLink))
            {
                buttonLink = string.Empty;
            }


            if (string.IsNullOrEmpty(buttonLink))
            {
                EmbeddedBarcodeImage = new HtmlString("Badge Error");
                SvgBarcodeImage = new HtmlString("Badge Error");
                return;
            }

            BarcodeModel = new BarcodeModel(buttonLink, imageFormat, barcodeFormat, w, h);

            System.Web.Mvc.TagBuilder tagBuilder = new System.Web.Mvc.TagBuilder("img");
            // tagBuilder.MergeAttributes<string, object>(tagParameters);

            string base64ImageString = string.Concat("data:", GetMimeType(imageFormat), ";base64,", Convert.ToBase64String(BarcodeModel.BarcodeImageEncoded));

            tagBuilder.Attributes.Add(new KeyValuePair<string, string>("src", base64ImageString));
            
            EmbeddedBarcodeImage = new HtmlString(tagBuilder.ToString(System.Web.Mvc.TagRenderMode.SelfClosing));

            SvgBarcodeImage = new HtmlString(BarcodeModel.BarcodeImageSvg);

        }
        public static string GetMimeType(ImageFormat imageFormat)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            return codecs.First(codec => codec.FormatID == imageFormat.Guid).MimeType;
        }
    }
}