using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace MyWebScraper
{
    public struct ScriptItem
    {
        public string Src;
        public string Filename { get; set; }
        public string PathAndQuery { get; set; }
        public string Query { get; set; }
        public string SSlSafePath { get; set; }

        public override string ToString()
        {
            return Src;
        }
    }
    static class ScriptFinder
    {
        public static HashSet<ScriptItem> Find(string file, string url)
        {
            HashSet<ScriptItem> list = new HashSet<ScriptItem>();

            // 1.
            // Find all matches in file.
            MatchCollection m1 = Regex.Matches(file, @"(<script.*?>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            // 2.
            // Loop over each match.
            foreach (Match m in m1)
            {
                string value = m.Groups[1].Value;
                ScriptItem i = new ScriptItem();

                // 3.
                // Get href attribute.
                Match m2 = Regex.Match(value, "src=[\"'](.+?)[\"']", RegexOptions.Singleline | RegexOptions.IgnoreCase);
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
                    i.PathAndQuery = u.PathAndQuery;
                    i.Query = u.Query;

                    Match m4 = Regex.Match(i.Filename, @"\.js$", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    if (m4.Success)
                    {
                        i.Src = val;
                        list.Add(i);
                    }
                }

            }
            return list;
        }
    }
}
