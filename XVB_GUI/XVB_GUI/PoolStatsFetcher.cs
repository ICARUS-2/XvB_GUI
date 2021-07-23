using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text.RegularExpressions;

namespace XVB_GUI
{
    public static class PoolStatsFetcher
    {
        public const string RAFFLE_HASHRATE_URL = "https://xmrvsbeast.com/bonus_hash.html";
        public const string BOOST_HASHRATE_URL = "https://xmrvsbeast.com/boost_hash.html";
        public const string BLOCKS_MINED_URL = "https://xmrvsbeast.com/stats_frame/mined_blocks_full.html";
        public const string MINER_PAYOUTS_URL = "https://xmrvsbeast.com/cgi-bin/miner_payouts.cgi";
        public const string WINNERS_URL = "https://xmrvsbeast.com/cgi-bin/winner_stats.cgi";
        private const int START_ARR = 2;
        private const int ARR_SIZE = 12;
        private const int SHORTENED_ADDRESS_LENGTH = 8;
        public static string GetRaffleHashrate()
        {
            Uri uri = new Uri(RAFFLE_HASHRATE_URL);
            HttpClient client = new HttpClient();
            HttpResponseMessage res = client.GetAsync(uri).GetAwaiter().GetResult();
            string body = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            string[] htmlArr = body.Split('\n');

            string hashrateStr = "";

            foreach(string str in htmlArr)
            {
                if (str.Contains("<p>"))
                    hashrateStr = str;
            }

            hashrateStr = hashrateStr.Replace("<p>", "");
            hashrateStr = hashrateStr.Replace("</p>", "");

            return hashrateStr;
        }

        public static string GetBoostHashrate()
        {
            Uri uri = new Uri(BOOST_HASHRATE_URL);
            HttpClient client = new HttpClient();
            HttpResponseMessage res = client.GetAsync(uri).GetAwaiter().GetResult();
            string body = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            string hashrateStr = "";
            string[] htmlArr = body.Split('\n');

            foreach (string str in htmlArr)
            {
                if (str.Contains("<h2>"))
                    hashrateStr = str;
            }

            hashrateStr = hashrateStr.Replace("<h2>", "");
            hashrateStr = hashrateStr.Replace("</h2>", "");
            hashrateStr = hashrateStr.Replace("\t", "");

            return hashrateStr;
        }

        public static Block[] GetBlocksMined()
        {
            Uri uri = new Uri("https://xmrvsbeast.com/stats_frame/mined_blocks_full.txt");
            HttpClient client = new HttpClient();
            HttpResponseMessage res = client.GetAsync(uri).GetAwaiter().GetResult();
            string body = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            string[] htmlArr = body.Split('\n');
            Array.Resize(ref htmlArr, ARR_SIZE);

            Block[] blocks = new Block[ARR_SIZE - START_ARR];

            for (int i = START_ARR; i < ARR_SIZE; i++)
            {
                Block newBlock = Block.Parse(htmlArr[i]);
                blocks[i - START_ARR] = newBlock;
            }

            return blocks;
        }

        public static Transaction[] GetPayouts(string address)
        {
            Uri payoutUri = new Uri(MINER_PAYOUTS_URL);
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseCookies = false;
            HttpClient apiClient = new HttpClient(handler);
            apiClient.BaseAddress = payoutUri;

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, payoutUri);
            msg.Headers.Add("Cookie", String.Format("wa={0}", address));

            HttpResponseMessage res = apiClient.SendAsync(msg).GetAwaiter().GetResult();

            string body = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (body.Contains("0 Payouts"))
                return new Transaction[0];

            string[] htmlArr = body.Split('\n');

            List<string> transactionStrings = new List<string>();

            bool firstTX = true;

            foreach(string str in htmlArr)
            {
                if (str.Contains(address.Substring(0, SHORTENED_ADDRESS_LENGTH)))
                {
                    if (firstTX)
                    {
                        string temp = str.Replace("</p>", "");
                        temp = temp.Replace("<p>", "");
                        temp = temp.Replace("<code id=payouts>", "");
                        temp = temp.Replace("<br>", "");
                        firstTX = false;
                        transactionStrings.Add(temp);
                    }
                    else
                        transactionStrings.Add(str.Replace("<br>", ""));
                }
            }

            Transaction[] transactions = new Transaction[transactionStrings.Count];

            for (int i = 0; i < transactions.Length; i++)
                transactions[i] = Transaction.Parse(transactionStrings[i]);
            
            return transactions;
        }

        //public static string GetWinners()
        //{
        //    throw new NotImplementedException();
        //}

        public static int GetEstimatedBoostTime(string address)
        {
            Uri payoutUri = new Uri(WINNERS_URL);
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseCookies = false;
            HttpClient apiClient = new HttpClient(handler);
            apiClient.BaseAddress = payoutUri;

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, payoutUri);
            msg.Headers.Add("Cookie", String.Format("wa={0}", address));

            HttpResponseMessage res = apiClient.SendAsync(msg).GetAwaiter().GetResult();

            string body = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            string[] htmlArr = body.Split('\n');

            string stringWithData = "";

            foreach (string str in htmlArr)
                if (str.Contains("Next Boost"))
                    stringWithData = str;

            stringWithData = Regex.Replace(stringWithData, @"[^\d]", "");

            return int.Parse(stringWithData);
        }
    }
}
