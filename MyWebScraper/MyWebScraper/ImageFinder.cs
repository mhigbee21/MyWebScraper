using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace MyWebScraper
{
    public struct ImageItem
    {
        public string Src;
        public string Filename { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return Src;
        }
    }
    static class ImageFinder
    {
        public static HashSet<ImageItem> Find(string file, string url)
        {
            HashSet<ImageItem> list = new HashSet<ImageItem>();

            // 1.
            // Find all matches in file.
            MatchCollection m1 = Regex.Matches(file, @"(<img.*?>)",
                RegexOptions.Singleline | RegexOptions.IgnoreCase);

            // 2.
            // Loop over each match.
            foreach (Match m in m1)
            {
                string value = m.Groups[1].Value;
                ImageItem i = new ImageItem();

                // 3.
                // Get src attribute.
                Match m2 = Regex.Match(value, @"src=\""(.*?)\""",
                RegexOptions.Singleline | RegexOptions.IgnoreCase);
                if (m2.Success)
                {
                    string val = m2.Groups[1].Value;
                    Match m3 = Regex.Match(val, @"^http", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    if (!m3.Success)
                    {
                        val = Regex.Replace(val, @"^\/", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        url = Regex.Replace(url, @"\/$", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        val = url + "/" + val;
                    }

                    Uri u = new Uri(val);
                    i.Filename = Path.GetFileName(u.AbsolutePath);
                    i.Path = u.LocalPath;

                    i.Src = val;
                    list.Add(i);
                }

            }
            return list;
        }
    }
}
