using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace XVB_GUI
{
    /// <summary>
    /// Interaction logic for BoostReport.xaml
    /// </summary>
    public partial class BoostReport : Window
    {
        /// <summary>
        /// The wallet address whose boosts are being retrieved
        /// </summary>
        public string address;

        /// <summary>
        /// Takes the address and sets it as the field, queries the site and builds a datagrid containing all of the boost data
        /// </summary>
        /// <param name="address">The wallet address</param>
        public BoostReport(string address)
        {
            this.address = address;
            InitializeComponent();

            dg_BoostData.AutoGenerateColumns = false;
            dg_BoostData.ItemsSource = null;

            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = "Miner";
            column.Binding = new Binding("Address");
            dg_BoostData.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Type";
            column.Binding = new Binding("Type");
            dg_BoostData.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Hashrate";
            column.Binding = new Binding("Hashrate");
            dg_BoostData.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Date";
            column.Binding = new Binding("Date");
            dg_BoostData.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Time";
            column.Binding = new Binding("Time");
            dg_BoostData.Columns.Add(column);

            List<BoostRecord> records = StatsFetcher.GetWinners(address);
            dg_BoostData.ItemsSource = records;

            int boostCount = 0;
            int raffleCount = 0;
            int tRaffleCount = 0;

            foreach(BoostRecord record in records)
            {
                switch (record.Type)
                {
                    case BoostRecord.BoostType.BOOST:
                        boostCount++;
                        break;

                    case BoostRecord.BoostType.RAFFLE:
                        raffleCount++;
                        break;

                    case BoostRecord.BoostType.T_RAFFLE:
                        tRaffleCount++;
                        break;

                    default:
                        //
                        break;
                }
            }

            tb_BoostCounts.Text = String.Format("Total Boost/Bonus Count: {0} -> Queue Boosts: {1}   Raffle Bonuses: {2}   T/Raffle Bonuses: {3}", records.Count, boostCount, raffleCount, tRaffleCount);
        }
    }
}
