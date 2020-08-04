using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Debug.
using System.Diagnostics;

namespace mzml_web_scraper
{
    public static class HTML_Parser
    {

        static readonly string specfile = "html_spec.txt";
        static HTML_Parser(){
            Read_HTML_Spec(specfile);
        }

        private static void Read_HTML_Spec(string specfile)
        {
            foreach (string line in File.ReadAllLines(specfile))
            {
                if (!line.Equals(""))
                {
                    string[] line_parts = line.Split(' ');
                    Debug.WriteLine(line_parts[0]);
                }
            }
        }

        public static void Parse_HTML(string html)
        {

        }
    }

}
