using System;
using System.Collections.Generic;
using TVA_CCU.Models;


namespace TVA_CCU
{
    public static class ValidationService
    {
      public static ValidationDetail ValidateLoanInformation(BorrowerAndLoanInformation input)
        {
            ValidationDetail result = new ValidationDetail();

            result.ErrorMessages = new List<string>();

            if(string.IsNullOrEmpty(input.Date))
            {
                result.IsValid = false;
                result.ErrorMessages.Add("Date is empty");
            }

            if(string.IsNullOrWhiteSpace(input.ApplicantName))
            {
                result.IsValid = false;
                result.ErrorMessages.Add("Applicant Name was not entered.");
            }
            
            if(string.IsNullOrWhiteSpace(input.ApplicantStreetAddress))
            {
                result.IsValid = false;
                result.ErrorMessages.Add("Applicant Street Address was not entered.");
            }

            if (string.IsNullOrWhiteSpace(input.ApplicantCityStateZip))
            {
                result.IsValid = false;
                result.ErrorMessages.Add("Applicant City, State, Zip was not entered");
            }

            if (string.IsNullOrWhiteSpace(input.PbmStreetAddress))
            {
                result.IsValid = false;
                result.ErrorMessages.Add("Property Being Mortgaged - Street Address was not entered.");
            }

            if (string.IsNullOrWhiteSpace(input.SalePrice))
            {
                result.IsValid = false;
                result.ErrorMessages.Add("Sale Price was not entered.");
            }




            return result;
        }
    }
}

