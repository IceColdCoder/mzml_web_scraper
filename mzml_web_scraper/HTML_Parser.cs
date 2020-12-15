using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Debug.
using System.Diagnostics;
using System.Collections.Specialized;
using System.Reflection.Emit;
using System.Collections;
using System.Text.RegularExpressions;

namespace mzml_web_scraper
{
    public static class HTML_Parser
    {

        static readonly string specfile = Directory.GetCurrentDirectory() + "\\specs\\html_spec.txt";
        static OrderedDictionary html_spec;
        static HTML_Parser()
        {
            html_spec = new OrderedDictionary();
            Read_HTML_Spec(specfile);
        }

        private static void Read_HTML_Spec(string specfile)
        {
            using (StreamReader file = new StreamReader(specfile))
            {
                string line = "";
                string html_pattern = @"^(<(?'Tag'([^<>]*))>)?";

                Regex html_regex = new Regex(html_pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

                while ((line = file.ReadLine()) != null)
                {

                    if (line.Length > 1)
                    {
                        Match match = html_regex.Match(line);
                        html_spec.Add(match.Groups[match.Groups.Count - 1].Value, line);
                    }
                }
            }
        }

        public static string HTML_Spec_ToString()
        {
            StringBuilder sb = new StringBuilder("[");
            IDictionaryEnumerator _e = html_spec.GetEnumerator();
            while(_e.MoveNext())
            {
                sb.Append("(" + _e.Key + "," + _e.Value + "),\n");
            }
            sb[sb.Length - 3] = ']';
            return sb.ToString();
        }

        public static void Parse_HTML(string html)
        {
            string reader_pattern = @"^(<(?'Tag'([^<>]*))>)?";

            Regex reader = new Regex(reader_pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            MatchCollection matches = reader.Matches(html);

            foreach (Match m in matches)
            {
                foreach (Capture capture in m.Captures)
                {
                    Debug.WriteLine(capture.Index, capture.Value);
                }
            }
        }
    }

}
