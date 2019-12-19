using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace WebCrawler.CR.Core.Models
{
    public class DisplayModel
    {
        public String Node { get; set; }
        //public Uri PageUri { get; set; }
        public String PageUri { get; set; }
        public String AttributeType { get; set; }
        public String TagType { get; set; }
    }
}
