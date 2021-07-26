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
        public const int ACTIVE_WINDOW_REFRESH_RATE = 60000;
        public const int INACTIVE_WINDOW_REFRESH_RATE = 5000;
        public const string ADDRESS_CONFIG_FILE = "../../addresses.txt";
        public const string CURRENCY_CONFIG_FILE = "../../currency.txt";
        public const int BAR_SIZE = 10;

        public int mainRefreshRate = ACTIVE_WINDOW_REFRESH_RATE;
        public StatsFetcher.Currency currency;
        public MainWindow()
        {
            InitializeComponent();
            TLUpdateMainStats();
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

            string[] addresses = File.ReadAllLines(ADDRESS_CONFIG_FILE);
            tb_Address1.Text = addresses[0];
            tb_Address2.Text = addresses[1];
            tb_Address3.Text = addresses[2];

            RefreshStats(cancellation);
        }
        public async Task RefreshStats(CancellationToken cancellationToken)
        {
            while (true)
            {
                TLUpdateMainStats();
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

        public void TLUpdateMainStats()
        {
            //PoolApiResponse res = StatsApiCaller.Query("447ywns3EeHas2tp5SNdecY79kCcnpKP628XavFhwhgmRYWPBreiiGNH4FTbtog7VyMsJqfjATP21GqDjAbNScYP1D451qk");
            UpdateStatusBar();

            try
            {
                UpdateTopBar();
                UpdateAddresses();
                UpdateExchangeRates();
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
            PoolApiResponse apiResponse = StatsApiCaller.Query();
            SolidColorBrush green = new SolidColorBrush(Colors.LightGreen);

            tb_NetworkHashrate.Text = StatsFetcher.ParseHashrate(apiResponse.NetworkHashrate);
            tb_NetworkHashrate.Foreground = green;

            tb_PoolHashrate.Text = StatsFetcher.ParseHashrate(apiResponse.PoolHashrate);
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

        private void UpdateAddresses()
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
                tb_Address1BoostETA.Text = "~"+StatsFetcher.GetEstimatedMinerBoostTime(address1).ToString() + " hours";

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
                tb_Address2BoostETA.Text = "~" + StatsFetcher.GetEstimatedMinerBoostTime(address2).ToString() + " hours";

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
                tb_Address3BoostETA.Text = "~" + StatsFetcher.GetEstimatedMinerBoostTime(address3).ToString() + " hours";

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

        private void UpdateExchangeRates()
        {
            SolidColorBrush green = new SolidColorBrush(Colors.LightGreen);
            string fiatPrice = StatsFetcher.GetMoneroPrice(currency);
            string btcPrice = StatsFetcher.GetMoneroPrice(StatsFetcher.Currency.BTC);
            string ethPrice = StatsFetcher.GetMoneroPrice(StatsFetcher.Currency.ETH);

            tb_FiatCurrency.Text = fiatPrice;
            tb_FiatCurrency.Foreground = green;
            tb_BTCCurrency.Text = btcPrice;
            tb_BTCCurrency.Foreground = green;
            tb_ETHCurrency.Text = ethPrice;
            tb_ETHCurrency.Foreground = green;
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
            AddressWindow addressWindow = new AddressWindow(this, addressNumber, address);
            addressWindow.ShowDialog();
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
    }
}
