using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebCrawler.CR.Core
{
    public abstract class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Start crawling");

            // get url of site to crawl
            Uri builditBaseUrl = new Uri("https://connectforhealthco.com/");

            // jump into method to search the site and do the heavy lifting
            BeginCrawl(builditBaseUrl);
            // count pages to crawl that are internal to that domain
            // count pages to crawl that are external to that domain

            //Console.WriteLine(colReturn);

        }

        private static void BeginCrawl(Uri builditBaseUrl)
        {
            var sitemap = new HtmlDocument();
            var url = builditBaseUrl;
            var web = new HtmlWeb();
            var doc = web.Load(url);
            int indexValue = 0;

            Dictionary<string, string> attrValue = new Dictionary<string, string>();

            System.Diagnostics.Debug.WriteLine(doc.DocumentNode.ChildNodes.ToHashSet());
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute attr = node.Attributes["href"];
                if (attr.Value.Contains("a"))
                {
                    var title = (node.Attributes["title"] != null) ? node.Attributes["title"].Value : "";
                    Console.WriteLine(title + " " + attr.Value);
                    //attrValue.Add(node.Attributes["title"].Value, attr.Value);
                }
            }

        }
    }
}
