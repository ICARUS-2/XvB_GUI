using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVB_GUI
{
    public class Transaction
    {
        private string _address;
        private double _amount;
        private DateTime _timeStamp;

        public Transaction(string address, double amount, DateTime timeStamp)
        {
            Address = address;
            Amount = amount;
            TimeStamp = timeStamp;
        }

        public static Transaction Parse(string strToParse)
        {
            string[] splitStrings = strToParse.Split(new String[] { "&emsp;&emsp;" }, StringSplitOptions.RemoveEmptyEntries);
            string address = splitStrings[0];
            double amount = double.Parse(splitStrings[1]);
            DateTime timeStamp = DateTime.Parse(splitStrings[2]);

            return new Transaction(address, amount, timeStamp);
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

        public DateTime TimeStamp
        {
            get
            {
                return _timeStamp;
            }
            set
            {
                _timeStamp = value;
            }
        }
    }
}
