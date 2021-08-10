using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace XVB_GUI
{
    /// <summary>
    /// A tool used to call the https://xmrvsbeast.com/stats API
    /// </summary>
    static class StatsApiCaller
    {
        /// <summary>
        /// The URL of the stats API
        /// </summary>
        public const string BASE_URL = "https://xmrvsbeast.com/stats";

        /// <summary>
        /// Takes a wallet address and retrieves the API data for it, as well as the pool's stats
        /// </summary>
        /// <param name="_wAddr"></param>
        /// <returns>A PoolApiResponse object with the general stats as well as address specific stats</returns>
        public static PoolApiResponse Query(string _wAddr)
        {
            Uri baseUri = new Uri(BASE_URL);
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseCookies = false;
            HttpClient apiClient = new HttpClient(handler);
            apiClient.BaseAddress = baseUri;

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, baseUri);
            msg.Headers.Add("Cookie", String.Format("wa={0}", _wAddr));

            HttpResponseMessage res = null;

            try
            {
                res = apiClient.SendAsync(msg).GetAwaiter().GetResult();
            }
            catch(Exception ex)
            {
                return new PoolApiResponse();
            }    

            string body = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return new PoolApiResponse(body, _wAddr);
        }

        /// <summary>
        /// Retrieves pool's general stats API data
        /// </summary>
        /// <returns>A PoolApiResponse object containing the pool's general stats. Miner specific stats will be set to zero</returns>
        public static PoolApiResponse Query()
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri(BASE_URL);

            HttpResponseMessage res = null;

            try
            {
                res = client.GetAsync(uri).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                return new PoolApiResponse();
            }

            string body = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return new PoolApiResponse(body, "0");
        }
    }
}
