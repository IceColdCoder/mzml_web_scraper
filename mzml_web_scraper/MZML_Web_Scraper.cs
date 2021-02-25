using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System.IO;

namespace mzml_web_scraper
{
    class MZML_Web_Scraper
    {
        
        static void Main(string[] args)
        {
            Debug.WriteLine(Directory.GetCurrentDirectory());

            MZML_Web_Scraper scraper = new MZML_Web_Scraper();
        }

        public MZML_Web_Scraper()
        {
            Log.Add_Logger(Log.Log_Type.File_Logger);
            Log.WriteTime("Program Start");

            

            string page = HTTP_Page_Reader.HTTP_GET_String("https://www.google.com/", TimeSpan.FromSeconds(15)).Result;
            string f_path = Environment.CurrentDirectory + "_DefaultDebug.html";
            Debug.WriteLine("Writing HTML page to: " + f_path);
            File.WriteAllText(f_path, page);
/*            HTML_Parser.Parse_HTML(page);*/

        }

    }
}
