using System;

namespace WebCrawler.CR.Core.Models
{
    public struct WebCrawlerOutput
    {
        // struct needs to move out of this file and into its own
        public int PagesCount { get; set; }
        public Uri PageUri { get; set; }
        public bool HasStaticContent { get; set; }
        public string Content { get; set; }
        public static string FormatValues = "{0,-12} {1,-13}";
        public static string OutputHeading
        {
            get { return string.Format(FormatValues, new string[] {"Page URL", "Has Static Content" }); }
        }

        public override string ToString()
        {
            return string.Format(FormatValues, new string[]
            {
               PageUri.ToString(), HasStaticContent.ToString()
            });
        }
    }
}
