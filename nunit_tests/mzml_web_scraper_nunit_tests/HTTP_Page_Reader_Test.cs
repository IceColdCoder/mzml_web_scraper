using NUnit.Framework;
using mzml_web_scraper;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework.Internal;
using System.Net.Http;
using System.IO;
using Microsoft.VisualBasic;
using System.Threading.Tasks;
using System.Net;

namespace mzml_web_scraper_nunit_tests
{
    [TestFixture]
    class HTTP_Page_Reader_Test
    {
        private static readonly string url = "http://www.google.com/";
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
            var GET_Task = mzml_web_scraper.HTTP_Page_Reader.HTTP_GET_String(url, TimeSpan.FromMilliseconds(1000));
            Assert.DoesNotThrow( delegate{ string page = GET_Task.Result;} );
        }
    }
}
