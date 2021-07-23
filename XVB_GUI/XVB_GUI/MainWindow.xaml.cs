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

namespace XVB_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //TestApiCall();
            TestPoolStats();
        }

        public void TestApiCall()
        {
            StatsApiCaller apiCall = new StatsApiCaller("447ywns3EeHas2tp5SNdecY79kCcnpKP628XavFhwhgmRYWPBreiiGNH4FTbtog7VyMsJqfjATP21GqDjAbNScYP1D451qk");
            PoolApiResponse pr = apiCall.Query();

            HttpClient http = new HttpClient();
            HttpResponseMessage res = http.GetAsync("https://xmrvsbeast.com/boost_hash.html").GetAwaiter().GetResult();
            string str = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return;
        }

        public void TestPoolStats()
        {
            //string raffleHr = PoolStatsFetcher.GetRaffleHashrate();
            //string boostHr = PoolStatsFetcher.GetBoostHashrate();
            //Block[] blocks = PoolStatsFetcher.GetBlocksMined();
            //Transaction[] payouts = PoolStatsFetcher.GetPayouts("447ywns3EeHas2tp5SNdecY79kCcnpKP628XavFhwhgmRYWPBreiiGNH4FTbtog7VyMsJqfjATP21GqDjAbNScYP1D451qk");
            int hoursToBoost = PoolStatsFetcher.GetEstimatedBoostTime("447ywns3EeHas2tp5SNdecY79kCcnpKP628XavFhwhgmRYWPBreiiGNH4FTbtog7VyMsJqfjATP21GqDjAbNScYP1D451qk");
            return;
        }
    }
}
