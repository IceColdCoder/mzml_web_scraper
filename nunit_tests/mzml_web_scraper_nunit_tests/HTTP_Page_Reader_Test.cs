using NUnit.Framework;
using mzml_web_scraper;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework.Internal;
using System.Net.Http;
using System.IO;
using Microsoft.VisualBasic;

namespace mzml_web_scraper_nunit_tests
{
    class HTTP_Page_Reader_Test
    {
        [SetUp]
        public void setup()
        {

        }
        [TearDown]
        public void dispose()
        {

        }
        [Test]
        public void Test1()
        {
            string directory = Directory.GetCurrentDirectory().Replace('\\', '/');
            string url = "file:///" + directory + "/test_resources/HTML5 Test Page.html";
            string page = null;

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, url);
            var GET_Task = mzml_web_scraper.HTTP_Page_Reader.HTTP_GET_String(msg);

            try { 
                if (GET_Task.Wait(1000))
                {
                    page = GET_Task.Result;
                }
                else
                {
                    Assert.Fail("HTTP_Page_Readed:Test1 Failed. Page request timed out.");
                }
            } catch (HttpRequestException e)
            {
                Assert.Fail("")
            }
        }
    }
}
