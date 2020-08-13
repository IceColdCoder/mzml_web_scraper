using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System.Threading;
using System.Collections.Concurrent;
using System.CodeDom;
using System.Net;

namespace mzml_web_scraper
{
    /// <summary>
    /// Static utility class for reading data from web pages. Provides methods to request data and handle exceptions from web access.
    /// Default timeout is 15 seconds.
    /// </summary>
    public static class HTTP_Page_Reader
    {

        private static readonly HttpClient http_client = null;
        static HTTP_Page_Reader()
        {
            http_client = new HttpClient();
            http_client.Timeout = TimeSpan.FromSeconds(15);
        }

        /// <summary>
        /// Override the default time out of this static class.
        /// </summary>
        /// <param name="timeout"></param>
        public static void Set_Default_Timeout(TimeSpan timeout)
        {
            http_client.Timeout = timeout;
        }

        /// <summary>
        /// Returns the string representation of the specified HttpRequestMessage with the specific time out.
        /// Exceptions are logged at and then bubbled up from this method.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static async Task<string> HTTP_GET_String(HttpRequestMessage msg, TimeSpan timeout)
        {
            var cts = new CancellationTokenSource(timeout);
            try
            {
                HttpResponseMessage response = await http_client.SendAsync(msg, cts.Token);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            //Thrown when a HTTP failure code was recieved.
            catch (HttpRequestException ex)
            {
                Log.Write(ex.Source + " HttpRequestException " + ex.Message);
                throw ex;
            }
            catch (TaskCanceledException ex)
            {
                //Fix for checking for cancelled web request vs a time out.
                if (cts.Token.IsCancellationRequested)
                {
                    Log.Write(ex.Source + " Timeout Exception " + ex.Message);
                }
                else
                {
                    Log.Write(ex.Source + " Non-Timout Exception " + ex.Message);
                }
                throw ex;
            }
            //Thrown for general web exceptions.
            catch (WebException ex)
            {
                Log.Write(ex.Source + " WebException " + ex.Message);
                throw ex;
            }
            //Thrown for catching any on handled exceptions. This code is here for robustness and should never be reached.
            catch (Exception ex)
            {
                Log.Write(ex.Source + " Unknown Exception " + ex.Message);
                throw ex;
            }
            finally
            {
                msg.Dispose();
                cts.Dispose();
            }
        }
    }
}
