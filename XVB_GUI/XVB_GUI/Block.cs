using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVB_GUI
{
    /// <summary>
    /// Represents a block mined on the pool, stores its index, hash, status, reward, and timestamp
    /// </summary>
    public class Block
    {
        private Int64 _indexNum;
        private string _hash;
        private BlockStatus _status;
        private string _blockReward;
        private DateTime _timeStamp;

        /// <summary>
        /// Constructor takes index, hash, status, block reward, and timestamp and assigns them
        /// </summary>
        /// <param name="indexNum">The block's location on the blockchain</param>
        /// <param name="hash">The block's hash code (RandomX)</param>
        /// <param name="status">Whether the block is Locked, Unlocked, or Orphaned</param>
        /// <param name="blockReward">The block's reward in XMR</param>
        /// <param name="timeStamp">The date and time the block was mined at</param>
        public Block(Int64 indexNum, string hash, BlockStatus status, string blockReward, DateTime timeStamp)
        {
            BlockNumber = indexNum;
            Hash = hash;
            Status = status;
            BlockReward = blockReward;
            TimeStamp = timeStamp;
        }

        /// <summary>
        /// Takes a string from the website's text file (see) and uses it to generate a block data by parsing it
        /// </summary>
        /// <param name="strToParse"></param>
        /// <example>2424062	135a...1aba	LOCKED		PENDING		2021-08-10 15:55:47 -> New block with address 135..., locked status, pending block reward, date and time</example>
        /// <see cref="https://xmrvsbeast.com/stats_frame/mined_blocks_full.html"/>
        /// <returns>The block object with the string's data</returns>
        public static Block Parse(string strToParse)
        {
            string[] splitArr = strToParse.Split('\t');

            Int64 blockNumber = 0;
            string hash = null;
            BlockStatus status = BlockStatus.ORPHANED;
            string blockReward = "";
            DateTime timeStamp = DateTime.Now;

            blockNumber = Int64.Parse(splitArr[0]);
            hash = splitArr[1];
            status = (BlockStatus)Enum.Parse(typeof(BlockStatus), splitArr[2]);

            if (strToParse.Contains("PENDING"))
            {
                blockReward = splitArr[4];
                timeStamp = DateTime.Parse(splitArr[6]);
            }
            else
            {
                blockReward = splitArr[3];
                timeStamp = DateTime.Parse(splitArr[4]);
            }
            return new Block(blockNumber, hash, status, blockReward, timeStamp);
        }

        /// <summary>
        /// The blockchain height when this block was 
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if value is negative</exception>
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

        /// <summary>
        /// The block's RandomX hash code
        /// </summary>
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

        /// <summary>
        /// Represents whether the block is Locked, Unlocked, or Orphaned
        /// </summary>
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

        /// <summary>
        /// The reward of this block. Can be a numberical value represented as a string, or "PENDING"
        /// </summary>
        public string BlockReward
        {
            get
            {
                return _blockReward;
            }
            private set
            {
                _blockReward = value;
            }
        }

        /// <summary>
        /// The date and time the block was mined
        /// </summary>
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

    /// <summary>
    /// The block's possible statuses: Locked (validation needed, block reward TBD), Unlocked (validated block, has a reward), Orphaned (invalid block, no reward)
    /// </summary>
    public enum BlockStatus
    {
        LOCKED,
        UNLOCKED,
        ORPHANED
    }
}
