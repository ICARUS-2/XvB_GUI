using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVB_GUI
{
    /// <summary>
    /// Represents a payout transaction from the pool to an address
    /// </summary>
    public class Transaction
    {
        private string _address;
        private double _amount;
        private DateTime _timeStamp;

        /// <summary>
        /// Takes an address, the amount and timestamp and sets the properties
        /// </summary>
        /// <param name="address">The receiving address of the transaction</param>
        /// <param name="amount">The amount sent to that address</param>
        /// <param name="timeStamp">The date and time the transaction took place</param>
        public Transaction(string address, double amount, DateTime timeStamp)
        {
            Address = address;
            Amount = amount;
            TimeStamp = timeStamp;
        }

        /// <summary>
        /// Takes a string of raw data and parses it to construct a Transaction object
        /// </summary>
        /// <example>447ywns3...P1D451qk  0.010036  2021-08-05 12:06:12 -> New block with address 447ywns3...P1D451qk, amount 0.010036 and 2021-08-05 12:06:12 timestamp</example>
        /// <param name="strToParse">The raw data retrieved from the site</param>
        /// <returns>The Transaction object containing the data parsed from the raw string</returns>
        public static Transaction Parse(string strToParse)
        {
            string[] splitStrings = strToParse.Split(new String[] { "&emsp;&emsp;" }, StringSplitOptions.RemoveEmptyEntries);
            string address = splitStrings[0];
            double amount = double.Parse(splitStrings[1]);
            DateTime timeStamp = DateTime.Parse(splitStrings[2]);

            return new Transaction(address, amount, timeStamp);
        }

        /// <summary>
        /// The receiving address of the transaction
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
        /// The amount sent through the 
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the amount is negative</exception>
        public double Amount
        {
            get
            {
                return _amount;
            }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Amount","ERROR: TRANSACTION CANNOT HAVE NEGATIVE AMOUNT VALUE");

                _amount = value;
            }
        }

        /// <summary>
        /// The date and time the transaction took place
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
}
