using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models.ViewRenderingModels
{
    public class FormViewRendering : Sitecore.Mvc.Presentation.RenderingModel
    {

        public static class SelectViewRenderingFields
        {
        }

        public static readonly string FormSuccessRedirectResultItemFieldName = "formSuccessRedirectResult";

        public FormViewRendering()
            : base()
        {
            FormName = "Form";
            FormControllerName = "MobileForm";
            FormControllerAction = "Index";

        }

        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);
            InitalizeFormViewRendering();
        }

        public string PlaceholderName { get; protected set; }
        public string FormName { get; protected set; }
        public string FormControllerName { get; protected set; }
        public string FormControllerAction { get; protected set; }
        public string FormSuccessRedirectResultItem { get; protected set; }

        

        private void InitalizeFormViewRendering()
        {
            PlaceholderName = Rendering.Parameters[SitecoreMobile.MobileFieldNames.PlaceholderViewRenderingParameters.PlaceholderName];
            
            FormControllerName = Rendering.Parameters[MobileFieldNames.StandardFormViewRenderingParameters.FormControllerName];

            FormControllerAction = Rendering.Parameters[MobileFieldNames.StandardFormViewRenderingParameters.FormControllerAction];

            FormName = Rendering.Parameters[MobileFieldNames.StandardFormViewRenderingParameters.FormName];

            FormSuccessRedirectResultItem = Rendering.Parameters[MobileFieldNames.StandardFormViewRenderingParameters.FormSuccessRedirectResultItem];


            if (Rendering.Item == null || string.IsNullOrEmpty(FormControllerName))
            {
                return;
            } 
            if (Rendering.Item == null || string.IsNullOrEmpty(FormControllerAction))
            {
                return;
            } 
            if (Rendering.Item == null || string.IsNullOrEmpty(FormName))
            {
                return;
            }
            if (string.IsNullOrEmpty(PlaceholderName))
            {
                return;
            }


        }



    }
}