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

namespace XVB_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int DEFAULT_REFRESH_RATE = 4000;
        public int _refreshRate = 4000;
        public MainWindow()
        {
            InitializeComponent();
            UpdateStats();
            CancellationToken cancellation = new CancellationToken();
            StatsFetcher.GetMoneroPrice(StatsFetcher.Currency.BTC);
            StartTimer(cancellation);
        }
        public async Task StartTimer(CancellationToken cancellationToken)
        {
            while (true)
            {
                UpdateStats();
                await Task.Delay(_refreshRate, cancellationToken);
            }
        }

        public void UpdateStats()
        {
            //PoolApiResponse res = StatsApiCaller.Query("447ywns3EeHas2tp5SNdecY79kCcnpKP628XavFhwhgmRYWPBreiiGNH4FTbtog7VyMsJqfjATP21GqDjAbNScYP1D451qk");
            UpdateStatusBar();

            if (!StatsFetcher.IsConnected())
            {
                DisplayErrors();
                return;
            }

            UpdateTopBar();
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


            tb_BoostHashrate.Text = StatsFetcher.GetBoostHashrate() + "->";
            tb_BoostHashrate.Foreground = green;
            tb_BoostAddress.Text = StatsFetcher.GetBoostHashrateAddress();
            tb_BoostAddressTime.Text = StatsFetcher.GetBoostHashrateDuration() + "m Remaining";

            tb_BonusHashrate.Text = StatsFetcher.GetRaffleHashrate();
            tb_BonusHashrate.Foreground = green;

            tb_TimeBonusHashrate.Text = StatsFetcher.GetTimeRaffleHashrate() + "->";
            tb_TimeBonusHashrate.Foreground = green;
            tb_TimeBonusAddress.Text = StatsFetcher.GetTimeRaffleAddress();
            tb_TimeBonusTime.Text = StatsFetcher.GetTimeRaffleDuration() + "m Remaining";
        }
    }
}
