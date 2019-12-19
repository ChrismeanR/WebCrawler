using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebCrawler.CR.Core.Models;

namespace WebCrawler.CR.Core
{
    public abstract class Program
    {
        protected static string gobjDomain = "connectforhealthco.com";
        protected static List<List<DisplayModel>> gobjPageOutput = new List<List<DisplayModel>>();
        //protected static ListOfDisplay<DisplayModel> gobjPageOutput = new ListOfDisplay<DisplayModel>();
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Start crawling");

            // get url of site to crawl
            Uri builditBaseUrl = new Uri("https://" + gobjDomain + "/");

            // jump into method to search the site and do the heavy lifting
            BeginCrawl(builditBaseUrl);
            // count pages to crawl that are internal to that domain
            // count pages to crawl that are external to that domain

        }

        public static void BeginCrawl(Uri builditBaseUrl)
        {
            var sitemap = new HtmlDocument();
            var url = builditBaseUrl;
            var web = new HtmlWeb();
            var doc = web.Load(url);
            int indexValue = 0;

            Dictionary<string, string> attrValue = new Dictionary<string, string>();

            // get all <link>
            var link = doc.DocumentNode.SelectNodes("//link[@href]");
            // get all <img> content 
            var images = doc.DocumentNode.SelectNodes("//img[@src]");
            var cssLinks = doc.DocumentNode.SelectNodes("//link[@rel]");
            var atagLinks = doc.DocumentNode.SelectNodes("//a[@href]");

            var pageColReturn = GetNodeAttributesByTag(atagLinks, "href", "a");
            var contentColReturn = GetNodeAttributesByTag(images, "src", "img");
            var cssColReturn = GetNodeAttributesByTag(cssLinks, "rel", "link");

            var itemsList = gobjPageOutput;
                            
            // format this as a return by combining the above

            foreach(var item in itemsList)
            {
                foreach(var detail in item)
                {
                    Console.WriteLine(detail.PageUri);
                }
            }
        }

        private static List<List<DisplayModel>> GetNodeAttributesByTag(HtmlNodeCollection colHtmlNode, string strAttribute, string tagType)
        {
            List<DisplayModel> display = new List<DisplayModel>();

            foreach (HtmlNode node in colHtmlNode)
            {
                HtmlAttribute attrHref = node.Attributes[strAttribute];

                if (attrHref != null)
                {
                    if (attrHref != null && attrHref.Value.Contains(tagType))
                    {
                        var title = (node.Attributes["title"] != null) ? node.Attributes["title"].Value : "";
                        //Console.WriteLine(title + " " + attrHref.Value);
                        display.Add(new DisplayModel { PageUri = attrHref.Value,  TagType = tagType, AttributeType = strAttribute, Node = node.NodeType.ToString()});
                       // gobjPageOutput.Add(new DisplayModel { PageUri = attrHref.Value, TagType = tagType, AttributeType = strAttribute, Node = node.NodeType.ToString() });

                    }

                }
                display.OrderBy(x => x.PageUri).ThenBy(y => y.AttributeType).ThenBy(z => z.TagType);
            }

            gobjPageOutput.Add(display);

            return gobjPageOutput;
        }
    }
}
