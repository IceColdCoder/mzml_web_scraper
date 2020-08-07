using NUnit.Framework;
using mzml_web_scraper;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework.Internal;


namespace mzml_web_scraper_nunit_tests
{

    public class HTML_Parser_Test
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
            HTML_Parser.Parse_HTML("");
        }
    }

}
