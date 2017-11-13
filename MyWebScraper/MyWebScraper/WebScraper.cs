using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Xml;
using System.ServiceModel.Syndication;

namespace MyWebScraper
{
    public class WebScraper
    {
        private string UserAgent;
        private string URL;
        public string html { get; set; }
        public string Filename { get; set; } // used for html file names
        public string CssFilePath { get; set; }
        public string JsFilePath { get; set; }
        public string ImgFilePath { get; set; }
        public string HtmlFilePath { get; set; }
        public string errorLog { get; set; }
        public string script { get; set; }
        public string style { get; set; }
        byte[] fileBytes;

        public HashSet<StyleItem> Styles;
        public HashSet<ImageItem> Images;
        public HashSet<LinkItem> Links;
        public HashSet<ScriptItem> Scripts;


        public WebScraper()
        {
            UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
        }

        public WebScraper(string userAgent)
        {
            UserAgent = userAgent;
        }

        public WebScraper(string url, string userAgent)
        {
            URL = url;
            Uri u = new Uri(URL);
            Filename = Path.GetFileName(u.AbsolutePath);
            UserAgent = userAgent;
        }

        public byte[] GetImage(string url)
        {
            URL = url;
            using (var client = new MyWebClient())
            {
                if (string.IsNullOrEmpty(UserAgent))
                {
                    client.Headers[HttpRequestHeader.UserAgent] =
                        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                }
                else
                {
                    client.Headers[HttpRequestHeader.UserAgent] = UserAgent;
                }

                fileBytes = client.DownloadData(url);
                return fileBytes;
            }
        }

        public string GetScript(string url)
        {
            URL = url;
            using (var client = new MyWebClient())
            {
                if (string.IsNullOrEmpty(UserAgent))
                {
                    client.Headers[HttpRequestHeader.UserAgent] =
                        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                }
                else
                {
                    client.Headers[HttpRequestHeader.UserAgent] = UserAgent;
                }

                client.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
                try
                {
                    script = client.DownloadString(url);
                    return script;
                }
                catch (WebException ex)
                {
                    return string.Empty;
                }
            }
        }

        public string GetStyle(string url)
        {
            URL = url;
            style = GetScript(url);
            return style;
        }


        public HashSet<LinkItem> GetLinks(string url)
        {
            URL = url;

            if (string.IsNullOrEmpty(Filename))
            {
                Uri u = new Uri(URL);
                Filename = Path.GetFileName(u.AbsolutePath);
            }

            using (var client = new MyWebClient())
            {
                if (string.IsNullOrEmpty(UserAgent))
                {
                    client.Headers[HttpRequestHeader.UserAgent] =
                        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                }
                else
                {
                    client.Headers[HttpRequestHeader.UserAgent] = UserAgent;
                }

                client.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
                html = client.DownloadString(url);

                Links = LinkFinder.Find(html, url);
            }

            return Links;
        }

        public HashSet<ImageItem> GetImages(string url)
        {
            URL = url;

            if (string.IsNullOrEmpty(Filename))
            {
                Uri u = new Uri(URL);
                Filename = Path.GetFileName(u.AbsolutePath);
            }

            using (var client = new MyWebClient())
            {
                if (string.IsNullOrEmpty(UserAgent))
                {
                    client.Headers[HttpRequestHeader.UserAgent] =
                        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                }
                else
                {
                    client.Headers[HttpRequestHeader.UserAgent] = UserAgent;
                }

                client.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
                html = client.DownloadString(url);

                Images = ImageFinder.Find(html, url);
            }

            return Images;
        }

        public HashSet<StyleItem> GetStyles(string url)
        {
            URL = url;

            if (string.IsNullOrEmpty(Filename))
            {
                Uri u = new Uri(URL);
                Filename = Path.GetFileName(u.AbsolutePath);
            }

            using (var client = new MyWebClient())
            {
                if (string.IsNullOrEmpty(UserAgent))
                {
                    client.Headers[HttpRequestHeader.UserAgent] =
                        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                }
                else
                {
                    client.Headers[HttpRequestHeader.UserAgent] = UserAgent;
                }

                client.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
                html = client.DownloadString(url);

                Styles = StyleFinder.Find(html, url);
            }

            return Styles;
        }

        public HashSet<ScriptItem> GetScripts(string url)
        {
            URL = url;

            if (string.IsNullOrEmpty(Filename))
            {
                Uri u = new Uri(URL);
                Filename = Path.GetFileName(u.AbsolutePath);
            }

            using (var client = new MyWebClient())
            {
                if (string.IsNullOrEmpty(UserAgent))
                {
                    client.Headers[HttpRequestHeader.UserAgent] =
                        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                }
                else
                {
                    client.Headers[HttpRequestHeader.UserAgent] = UserAgent;
                }

                client.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
                html = client.DownloadString(url);

                Scripts = ScriptFinder.Find(html, url);
            }

            return Scripts;
        }

        public WebScraper GetUrl(string url)
        {
            URL = url;

            if (string.IsNullOrEmpty(Filename))
            {
                Uri u = new Uri(URL);
                Filename = Path.GetFileName(u.AbsolutePath);
            }

            using (var client = new MyWebClient())
            {
                if (string.IsNullOrEmpty(UserAgent))
                {
                    client.Headers[HttpRequestHeader.UserAgent] =
                        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                }
                else
                {
                    client.Headers[HttpRequestHeader.UserAgent] = UserAgent;
                }

                client.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
                try
                {
                    html = client.DownloadString(url);
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error: " + ex.Message);
                }

                Links = LinkFinder.Find(html, url);
                Images = ImageFinder.Find(html, url);
                Styles = StyleFinder.Find(html, url);
                Scripts = ScriptFinder.Find(html, url);

                return this;
            }
        }

        public SyndicationFeed getRssFeed(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            return feed;
        }

        //used to save all the files that go along with the html file...
        public bool SaveUrl()
        {    
            try
            {
                //SAVE IMAGES FIX PATHS...
                foreach (ImageItem i in Images)
                {
                    //Console.WriteLine("'" + i + "'");
                    byte[] imgBytes = GetImage(i.Src);
                    try
                    {
                        SaveImage(i.Filename);
                    }
                    catch (Exception e)
                    {
                        errorLog += "Error saving image FileName: " + i.Filename + e.Message;
                    }
                    if (html.Contains(i.Src))
                    {
                        html = html.Replace(i.Src, "images/" + i.Filename);
                    }
                }

            }
            catch (Exception e)
            {
                //throw new Exception("Url: " + URL + e.Message);
                errorLog += "Url: " + URL + e.Message;
            }

            try
            {
                //SAVE JavaScripts
                foreach (ScriptItem i in Scripts)
                {
                    string js = GetScript(i.Src);
                    try
                    {
                        SaveJsScript(i.Filename);
                    }
                    catch (Exception e)
                    {
                        errorLog += "Error saving javascript FileName: " + i.Filename + e.Message;
                    }


                    if (html.Contains(i.Src))
                    {
                        html = html.Replace(i.Src, "js/" + i.Filename + i.Query);
                    }
                    else
                    {
                        if (i.SSlSafePath != i.Src)
                        {
                            if (html.Contains(i.SSlSafePath))
                            {
                                html = html.Replace(i.SSlSafePath, "js/" + i.Filename + i.Query);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //throw new Exception("Url: " + URL + e.Message);
                errorLog += "Url: " + URL + e.Message;
            }

            try
            {
                //SAVE CSS Files
                foreach (StyleItem i in Styles)
                {
                    //Console.WriteLine("'"+i+"'");
                    string css = GetStyle(i.Href);
                    try
                    {
                        SaveCss(i.Filename);
                    }
                    catch (Exception e)
                    {
                        errorLog += "Error saving css FileName: " + i.Filename + e.Message;
                    }

                    if (html.Contains(i.Href))
                    {
                        html = html.Replace(i.Href, "css/" + i.Filename + i.Query);
                    }
                    else
                    {
                        if (i.SSlSafePath != i.Href)
                        {
                            if (html.Contains(i.SSlSafePath))
                            {
                                html = html.Replace(i.SSlSafePath, "css/" + i.Filename + i.Query);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //throw new Exception("Url: " + URL + e.Message);
                errorLog += "Url: " + URL + e.Message;
            }

            return SaveHtml(Filename);
        }


        public bool SaveHtml(string filename)
        {

            if (string.IsNullOrEmpty(HtmlFilePath))
            {
                throw new Exception("The property HtmlFilePath cannot be empty!");
            }

            if (string.IsNullOrEmpty(filename))
            {
                filename = "index.html";
            }

            try
            {
                System.IO.Directory.CreateDirectory(HtmlFilePath);
                File.WriteAllText(HtmlFilePath + "\\" + filename, html);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }

        public bool SaveJsScript(string filename)
        {
            if (script == string.Empty)
                return false;

            if (string.IsNullOrEmpty(JsFilePath))
            {
                if (!string.IsNullOrEmpty(HtmlFilePath))
                {
                    JsFilePath = HtmlFilePath + "\\js\\";
                }
                else
                {
                    throw new Exception("The property JsFilePath and HtmlFilePath cannot be empty!");
                }
            }
            try
            {
                System.IO.Directory.CreateDirectory(JsFilePath);
                File.WriteAllText(JsFilePath + filename, script);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }

        public bool SaveCss(string filename)
        {
            if (style == string.Empty)
                return false;

            if (string.IsNullOrEmpty(CssFilePath))
            {
                if (!string.IsNullOrEmpty(HtmlFilePath))
                {
                    CssFilePath = HtmlFilePath + "\\css\\";
                }
                else
                {
                    throw new Exception("The property CssFilePath and HtmlFilePath cannot be empty!");
                }
            }

            try
            {
                System.IO.Directory.CreateDirectory(CssFilePath);
                File.WriteAllText(CssFilePath + filename, style);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }

        public bool SaveImage(string filename)
        {
            if (style == string.Empty)
                return false;

            if (string.IsNullOrEmpty(ImgFilePath))
            {
                if (!string.IsNullOrEmpty(HtmlFilePath))
                {
                    ImgFilePath = HtmlFilePath + "\\images\\";
                }
                else
                {
                    throw new Exception("The property ImgFilePath and HtmlFilePath cannot be empty!");
                }
            }
            try
            {
                System.IO.Directory.CreateDirectory(ImgFilePath);
                File.WriteAllBytes(ImgFilePath + filename, fileBytes);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }

    }
}
