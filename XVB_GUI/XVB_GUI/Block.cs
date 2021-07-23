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

        public Block(Int64 indexNum, string hash, BlockStatus status, double blockReward, DateTime timeStamp)
        {
            BlockNumber = indexNum;
            Hash = hash;
            Status = status;
            BlockReward = blockReward;
            TimeStamp = timeStamp;
        }

        public static Block Parse(string strToParse)
        {
            string[] splitArr = strToParse.Split('\t');

            Int64 blockNumber = Int64.Parse(splitArr[0]);
            string hash = splitArr[1];
            BlockStatus status = (BlockStatus)Enum.Parse(typeof(BlockStatus), splitArr[2]);
            double blockReward = double.Parse(splitArr[3]);
            DateTime timeStamp = DateTime.Parse(splitArr[4]);

            return new Block(blockNumber, hash, status, blockReward, timeStamp);
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
    }

    public enum BlockStatus
    {
        LOCKED,
        UNLOCKED,
        ORPHANED
    }
}
