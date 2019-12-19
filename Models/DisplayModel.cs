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

    public class ListOfDisplay<T> : List<DisplayModel>
    {
        internal void Add(List<DisplayModel> display)
        {
            // TOOD: create override method for this for adds to a list of lists
            throw new NotImplementedException();
        }
    }
}
