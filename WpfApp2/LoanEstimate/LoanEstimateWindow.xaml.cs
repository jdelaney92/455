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
                BorrowerAndLoanInformation borrowData = MapInput_BorrowerInformation();
                ClosingCostInformation closingData = MapInput_ClosingInformation();
                ValidationDetail validation = ValidationService.ValidateLoanInformation(borrowData);
                MapOutput_BorrowerInformation(borrowData);
                MapOutput_LoanCosts(closingData);
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
