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
using Microsoft.Win32;

namespace WpfApp2.Excel1
{
    /// <summary>
    /// Interaction logic for Excel1InputWindow.xaml
    /// </summary>
    public partial class Excel1InputWindow : Window
    {
        public Excel1InputWindow()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Output.IsSelected = true;

            if (date.Text.Length != 0)
            {
                dateIssued.Content = date.Text;
            }

            if (applicantName.Text.Length != 0)
            {
                applicantNameOutput.Content = applicantName.Text;
                applicantStreetOutput.Content = applicantStreetAddress.Text;
                applicantCityOutput.Content = applicantC_S_Z.Text;
            }
            
            if (propertyAddress.Text.Length != 0)
            {
                propertyStreetOutput.Content = propertyAddress.Text;
                propertyCityOutput.Content = propertyC_S_Z.Text;
            }
            
            if (salePrice.Text.Length != 0)
            {
                salePriceOutput.Content = salePrice.Text;
            }
            
            if (loanTerm.Text.Length != 0)
            {
                loanTermOutput.Content = loanTerm.Text;
                if (inYears.IsChecked == true)
                {
                    loanTermTimeOutput.Content = "in years";
                }
                else if (inMonths.IsChecked == true)
                {
                    loanTermTimeOutput.Content = "in months";
                }
            }

            if (purpose.Text.Length != 0)
            {
                purposeOutput.Content = purpose.Text;
            }

            if (loanID.Text.Length != 0)
            {
                loanIDOutput.Content = loanID.Text;
            }

            if (rateLockYes.IsChecked == true)
            {
                //rateLockDateOutput.Content = need to do date addition here. According to excel sheet it is date issued + 45 days
            }

            if (loanAmount.Text.Length != 0)
            {
                loanAmountOutput.Content = loanAmount.Text;
            }

            if (interestRate.Text.Length != 0)
            {
                interestRateOutput.Content = interestRate.Text + '%';
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InputData_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

        }

        
        
    }
}
