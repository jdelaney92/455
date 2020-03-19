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
using TVA_CCU.Models;


namespace TVA_CCU.Excel1
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

            try
            {
                BorrowerAndLoanInformation data = MapInput_BorrowerInformation();
                ValidationDetail validation = ValidationService.ValidateLoanInformation(data);
                MapOutput_BorrowerInformation(data);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

 
        }

        private BorrowerAndLoanInformation MapInput_BorrowerInformation()
        {       
            return new BorrowerAndLoanInformation
            {
                Date = date.Text,
                ApplicantName = applicantName.Text,
                ApplicantStreetAddress = applicantStreetAddress.Text,
                ApplicantCityStateZip = applicantC_S_Z.Text,
                PbmStreetAddress = propertyAddress.Text,
                PbmCityStateZip = propertyC_S_Z.Text,
                SalePrice = salePrice.Text,
                LoanTerm = "",
                Purpose = purpose.Text,
                LoanID = loanID.Text,
                LoanAmount = loanAmount.Text,
                InterestRate = interestRate.Text,
                EstimatedTaxesAndInsurance = estimatedTax_Insurance.Text
            };       
        }
           
        private void MapOutput_BorrowerInformation(BorrowerAndLoanInformation obj)
        {
            if (inYears.IsChecked == true || inMonths.IsChecked == true)
            {

                if (inYears.IsChecked == true)
                {
                    loanTermOutput.Content = "in years";
                }
                else if (inMonths.IsChecked == true)
                {
                    loanTermOutput.Content = "in months";
                }
            }
            else
            {
                loanTermOutput.Content = "'In years' or 'In months' not selected";
            }

            dateIssued.Content = obj.Date;
            applicantNameOutput.Content = obj.ApplicantName;
            applicantStreetOutput.Content = obj.ApplicantStreetAddress;
            applicantCityOutput.Content = obj.ApplicantStreetAddress;
            propertyStreetOutput.Content = obj.PbmStreetAddress;
            propertyCityOutput.Content = obj.PbmCityStateZip;
            salePriceOutput.Content = obj.SalePrice;
            purposeOutput.Content = obj.Purpose;
            loanIDOutput.Content = obj.LoanID;
            loanAmountOutput.Content = obj.LoanAmount;
            interestRateOutput.Content = obj.InterestRate;
            //estimatedTax_Insurance.Content = obj.EstimatedTaxesAndInsurance;
        }

        
        // if you delete this it yells
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
