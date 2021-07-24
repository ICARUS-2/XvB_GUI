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
        public const int REFRESH_RATE = 5000;
        public MainWindow()
        {
            InitializeComponent();
            UpdateStats();
            CancellationToken cancellation = new CancellationToken();
            PoolStatsFetcher.GetBoostHashrateAddress();
            StartTimer(cancellation);
        }
        public async Task StartTimer(CancellationToken cancellationToken)
        {
            while (true)
            {
                UpdateStats();
                await Task.Delay(REFRESH_RATE, cancellationToken);
            }
        }

        public void UpdateStats()
        {
            StatsApiCaller stats = new StatsApiCaller("447ywns3EeHas2tp5SNdecY79kCcnpKP628XavFhwhgmRYWPBreiiGNH4FTbtog7VyMsJqfjATP21GqDjAbNScYP1D451qk");
            PoolApiResponse res = stats.Query();
        }
    }
}
