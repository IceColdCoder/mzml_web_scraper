using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace mzml_web_scraper
{
    class HTTP_Log
    {

        private StreamWriter writer = null;

        public HTTP_Log()
        {
            string target_path = Directory.GetCurrentDirectory()  + "..\\http_logs\\";
            string target_name = $"{DateTime.Now.ToLongDateString()}_{DateTime.Now.ToLongTimeString()}_httplog.txt";
            
            if (!Directory.Exists(target_path))
            {
                Directory.CreateDirectory(target_path);
            }

            writer = File.AppendText(target_path + target_name);
        }

        public async void Log(string txt)
        {
            UnicodeEncoding uniencoding = new UnicodeEncoding();
            await this.writer.WriteLineAsync(txt);
        }
    }
}
