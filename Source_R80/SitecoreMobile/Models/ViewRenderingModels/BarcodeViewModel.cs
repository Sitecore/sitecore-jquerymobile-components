using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using SitecoreMobile.Common;



namespace SitecoreMobile.Models.ViewRenderingModels
{
    public class BarcodeViewModel: Sitecore.Mvc.Presentation.RenderingModel
    {
        public const int defaultWidthPx = 200;
        public const int defaultHeightPx = 200;

        public const string defaultBarcodeFormat = "QR_CODE";
        public const string defaultImageFormat = "PNG";

        public BarcodeModel BarcodeModel { get; protected set; }

        public Sitecore.Data.Items.Item DisplayItem { get; protected set; }

        public HtmlString EmbeddedBarcodeImage { get; protected set; }
        public HtmlString SvgBarcodeImage { get; protected set; }

        public string BarcodeFormatParameter { get; protected set; }
        public string ImageFormatParameter { get; protected set; }


        public BarcodeViewModel(): base()
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
            DisplayItem = Rendering.Item;

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
                case "svg":
                    break;
                default:
                    throw new ArgumentException("Image format isn't supported", ImageFormatParameter.ToLower());
            }


            var buttonText = (string)null;
            var buttonLink = (string)null;
            var buttonTarget = (string)null;


            if (!string.IsNullOrEmpty(generalLinkParameter))
            {
                var generalLinkXml = HttpUtility.HtmlDecode(generalLinkParameter);

                var renderingParameterTemplateField = Rendering.RenderingItem.InnerItem.Fields[MobileFieldNames.ViewRenderingFields.ParametersTemplate];
                var renderingParameterTemplateLookupField = new Sitecore.Data.Fields.LookupField(renderingParameterTemplateField);
                var renderingParameterTemplateItem = new Sitecore.Data.Items.TemplateItem(renderingParameterTemplateLookupField.TargetItem);

                var standardValuesGeneralLinkField = renderingParameterTemplateItem.StandardValues.Fields[MobileFieldNames.BarcodeViewRenderingParameters.GeneralLink];

                var generalLinkField = new Sitecore.Data.Fields.LinkField(standardValuesGeneralLinkField, generalLinkXml);

                buttonText = generalLinkField.Text;
                buttonTarget = !string.IsNullOrEmpty(generalLinkField.Target) ? generalLinkField.Target : null;

                if (!string.IsNullOrEmpty(generalLinkField.Anchor))
                {
                    buttonLink = string.Format("#{0}", generalLinkField.Anchor);
                }
                else
                {
                    buttonLink = generalLinkField.Url;
                }
            }

            if (string.IsNullOrEmpty(buttonLink) && !string.IsNullOrEmpty(linkFieldNameParameter) && DisplayItem != null)
            {
                var itemField = DisplayItem.Fields[linkFieldNameParameter];
                if (itemField == null) { return; }

                var fieldItem = DisplayItem.Database.GetItem(itemField.ID);
                if (fieldItem == null) { return; }

                var linkField = new Sitecore.Data.Fields.LinkField(itemField);
                if (linkField.TargetItem == null) {
                    EmbeddedBarcodeImage = new HtmlString("Undefined barcode link field target item");
                    SvgBarcodeImage = new HtmlString("Undefined barcode link field target item"); 
                    return;
                }
                
                buttonLink = DisplayItem.GetRoutePathInfo(
                    new Sitecore.Links.UrlOptions()
                    {
                        AlwaysIncludeServerUrl = true
                    });

            }

            if (string.IsNullOrEmpty(buttonLink) && DisplayItem != null)
            {
                buttonLink = DisplayItem.GetRoutePathInfo(
                    new Sitecore.Links.UrlOptions()
                    {
                        AlwaysIncludeServerUrl = true
                    });
            }


            if (string.IsNullOrEmpty(buttonLink))
            {
                EmbeddedBarcodeImage = new HtmlString("Missing barcode link");
                SvgBarcodeImage = new HtmlString("Missing barcode link");
                return;
            }

            BarcodeModel = new BarcodeModel(buttonLink, imageFormat, barcodeFormat, w, h);

            System.Web.Mvc.TagBuilder tagBuilder = new System.Web.Mvc.TagBuilder("img");
            // tagBuilder.MergeAttributes<string, object>(tagParameters);

            string base64ImageString = string.Concat("data:", GetMimeType(imageFormat), ";base64,", Convert.ToBase64String(BarcodeModel.BarcodeImageEncoded));

            tagBuilder.Attributes.Add(new KeyValuePair<string, string>("src", base64ImageString));

            EmbeddedBarcodeImage = new HtmlString(tagBuilder.ToString());

            SvgBarcodeImage = new HtmlString(BarcodeModel.BarcodeImageSvg);

        }

        public static string GetMimeType(ImageFormat imageFormat)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            return codecs.First(codec => codec.FormatID == imageFormat.Guid).MimeType;
        }

    }
}