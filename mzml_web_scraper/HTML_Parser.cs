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

    public class HTMLDom
    {

        public class HTML_HEAD
        {

        }

        public class HTML_BODY
        {

        }

    }
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
            HTMLDom Dom = new HTMLDom();

            Stack<char> S = new Stack<char>(html.ToCharArray());

            ArrayList clist = new ArrayList();
            while (S.Count() != 0)
            {

                //If the code finds what looks like an html statement it will iterate over it using a stack in order to
                //ensure that is is a regular statement.
                char c = S.Pop();
                if (c.Equals('<'))
                {
                    clist.Clear();
                    clist.Add(c);

                    while(S.Count != 0 && !c.Equals('>'))
                    {
                        c = S.Pop();
                        clist.Add(c);
                    }

                    //Now handle it using a regex expression.

                }
            }
        }
    }

}
