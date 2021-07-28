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
using System.IO;
using System.Windows.Forms;

namespace XVB_GUI
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        private string _configPath;
        private string _currencyConfigPath;
        private MainWindow _mainWindow;
        public OptionsWindow(string configPath, string currencyConfigPath, MainWindow mainWindow)
        {
            InitializeComponent();
            _configPath = configPath;
            _currencyConfigPath = currencyConfigPath;
            _mainWindow = mainWindow;
            cb_SelectCurrency.ItemsSource = null;
            cb_SelectCurrency.ItemsSource = Enum.GetValues(typeof(StatsFetcher.Currency));
            cb_SelectCurrency.SelectedIndex = 0;
            tb_PathDisplay.Text = "";
            string[] config = File.ReadAllLines(_configPath);
            if (bool.Parse(config[0]))
            {
                chb_LogData.IsChecked = true;
                tb_PathDisplay.Text = config[1];
            }

            cb_SelectCurrency.SelectedItem = (StatsFetcher.Currency)Enum.Parse(typeof(StatsFetcher.Currency), File.ReadAllText(_currencyConfigPath));
        }

        private void btn_QuitNoSave_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_SaveExit_Click(object sender, RoutedEventArgs e)
        {
            if (tb_PathDisplay.Text != "")
            {
                if (!Directory.Exists(tb_PathDisplay.Text))
                {
                    System.Windows.MessageBox.Show("Selected folder does not exist", "Nonexistant folder path", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (tb_PathDisplay.Text == "" && chb_LogData.IsChecked == true)
            {
                System.Windows.MessageBox.Show("You must select a file path if you wish to log the data", "File path not selected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (tb_PathDisplay.Text == "")
                    File.WriteAllText(_configPath, chb_LogData.IsChecked + "\n" + "_");
                else
                    File.WriteAllText(_configPath, chb_LogData.IsChecked + "\n" + tb_PathDisplay.Text);

                File.WriteAllText(_currencyConfigPath, cb_SelectCurrency.SelectedItem.ToString());
                _mainWindow.currency = (StatsFetcher.Currency)cb_SelectCurrency.SelectedItem;
                _mainWindow.logData = (bool)chb_LogData.IsChecked;
                _mainWindow.loggerFilePath = tb_PathDisplay.Text;
                _mainWindow.UpdateExchangeRates();
                Close();
            }
        }

        private void btn_SelectFilePath_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                tb_PathDisplay.Text = dialog.SelectedPath;
            }
        }
    }
}
