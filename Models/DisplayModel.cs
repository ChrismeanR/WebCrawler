using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace WebCrawler.CR.Core.Models
{
    class DisplayModel
    {
        // I want the properties here and referenced.
        public int PagesCount { get; set; }
        public Uri PageUri { get; set; }
        public bool HasStaticContent { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
    }

    public class Error : Program
    {
        public int HttpResult { get; set; }
        public Exception Exception { get; set; }
    }

    public class ExternalPage : Program { }

    public class InternalPage: Program
    {
        public virtual HtmlDocument HtmlDocument { get; internal set; }
    }
}
