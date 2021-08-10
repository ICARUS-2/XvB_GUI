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

namespace XVB_GUI
{
    /// <summary>
    /// Interaction logic for AddressWindow.xaml
    /// </summary>
    public partial class AddressWindow : Window
    {
        /// <summary>
        /// Maps 1-based index to the address textblocks that are modified
        /// </summary>
        Dictionary<int, TextBlock> keyValuePairs;

        /// <summary>
        /// The index of the address being modified
        /// </summary>
        int _addressIndex;

        /// <summary>
        /// The wallet address to be modified
        /// </summary>
        string _addressVal;

        /// <summary>
        /// Handle of the app's main window to mo
        /// </summary>
        MainWindow _mainWindow;

        /// <summary>
        /// Constructor takes a MainWindow Handle, address index, and the address to modify and assigns them.
        /// </summary>
        /// <param name="mainWindow">The handle of the application's main window</param>
        /// <param name="addressIndex">Represents address 1, 2, or 3 that will be modified</param>
        /// <param name="addressVal">The Monero address that will be modified on the page</param>
        public AddressWindow(MainWindow mainWindow, int addressIndex, string addressVal)
        {
            InitializeComponent();
            keyValuePairs = new Dictionary<int, TextBlock>();
            keyValuePairs.Add(1, mainWindow.tb_Address1);
            keyValuePairs.Add(2, mainWindow.tb_Address2);
            keyValuePairs.Add(3, mainWindow.tb_Address3);

            _addressIndex = addressIndex;
            if (addressVal != "-")
                tbx_Address.Text = addressVal;
            _addressVal = addressVal;
            _mainWindow = mainWindow;
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_Address.Text == "-" || tbx_Address.Text == String.Empty)
            {
                MessageBox.Show("Invalid Address");
                return;
            }

            TextBlock blockToModify = keyValuePairs[_addressIndex];
            blockToModify.Text = tbx_Address.Text;
            string[] addrsConfigData = File.ReadAllLines(MainWindow.ADDRESS_CONFIG_FILE);

            addrsConfigData[_addressIndex - 1] = blockToModify.Text;
            File.WriteAllLines(MainWindow.ADDRESS_CONFIG_FILE, addrsConfigData);

            _mainWindow.UpdateAddresses();

            Close();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
