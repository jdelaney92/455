using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Collections;


namespace TVA_CCU.Excel1
{
    public class LoanSchedule
    {
        public List<LoanType> list;
        public List<Loan_Summary2> list2;
        public List<Loan_Summary> list1;
        public LoanSchedule(decimal balance, decimal extraPay, int months, decimal rate, DateTime date, int next) 
        { 
       
            list = new List<LoanType>();
            list2 = new List<Loan_Summary2>();
            list1 = new List<Loan_Summary>();
            getLoanInf(balance,months, rate, date);
            Calculate(balance,extraPay,months,rate, date, next);
        }
        protected void getLoanInf(decimal balance,int months, decimal rate, DateTime date)
        {
            list1.Add(new Loan_Summary()
            {
                AmountValue = balance,
                LoanTerm = months/12,
                NumPay = 12,
                StartDate = date.ToString("MM/dd/yyyy"),
                InterestRate = rate
            });
        }

        protected void Calculate(decimal balance, decimal extraPay, int months, decimal rate, DateTime date, int next)
        {
            decimal totalInterest = 0, startAmount = Round2(balance);
            decimal monthRate0 = decimal.MinValue, monthAmount = decimal.MinValue;
            decimal tmpBalance = startAmount;
            

            var monthRate = rate / 100 / 12;

            if (monthRate != monthRate0)
            {
                double tmp = Math.Pow(1 + (double)monthRate, months);
                monthAmount = Round2((decimal)((double)balance * (double)monthRate * tmp / (tmp - 1)));

            }

            date = date.AddDays(31);
            int month = months;
            int id = 1;
            for (decimal baseAmount = 0; balance >=0; month--,id++,date = NextPay(date, next))
            {

                var interest = Round2(balance * monthRate);
                baseAmount = monthAmount + extraPay - interest;
                balance -= baseAmount;

                if (month == 1 && balance != 0)
                {
                    baseAmount += balance;
                    balance = 0;
                }

                totalInterest += interest;

                list.Add(new LoanType()
                {
                    ID = id,
                    Date = date.ToString("MM/dd/yyyy"),
                    startBalance = startAmount,
                    payment = monthAmount,
                    extraPayment = extraPay,
                    totalPayment = extraPay + monthAmount,
                    principal = baseAmount,
                    Interest = interest,
                    endBalance = balance,
                    totalInterest = totalInterest
                }

                );
                startAmount -= baseAmount;
            }

            list2.Add(new Loan_Summary2() {
                Scheduledpay = monthAmount,
                ScheNumPay = months,
                ActualPay = id-1,
                TotalInterest = totalInterest
            });


        }

        protected decimal Round2(decimal dec)
        {
            return decimal.Round(dec, 2);
        }
        protected DateTime NextPay(DateTime date, int next)
        {
            DateTime tmpdate = date.AddDays(31 * next);
            return tmpdate;
        }
        public class LoanType
        {
            public int ID { get; set; }
            public string Date { get; set; }
            public decimal startBalance { get; set; }
            public decimal payment { get; set; }
            public decimal extraPayment { get; set; }
            public decimal totalPayment { get; set; }
            public decimal principal { get; set; }
            public decimal Interest { get; set; }
            public decimal endBalance { get; set; }
            public decimal totalInterest { get; set; }

        }
        public class Loan_Summary
        {
            public decimal AmountValue { get; set; }
            public decimal InterestRate { get; set; }
            public int NumPay { get; set; }
            public string StartDate { get; set; }
            public int LoanTerm { get; set; }


        }
        public class Loan_Summary2
        {
            public decimal Scheduledpay { get; set; }
            public int ScheNumPay { get; set; }
            public int ActualPay { get; set; }
            public decimal TotalInterest { get; set; }

        }
    }

}
