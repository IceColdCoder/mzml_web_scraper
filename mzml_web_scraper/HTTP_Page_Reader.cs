using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System.Threading;
using System.Collections.Concurrent;

namespace mzml_web_scraper
{
    public static class HTTP_Page_Reader
    {

        public static ConcurrentQueue<string> outgoing_url;
        public static ConcurrentQueue<string> incomming_http;

        private static readonly HttpClient http_client = null;
        static HTTP_Page_Reader()
        {
            http_client = new HttpClient();
            outgoing_url = new ConcurrentQueue<string>();
            incomming_http = new ConcurrentQueue<string>();
        }

        public static async Task<string> HTTP_GET_String(HttpRequestMessage msg, TimeSpan timeout)
        {
            string page_data = null;

            try
            {
                var cts = new CancellationTokenSource(timeout);
                HttpResponseMessage response = await http_client.SendAsync(msg);
                response.EnsureSuccessStatusCode();
                page_data = await response.Content.ReadAsStringAsync();
            } catch (HttpRequestException e)
            {
                Log.Write(e.Source + " " + e.Message);
                throw e;
            }
            finally { }
            return page_data;
        }
    }
}
