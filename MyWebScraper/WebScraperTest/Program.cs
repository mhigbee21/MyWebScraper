using System;
using System.Net;
using MyWebScraper;
using System.ServiceModel.Syndication;

namespace WebScraperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            WebScraper w = new WebScraper();
            
            //w.CssFilePath = "C:\\prodigy5\\css\\";
            //w.ImgFilePath = "C:\\prodigy5\\images\\";
            //w.JsFilePath = "C:\\prodigy5\\js\\";
            w.HtmlFilePath = "C:\\friendlydb\\";

            Console.WriteLine("Getting url...");
            var result = w.GetUrl("http://www.friendlydb.com");
            string html = result.html;

            Console.WriteLine("Saving url...");
            try
            {
                bool saved = w.SaveUrl();
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an error: " + e.Message);
            }

            /*SyndicationFeed feed  = w.getRssFeed("http://rss.cnn.com/rss/cnn_topstories.rss");

            foreach (SyndicationItem item in feed.Items)
            {
                String subject = item.Title.Text;
                String summary = item.Summary.Text;
                Console.WriteLine("title:" + subject);
                Console.WriteLine("Summary:" + summary);
                Console.WriteLine("-----------------------------------------------------------------------");
            }
            */

            Console.WriteLine("Done!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
           
        }
    }
}
