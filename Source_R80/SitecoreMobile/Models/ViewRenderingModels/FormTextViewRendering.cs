using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models.ViewRenderingModels
{
    public class FormTextViewRendering : Sitecore.Mvc.Presentation.RenderingModel
    {
        
        public static class SelectViewRenderingFields
        {
        }


        public FormTextViewRendering()
            : base()
        {
        }

        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);
            InitalizeSelectViewRendering();
        }

        public string FormFieldName { get; protected set; }

        public string FormFieldValueControllerName { get; protected set; }
        public string FormFieldValueControllerAction { get; protected set; }

        public string FormFieldValue { get; protected set; }

        public bool ReadOnly { get; protected set; }

        public string DataInline { get; protected set; }
        public string DataMini { get; protected set; }
        public string DataRole { get; protected set; }

        public string FormFieldLabel { get; protected set; }

        private void InitalizeSelectViewRendering()
        {
            ReadOnly = Rendering.Parameters[MobileFieldNames.StandardFormViewRenderingParameters.FormFieldReadOnly] == "1" ? true : false;
            
            FormFieldName = Rendering.Parameters[MobileFieldNames.StandardFormViewRenderingParameters.FormFieldName] ;
            if (Rendering.Item == null || string.IsNullOrEmpty(FormFieldName))
            {
                return;
            }

            FormFieldValueControllerName = Rendering.Parameters[MobileFieldNames.StandardFormViewRenderingParameters.FormFieldValueControllerName];
            FormFieldValueControllerAction = Rendering.Parameters[MobileFieldNames.StandardFormViewRenderingParameters.FormFieldValueControllerAction];

            DataInline = Rendering.Parameters[MobileFieldNames.TextViewRenderingParameters.DataInline] == "1" ? "true" : "false";
            DataMini = Rendering.Parameters[MobileFieldNames.TextViewRenderingParameters.DataMini] == "1" ? "true" : "false";

            DataRole = !string.IsNullOrEmpty(Rendering.Parameters[MobileFieldNames.TextViewRenderingParameters.DataRole]) ? Rendering.Parameters[MobileFieldNames.TextViewRenderingParameters.DataRole] : "";
   
            string fieldTitle = Rendering.Parameters[MobileFieldNames.StandardFormViewRenderingParameters.FormFieldTitle];

            if (!string.IsNullOrEmpty(fieldTitle))
            {
                FormFieldLabel = fieldTitle;
            }

            if (!string.IsNullOrEmpty(FormFieldValueControllerName) &&
                !string.IsNullOrEmpty(FormFieldValueControllerAction))
            {
                var r = new Sitecore.Mvc.Controllers.ControllerRunner(
                    FormFieldValueControllerName,
                    FormFieldValueControllerAction);
                FormFieldValue = r.Execute();

            }

        }

        

    }
}