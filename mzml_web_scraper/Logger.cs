using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace mzml_web_scraper
{
    interface Logger
    {
        /// <summary>
        /// Method where logs are to be written. Explicit because of queuing and database logging systems I intend for the future.
        /// </summary>
        /// <param name="txt"></param>
        void Log(string txt);

        /// <summary>
        /// Force flush the logger.
        /// </summary>
        void Flush();

        /// <summary>
        /// Force close the logger.
        /// </summary>
        void Close();
    }
}
