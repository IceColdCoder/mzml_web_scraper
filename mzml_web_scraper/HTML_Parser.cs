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
                Queue<char> open_q = new Queue<char>();
                Queue<char> close_q = new Queue<char>();
                string html_pattern = @"<.*?\s??.*?>";
                Regex html_regex = new Regex(html_pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                while ((line = file.ReadLine()) != null)
                {

                    if(line.Length > 1)
                    {
                        Match match = html_regex.Match(line);
                        Debug.WriteLine(match.Value + " at " + match.Index + " with length " + match.Length);
                    }


                    //if (!line.Equals(""))
                    //{
                    //    //General case.
                    //    if (line[1] != '!')
                    //    {
                    //        int i = 1;
                    //        StringBuilder open_statement = new StringBuilder();
                    //        StringBuilder close_statement = new StringBuilder();
                    //        open_statement.Append("<");
                    //        close_statement.Append("</");
           

                    //        while (line[i] != '>' && i < line.Length)
                    //        {
                    //            open_statement.Append(line[i]);
                    //            close_statement.Append(line[i]);
                    //            i++;
                    //        }

                    //        open_statement.Append(">");
                    //        close_statement.Append(">");

                    //        string open_string = open_statement.ToString();
                    //        string close_string = close_statement.ToString();

                    //        html_spec.Add(open_string, close_string);
                    //        html_spec.Add(close_string, open_string);
                    //    }

                    //    //Edge cases.
                    //    else
                    //    {
                    //        int i = 1;
                    //        StringBuilder open_statement = new StringBuilder();
                    //        StringBuilder close_statement = new StringBuilder();
                    //        open_statement.Append("<");
                    //        close_statement.Append("</");
                   
                    //        //For <!Doctype html>
                    //        if(Char.IsLetterOrDigit(line[i + 1]))
                    //        {
                    //            StringBuilder open_statement = new StringBuilder();
                    //            open_statement.Append("<!");
                    //        }
                    //        //For <!-- -->
                    //        else
                    //        {

                    //        }

                    //    }
                    //}

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
