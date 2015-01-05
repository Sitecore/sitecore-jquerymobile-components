
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models.ViewRenderingModels
{
    public class FormSubmitViewRendering : Sitecore.Mvc.Presentation.RenderingModel
    {

        public ButtonModel ButtonModel { get; protected set; }
        public Sitecore.Data.Items.Item DisplayItem { get; protected set; }

        public string FormFieldName { get; protected set; }
        public string FormFieldValue { get; protected set; }
        public string FormFieldTitle { get; protected set; }

        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);

            DisplayItem = Rendering.Item;

            FormFieldName = Rendering.Parameters[MobileFieldNames.StandardFormViewRenderingParameters.FormFieldName] ?? "submit";
            FormFieldValue = Rendering.Parameters[MobileFieldNames.StandardFormViewRenderingParameters.FormFieldValue] ?? "submit";
            FormFieldTitle = Rendering.Parameters[MobileFieldNames.StandardFormViewRenderingParameters.FormFieldTitle] ?? "Submit";
           
            var buttonText = (string)null;
            var buttonClass = (string)null;

            buttonText = Rendering.Parameters[MobileFieldNames.StandardFormViewRenderingParameters.FormFieldTitle];
            buttonClass = Rendering.Parameters[MobileFieldNames.ButtonViewRenderingParameters.ButtonStyle] ?? null;

            ButtonModel = new SitecoreMobile.Models.ButtonModel()
            {
                ButtonType = "submit",
                ButtonId = FormFieldName,
                ButtonName = FormFieldName,
                ButtonText = buttonText,
                CssClass = buttonClass,
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