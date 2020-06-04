using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace mzml_web_scraper
{
    class MZML_Web_Scraper
    {
        public static readonly HttpClient web_client = new HttpClient();
        static void Main(string[] args)
        {

        }

        public MZML_Web_Scraper()
        {

        }

        static async Task Scrape_URL(string url)
        {
            try
            {
                HttpResponseMessage response = await web_client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

        }
    }
}
