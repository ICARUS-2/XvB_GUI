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
    /// <summary>
    /// Represents the interaction for everything that is not directly linked to the Stats API.
    /// Data retrieval/object construction is done via HTML parsers
    /// All Get methods query the site in real/time
    /// </summary>
    public static class StatsFetcher
    {
        /// <summary>
        /// The URL for the stats API
        /// </summary>
        public const string BASE_URL = "https://xmrvsbeast.com/stats";

        /// <summary>
        /// The URL containing the Raffle hashrate
        /// </summary>
        public const string RAFFLE_HASHRATE_URL = "https://xmrvsbeast.com/bonus_hash.html";

        /// <summary>
        /// The URL containing the T/Raffle hashrate, duration and address
        /// </summary>
        public const string T_RAFFLE_HASHRATE_URL = "https://xmrvsbeast.com/tbonus_hash.html";

        /// <summary>
        /// The URL containing the Boost hashrate, duration and address
        /// </summary>
        public const string BOOST_HASHRATE_URL = "https://xmrvsbeast.com/boost_hash.html";

        /// <summary>
        /// The URL containing the data of the blocks mined by the pool
        /// </summary>
        public const string BLOCKS_MINED_URL = "https://xmrvsbeast.com/stats_frame/mined_blocks_full.html";

        /// <summary>
        /// The URL containing the payout TX data for a given address (cookie: wa -> address is necessary to get valid info)
        /// </summary>
        public const string MINER_PAYOUTS_URL = "https://xmrvsbeast.com/cgi-bin/miner_payouts.cgi";

        /// <summary>
        /// The URL containing the boost/bonus data for a given address (cookie: wa -> address is necessry to get valid info)
        /// </summary>
        public const string WINNERS_URL = "https://xmrvsbeast.com/cgi-bin/winner_stats.cgi";

        /// <summary>
        /// The URL for the crypto price API
        /// </summary>
        public const string MONERO_PRICE_BASE_URL = "https://min-api.cryptocompare.com/data/price?fsym=XMR&tsyms=";

        private const int START_ARR = 2;
        private const int ARR_SIZE = 15;
        private const int SHORTENED_ADDRESS_LENGTH = 8;

        /// <summary>
        /// Takes an integer in H/S and formats it to its shorthand and returns it
        /// </summary>
        /// <example>1000 -> 1KH/S</example>
        /// <param name="hashRate">The integer hashrate in H/S</param>
        /// <returns>The formatted shorthand string representing the hashrate</returns>
        public static string ParseHashrate(Int64 hashRate)
        {
            int kh = 1000;
            int mh = 1000000;
            int gh = 1000000000;

            if (hashRate >= gh)
            {
                double temp = (double)hashRate / gh;
                return temp + " gH/s";
            }
            else if (hashRate >= mh)
            {
                double temp = (double)hashRate / mh;
                return temp + " mH/s";
            }
            else if (hashRate >= kh)
            {
                double temp = (double)hashRate / kh;
                return temp + " kH/s";
            }
            return hashRate + " H/s";
        }

        /// <summary>
        /// Takes a currency type and gets the exhange rates for Monero -> Currency
        /// </summary>
        /// <param name="currency">The currency to convert 1XMR to</param>
        /// <returns>The price + the currency symbol</returns>
        public static string GetMoneroPrice(Currency currency)
        {
            return GetMoneroPriceHelper(currency.ToString());
        }

        /// <summary>
        /// Takes a currency type and gets the exhange rates for Monero -> Cryptocurrency
        /// </summary>
        /// <param name="currency">The currency to convert 1XMR to</param>
        /// <returns>The price + the cryptocurrency symbol</returns>
        public static string GetMoneroPrice(CryptoCurrency currency)
        {
            return GetMoneroPriceHelper(currency.ToString());
        }
        
        private static string GetMoneroPriceHelper(string str)
        {
            Uri uri = new Uri(MONERO_PRICE_BASE_URL + str);
            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = client.GetAsync(uri).GetAwaiter().GetResult();

            string body = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            body = body.Replace("\"", "").Replace("{", "").Replace("}", "");

            string amount = body.Split(':')[1];
            return amount + " " + str.ToString();
        }

        /// <summary>
        /// Does a base API query and checks if it can retrieve data. Returns true if query is successful, false otherwise
        /// </summary>
        /// <returns>Whether the connection to the API is successful or not</returns>
        public static bool IsConnected()
        {
            Uri uri = new Uri(BASE_URL);
            HttpClient client = new HttpClient();

            try
            {
                client.GetAsync(uri).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the raffle hashrate of the pool
        /// </summary>
        /// <example>145 KH/s</example>
        /// <returns>The raffle hashrate in shorthand</returns>
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

        /// <summary>
        /// Gets the time-raffle hashrate of the pool
        /// </summary>
        /// <example>145 KH/s</example>
        /// <returns>The time-raffle hashrate in shorthand</returns>
        public static string GetTimeRaffleHashrate()
        {
            Uri uri = new Uri(T_RAFFLE_HASHRATE_URL);
            HttpClient client = new HttpClient();
            HttpResponseMessage res = client.GetAsync(uri).GetAwaiter().GetResult();
            string body = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            string[] htmlArr = body.Split('\n');

            string hashrateStr = "";
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

        /// <summary>
        /// Gets the time-raffle duration
        /// </summary>
        /// <example>196m</example>
        /// <returns>time-raffle duration string</returns>
        public static string GetTimeRaffleDuration()
        {
            Uri uri = new Uri(T_RAFFLE_HASHRATE_URL);
            HttpClient client = new HttpClient();
            HttpResponseMessage res = client.GetAsync(uri).GetAwaiter().GetResult();
            string body = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            string[] htmlArr = body.Split('\n');

            string hashrateStr = "";
            foreach (string str in htmlArr)
            {
                if (str.Contains("T-Raffle HR:"))
                    hashrateStr = str;
            }

            hashrateStr = hashrateStr.Replace("<h1>", "");
            hashrateStr = hashrateStr.Replace("</h1>", "");
            hashrateStr = hashrateStr.Replace("\t", "");
            hashrateStr = Regex.Replace(hashrateStr, @"[^\d]", "");
            return hashrateStr;
        }

        /// <summary>
        /// Gets the address for the time-raffle winner
        /// </summary>
        /// <example>446Kfwpw...RAkbu5kd</example>
        /// <returns>The shortened address of the time-raffle winner</returns>
        public static string GetTimeRaffleAddress()
        {
            Uri uri = new Uri(T_RAFFLE_HASHRATE_URL);
            HttpClient client = new HttpClient();
            HttpResponseMessage res = client.GetAsync(uri).GetAwaiter().GetResult();
            string body = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            string[] htmlArr = body.Split('\n');

            string addressStr = "";
            foreach (string str in htmlArr)
            {
                if (str.Contains("\t<p></code>"))
                    addressStr = str;
            }

            addressStr = addressStr.Replace("<p>", "");
            addressStr = addressStr.Replace("</code>", "");
            addressStr = addressStr.Replace("\t", "");
            addressStr = addressStr.Replace("</p>", "");
            return addressStr;
        }

        /// <summary>
        /// Gets the queue boost hashrate
        /// </summary>
        /// <example>211.4kH/S</example>
        /// <returns>The shorthand hashrate string</returns>
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

        /// <summary>
        /// Gets the address for the current boost round
        /// </summary>
        /// <example>446Kfwpw...RAkbu5kd</example>
        /// <returns>The shortened address of the current boost round</returns>
        public static string GetBoostHashrateAddress()
        {
            Uri uri = new Uri(BOOST_HASHRATE_URL);
            HttpClient client = new HttpClient();
            HttpResponseMessage res = client.GetAsync(uri).GetAwaiter().GetResult();
            string body = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            string[] htmlArr = body.Split('\n');

            string addressStr = "";
            foreach (string str in htmlArr)
            {
                if (str.Contains("\t<p></code>"))
                    addressStr = str;
            }

            addressStr = addressStr.Replace("<p>", "");
            addressStr = addressStr.Replace("</code>", "");
            addressStr = addressStr.Replace("\t", "");
            addressStr = addressStr.Replace("</p>", "");
            return addressStr;
        }

        /// <summary>
        /// Gets the queue boost hashrate time remaining
        /// </summary>
        /// <example>61.7kH/S</example>
        /// <returns>String representing the time remaining in the boost</returns>
        public static string GetBoostHashrateDuration()
        {
            Uri uri = new Uri(BOOST_HASHRATE_URL);
            HttpClient client = new HttpClient();
            HttpResponseMessage res = client.GetAsync(uri).GetAwaiter().GetResult();
            string body = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            string[] htmlArr = body.Split('\n');

            string hashrateStr = "";
            foreach (string str in htmlArr)
            {
                if (str.Contains("Boost HR:"))
                    hashrateStr = str;
            }

            hashrateStr = hashrateStr.Replace("<h1>", "");
            hashrateStr = hashrateStr.Replace("</h1>", "");
            hashrateStr = hashrateStr.Replace("\t", "");
            hashrateStr = Regex.Replace(hashrateStr, @"[^\d]", "");
            return hashrateStr;
        }

        /// <summary>
        /// Gets an array of size ARR_SIZE representing the most recently mined blocks
        /// </summary>
        /// <returns>An array containing the recent blocks</returns>
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

        /// <summary>
        /// Takes a Monero address string and retrieves the payout TX data for that address
        /// </summary>
        /// <param name="address">The address whose payments will be retrieved</param>
        /// <returns>An array of the payout transactions for that address</returns>
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

        /// <summary>
        /// Takes a Monero address and gets a list of all the boosts/bonuses for that address
        /// </summary>
        /// <param name="address">The address whose boosts will be retrieved</param>
        /// <returns>A list of records representing all the boosts from the passed address</returns>
        public static List<BoostRecord> GetWinners(string address)
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
            List<string> filteredList = htmlArr.ToList().Where(e => e.Contains("<br>")).ToList();

            BoostRecord.BoostType boostType = BoostRecord.BoostType.NONE;

            List<BoostRecord> records = new List<BoostRecord>();

            foreach(string str in filteredList)
            {
                if (str.Contains("<code id=raffle>"))
                {
                    boostType = BoostRecord.BoostType.RAFFLE;
                }
                else if (str.Contains("<code id=traffle"))
                {
                    boostType = BoostRecord.BoostType.T_RAFFLE;
                }
                else if (str.Contains("<code id=boost>"))
                {
                    boostType = BoostRecord.BoostType.BOOST;
                }
                string temp = str.Replace("<p>", "");
                temp = temp.Replace("<br>", "");
                temp = temp.Replace("</p>", "");
                temp = temp.Replace("<code id=boost>", "");
                temp = temp.Replace("<code id=raffle>", "");
                temp = temp.Replace("<code id=traffle>", "");
                temp = temp.Replace("&emsp;&emsp;", " ");

                string[] split = temp.Split(' ');
                records.Add(new BoostRecord(split[0], boostType, split[1], split[2], split[3]));
            }

            return records;
        }

        /// <summary>
        /// Takes a Monero address and gets the boost ETA for it
        /// </summary>
        /// <param name="address">The address whose boost ETA will be retrieved</param>
        /// <returns>The boost ETA in hours</returns>
        public static int GetEstimatedMinerBoostTime(string address)
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

        /// <summary>
        /// FIAT currencies whose exchange rates can be compared by the app
        /// </summary>
        public enum Currency
        {
            USD,
            CAD, 
            EUR,
            GBP,
            AUD
        }

        /// <summary>
        /// CryptoCurrencies whose exchange rates can be compared by the app
        /// </summary>
        public enum CryptoCurrency
        {
            BTC,
            ETH
        }
    }
}
