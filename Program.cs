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
        public static string gobjDomain = "connectforhealthco.com";
        public static List<List<DisplayModel>> gobjPageOutput = new List<List<DisplayModel>>();
        //protected static ListOfDisplay<DisplayModel> gobjPageOutput = new ListOfDisplay<DisplayModel>();
        public static void Main(string[] args)
        {
            Console.WriteLine("Start crawling");

            // get url of site to crawl
            Uri builditBaseUrl = new Uri("https://" + gobjDomain + "/");

            // jump into method to search the site and do the heavy lifting
            BeginCrawl(builditBaseUrl);
            
        }

        public static void BeginCrawl(Uri builditBaseUrl)
        {
            var sitemap = new HtmlDocument();
            var url = builditBaseUrl;
            var web = new HtmlWeb();
            var doc = web.Load(url);

            // get all <link>
            var link = doc.DocumentNode.SelectNodes("//link[@href]");
            // get all <img> content 
            var images = doc.DocumentNode.SelectNodes("//img[@src]");
            var cssLinks = doc.DocumentNode.SelectNodes("//link[@rel]");
            var atagLinks = doc.DocumentNode.SelectNodes("//a[@href]");

            // run these through the function and add them to the global list
            var pageColReturn = GetNodeAttributesByTag(atagLinks, "href", "a");
            var contentColReturn = GetNodeAttributesByTag(images, "src", "img");
            var linkColReturn = GetNodeAttributesByTag(link, "href", "link");
            var cssColReturn = GetNodeAttributesByTag(cssLinks, "rel", "link");

            #region TODO: if more time, group these better and use this as the data context
            //var colReturn = from x in pageColReturn
            //                from y in contentColReturn
            //                from z in cssColReturn
            //                select new
            //                {
            //                    page = x.Select(x => x.PageUri),
            //                    image = y.Select(y => y.PageUri),
            //                    css = z.Select(z => z.PageUri),
            //                };
            //Console.WriteLine(colReturn); 
            #endregion

            var itemsList = gobjPageOutput;

            // format this as a return by combining the above

            foreach (var item in itemsList)
            {
                foreach (var detail in item)
                {
                    // TODO: if more time, get this hierarchy correct with relative urls & page content. It appends to the end currently

                    //if (detail.PageUri.Contains(gobjDomain))
                    //{
                    //    Console.WriteLine(detail.PageUri);
                    //}
                    //else
                    //{
                        Console.WriteLine(detail.PageUri);
                    //}
                }
            }
        }

        public static List<List<DisplayModel>> GetNodeAttributesByTag(HtmlNodeCollection colHtmlNode, string strAttribute, string tagType)
        {
            List<DisplayModel> display = new List<DisplayModel>();

            foreach (HtmlNode node in colHtmlNode)
            {
                HtmlAttribute attrHref = node.Attributes[strAttribute];

                if (attrHref != null)
                {
                    if (attrHref != null && attrHref.Value.Contains(tagType))
                    {
                        display.Add(new DisplayModel { PageUri = attrHref.Value, TagType = tagType, AttributeType = strAttribute, Node = node.NodeType.ToString() });
                    }
                }
            }

            gobjPageOutput.Add(display.OrderBy(y => y.PageUri).ThenBy(x => x.AttributeType).ThenBy(x => x.TagType).ToList());

            return gobjPageOutput;
        }
    }
}
