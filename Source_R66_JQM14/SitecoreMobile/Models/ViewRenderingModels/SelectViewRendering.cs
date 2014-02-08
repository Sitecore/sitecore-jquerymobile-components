using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreMobile.Models.ViewRenderingModels
{
    public class SelectViewRendering : Sitecore.Mvc.Presentation.RenderingModel
    {
        
        public static class SelectViewRenderingFields
        {
        }


        public SelectViewRendering() : base()
        {
            SelectOptions = new List<Tuple<string, string, bool>>();
        }

        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);
            InitalizeSelectViewRendering();
        }

        public List<Tuple<string, string, bool>> SelectOptions { get; protected set; }
        public string FieldName { get; protected set; }


        public string DataInline { get; protected set; }
        public string DataMini { get; protected set; }
        public string DataRole { get; protected set; }

        public string PageEditorLabel { get; protected set; }
        public bool HiddenEditingField { get; protected set; }
        public bool EditingEnabled { get; protected set; }


        private void InitalizeSelectViewRendering()
        {
            EditingEnabled = (Sitecore.Context.Site.DisplayMode == Sitecore.Sites.DisplayMode.Edit);

            FieldName = Rendering.Parameters[MobileFieldNames.StandardViewRenderingParameters.FieldName];
            if (Rendering.Item == null || string.IsNullOrEmpty(FieldName))
            {
                return;
            }

            DataInline = Rendering.Parameters[MobileFieldNames.SelectViewRenderingParameters.DataInline] == "1" ? "true" : "false";
            DataMini = Rendering.Parameters[MobileFieldNames.SelectViewRenderingParameters.DataMini] == "1" ? "true" : "false";

            DataRole = !string.IsNullOrEmpty(Rendering.Parameters[MobileFieldNames.SelectViewRenderingParameters.DataRole]) ? Rendering.Parameters[MobileFieldNames.SelectViewRenderingParameters.DataRole] : "";

            string selectDataSource = Rendering.Parameters[MobileFieldNames.SelectViewRenderingParameters.SelectDataSource] ?? "";
            //string selectFieldValue = Rendering.Parameters["SelectFieldValue"] ?? "Name";
            //string selectFieldName = Rendering.Parameters["SelectFieldName"] ?? "__Display Name";
            bool selectIncludeEmptyOption = Rendering.Parameters[MobileFieldNames.SelectViewRenderingParameters.SelectIncludeEmptyOption] == "1" ? true : false;

            HiddenEditingField = Rendering.Parameters[MobileFieldNames.StandardViewRenderingParameters.HiddenEditingField] == "1" ? true : false;
            if (HiddenEditingField && !EditingEnabled)
            {
                return;
            }

            bool displayFieldTitle = Rendering.Parameters[MobileFieldNames.StandardViewRenderingParameters.DisplayFieldTitle] == "1" ? true : false;
            bool displayEditingFieldTitle = Rendering.Parameters[MobileFieldNames.StandardViewRenderingParameters.DisplayEditingFieldTitle] == "1" ? true : false;
            
            if (displayFieldTitle
                || (displayEditingFieldTitle && EditingEnabled))
            {
                var itemField = Rendering.Item.Fields[FieldName];
                var fieldItem = Rendering.Item.Database.GetItem(itemField.ID);
                PageEditorLabel = string.Format("{0}:", fieldItem.DisplayName);
            }

            if (selectIncludeEmptyOption)
            {
                SelectOptions.Add(new Tuple<string, string, bool>(string.Empty, string.Empty, false));
            }

            var selectDataItem = Sitecore.Context.Database.GetItem(selectDataSource);
            if (selectDataItem != null && selectDataItem.HasChildren)
            {
                foreach (Sitecore.Data.Items.Item item in selectDataItem.Children)
                {
                    SelectOptions.Add(new Tuple<string, string, bool>(item.Name, item.DisplayName, false));
                }
            }    

          

        }

        

    }
}