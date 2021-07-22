using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVB_GUI
{
    static class PoolStatsFetcher
    {
        public const string RAFFLE_HASHRATE_URL = "https://xmrvsbeast.com/bonus_hash.html";
        public const string BOOST_HASHRATE_URL = "https://xmrvsbeast.com/boost_hash.html";
        public const string BLOCKS_MINED_URL = "https://xmrvsbeast.com/stats_frame/mined_blocks_full.html";
        public const string MINER_PAYOUTS_URL = "https://xmrvsbeast.com/cgi-bin/miner_payouts.cgi";
        public const string WINNERS_URL = "https://xmrvsbeast.com/cgi-bin/winner_stats.cgi";

        public static string GetRaffleHashrate()
        {
            throw new NotImplementedException();
        }

        public static string GetBoostHashrate()
        {
            throw new NotImplementedException();
        }

        public static Block[] GetBlocksMined()
        {
            throw new NotImplementedException();
        }

        public static string GetPayouts()
        {
            throw new NotImplementedException();
        }

        public static string GetWinners()
        {
            throw new NotImplementedException();
        }

        public static string GetEstimatedBoostTime(string address)
        {
            throw new NotImplementedException();
        }
    }
}
