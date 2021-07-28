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
    /// Interaction logic for TransactionData.xaml
    /// </summary>
    public partial class TransactionData : Window
    {
        public TransactionData(string addr)
        {
            InitializeComponent();

            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = "Address";
            column.Binding = new Binding("Address");
            dg_TXData.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Amount";
            column.Binding = new Binding("Amount");
            dg_TXData.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Timestamp";
            column.Binding = new Binding("TimeStamp");
            dg_TXData.Columns.Add(column);

            dg_TXData.AutoGenerateColumns = false;
            dg_TXData.ItemsSource = null;

            List<Transaction> txList = StatsFetcher.GetPayouts(addr).ToList();

            dg_TXData.ItemsSource = txList;

            double amount = 0;

            foreach (Transaction tx in txList)
                amount += tx.Amount;

            tb_TXCount.Text = "Payout TX Count: " + txList.Count.ToString() + " -> Total Payout: " + amount + " XMR";
        }
    }
}
