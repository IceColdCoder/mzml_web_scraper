using NUnit.Framework;
using mzml_web_scraper;
using System.IO;
using System;
using System.Linq;
using NUnit.Framework.Internal;

namespace mzml_web_scraper_nunit_tests
{
    [TestFixture]
    public class Log_Test
    {
        string target_path = Directory.GetCurrentDirectory() + "\\logs\\http_logs";
        string target_name = "\\HTTPLog.txt";
        static readonly string test_txt = "NUnit write log test.";

        [SetUp]
        public void Setup()
        {
            Log.Add_Logger(Log.Log_Type.File_Logger);
        }

        [TearDown]
        public void Dispose()
        {
            Log.ClearAllLoggers();
            GC.Collect();
            System.Threading.Thread.Sleep(100);
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
            this.Dispose();

            string[] lines = File.ReadAllLines(target_path + target_name);

            if (lines.Length == 0)
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