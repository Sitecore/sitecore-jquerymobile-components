using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SitecoreMobile.Common;

namespace SitecoreMobile.Models.ViewRenderingModels
{

    
    public class SlideshowViewRendering : ListViewRendering
    {
        public class SlideshowDisplayItem
        {
            public SlideshowDisplayItem(Item item, string imageFieldName)
            {
                DisplayItem = item;
                LinkSrc = item.GetRoutePathInfo();
                DataSrc = string.Empty;
                ThumbSrc = string.Empty;

                var imageField = item.Fields[imageFieldName];
                if (imageField == null) 
                { 
                    return; 
                }

                var imageFieldItem = new Sitecore.Data.Fields.ImageField(imageField);
                var mediaItem = imageFieldItem.MediaItem;
                var mediaLink = Sitecore.Resources.Media.MediaManager.GetMediaUrl(mediaItem); // new Sitecore.Resources.Media.MediaUrlOptions(400, 400, false)
                var mediaThumbLink = Sitecore.Resources.Media.MediaManager.GetMediaUrl(mediaItem); // new Sitecore.Resources.Media.MediaUrlOptions(400, 400, false)

                DataSrc = mediaLink;
                ThumbSrc = mediaThumbLink;
          
            }

            public string DataSrc { get; protected set; }
            public string ThumbSrc { get; protected set; }
            public string LinkSrc { get; protected set; }
            public Item DisplayItem { get; protected set; } 

        }



        

        public SlideshowViewRendering()
            : base()
        {

        }

        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);

            ImageFieldName = Rendering.Parameters[MobileFieldNames.SlideshowViewRenderingParameters.ImageFieldName];
            CaptionFieldName = Rendering.Parameters[MobileFieldNames.SlideshowViewRenderingParameters.CaptionFieldName];

            JsonParameters = Rendering.Parameters[MobileFieldNames.SlideshowViewRenderingParameters.JsonParameters];

            int w = 400;
            int h = 240;
            int.TryParse(Rendering.Parameters[MobileFieldNames.SlideshowViewRenderingParameters.ImageWidth], out w);
            int.TryParse(Rendering.Parameters[MobileFieldNames.SlideshowViewRenderingParameters.ImageHeight], out h);

            ImageWidth = w;
            ImageHeight = h;

            DisplayItems = DisplayItems.Where(i => !string.IsNullOrEmpty(i[ImageFieldName])).ToArray();

            List<SlideshowDisplayItem> resultItems = new List<SlideshowDisplayItem>();

            foreach (var i in DisplayItems)
            {                
                resultItems.Add(new SlideshowDisplayItem(i, ImageFieldName));
            }
            SlideshowDisplayItems = resultItems.ToArray();
        }

        public int MaxSlideshowLength { get; protected set; }
        public string ImageFieldName { get; protected set; }
        public string CaptionFieldName { get; protected set; }


        public string JsonParameters { get; protected set; }

        public int ImageWidth { get; protected set; }
        public int ImageHeight { get; protected set; }

        public SlideshowDisplayItem[] SlideshowDisplayItems { get; protected set; }
    }
}