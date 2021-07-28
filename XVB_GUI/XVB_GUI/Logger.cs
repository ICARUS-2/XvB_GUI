using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace XVB_GUI
{
    public static class Logger
    {
        public const string fileName = "logfile.txt";
        public static void LogData(string directory, MainWindow window)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[" + DateTime.Now + "] ---- ");

            if (window.tb_Address1.Text != "-")
            {
                sb.Append("ADDR1 (" + window.tb_Address1.Text.Substring(0, 5) + ")");
                sb.Append(" HR: " + window.tb_Address1Hashrate.Text);
                sb.Append(" - Miners: " + window.tb_Address1ActiveMiners.Text);
                sb.Append(" - Balance: " + window.tb_Address1BalanceNumber.Text);
            }

            if (window.tb_Address2.Text != "-")
            {
                sb.Append(" ---- ADDR2 (" + window.tb_Address2.Text.Substring(0, 5) + ")");
                sb.Append(" HR: " + window.tb_Address2Hashrate.Text);
                sb.Append(" - Miners: " + window.tb_Address2ActiveMiners.Text);
                sb.Append(" - Balance: " + window.tb_Address2BalanceNumber.Text);
            }

            if (window.tb_Address3.Text != "-")
            {
                sb.Append(" ---- ADDR3 (" + window.tb_Address3.Text.Substring(0, 5) + ")");
                sb.Append(" HR: " + window.tb_Address3Hashrate.Text);
                sb.Append(" - Miners: " + window.tb_Address3ActiveMiners.Text);
                sb.Append(" - Balance: " + window.tb_Address3BalanceNumber.Text);
            }

            StreamWriter writer = null;
            try
            {
                string path = directory + "\\" + fileName;
                writer = new StreamWriter(path, true);
                writer.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                //YOLO
            }
            finally
            {
                writer.Close();
            }
        }
    }
}
