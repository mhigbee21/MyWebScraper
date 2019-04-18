using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyWebScraper
{
    public struct MetaTagItem
    {
        public string Name;
        public string Value;

        public override string ToString()
        {
            return Name + ":" + Value;
        }
    }

    static class MetaTagFinder
    {
        public static HashSet<MetaTagItem> Find(string file, string url)
        {
            HashSet<MetaTagItem> list = new HashSet<MetaTagItem>();

            // 1.
            // Find all matches in file.
            MatchCollection m1 = Regex.Matches(file, @"(<meta.*?>)",
                RegexOptions.Singleline | RegexOptions.IgnoreCase);

            // 2.
            // Loop over each match.
            foreach (Match m in m1)
            {
                string value = m.Groups[1].Value;
                MetaTagItem i = new MetaTagItem();

                // 3.
                // Get name attribute.
                Match m2 = Regex.Match(value, "name=[\"'](.+?)[\"']", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                if (m2.Success)
                {
                    string val = m2.Groups[1].Value;
                    i.Name = val;

                    Match m3 = Regex.Match(value, "content=[\"'](.+?)[\"']", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    if (m3.Success)
                    {
                        string content = m3.Groups[1].Value;
                        i.Value = content;
                    }
                    list.Add(i);
                    continue;
                }

                // 4.
                // Get name property.
                Match m4 = Regex.Match(value, "property=[\"'](.+?)[\"']", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                if (m4.Success)
                {
                    string val = m4.Groups[1].Value;
                    i.Name = val;

                    Match m5 = Regex.Match(value, "content=[\"'](.+?)[\"']", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    if (m5.Success)
                    {
                        string content = m5.Groups[1].Value;
                        i.Value = content;
                    }

                    list.Add(i);
                }

            }
            return list;
        }

    }
}
