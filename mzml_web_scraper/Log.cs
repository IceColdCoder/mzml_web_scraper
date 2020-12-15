using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mzml_web_scraper
{

    /// <summary>
    /// Main logging interface. Can apply multiple logging methods at once depending on instantiation.
    /// </summary>
    public static class Log
    {
        public enum Log_Type
        {
            File_Logger,
        }

        private static List<Logger> logger_list = new List<Logger>();

        static Log(){}

        public static void ClearAllLoggers()
        {
            foreach (Logger logger in logger_list)
            {
                logger.Dispose();
            }
            logger_list.Clear();
        }


        /// <summary>
        /// Add a new logging system. Logging systems can be used concurrently.
        /// </summary>
        /// <param name="logger"></param>
        public static void Add_Logger(Log_Type log_type)
        {

            switch (log_type)
            {
                case Log_Type.File_Logger:
                    logger_list.Add(new File_Logger());
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Return the Class Type object from the log_type enum.
        /// </summary>
        /// <param name="log_type"></param>
        /// <returns></returns>
        private static Type Get_Type(Log_Type log_type)
        {
            switch (log_type)
            {
                case Log_Type.File_Logger:
                    return typeof(File_Logger);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Write log message to all log systems. Intended to be overloaded at a later date.
        /// </summary>
        /// <param name="txt"></param>
        public static void Write(string txt)
        {
            foreach (Logger logger in logger_list)
            {
                logger.Log(txt);
            }
        }

        /// <summary>
        /// Write Log message with date & time wrapper using C# standard date and time prefixed to log message.
        /// </summary>
        /// <param name="txt"></param>
        public static void WriteTime(string txt)
        {
            string time = DateTime.Now.ToString();
            Log.Write(time + " -> " + txt);
        }

        /// <summary>
        /// Force flush all log systems.
        /// </summary>
        public static void FlushAll()
        {
            foreach (Logger logger in logger_list)
            {
                logger.Flush();
            }
        }

        /// <summary>
        /// Force flush all loggers of a specific type.
        /// </summary>
        /// <param name="log_type"></param>
        public static void Flush(Log_Type log_type)
        {
            foreach (Logger logger in logger_list)
            {
                if(logger.GetType().Equals(Get_Type(log_type)))
                {
                    logger.Flush();
                }
            }
        }
    }
}
