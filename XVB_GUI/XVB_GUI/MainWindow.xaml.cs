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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Timers;
using System.Threading;
using System.IO;

namespace XVB_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The refresh rate in MS when the window is active (higher to minimize lag spikes when calling API)
        /// </summary>
        public const int ACTIVE_WINDOW_REFRESH_RATE = 60000;

        /// <summary>
        /// The refresh rate in MS when the window is off to the side (faster, but slow enough to not bust the currency API's call limit)
        /// </summary>
        public const int INACTIVE_WINDOW_REFRESH_RATE = 11500;

        /// <summary>
        /// The relative path of the config file storing the addresses
        /// </summary>
        public const string ADDRESS_CONFIG_FILE = "../../addresses";

        /// <summary>
        /// The relative path of the config file that stores the selected currency
        /// </summary>
        public const string CURRENCY_CONFIG_FILE = "../../currency";

        /// <summary>
        /// The size of the balance bar
        /// </summary>
        public const int BAR_SIZE = 10;

        /// <summary>
        /// The dynamic refresh rate of the window
        /// </summary>
        public int mainRefreshRate = ACTIVE_WINDOW_REFRESH_RATE;

        /// <summary>
        /// The current dataset from the API for this refresh cycle
        /// </summary>
        public PoolApiResponse currentTemplate;

        /// <summary>
        /// Toggles main window query-and-refresh
        /// </summary>
        public bool stopRefresh;

        /// <summary>
        /// The exchange rate FIAT currency selected by the user
        /// </summary>
        public StatsFetcher.Currency currency;

        /// <summary>
        /// The relative path of the config file for logging miner data
        /// </summary>
        public const string OPTIONS_CONFIG_FILE = "../../genconfig";

        /// <summary>
        /// The default logging options: Do not log data, and folder path set to _ for no path
        /// </summary>
        public const string DEFAULT_OPTIONS = "false\n" + "_";

        /// <summary>
        /// The path of the logfile
        /// </summary>
        public string loggerFilePath;

        /// <summary>
        /// Toggles whether to log miner data to file or not
        /// </summary>
        public bool logData = false;

        /// <summary>
        /// Constructor reads the data from all of the config files and sets addresses, currencies, logfiles accordingly. Begins the refresh cycle.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            stopRefresh = false;
            TLUpdateMainStatsAndLogData();
            CancellationToken cancellation = new CancellationToken();

            if (!File.Exists(ADDRESS_CONFIG_FILE))
            {
                File.Create(ADDRESS_CONFIG_FILE).Close();
                string defaultConfig = "-\n-\n-";
                File.WriteAllText(ADDRESS_CONFIG_FILE, defaultConfig);
            }

            if (!File.Exists(CURRENCY_CONFIG_FILE))
            {
                File.Create(CURRENCY_CONFIG_FILE).Close();
                string defaultCurrency = StatsFetcher.Currency.USD.ToString();
                currency = StatsFetcher.Currency.USD;
                File.WriteAllText(CURRENCY_CONFIG_FILE, defaultCurrency);
            }
            else
            {
                currency = (StatsFetcher.Currency)Enum.Parse(typeof(StatsFetcher.Currency), File.ReadAllText(CURRENCY_CONFIG_FILE));
            }

            if (!File.Exists(OPTIONS_CONFIG_FILE))
            {
                File.Create(OPTIONS_CONFIG_FILE).Close();
                File.WriteAllText(OPTIONS_CONFIG_FILE, DEFAULT_OPTIONS);
                loggerFilePath = "_";
                logData = false;
            }
            else
            {
                try
                {
                    string[] arr = File.ReadAllLines(OPTIONS_CONFIG_FILE);
                    logData = bool.Parse(arr[0]);
                    loggerFilePath = arr[1];
                }
                catch(Exception ex)
                {
                    File.WriteAllText(OPTIONS_CONFIG_FILE, DEFAULT_OPTIONS);
                    loggerFilePath = "";
                    logData = false;
                }
            }

            string[] addresses = File.ReadAllLines(ADDRESS_CONFIG_FILE);
            tb_Address1.Text = addresses[0];
            tb_Address2.Text = addresses[1];
            tb_Address3.Text = addresses[2];

            RefreshStats(cancellation);
        }

        /// <summary>
        /// Refresh the window with API calls and HTML page queries based on the set refresh rate and the window active state
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel the async refresh</param>
        /// <returns>Task</returns>
        public async Task RefreshStats(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (!stopRefresh)
                    TLUpdateMainStatsAndLogData();

                if (IsActive)
                {
                    mainRefreshRate = ACTIVE_WINDOW_REFRESH_RATE;
                }
                else
                {
                    mainRefreshRate = INACTIVE_WINDOW_REFRESH_RATE;
                }
                await Task.Delay(mainRefreshRate, cancellationToken);
            }
        }

        /// <summary>
        /// The top level method containing all of the sub-methods to update every part of the UI. Displays error messages onscreen if a connection/parse issue is encountered
        /// </summary>
        public void TLUpdateMainStatsAndLogData()
        {
            currentTemplate = StatsApiCaller.Query();
            UpdateStatusBar();
            
            try
            {
                UpdateTopBar();
                UpdateAddresses();
                UpdateExchangeRates();
                UpdateMinerCountInfo();
                UpdateTotalBlocksFound();
                UpdateBlockData();

                if (logData)
                    Logger.LogData(loggerFilePath, this);
            }
            catch(Exception ex)
            {
                DisplayErrors();
                tb_ConnectionStatus.Text = "FAILED";
                tb_ConnectionStatus.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void DisplayErrors()
        {
            string msg = "ERROR";
            SolidColorBrush red = new SolidColorBrush(Colors.Red);
            tb_NetworkHashrate.Text = msg;
            tb_NetworkHashrate.Foreground = red;

            tb_PoolHashrate.Text = msg;
            tb_PoolHashrate.Foreground = red;


            tb_BoostHashrate.Text = msg;
            tb_BoostHashrate.Foreground = red;
            tb_BoostAddress.Text = msg;
            tb_BoostAddressTime.Text = msg;

            tb_BonusHashrate.Text = msg;
            tb_BonusHashrate.Foreground = red;

            tb_TimeBonusHashrate.Text = msg;
            tb_TimeBonusHashrate.Foreground = red;
            tb_TimeBonusAddress.Text = msg;
            tb_TimeBonusTime.Text = msg;


            if (tb_Address1.Text != "-")
            {
                tb_Address1Hashrate.Text = msg;
                tb_Address1BalanceNumber.Text = msg;
                tb_Address1ActiveMiners.Text = msg;
                tb_Address1PayoutCount.Text = msg;
                tb_Address1BoostETA.Text = msg;
                tb_Address1BalanceBarCurrent.Text = "";
                tb_Address1BalanceBarRemaining.Text = "";
            }

            if (tb_Address2.Text != "-")
            {
                tb_Address2Hashrate.Text = msg;
                tb_Address2BalanceNumber.Text = msg;
                tb_Address2ActiveMiners.Text = msg;
                tb_Address2PayoutCount.Text = msg;
                tb_Address2BoostETA.Text = msg;
                tb_Address2BalanceBarCurrent.Text = "";
                tb_Address2BalanceBarRemaining.Text = "";
            }

            if (tb_Address3.Text != "-")
            {
                tb_Address3Hashrate.Text = msg;
                tb_Address3BalanceNumber.Text = msg;
                tb_Address3ActiveMiners.Text = msg;
                tb_Address3PayoutCount.Text = msg;
                tb_Address3BoostETA.Text = msg;
                tb_Address3BalanceBarCurrent.Text = "";
                tb_Address3BalanceBarRemaining.Text = "";
            }

            tb_ActiveMiners.Text = msg;
            tb_ActiveMiners.Foreground = red;
            tb_AvgHRPerMiner.Text = msg;
            tb_AvgHRPerMiner.Foreground = red;

            tb_FiatCurrency.Text = msg;
            tb_FiatCurrency.Foreground = red;
            tb_BTCCurrency.Text = msg;
            tb_BTCCurrency.Foreground = red;
            tb_ETHCurrency.Text = msg;
            tb_ETHCurrency.Foreground = red;

            tb_BlocksFoundByPool.Text = msg;
            tb_BlocksFoundByPool.Foreground = red;
            tb_BlockchainHeight.Text = msg;
            tb_BlockchainHeight.Foreground = red;

            tb_BlockDataHeader.Text = "API CONNECTION ERROR";
            tb_BlockDataHeader.Foreground = red;
        }

        private void UpdateStatusBar()
        {
            if (StatsFetcher.IsConnected())
            {
                tb_ConnectionStatus.Foreground = new SolidColorBrush(Colors.LightGreen);
                tb_ConnectionStatus.Text = "ONLINE";
            }
            else
            {
                tb_ConnectionStatus.Foreground = new SolidColorBrush(Colors.Red);
                tb_ConnectionStatus.Text = "FAILED";
            }
        }

        private void UpdateTopBar()
        {
            SolidColorBrush green = new SolidColorBrush(Colors.LightGreen);

            tb_NetworkHashrate.Text = StatsFetcher.ParseHashrate(currentTemplate.NetworkHashrate);
            tb_NetworkHashrate.Foreground = green;

            tb_PoolHashrate.Text = StatsFetcher.ParseHashrate(currentTemplate.PoolHashrate);
            tb_PoolHashrate.Foreground = green;


            tb_BoostHashrate.Text = StatsFetcher.GetBoostHashrate() + " ->";
            tb_BoostHashrate.Foreground = green;
            tb_BoostAddress.Text = StatsFetcher.GetBoostHashrateAddress();
            tb_BoostAddressTime.Text = StatsFetcher.GetBoostHashrateDuration() + "m Remaining";

            tb_BonusHashrate.Text = StatsFetcher.GetRaffleHashrate();
            tb_BonusHashrate.Foreground = green;

            tb_TimeBonusHashrate.Text = StatsFetcher.GetTimeRaffleHashrate() + " ->";
            tb_TimeBonusHashrate.Foreground = green;
            tb_TimeBonusAddress.Text = StatsFetcher.GetTimeRaffleAddress();
            tb_TimeBonusTime.Text = StatsFetcher.GetTimeRaffleDuration() + "m Remaining";
        }

        /// <summary>
        /// Retrieves address data from the site and updates all of the miner stats onscreen
        /// </summary>
        public void UpdateAddresses()
        {
            string address1 = tb_Address1.Text;
            string address2 = tb_Address2.Text;
            string address3 = tb_Address3.Text;

            if (address1 != "-")
            {
                PoolApiResponse response = StatsApiCaller.Query(address1);
                tb_Address1Hashrate.Text = StatsFetcher.ParseHashrate(response.MinerHashrate);
                tb_Address1BalanceNumber.Text = response.MinerBalance + "/" + response.PaymentThreshold;
                tb_Address1ActiveMiners.Text = response.WorkerCount.ToString();
                tb_Address1PayoutCount.Text = StatsFetcher.GetPayouts(address1).Length.ToString();
                try
                {
                    tb_Address1BoostETA.Text = "~" + StatsFetcher.GetEstimatedMinerBoostTime(address1).ToString() + " hours";
                }
                catch(Exception ex)
                {
                    tb_Address1BoostETA.Text = "Unavailable";
                }
                int progress = (int)((response.MinerBalance / response.PaymentThreshold)*BAR_SIZE);
                StringBuilder progressSb = new StringBuilder();
                StringBuilder remainingSb = new StringBuilder();

                for (int i = 0; i < progress; i++)
                    progressSb.Append("■");

                int remaining = BAR_SIZE - progress;

                for (int i = 0; i < remaining; i++)
                    remainingSb.Append("■");

                tb_Address1BalanceBarCurrent.Foreground = new SolidColorBrush(Colors.LightGreen);
                tb_Address1BalanceBarRemaining.Foreground = new SolidColorBrush(Colors.Red);

                tb_Address1BalanceBarCurrent.Text = progressSb.ToString();
                tb_Address1BalanceBarRemaining.Text = remainingSb.ToString();
            }

            if (address2 != "-")
            {
                PoolApiResponse response = StatsApiCaller.Query(address2);
                tb_Address2Hashrate.Text = StatsFetcher.ParseHashrate(response.MinerHashrate);
                tb_Address2BalanceNumber.Text = response.MinerBalance + "/" + response.PaymentThreshold;
                tb_Address2ActiveMiners.Text = response.WorkerCount.ToString();
                tb_Address2PayoutCount.Text = StatsFetcher.GetPayouts(address2).Length.ToString();

                try
                {
                    tb_Address2BoostETA.Text = "~" + StatsFetcher.GetEstimatedMinerBoostTime(address2).ToString() + " hours";
                }
                catch(Exception ex)
                {
                    tb_Address2BoostETA.Text = "Unavailable";
                }

                int progress = (int)((response.MinerBalance / response.PaymentThreshold) * BAR_SIZE);
                StringBuilder progressSb = new StringBuilder();
                StringBuilder remainingSb = new StringBuilder();

                for (int i = 0; i < progress; i++)
                    progressSb.Append("■");

                int remaining = BAR_SIZE - progress;

                for (int i = 0; i < remaining; i++)
                    remainingSb.Append("■");

                tb_Address2BalanceBarCurrent.Foreground = new SolidColorBrush(Colors.LightGreen);
                tb_Address2BalanceBarRemaining.Foreground = new SolidColorBrush(Colors.Red);

                tb_Address2BalanceBarCurrent.Text = progressSb.ToString();
                tb_Address2BalanceBarRemaining.Text = remainingSb.ToString();
            }

            if (address3 != "-")
            {
                PoolApiResponse response = StatsApiCaller.Query(address3);
                tb_Address3Hashrate.Text = StatsFetcher.ParseHashrate(response.MinerHashrate);
                tb_Address3BalanceNumber.Text = response.MinerBalance + "/" + response.PaymentThreshold;
                tb_Address3ActiveMiners.Text = response.WorkerCount.ToString();
                tb_Address3PayoutCount.Text = StatsFetcher.GetPayouts(address3).Length.ToString();

                try
                {
                    tb_Address3BoostETA.Text = "~" + StatsFetcher.GetEstimatedMinerBoostTime(address3).ToString() + " hours";
                }
                catch(Exception ex)
                {
                    tb_Address3BoostETA.Text = "Unavailable";
                }
                int progress = (int)((response.MinerBalance / response.PaymentThreshold) * BAR_SIZE);
                StringBuilder progressSb = new StringBuilder();
                StringBuilder remainingSb = new StringBuilder();

                for (int i = 0; i < progress; i++)
                    progressSb.Append("■");

                int remaining = BAR_SIZE - progress;

                for (int i = 0; i < remaining; i++)
                    remainingSb.Append("■");

                tb_Address3BalanceBarCurrent.Foreground = new SolidColorBrush(Colors.LightGreen);
                tb_Address3BalanceBarRemaining.Foreground = new SolidColorBrush(Colors.Red);

                tb_Address3BalanceBarCurrent.Text = progressSb.ToString();
                tb_Address3BalanceBarRemaining.Text = remainingSb.ToString();
            }
        }

        /// <summary>
        /// Updates the exchange rates for the selected FIAT currency, as well as Bitcoin and Ethereum
        /// </summary>
        public void UpdateExchangeRates()
        {
            SolidColorBrush green = new SolidColorBrush(Colors.LightGreen);
            string fiatPrice = StatsFetcher.GetMoneroPrice(currency);
            string btcPrice = StatsFetcher.GetMoneroPrice(StatsFetcher.CryptoCurrency.BTC);
            string ethPrice = StatsFetcher.GetMoneroPrice(StatsFetcher.CryptoCurrency.ETH);

            tb_FiatCurrency.Text = "1XMR = "+fiatPrice;
            tb_FiatCurrency.Foreground = green;
            tb_BTCCurrency.Text = btcPrice;
            tb_BTCCurrency.Foreground = green;
            tb_ETHCurrency.Text = ethPrice;
            tb_ETHCurrency.Foreground = green;
        }

        private void UpdateMinerCountInfo()
        {
            SolidColorBrush green = new SolidColorBrush(Colors.LightGreen);
            tb_ActiveMiners.Text = currentTemplate.ConnectedMiners.ToString();
            tb_ActiveMiners.Foreground = green;


            if (currentTemplate.ConnectedMiners != 0)
            {
                int avgHr = (int)(currentTemplate.PoolHashrate / currentTemplate.ConnectedMiners);

                tb_AvgHRPerMiner.Text = StatsFetcher.ParseHashrate(avgHr);
                tb_AvgHRPerMiner.Foreground = green;
            }
            else
                tb_AvgHRPerMiner.Text = "0 H/S";
        }

        private void UpdateTotalBlocksFound()
        {
            SolidColorBrush green = new SolidColorBrush(Colors.LightGreen);
            tb_BlocksFoundByPool.Text = currentTemplate.PoolBlocksFound.ToString();
            tb_BlocksFoundByPool.Foreground = green;
            tb_BlockchainHeight.Text = currentTemplate.NetworkHeight.ToString();
            tb_BlockchainHeight.Foreground = green;
        }

        private void UpdateBlockData()
        {
            tb_BlockDataHeader.Text = "Recent Block Data ---- (Last found: " + DateTimeOffset.FromUnixTimeSeconds(currentTemplate.LastBlockFound) + ") UTC+1 Time";
            tb_BlockDataHeader.Foreground = new SolidColorBrush(Colors.White);

            dg_BlockData.Columns.Clear();
            dg_BlockData.AutoGenerateColumns = false;

            List<Block> recentBlocks = StatsFetcher.GetBlocksMined().ToList();

            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = "Block Number        ";
            column.Binding = new Binding("BlockNumber");
            dg_BlockData.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Hash        ";
            column.Binding = new Binding("Hash");
            dg_BlockData.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Status        ";
            column.Binding = new Binding("Status");
            dg_BlockData.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Block Reward        ";
            column.Binding = new Binding("BlockReward");
            dg_BlockData.Columns.Add(column);

            column = new DataGridTextColumn();
            column.Header = "Timestamp        ";
            column.Binding = new Binding("TimeStamp");
            dg_BlockData.Columns.Add(column);

            dg_BlockData.ItemsSource = null;
            dg_BlockData.ItemsSource = recentBlocks;
        }

        private void btn_Address1Edit_Click(object sender, RoutedEventArgs e)
        {
            EditAddress(1, tb_Address1.Text);
        }

        private void btn_Address2Edit_Click(object sender, RoutedEventArgs e)
        {
            EditAddress(2, tb_Address2.Text);
        }

        private void btn_Address3Edit_Click(object sender, RoutedEventArgs e)
        {
            EditAddress(3, tb_Address3.Text);
        }

        private void EditAddress(int addressNumber, string address)
        {
            stopRefresh = true;
            AddressWindow addressWindow = new AddressWindow(this, addressNumber, address);
            addressWindow.ShowDialog();
            stopRefresh = false;
        }

        private void btn_Address1Remove_Click(object sender, RoutedEventArgs e)
        {
            RemoveAddress(1);
        }

        private void btn_Address2Remove_Click(object sender, RoutedEventArgs e)
        {
            RemoveAddress(2);
        }

        private void btn_Address3Remove_Click(object sender, RoutedEventArgs e)
        {
            RemoveAddress(3);
        }

        private void RemoveAddress(int addressNumber)
        {
            if (addressNumber == 1)
            {
                tb_Address1.Text = "-";
                tb_Address1Hashrate.Text = "-";
                tb_Address1BalanceNumber.Text = "-";
                tb_Address1BalanceBarCurrent.Text = "";
                tb_Address1BalanceBarRemaining.Text = "";
                tb_Address1ActiveMiners.Text = "-";
                tb_Address1PayoutCount.Text = "-";
                tb_Address1BoostETA.Text = "-";
            }

            if (addressNumber == 2)
            {
                tb_Address2.Text = "-";
                tb_Address2Hashrate.Text = "-";
                tb_Address2BalanceNumber.Text = "-";
                tb_Address2BalanceBarCurrent.Text = "";
                tb_Address2BalanceBarRemaining.Text = "";
                tb_Address2ActiveMiners.Text = "-";
                tb_Address2PayoutCount.Text = "-";
                tb_Address2BoostETA.Text = "-";
            }

            if (addressNumber == 3)
            {
                tb_Address3.Text = "-";
                tb_Address3Hashrate.Text = "-";
                tb_Address3BalanceNumber.Text = "-";
                tb_Address3BalanceBarCurrent.Text = "";
                tb_Address3BalanceBarRemaining.Text = "";
                tb_Address3ActiveMiners.Text = "-";
                tb_Address3PayoutCount.Text = "-";
                tb_Address3BoostETA.Text = "-";
            }

            string[] addresses = File.ReadAllLines(ADDRESS_CONFIG_FILE);
            addresses[addressNumber - 1] = "-";
            File.WriteAllLines(ADDRESS_CONFIG_FILE, addresses);
        }

        private void btn_Address1TXReport_Click(object sender, RoutedEventArgs e)
        {
            ShowTXData(tb_Address1.Text);
        }

        private void btn_Address2TXReport_Click(object sender, RoutedEventArgs e)
        {
            ShowTXData(tb_Address2.Text);
        }

        private void btn_Address3TXReport_Click(object sender, RoutedEventArgs e)
        {
            ShowTXData(tb_Address3.Text);
        }

        private void btn_Address1BoostReport_Click(object sender, RoutedEventArgs e)
        {
            ShowBoostData(tb_Address1.Text);
        }

        private void btn_Address2BoostReport_Click(object sender, RoutedEventArgs e)
        {
            ShowBoostData(tb_Address2.Text);
        }

        private void btn_Address3BoostReport_Click(object sender, RoutedEventArgs e)
        {
            ShowBoostData(tb_Address3.Text);
        }

        private void ShowTXData(string address)
        {
            stopRefresh = true;
            new TransactionData(address).ShowDialog();
            stopRefresh = false;
        }

        private void btn_DevInfo_Click(object sender, RoutedEventArgs e)
        {
            stopRefresh = true;
            new DeveloperInfoWindow().ShowDialog();
            stopRefresh = false;
        }

        private void btn_Settings_Click(object sender, RoutedEventArgs e)
        {
            stopRefresh = true;
            new OptionsWindow(OPTIONS_CONFIG_FILE, CURRENCY_CONFIG_FILE, this).ShowDialog();
            stopRefresh = false;
        }

        private void ShowBoostData(string address)
        {
            stopRefresh = true;
            new BoostReport(address).ShowDialog();
            stopRefresh = false;
        }
    }
}
