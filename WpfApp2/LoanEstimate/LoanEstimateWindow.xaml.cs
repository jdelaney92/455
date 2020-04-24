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
                MapOutput_loanSchedule();
                MapOutput_LoanCosts(closingData);
                MapOutput_ProjectedPayments(borrowData);
                MapOutput_APTable(borrowData);
                MapOuput_AIRTable(borrowData);
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
                Date2 = date.SelectedDate ?? DateTime.Now,
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
                Margin = margin.Text,
                EstimatedTaxesAndInsurance = estimatedTax_Insurance.Text
            };       
        }
           
        private void MapOutput_BorrowerInformation(BorrowerAndLoanInformation obj)
        {
            if (inYears.IsChecked == true || inMonths.IsChecked == true)
            {

                if (inYears.IsChecked == true)
                {
                    loanTermOutput.Content = loanTerm.Text + " years";
                }
                else if (inMonths.IsChecked == true)
                {
                    loanTermOutput.Content = loanTerm.Text + " months";
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
            loanIDOutput2.Content = obj.LoanID;
            DateTime date2 = date.SelectedDate ?? DateTime.Now;
            rateLockDateOutput.Content = date2.AddDays(45);
            expireDateOutput.Content = date2.AddDays(15);
            //estimatedTax_Insurance.Content = obj.EstimatedTaxesAndInsurance;
            loanAmountOutput.Content = obj.LoanAmount;
            interestRateOutput.Content = obj.InterestRate;
            monthlyPrincipleOutput.Content = PaymentCalculation(Convert.ToDouble(obj.LoanAmount), Convert.ToDouble(obj.InterestRate), Convert.ToInt32(obj.LoanTerm));
            loanTermsMax.Content = PaymentCalculation(Convert.ToDouble(obj.LoanAmount), Convert.ToDouble(obj.InterestRate + 6), Convert.ToInt32(obj.LoanTerm));
            maxInterest.Content = Convert.ToDouble(obj.InterestRate) + 6 + "%";
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

        private void MapOutput_APTable(BorrowerAndLoanInformation obj)
        {
            firstChange.Content = PaymentCalculation(Convert.ToDouble(obj.LoanAmount), Convert.ToDouble(obj.InterestRate + 2), Convert.ToInt32(obj.LoanTerm));
            maxPayment.Content = PaymentCalculation(Convert.ToDouble(obj.LoanAmount), Convert.ToDouble(obj.InterestRate + 6), Convert.ToInt32(obj.LoanTerm));
        }

        private void MapOutput_loanSchedule()
        {
            int periods, LoanTerm, num_pay = 12;
            decimal balance, rate, extraPay;
            var Date = date.SelectedDate ?? DateTime.Now;

            int.TryParse(loanTerm.Text, out LoanTerm);
            decimal.TryParse(loanAmount.Text, out balance);
            decimal.TryParse(interestRate.Text, out rate);
            decimal.TryParse(ExtraPayment.Text, out extraPay);


            periods = LoanTerm * num_pay;

            var loan = new LoanSchedule(balance, extraPay, periods, rate, Date, 12 / num_pay) as LoanSchedule;
            loanGrid.ItemsSource = loan.list;
            Loan_Summary.DataContext = loan.list2;
            LoanInf.DataContext = loan.list1;

        }
        private void MapOuput_AIRTable(BorrowerAndLoanInformation obj1)
        {
            indexMarginOutput.Content = obj1.Margin;
            initialRateOutput.Content = obj1.InterestRate;
            minInterestRateOutput.Content = obj1.InterestRate;
           
        }

        // if you delete this it yells
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        
        //function for importing file
        private void InputData_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

        }

        //function for exporting file
        private void ExportData_Click(object sender, RoutedEventArgs e)
        {
            TextWriter txt = new StreamWriter("C:\\test\\test.txt");
            //txt.Write();
            txt.Close();
        }

        //calculates the amount owed per month of a loan
        //takes in the amount of the loan left to pay, the current interest rate, and the term left of the loan
        private double PaymentCalculation(double amount, double interest, int term)
        {
            if (inYears.IsChecked == true)
            {
                interest = (interest / 100) / 12;
                double power = (-1 * term * 12);
                double payment = Math.Round((amount * interest) / (1 - (Math.Pow(interest + 1, power))), 2);
                return payment;
            }
            else
            {
                interest = (interest / 100);
                double power = (-1 * term);
                double payment = Math.Round((amount * interest) / (1 - (Math.Pow(interest + 1, power))), 2);
                return payment;
            }
        }
        
        private void ExtraPay_Change(object sender, RoutedEventArgs e)
        {
            MapOutput_loanSchedule();
        }

        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
   
}
