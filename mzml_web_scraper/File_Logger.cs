using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace mzml_web_scraper
{
    /// <summary>
    /// Simple thread safe file logger intended for internal debugging only. Instantiated using a lazy singleton approach.
    /// </summary>
    public sealed class File_Logger : Logger
    {

        private StreamWriter writer = null;
        private bool disposedValue;

        public File_Logger()
        {

            string target_path = Directory.GetCurrentDirectory() + "\\logs\\http_logs";
            string target_name = "\\HTTPLog.txt";
            if (!Directory.Exists(target_path))
            {
                Directory.CreateDirectory(target_path);
                //System.Diagnostics.Debug.WriteLine("Path Exists!");
            }

            if (File.Exists(target_path + target_name))
            {
                File.Delete(target_path + target_name);
            }

            writer = new StreamWriter(new FileStream(target_path + target_name, FileMode.Create));
        }

        /// <summary>
        /// Thread safe logger. Can have a high performance impact due to requiring a lock on the file stream directly.
        /// Intended for debugging only.
        /// </summary>
        /// <param name="txt"></param>
        public async void Log(string txt)
        {
            await writer.WriteLineAsync(txt);
            await writer.FlushAsync();
        }

        /// <summary>
        /// Force flush the log writer.
        /// </summary>
        public void Flush()
        {
            writer.Flush();
        }

        public void Dispose()
        {
            int disposed = 0;
            while(disposed < 5)
            {
                try
                {
                    writer.Dispose();
                    disposed = 5;
                }
                catch ( Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                finally
                {
                    disposed++;
                }
            }
            
        }
    }
}
