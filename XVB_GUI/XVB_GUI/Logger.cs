using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace XVB_GUI
{
    /// <summary>
    /// Used for logging miner data to a file
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// The file name that the data will be logged to
        /// </summary>
        public const string fileName = "logfile.txt";

        /// <summary>
        /// Takes the directory and the MainWindow handle and logs the miner data from the window to the file in the folder path specified
        /// </summary>
        /// <param name="directory">The folder path where logfile.txt is located</param>
        /// <param name="window">The window it is retrieving the data from</param>
        public static void LogData(MainWindow window)
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
                //string pathString = directory + "\\" + fileName;
                //writer = new StreamWriter(Path.Combine(directory, fileName), true);

                
                writer = new StreamWriter(MainWindow.appDataBasePath + "\\" + fileName, true);
                writer.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace + ex.Data);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
