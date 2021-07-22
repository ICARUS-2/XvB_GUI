using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVB_GUI
{
    public class Block
    {
        private Int64 _indexNum;
        private string _hash;
        private BlockStatus _status;
        private double _blockReward;
        private DateTime _timeStamp;
        private double _blockTime;
        private Int64 _avgHashrate;

        public Block(Int64 indexNum, string hash, BlockStatus status, double blockReward, DateTime timeStamp, double blockTime, Int64 avgHashrate)
        {
            BlockNumber = indexNum;
            Hash = hash;
            Status = status;
            BlockReward = blockReward;
            TimeStamp = timeStamp;
            BlockTime = _blockTime;
            AverageHashrate = _avgHashrate;
        }

        public Int64 BlockNumber
        {
            get
            {
                return _indexNum;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("BlockNumber", "ERROR: BLOCK NUMBER CANNOT BE NEGATIVE");

                _indexNum = value;
            }
        }

        public string Hash
        {
            get
            {
                return _hash;
            }
            private set
            {
                _hash = value;
            }
        }

        public BlockStatus Status
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

        public double BlockReward
        {
            get
            {
                return _blockReward;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("BlockReward","ERROR: BLOCK REWARD CANNOT BE NEGATIVE");

                _blockReward = value;
            }
        }

        public DateTime TimeStamp
        {
            get
            {
                return _timeStamp;
            }
            private set
            {
                _timeStamp = value;
            }
        }

        public double BlockTime
        {
            get
            {
                return _blockTime;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("BlockTime","ERROR: CANNOT HAVE NEGATIVE BLOCK TIME");

                _blockTime = value;
            }
        }

        public Int64 AverageHashrate
        {
            get
            {
                return _avgHashrate;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("AverageHashrate","ERROR: BLOCK HASHRATE CANNOT BE NEGATIVE");

                _avgHashrate = value;
            }
        }
    }

    public enum BlockStatus
    {
        Locked,
        Unlocked,
        Orphaned
    }
}
