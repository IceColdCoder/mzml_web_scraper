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

                string html_pattern = @"<.*?\s??.*?>";
                Regex html_regex = new Regex(html_pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

                string tag_pattern = @"<.*?>";
                Regex tag_regex = new Regex(tag_pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

                while ((line = file.ReadLine()) != null)
                {

                    if (line.Length > 1)
                    {
                        Match match = html_regex.Match(line);

                        //Case for <...> </...> IE normal Tags.
                        if (tag_regex.IsMatch(match.Value))
                        {
                            string html_opener = match.Value;
                            string html_closer = match.Value.Insert(1, "/");

                            html_spec.Add(html_opener, html_closer);
                            html_spec.Add(html_closer, html_opener);
                        }

                        //Case for <! ... >
                        else
                        {

                        }



                    }

                }


            }

            foreach(DictionaryEntry tmp in html_spec)
            {
                Debug.WriteLine(tmp.Key + ", " + tmp.Value);
            }
        }

        public static void Parse_HTML(string html)
        {

        }
    }

}
