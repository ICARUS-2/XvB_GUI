using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVB_GUI
{
    public class BoostRecord
    {
        private string _address;
        private BoostType _boostType;
        private string _hashRate;
        private string _date;
        private string _time;

        public BoostRecord(string address, BoostType boostType, string hashrate, string date, string time)
        {
            Address = address;
            Type = boostType;
            Hashrate = hashrate;
            Date = date;
            Time = time;
        }

        public enum BoostType
        {
            NONE,
            BOOST,
            RAFFLE,
            T_RAFFLE,
        }

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
