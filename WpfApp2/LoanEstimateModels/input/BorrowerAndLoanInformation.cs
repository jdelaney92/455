using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVA_CCU.Models
{
    public class BorrowerAndLoanInformation
    {
        public string Date { get; set; }

        public string ApplicantName { get; set; }

        public string ApplicantStreetAddress { get; set; }

        public string ApplicantCityStateZip { get; set; }

        public string PbmStreetAddress { get; set; }

        public string PbmCityStateZip { get; set; }

        public string SalePrice { get; set; }

        public string LoanTerm { get; set; }

        public string Purpose { get; set; }

        public string LoanID { get; set; }

        public string LoanAmount { get; set; }

        public string InterestRate { get; set; }

        public string EstimatedTaxesAndInsurance { get; set; }
    }
}
