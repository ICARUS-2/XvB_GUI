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
        MainWindow _mainWindow;
        Dictionary<int, TextBlock> keyValuePairs;
        int _addressNumber;
        public AddressWindow(MainWindow mainWindow, int addressNumber, string content)
        {
            InitializeComponent();
            keyValuePairs = new Dictionary<int, TextBlock>();
            keyValuePairs.Add(1, mainWindow.tb_Address1);
            keyValuePairs.Add(2, mainWindow.tb_Address2);
            keyValuePairs.Add(3, mainWindow.tb_Address3);

            _addressNumber = addressNumber;
            tbx_Address.Text = content;
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_Address.Text == "-" || tbx_Address.Text == "")
            {
                MessageBox.Show("Invalid Address");
                return;
            }

            TextBlock blockToModify = keyValuePairs[_addressNumber];
            blockToModify.Text = tbx_Address.Text;
            string[] addrsConfigData = File.ReadAllLines(MainWindow.ADDRESS_CONFIG_FILE);

            addrsConfigData[_addressNumber - 1] = blockToModify.Text;
            File.WriteAllLines(MainWindow.ADDRESS_CONFIG_FILE, addrsConfigData);

            Close();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
