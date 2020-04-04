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
using System.Windows.Navigation;
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
                BorrowerAndLoanInformation borrowData = MapInput_BorrowerInformation();
                ClosingCostInformation closingData = MapInput_ClosingInformation();
                ValidationDetail validation = ValidationService.ValidateLoanInformation(borrowData);
                MapOutput_BorrowerInformation(borrowData);
                MapOutput_loanSchedule(borrowData);
                MapOutput_LoanCosts(closingData);
                MapOutput_ProjectedPayments(borrowData);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                MessageBox.Show(message,"Error");
                
            }

            

 
        }

        private BorrowerAndLoanInformation MapInput_BorrowerInformation()
        {
            return new BorrowerAndLoanInformation
            {
                Date = date.Text,
                Date2 = date.SelectedDate??DateTime.Now,
                ApplicantName = applicantName.Text,
                ApplicantStreetAddress = applicantStreetAddress.Text,
                ApplicantCityStateZip = applicantC_S_Z.Text,
                PbmStreetAddress = propertyAddress.Text,
                PbmCityStateZip = propertyC_S_Z.Text,
                SalePrice = salePrice.Text,
                LoanTerm = loanTerm.Text,
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
            //estimatedTax_Insurance.Content = obj.EstimatedTaxesAndInsurance;
            loanAmountOutput.Content = obj.LoanAmount;
            interestRateOutput.Content = obj.InterestRate;
            monthlyPrincipleOutput.Content = PaymentCalculation(Convert.ToDouble(obj.LoanAmount), Convert.ToDouble(obj.InterestRate), Convert.ToInt32(obj.LoanTerm));

        }
        private ClosingCostInformation MapInput_ClosingInformation()
        {
            return new ClosingCostInformation
            {
                OriginationFee = Int32.Parse(originationFee.Text),
                ProcessingFee = Int32.Parse(processingFee.Text),
                AppraisalFee = Int32.Parse(appraisalFee.Text),
                CreditReportFee = Int32.Parse(creditReportFee.Text),
                FloodDeterminationFee = Int32.Parse(floodDeterminationFee.Text),
                LendersAttorneyFee = Int32.Parse(lendersAttorneyFee.Text),
                TaxServiceFee = Int32.Parse(taxServiceFee.Text),
                PestInspectionFee = Int32.Parse(pestInspectionFee.Text),
                SurveyFee = Int32.Parse(surveyFee.Text),
                TitleCourierFee = Int32.Parse(titleCourierFee.Text),
                TitleLendersPolicy = Int32.Parse(titleLendersPolicy.Text),
                TitleOwnersPolicy = Int32.Parse(titleOwnersPolicy.Text),
                TitleSettlementAgent = Int32.Parse(titleSettlementAgent.Text),
                TitleSearch = Int32.Parse(titleSearch.Text),
                RecordingFees = Int32.Parse(recordingFees.Text),
                TransferTaxes = Int32.Parse(transferTaxes.Text),
                HomeownersInsurance = Int32.Parse(homeownersInsurance.Text),
                MortgageInsurance = Int32.Parse(mortgageInsurance.Text),
                PrepaidInterest = Int32.Parse(prepaidInterest.Text),
                HoaFees = Int32.Parse(hoaFees.Text),
                PropertyTaxes = Int32.Parse(propertyTaxes.Text),
                DownpaymentFromBorrower = Int32.Parse(borrowerDownpayment.Text),
                EarnestMoney = Int32.Parse(earnestMoney.Text),
                SellerCreditForPurchase = Int32.Parse(sellerCredit.Text)
            };
        }

        private void MapOutput_LoanCosts(ClosingCostInformation obj)
        {

            int origCharges = obj.OriginationFee + obj.ProcessingFee;
            int servYouCant = obj.AppraisalFee + obj.CreditReportFee + obj.FloodDeterminationFee
                                               + obj.LendersAttorneyFee + obj.TaxServiceFee;
            int servYouCan = obj.PestInspectionFee + obj.SurveyFee + obj.TitleCourierFee
                                               + obj.TitleLendersPolicy + obj.TitleSettlementAgent + +obj.TitleSearch;
            originationChargesOutput.Content = origCharges;
            originationFeeOutput.Content = obj.OriginationFee;
            processingFeeOutput.Content = obj.ProcessingFee;
            serviceYouCantShopForOutput.Content = servYouCant;
            appraisalFeeOutput.Content = obj.AppraisalFee;
            creditReportFeeOutput.Content = obj.CreditReportFee;
            floodDeterminationFeeOutput.Content = obj.FloodDeterminationFee;
            lendersAttorneyFeeOutput.Content = obj.LendersAttorneyFee;
            taxServiceFeeOutput.Content = obj.TaxServiceFee;
            serviceYouCanShopForOutput.Content = servYouCan;
            pestInspectionFeeOutput.Content = obj.PestInspectionFee;
            surveyFeeOutput.Content = obj.SurveyFee;
            titleCourierFeeOutput.Content = obj.TitleCourierFee;
            titleLendersPolicyOutput.Content = obj.TitleLendersPolicy;
            titleSettlementAgentOutput.Content = obj.TitleSettlementAgent;
            titleSearchOutput.Content = obj.TitleSearch;
            totalLoanCostsOutput.Content = origCharges + servYouCant + servYouCan;
        }

        private void MapOutput_ProjectedPayments(BorrowerAndLoanInformation obj)
        {
            minYearOne.Content = PaymentCalculation(Convert.ToDouble(obj.LoanAmount), Convert.ToDouble(obj.InterestRate), Convert.ToInt32(obj.LoanTerm));
            minYearSix.Content = PaymentCalculation(Convert.ToDouble(obj.LoanAmount), Convert.ToDouble(obj.InterestRate), Convert.ToInt32(obj.LoanTerm));
            minYearEleven.Content = PaymentCalculation(Convert.ToDouble(obj.LoanAmount), Convert.ToDouble(obj.InterestRate), Convert.ToInt32(obj.LoanTerm));
            minYearEnd.Content = PaymentCalculation(Convert.ToDouble(obj.LoanAmount), Convert.ToDouble(obj.InterestRate), Convert.ToInt32(obj.LoanTerm));
            maxYearSix.Content = PaymentCalculation(Convert.ToDouble(obj.LoanAmount), Convert.ToDouble(obj.InterestRate+2), Convert.ToInt32(obj.LoanTerm));
            maxYearEleven.Content = PaymentCalculation(Convert.ToDouble(obj.LoanAmount), Convert.ToDouble(obj.InterestRate+4), Convert.ToInt32(obj.LoanTerm));
            maxYearEnd.Content = PaymentCalculation(Convert.ToDouble(obj.LoanAmount), Convert.ToDouble(obj.InterestRate+6), Convert.ToInt32(obj.LoanTerm));
        }


        private void MapOutput_loanSchedule(BorrowerAndLoanInformation obj)
        {
            int periods, loanTerm, num_pay = 12;
            decimal balance, rate;
            var date = obj.Date2;

            int.TryParse(obj.LoanTerm, out loanTerm);
            decimal.TryParse(obj.LoanAmount, out balance);
            decimal.TryParse(obj.InterestRate, out rate);


            periods = loanTerm * num_pay;

            var loan = new LoanSchedule(balance, periods, rate, date, 12 / num_pay) as LoanSchedule;
            loanGrid.ItemsSource = loan.list;
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

        //calculates the amount owed per month of a loan
        //takes in the amount of the loan left to pay, the current interest rate, and the term left of the loan
        private double PaymentCalculation(double amount, double interest, int term)
        {
            interest = (interest / 100) / 12;
            double power = (-1 * term * 12);
            double payment = Math.Round((amount * interest) / (1 - (Math.Pow(interest+1, power))),2);
            return payment;

        }

    }
    public class ColorConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            var item = value as ListViewItem;

            var view = ItemsControl.ItemsControlFromItemContainer(item) as ListView;

            var index = view.ItemContainerGenerator.IndexFromContainer(item);

            if (index % 2 == 0) return Brushes.White;
            else return Brushes.Green;
        }
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
