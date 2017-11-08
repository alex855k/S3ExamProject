using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abot.Crawler;
using Abot.Poco;
using System.Net;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace TryAndDie.Model.WebCrawlers
{
    public class RvuniqueCrawler : TheWebCrawler
    {
        public IWebCrawler crawler;
        public List<HtmlDocument> agilityDocument;
        public HtmlDocument agilityPage;
        public RvuniqueCrawler()
        {
            crawler = GetManuallyConfiguredWebCrawler();
            //crawler = new PoliteWebCrawler();
            base.SubscriveEvents(crawler);
            agilityDocument = new List<HtmlDocument>();
            agilityPage = new HtmlDocument();
        }

        public List<ItemModel> Run()
        {
            List<string> someresult = new List<string>();
            List<string> productResult = new List<string>();
            List<ItemModel> items = new List<ItemModel>();
            List<decimal> prices = new List<decimal>();
            /*log4net.Config.XmlConfigurator.Configure();*///for the log file
            //SuperCrawler sw = new SuperCrawler();
            //Uri uriToCrawl = sw.GetUri();
            var uriList = GetUriCollection();
            foreach (var uri in uriList)
            {
                var domain = uri.Host;
                RvuniqueCrawler rw = new RvuniqueCrawler();
                CrawlResult result = rw.crawler.Crawl(uri);
                var agilityDoc = rw.agilityDocument;

                foreach (var page in agilityDoc)
                {
                    string category = "";
                    string price = "";
                    //string code = "";
                    //string name = "";
                    //decimal price = 0.0M;
                    var categoryNode = page.DocumentNode
                        .SelectSingleNode("//header[@class = 'ghead nopro clr']/h1");
                    if (categoryNode != null)
                    {
                        category = categoryNode.InnerText.Trim();
                    }
                    var tb = page.DocumentNode
                        .SelectNodes("//div[@class = 'products']//tbody")
                        .First();
                    var trNodes = tb.SelectNodes(".//tr");

                    //for (int i = 0; i < trNodes.Count; i++)
                    foreach (var tr in trNodes)
                    {
                        //var code = trNodes[i].SelectNodes("//span[@class='art']")[i].InnerText;
                        var code = tr
                            .SelectNodes(".//span[@class='art']")
                            .First()
                            .InnerText
                            .Trim();

                        var name = tr
                            .SelectNodes(".//td[@class = 'al name']//a")
                            .First()
                            .InnerText
                            .Trim();


                        if (tr.SelectNodes(".//span[@class = 'price-primary']") == null)
                        {
                            price = "Ring for pris";
                        }
                        else
                        {
                            price = tr
                                .SelectNodes(".//span[@class = 'price-primary']")
                                .First()
                                .InnerText
                                .Replace("ekskl. moms", "")
                                .Replace(",", ".")
                                .Trim();
                        }

                        var route = tr
                            .SelectNodes(".//td[@class = 'al name']//a[@href]")
                            .First()
                            .GetAttributeValue("href", string.Empty);
                        var bla = domain + route;
                        var link = new Uri("http://" + domain + route);

                        items.Add(new ItemModel
                        {
                            Category = category,
                            Code = code,
                            Name = name,
                            Price = price,
                            Link = link

                        });
                    }
                }

            }
            return items;
        }
        public override List<Uri> GetUriCollection()
        {
            return new List<Uri>
            {
                new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/special_produkter.aspx"),
                new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/sanitetsprodukter.aspx"),
                new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/klar-til-brug.aspx"),
                new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/svanemaerkede_produkter.aspx"),
                new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/vinduesprodukter.aspx"),
                new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/universal_produkter.aspx"),
                new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/toejvask.aspx"),
                new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/polish.aspx"),
                new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/industriprodukter.aspx"),
                new Uri("http://www.rvunique.dk/webshop/rengoeringsmidler/gulvprodukter.aspx")

            };
        }
        public IWebCrawler GetWc()
        {
            return new PoliteWebCrawler();
        }

        public override IWebCrawler GetManuallyConfiguredWebCrawler()
        {
            //Create a config object manually
            CrawlConfiguration config = new CrawlConfiguration();
            config.CrawlTimeoutSeconds = 0;
            config.DownloadableContentTypes = "text/html, text/plain";
            config.IsExternalPageCrawlingEnabled = false;
            config.IsExternalPageLinksCrawlingEnabled = false;
            config.IsRespectRobotsDotTextEnabled = false;
            config.IsUriRecrawlingEnabled = false;
            config.MaxConcurrentThreads = 10;
            config.MaxPagesToCrawl = 1;
            config.MaxPagesToCrawlPerDomain = 1;
            config.MinCrawlDelayPerDomainMilliSeconds = 1000;


            //Add you own values without modifying Abot's source code.
            //These are accessible in CrawlContext.CrawlConfuration.ConfigurationException object throughout the crawl
            config.ConfigurationExtensions.Add("Somekey1", "SomeValue1");
            config.ConfigurationExtensions.Add("Somekey2", "SomeValue2");

            //Initialize the crawler with custom configuration created above.
            //This override the app.config file values
            //return new PoliteWebCrawler(config, null, null, null, null, null, null, null, null);
            return new PoliteWebCrawler(config);
    }

    //public void SubscriveEvents(IWebCrawler politewc)
    //{
    //    politewc.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
    //    politewc.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
    //    politewc.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
    //    politewc.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;
    //}


    public override void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;
            agilityDocument.Add(crawledPage.HtmlDocument); //Html Agility Pack parser
        }

        
    }
}
