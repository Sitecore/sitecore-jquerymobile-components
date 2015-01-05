using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models.ViewRenderingModels
{
    public class FormSelectViewRendering : Sitecore.Mvc.Presentation.RenderingModel
    {
        
        public static class SelectViewRenderingFields
        {
        }


        public FormSelectViewRendering()
            : base()
        {
            SelectOptions = new List<Tuple<string, string, Nullable<bool>>>();
        }

        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);
            InitalizeSelectViewRendering();
        }

        public List<Tuple<string, string, Nullable<bool>>> SelectOptions { get; protected set; }
        public string FormFieldName { get; protected set; }

        public string FormFieldValueControllerName { get; protected set; }
        public string FormFieldValueControllerAction { get; protected set; }

        public string FormFieldValue { get; protected set; }

       


        public string DataInline { get; protected set; }
        public string DataMini { get; protected set; }
        public string DataRole { get; protected set; }

        public string FormFieldLabel { get; protected set; }

        private void InitalizeSelectViewRendering()
        {

            FormFieldName = Rendering.Parameters[MobileFieldNames.StandardFormViewRenderingParameters.FormFieldName];
            if (Rendering.Item == null || string.IsNullOrEmpty(FormFieldName))
            {
                return;
            }

            FormFieldValueControllerName = Rendering.Parameters[MobileFieldNames.StandardFormViewRenderingParameters.FormFieldValueControllerName];
            FormFieldValueControllerAction = Rendering.Parameters[MobileFieldNames.StandardFormViewRenderingParameters.FormFieldValueControllerAction];

            DataInline = Rendering.Parameters[MobileFieldNames.SelectViewRenderingParameters.DataInline] == "1" ? "true" : "false";
            DataMini = Rendering.Parameters[MobileFieldNames.SelectViewRenderingParameters.DataMini] == "1" ? "true" : "false";

            DataRole = !string.IsNullOrEmpty(Rendering.Parameters[MobileFieldNames.SelectViewRenderingParameters.DataRole]) ? Rendering.Parameters[MobileFieldNames.SelectViewRenderingParameters.DataRole] : "";

            string selectDataSource = Rendering.Parameters[MobileFieldNames.SelectViewRenderingParameters.SelectDataSource] ?? "";
            //string selectFieldValue = Rendering.Parameters["SelectFieldValue"] ?? "Name";
            //string selectFieldName = Rendering.Parameters["SelectFieldName"] ?? "__Display Name";
            bool selectIncludeEmptyOption = Rendering.Parameters[MobileFieldNames.SelectViewRenderingParameters.SelectIncludeEmptyOption] == "1" ? true : false;

            
            string fieldTitle = Rendering.Parameters[MobileFieldNames.StandardFormViewRenderingParameters.FormFieldTitle];

            if (!string.IsNullOrEmpty(fieldTitle))
            {
                FormFieldLabel = fieldTitle;
            }

            if (selectIncludeEmptyOption)
            {
                SelectOptions.Add(new Tuple<string, string, Nullable<bool>>(string.Empty, string.Empty, null));
            }

            var selectDataItem = Sitecore.Context.Database.GetItem(selectDataSource);
            if (selectDataItem != null && selectDataItem.HasChildren)
            {
                foreach (Sitecore.Data.Items.Item item in selectDataItem.Children)
                {
                    SelectOptions.Add(new Tuple<string, string, Nullable<bool>>(item.Name, item.DisplayName, null));
                }
            }


            if (!string.IsNullOrEmpty(FormFieldValueControllerName) &&
                !string.IsNullOrEmpty(FormFieldValueControllerAction))
            {
                var r = new Sitecore.Mvc.Controllers.ControllerRunner(
                    FormFieldValueControllerName,
                    FormFieldValueControllerAction);
                FormFieldValue = r.Execute();

                // formFieldControllerValue = Html.Partial(Model.FormFieldValueControllerAction, Model.FormFieldValueControllerName);

            }

        }

        

    }
}