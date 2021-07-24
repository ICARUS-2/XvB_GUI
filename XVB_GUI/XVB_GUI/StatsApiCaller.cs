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
    static class StatsApiCaller
    {
        public const string BASE_URL = "https://xmrvsbeast.com/stats";

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
    }
}
