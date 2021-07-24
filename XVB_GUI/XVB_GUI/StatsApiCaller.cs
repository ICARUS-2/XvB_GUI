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
    class StatsApiCaller
    {
        private const string BASE_URL = "https://xmrvsbeast.com/stats";
        private string _wAddr;
        public StatsApiCaller(string wAddr_)
        {
            _wAddr = wAddr_;    
        }

        public PoolApiResponse Query()
        {
            Uri baseUri = new Uri(BASE_URL);
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseCookies = false;
            HttpClient apiClient = new HttpClient(handler);
            apiClient.BaseAddress = baseUri;

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, baseUri);
            msg.Headers.Add("Cookie", String.Format("wa={0}", _wAddr));
            //TODO: handle this exception

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

        public string Address
        {
            get
            {
                return _wAddr;
            }
            private set
            {
                _wAddr = value;
            }
        }
    }
}
