using System;
using System.Collections.Generic;
using DotnetSpider.Core;
using DotnetSpider.Core.Pipeline;
using DotnetSpider.Core.Processor;
using DotnetSpider.Core.Scheduler;
using DotnetSpider.Core.Selector;
using DotnetSpider.Core.Downloader;
using System.Text;
using System.Web;
using System.Net;

namespace TryAndDie.Model.WebCrawlers
{
    public class RvuniqueCrawler 
    {
        static List<ItemModel> itemList = new List<ItemModel>();
        public static List<string> names = new List<string>();
        public RvuniqueCrawler()
        {
            
        }
        public static void Run()
        {
            // Config encoding, header, cookie, proxy etc... 定义采集的 Site 对象, 设置 Header、Cookie、代理等
            var site = new Site { EncodingName = "UTF-8", RemoveOutboundLinks = true };
            //for (int i = 1; i < 5; ++i)
            //{
            //    Add start/ feed urls.添加初始采集链接
            //    site.AddStartUrl($"http://list.youku.com/category/show/c_96_s_1_d_1_p_{i}.html");
            site.AddStartUrl($"http://www.rvunique.dk/webshop/rengoeringsmidler/special_produkter.aspx");
            //}

            Spider spider = Spider.Create(site,
            // use memoery queue scheduler. 使用内存调度
            new QueueDuplicateRemovedScheduler(),
            new PageProcessor())
            .AddPipeline(new Pipeline());
            spider.Downloader = new HttpClientDownloader();
            spider.ThreadNum = 1;
            spider.EmptySleepTime = 3000;

            // Start crawler 启动爬虫
            spider.Run();
        }
    }

    public class PageProcessor : BasePageProcessor
    {
        List<Uri> uriList;
        List<string> uriStringList;
        public PageProcessor()
        {
            uriList = new List<Uri>()
            {
                new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/special_produkter.aspx"),
                new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/sanitetsprodukter.aspx"),
                new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/klar-til-brug.aspx")
                //new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/svanemaerkede_produkter.aspx"),
                //new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/vinduesprodukter.aspx"),
                //new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/universal_produkter.aspx"),
                //new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/toejvask.aspx"),
                //new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/polish.aspx"),
                //new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/industriprodukter.aspx")
            };

            uriStringList = new List<string>()
            {
                "http://www.rvunique.dk/webshop/rengoeringsmidler/sanitetsprodukter.aspx",
                "http://www.rvunique.dk/webshop/rengoeringsmidler/klar-til-brug.aspx",
                "http://www.rvunique.dk/webshop/rengoeringsmidler/svanemaerkede_produkter.aspx",
                "http://www.rvunique.dk/webshop/rengoeringsmidler/vinduesprodukter.aspx",
                "http://www.rvunique.dk/webshop/rengoeringsmidler/universal_produkter.aspx",
                "http://www.rvunique.dk/webshop/rengoeringsmidler/toejvask.aspx",
                "http://www.rvunique.dk/webshop/rengoeringsmidler/polish.aspx",
                "http://www.rvunique.dk/webshop/rengoeringsmidler/industriprodukter.aspx"

            };
        }
             
        protected override void Handle(Page page)
        {
            var stringDomain = "http://www.rvunique.dk";
            List<ItemModel> items = new List<ItemModel>();
            var mylist = new List<string>();
            
            var tb = page.Selectable.SelectList(Selectors.XPath("//div[@class = 'products']//tbody")).Nodes()[0];
            var trNodes = tb.SelectList(Selectors.XPath(".//tr")).Nodes();
                
            foreach (var tr in trNodes)
            {
                var undecodedUriString = tr
                    .SelectList(Selectors.XPath(".//td[@class= 'ac thumb']//img/@src"))
                    .GetValue();
                var decodedeUriString = WebUtility.HtmlDecode(undecodedUriString);
                var uri = new Uri(decodedeUriString);





                //var code = tr
                //        .SelectList(Selectors.XPath(".//span[@class='art']"))
                //        .Nodes()[0]
                //        .GetValue()
                //        .Trim();
                //mylist.Add(r);
            }
            
            page.AddResultItem("codes", mylist);
            page.AddTargetRequests(uriStringList);

            //var final = mylist;
        }
    }

    public class Pipeline : BasePipeline
    {
        private static long count = 0;

        public override void Process(params ResultItems[] resultItems)
        {
            foreach(var result in resultItems)
            {
                foreach(var element in result.Results["codes"])
                {
                    RvuniqueCrawler.names.Add(element);
                }
            }
            var names = RvuniqueCrawler.names;
            //RvuniqueCrawler.names.AddRange(something.);
            //foreach (var resultItem in resultItems)
            //{
            //    //StringBuilder builder = new StringBuilder();
            //    //var something = resultItem.Results["VideoResult"];
            //    //foreach (YoukuVideo entry in resultItem.Results["VideoResult"])
            //    //{
            //    //    count++;
            //    //    builder.Append($" [YoukuVideo {count}] {entry.Name}");
            //    //}
            //    //Console.WriteLine(builder);
            //}
        }
    }


}
