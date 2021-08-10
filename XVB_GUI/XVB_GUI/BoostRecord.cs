using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVB_GUI
{
    /// <summary>
    /// A record representing a boost, bonus, or t-bonus
    /// </summary>
    public class BoostRecord
    {
        private string _address;
        private BoostType _boostType;
        private string _hashRate;
        private string _date;
        private string _time;

        /// <summary>
        /// Takes address, type, hashrate, date, and time and constructs a record with these values
        /// </summary>
        /// <param name="address">The address of the wallet that received the boost</param>
        /// <param name="boostType">Represents whether this was a queue boost, a block raffle, or a time raffle</param>
        /// <param name="hashrate">The average hashrate of the boost</param>
        /// <param name="date">The date the boost was given</param>
        /// <param name="time">The time the boost began</param>
        public BoostRecord(string address, BoostType boostType, string hashrate, string date, string time)
        {
            Address = address;
            Type = boostType;
            Hashrate = hashrate;
            Date = date;
            Time = time;
        }

        /// <summary>
        /// Represents whether this record is a queue boost, block bonus, or time bonus, or none
        /// </summary>
        public enum BoostType
        {
            NONE,
            BOOST,
            RAFFLE,
            T_RAFFLE,
        }

        /// <summary>
        /// The wallet address of the boosted miner
        /// </summary>
        public string Address
        {
            get
            {
                return _address;
            }
            private set
            {
                _address = value;
            }
        }

        /// <summary>
        /// Whether the boost is a raffle or queue boost
        /// </summary>
        public BoostType Type
        {
            get
            {
                return _boostType;
            }
            private set
            {
                _boostType = value;
            }
        }

        /// <summary>
        /// The average hashrate of the boost/bonus
        /// </summary>
        public string Hashrate
        {
            get
            {
                return _hashRate;
            }
            private set
            {
                _hashRate = value;
            }
        }

        /// <summary>
        /// The date the boost began
        /// </summary>
        public string Date
        {
            get
            {
                return _date;
            }
            private set
            {
                _date = value;
            }
        }

        /// <summary>
        /// The time the boost began
        /// </summary>
        public string Time
        {
            get
            {
                return _time;
            }
            private set
            {
                _time = value;
            }
        }
    }
}
