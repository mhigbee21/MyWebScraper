using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace MyWebScraper
{
    public struct StyleItem
    {
        public string Href;
        public string Filename { get; set; }
        public string Query { get; set; }
        public string SSlSafePath { get; set; }

        public override string ToString()
        {
            return Href;
        }
    }
    static class StyleFinder
    {
       
        public static HashSet<StyleItem> Find(string file, string url)
        {
            HashSet<StyleItem> list = new HashSet<StyleItem>();

            // 1.
            // Find all matches in file.
            MatchCollection m1 = Regex.Matches(file, @"(<link.*?>)",
                RegexOptions.Singleline);

            // 2.
            // Loop over each match.
            foreach (Match m in m1)
            {
                string value = m.Groups[1].Value;
                StyleItem i = new StyleItem();

                // 3.
                // Get href attribute.
                Match m2 = Regex.Match(value, "href=[\"'](.+?)[\"']", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                if (m2.Success)
                {
                    string val = m2.Groups[1].Value;
                    i.SSlSafePath = val;

                    Match m5 = Regex.Match(val, @"^\/\/", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    if (m5.Success)
                    {
                        i.SSlSafePath = val;
                        val = Regex.Replace(val, @"^\/\/", "http://", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    }

                    Match m3 = Regex.Match(val, @"^http", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    if (!m3.Success)
                    {
                        val = Regex.Replace(val, @"^\/", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        url = Regex.Replace(url, @"\/$", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        val = url + "/" + val;
                    }

                    Uri u = new Uri(val);
                    i.Filename = Path.GetFileName(u.AbsolutePath);

                    Match m4 = Regex.Match(i.Filename, @"\.css$", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    if (m4.Success)
                    {
                        i.Href = val;
                        list.Add(i);
                    }
                }
            }
            return list;
        }
    }
}
