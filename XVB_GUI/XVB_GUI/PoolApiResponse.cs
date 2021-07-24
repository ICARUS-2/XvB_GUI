using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace XVB_GUI
{
    class PoolApiResponse
    {
        public const int RESPONSE_SIZE = 23;

        private string _rawData;
        private ResponseStatus _status;

        private string _address;
        private Int64 _poolHashrate;
        private Int64 _roundHashes;
        private Int64 _networkHashrate;
        private Int64 _networkDifficulty;
        private Int64 _networkHeight;
        private Int64 _lastTemplateFetched;
        private Int64 _lastBlockFound;
        private Int64 _poolBlocksFound;
        private double _paymentThreshold;
        private double _poolFee;
        private Int64 _poolPort;
        private Int64 _poolSslPort;
        private Int64 _allowSelfSelect;
        private Int64 _connectedMiners;
        private Int64 _minerHashrate;
        private Int64 _twoMinuteHashRate;
        private Int64 _tenMinuteHashRate;
        private Int64 _thirtyMinuteHashRate;
        private Int64 _oneHourHashRate;
        private Int64 _oneDayHashRate;
        private Int64 _oneWeekHashRate;
        private double _minerBalance;
        private Int64 _workerCount;

        public PoolApiResponse()
        {
            Status = ResponseStatus.FAILED;
        }

        public PoolApiResponse(string objStr_, string address_)
        {
            RawData = objStr_;
            Address = address_;
            Parse();
            Status = ResponseStatus.OK;
        }

        private void Parse()
        {
            string numberProperties = RawData;
            numberProperties = numberProperties.Trim('{', '}');
            numberProperties = numberProperties.Replace("\"", "");
            numberProperties = numberProperties.Replace("_", "");
            numberProperties = numberProperties.Replace(":", "");
            numberProperties = numberProperties.Replace("[", "");
            numberProperties = numberProperties.Replace("]", "");
            numberProperties = Regex.Replace(numberProperties, "[A-Za-z ]", "");
            string[] propArr = numberProperties.Split(',');

            if (propArr.Length != RESPONSE_SIZE)
                throw new DataMisalignedException(String.Format("PARSE ERROR: PARSED DATASET SIZE INVALID -> EXPECTED: {0}, ACTUAL: {1}", RESPONSE_SIZE, propArr.Length));

            PoolHashrate = Int64.Parse(propArr[0]);
            RoundHashes = Int64.Parse(propArr[1]);
            NetworkHashrate = Int64.Parse(propArr[2]);
            NetworkDifficulty = Int64.Parse(propArr[3]);
            NetworkHeight = Int64.Parse(propArr[4]);
            LastTemplateFetched = Int64.Parse(propArr[5]);
            LastBlockFound = Int64.Parse(propArr[6]);
            PoolBlocksFound = Int64.Parse(propArr[7]);
            PaymentThreshold = double.Parse(propArr[8]);
            PoolFee = double.Parse(propArr[9]);
            PoolPort = Int64.Parse(propArr[10]);
            PoolSSLPort = Int64.Parse(propArr[11]);
            AllowSelfSelect = Int64.Parse(propArr[12]);
            ConnectedMiners = Int64.Parse(propArr[13]);
            MinerHashrate = Int64.Parse(propArr[14]);
            TwoMinuteHashRate = Int64.Parse(propArr[15]);
            TenMinuteHashRate = Int64.Parse(propArr[16]);
            ThirtyMinuteHashRate = Int64.Parse(propArr[17]);
            OneHourHashRate = Int64.Parse(propArr[18]);
            OneDayHashRate = Int64.Parse(propArr[19]);
            OneWeekHashRate = Int64.Parse(propArr[20]);
            MinerBalance = double.Parse(propArr[21]);
            WorkerCount = Int64.Parse(propArr[22]);
        }

        public enum ResponseStatus
        {
            OK,
            FAILED,
        }

        public ResponseStatus Status
        {
            get
            {
                return _status;
            }
            private set
            {
                _status = value;
            }
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
        public string RawData
        {
            get
            {
                return _rawData;
            }
            private set
            {
                _rawData = value;
            }
        }

        public Int64 PoolHashrate
        {
            get
            {
                return _poolHashrate;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("PoolHashrate","ERROR: POOL CANNOT HAVE NEGATIVE HASHRATE");

                _poolHashrate = value;
            }
        }

        public Int64 RoundHashes
        {
            get
            {
                return _roundHashes;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("RoundHashes","ERROR: ROUND HASHES CANNOT BE NEGATIVE");

                _roundHashes = value;
            }
        }

        public Int64 NetworkHashrate
        {
            get
            {
                return _networkHashrate;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("NetworkHashrate", "ERROR: NETWORK CANNOT HAVE NEGATIVE HASHRATE");

                _networkHashrate = value;
            }
        }

        public Int64 NetworkDifficulty
        {
            get
            {
                return _networkDifficulty;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("NetworkDifficulty", "ERROR: NETWORK CANNOT HAVE NEGATIVE DIFFICULTY SETTING");

                _networkDifficulty = value;
            }
        }

        public Int64 NetworkHeight
        {
            get
            {
                return _networkHeight;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("NetworkHeight", "ERROR: BLOCKCHAIN HEIGHT CANNOT BE NEGATIVE");

                _networkHeight = value;
            }
        }

        public Int64 LastTemplateFetched
        {
            get
            {
                return _lastTemplateFetched;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("LastTemplateFetched", "ERROR: TEMPLATE FETCH TIME CANNOT BE NEGATIVE");

                _lastTemplateFetched = value;
            }
        }

        public Int64 LastBlockFound
        {
            get
            {
                return _lastBlockFound;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("LastBlockFound", "ERROR: BLOCK TIME CANNOT BE NEGATIVE");

                _lastBlockFound = value;
            }
        }

        public Int64 PoolBlocksFound
        {
            get
            {
                return _poolBlocksFound;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("PoolBlocksFound", "ERROR: TOTAL POOL BLOCKS FOUND CANNOT BE NEGATIVE");

                _poolBlocksFound = value;
            }
        }

        public double PaymentThreshold
        {
            get
            {
                return _paymentThreshold;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("PaymentThreshold", "ERROR: POOL CANNOT HAVE A NEGATIVE PAYOUT VALUE");

                _paymentThreshold = value;
            }
        }

        public double PoolFee
        {
            get
            {
                return _poolFee;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("PoolFee", "ERROR: POOL CANNOT HAVE NEGATIVE FEE");

                _poolFee = value;
            }
        }

        public Int64 PoolPort
        {
            get
            {
                return _poolPort;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("PoolPort", "ERROR: POOL CANNOT HAVE NEGATIVE PORT NUMBER");

                _poolPort = value;
            }
        }

        public Int64 PoolSSLPort
        {
            get
            {
                return _poolSslPort;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("PoolSSLPort", "ERROR: POOL CANNOT HAVE NEGATIVE SSL PORT NUMBER");

                _poolSslPort = value;
            }
        }

        public Int64 AllowSelfSelect
        {
            get
            {
                return _allowSelfSelect;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("AllowSelfSelect", "ERROR: ALLOW SELF SELECT VALUE CANNOT BE NEGATIVE");

                _allowSelfSelect = value;
            }
        }

        public Int64 ConnectedMiners
        {
            get
            {
                return _connectedMiners;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("ConnectedMiners", "ERROR: POOL CANNOT HAVE A NEGATIVE NUMBER OF MINERS");

                _connectedMiners = value;
            }
        }

        public Int64 MinerHashrate
        {
            get
            {
                return _minerHashrate;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("MinerHashrate", "ERROR: POOL MINER CANNOT HAVE NEGATIVE HASH RATE");

                _minerHashrate = value;
            }
        }

        public Int64 TwoMinuteHashRate
        {
            get
            {
                return _twoMinuteHashRate;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("TwoMinuteHashRate", "ERROR: LAST TWO MINUTES' HASH RATE CANNOT BE NEGATIVE");

                _twoMinuteHashRate = value;
            }
        }

        public Int64 TenMinuteHashRate
        {
            get
            {
                return _tenMinuteHashRate;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("TenMinuteHashRate", "ERROR: LAST TEN MINUTES' HASH RATE CANNOT BE NEGATIVE");

                _tenMinuteHashRate = value;
            }
        }

        public Int64 ThirtyMinuteHashRate
        {
            get
            {
                return _thirtyMinuteHashRate;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("ThirtyMinuteHashRate", "ERROR: LAST THIRTY MINUTES' HASH RATE CANNOT BE NEGATIVE");

                _thirtyMinuteHashRate = value;
            }
        }

        public Int64 OneHourHashRate
        {
            get
            {
                return _oneHourHashRate;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("OneHourHashRate", "ERROR: LAST HOURS' HASH RATE CANNOT BE NEGATIVE");

                _oneHourHashRate = value;
            }
        }

        public Int64 OneDayHashRate
        {
            get
            {
                return _oneDayHashRate;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("OneDayHashRate", "ERROR: LAST DAY'S HASH RATE CANNOT BE NEGATIVE");

                _oneDayHashRate = value;
            }
        }

        public Int64 OneWeekHashRate
        {
            get
            {
                return _oneWeekHashRate;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("OneWeekHashRate", "ERROR: LAST WEEK'S HASH RATE CANNOT BE NEGATIVE");

                _oneWeekHashRate = value;
            }
        }

        public double MinerBalance
        {
            get
            {
                return _minerBalance;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("MinerBalance", "ERROR: MINER'S BALANCE CANNOT BE NEGATIVE");

                _minerBalance = value;
            }
        }

        public Int64 WorkerCount
        {
            get
            {
                return _workerCount;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("WorkerCount", "ERROR: MINER CANNOT HAVE A NEGATIVE NUMBER OF ACTIVE WORKERS");

                _workerCount = value;
            }
        }
    }
}
