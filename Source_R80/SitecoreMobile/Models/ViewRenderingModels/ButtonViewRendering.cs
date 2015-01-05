using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models.ViewRenderingModels
{
    public class ButtonViewRendering : Sitecore.Mvc.Presentation.RenderingModel
    {
        public ButtonViewRendering()
            : base()
        {
        }

        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);
            InitializeButtonViewRendering();
        }

        public ButtonModel ButtonModel { get; protected set; }
        public Item DisplayItem { get; protected set; }

        private void InitializeButtonViewRendering()
        {
            DisplayItem = Rendering.Item;

            var generalLinkParameter = Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.GeneralLink] ?? null;

            var buttonText = (string)null;
            var buttonLink = (string)null;
            var buttonTarget = (string)null;
            var buttonClass = (string)null;


            buttonClass = Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonStyle] ?? null;

            if (!string.IsNullOrEmpty(generalLinkParameter))
            {
                var generalLinkXml = HttpUtility.HtmlDecode(generalLinkParameter);

                var renderingParameterTemplateField = Rendering.RenderingItem.InnerItem.Fields[MobileFieldNames.ViewRenderingFields.ParametersTemplate];
                var renderingParameterTemplateLookupField = new Sitecore.Data.Fields.LookupField(renderingParameterTemplateField);        
                var renderingParameterTemplateItem = new Sitecore.Data.Items.TemplateItem(renderingParameterTemplateLookupField.TargetItem);

                var standardValuesGeneralLinkField = renderingParameterTemplateItem.StandardValues.Fields[MobileFieldNames.ButtonViewRenderingParameters.GeneralLink];

                var generalLinkField = new Sitecore.Data.Fields.LinkField(standardValuesGeneralLinkField, generalLinkXml);

                buttonText = generalLinkField.Text;
                buttonTarget = !string.IsNullOrEmpty(generalLinkField.Target) ? generalLinkField.Target : null;
                
                string spacer = " ";

                if (!string.IsNullOrEmpty(generalLinkField.Class))
                {
                    buttonClass = (!string.IsNullOrEmpty(buttonClass)) ? String.Concat(buttonClass, spacer, generalLinkField.Class) : generalLinkField.Class; 
                }
               
                if (!string.IsNullOrEmpty(generalLinkField.Anchor))
                {
                    buttonLink = string.Format("#{0}", generalLinkField.Anchor);            
                } 
                else
                {
                    buttonLink = generalLinkField.Url;            
                }        
            }

            ButtonModel = new SitecoreMobile.Models.ButtonModel()
            {
                Item = DisplayItem,
                ButtonText = buttonText,
                ButtonLink = buttonLink,
                ButtonTarget = buttonTarget,
                CssClass = buttonClass,
                LinkFieldName = Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.LinkFieldName] ?? null,
                TextFieldName = Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.TextFieldName] ?? null,
                ButtonIcon = Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonIcon],
                ButtonTheme = Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonTheme],
                ButtonInline = Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonInline] == "1" ? true : false,
                ButtonMini = Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonMini] == "1" ? true : false,
                ButtonIconPosition = Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonIconPosition],
                ButtonIconShadow = Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonIconShadow] == "1" || string.IsNullOrEmpty(Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonIconShadow]) ? true : false,
                ButtonCorners = Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonCorners] == "1" || string.IsNullOrEmpty(Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonCorners]) ? true : false,
                ButtonShadow = Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonShadow] == "1" || string.IsNullOrEmpty(Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonShadow]) ? true : false,
                ButtonPopupOverlay = Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonPopupOverlay] == "1" || string.IsNullOrEmpty(Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonPopupOverlay]) ? true : false,
                ButtonTransition = Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonTransition] ?? null,
                ButtonMode = Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonMode] ?? null,
                ButtonRole = Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonRole] ?? null
            };

        }
    }
}