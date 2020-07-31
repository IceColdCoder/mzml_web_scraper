using NUnit.Framework;
using mzml_web_scraper;
using System.IO;
using System;
using System.Linq;

namespace mzml_web_scraper_nunit_tests
{
    public class Log_Test
    {
        static string target_path = Directory.GetCurrentDirectory() + "..\\http_logs\\";
        static string target_name = "\\HTTPLog.txt";
        static readonly string test_txt = "NUnit write log test.";

        [SetUp]
        public void Setup()
        {
            Log.Add_Logger(Log.Log_Type.File_Logger);
        }

        [Test]
        public void Test1()
        {

            if (Directory.Exists(target_path))
            {
                Assert.Pass("Log_Test:Test1: Successfully created File_Logger directory.");
            }
            else
            {
                Assert.Fail("Log_Test:Test1: Failed to create File_Logger directory.");
            }
                
        }

        [Test]
        public void Test2()
        {
            Log.Write(test_txt);
            Log.CloseAll();

            string[] lines = null;
            for (int i = 0; i < 5; i++)
            {
                try{lines = File.ReadAllLines(target_path + target_name);}
                catch { System.Threading.Thread.Sleep(100 + 100*i); }
            }
            

            if(lines.Length == 0)
            {
                Assert.Fail("Log_Test:Test2: File had no text.");
            }

            if (lines[lines.Length - 1].SequenceEqual(test_txt))
            {
                Assert.Pass("Log_Test:Test2: Successfully read correct test text.");
            }

            Assert.Fail("Log_Test:Test2: Failed on fallthrough.");
        }
    }
}