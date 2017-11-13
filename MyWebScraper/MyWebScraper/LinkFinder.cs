using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace MyWebScraper
{
    public struct LinkItem
    {
        public string Href;
        public string Text;
        public string Filename { get; set; }

        public override string ToString()
        {
            return Text + ":" + Href;
        }
    }
    static class LinkFinder
    {
        public static HashSet<LinkItem> Find(string file, string url)
        {
            HashSet<LinkItem> list = new HashSet<LinkItem>();

            // 1.
            // Find all matches in file.
            MatchCollection m1 = Regex.Matches(file, @"(<a.*?>.*?</a>)",
                RegexOptions.Singleline | RegexOptions.IgnoreCase);

            // 2.
            // Loop over each match.
            foreach (Match m in m1)
            {
                string value = m.Groups[1].Value;
                LinkItem i = new LinkItem();

                // 3.
                // Get href attribute.
                Match m2 = Regex.Match(value, @"href=\""(.*?)\""",
                RegexOptions.Singleline | RegexOptions.IgnoreCase);
                if (m2.Success)
                {
                    string val = m2.Groups[1].Value;

                    Match m4 = Regex.Match(val, "javascript:|#", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    if (m4.Success)
                    {
                        //ignore javascript and # links
                        //Console.WriteLine("-----------> ignoring Link: " +val);
                        //Console.ReadLine();
                        continue;
                    }


                    Match m3 = Regex.Match(val, @"^http", RegexOptions.Singleline);

                    if (!m3.Success)
                    {
                        val = Regex.Replace(val, @"^\/", String.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        url = Regex.Replace(url, @"\/$", String.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        val = url + "/" + val;

                    }

                    Uri u = new Uri(val);
                    i.Filename = Path.GetFileName(u.AbsolutePath);

                    i.Href = val;
                    // 4.
                    // Remove inner tags from text.
                    string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    i.Text = t;

                    list.Add(i);
                }

            }
            return list;
        }
    }
}
