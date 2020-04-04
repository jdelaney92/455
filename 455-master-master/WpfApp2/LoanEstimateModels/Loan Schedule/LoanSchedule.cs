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
        public LoanSchedule(decimal balance, int months, decimal rate, DateTime date, int next)
        {
            list = new List<LoanType>();
            Calculate(balance, months, rate, date, next);
        }

        protected void Calculate(decimal balance, int months, decimal rate, DateTime date, int next)
        {
            decimal baseAmount = 0, totalInterest = 0, startAmount = Round2(balance);
            decimal monthRate0 = decimal.MinValue, monthAmount = decimal.MinValue;

            var monthRate = rate / 100 / 12;

            if (monthRate != monthRate0)
            {
                double tmp = Math.Pow(1 + (double)monthRate, months);
                monthAmount = Round2((decimal)((double)balance * (double)monthRate * tmp / (tmp - 1)));

            }

            date = date.AddDays(31);
            for (int day = date.Day, month = months; month >= 1; month--, date = NextMonth(date, day, next))
            {

                var interest = Round2(balance * monthRate);
                baseAmount = monthAmount - interest;
                balance -= baseAmount;

                if (month == 1 && balance != 0)
                {
                    baseAmount += balance;
                    interest -= balance;
                    balance = 0;
                }
                totalInterest += interest;

                list.Add(new LoanType()
                {
                    ID = months - month + 1,
                    Date = date.ToString("MM/dd/yyyy"),
                    startBalance = startAmount,
                    payment = monthAmount,
                    extraPayment = 0,
                    totalPayment = 0+ monthAmount,
                    principal = baseAmount,
                    Interest = interest,
                    endBalance = balance,
                    totalInterest = totalInterest
                }
                );
                startAmount -= baseAmount;
            }


        }

        protected decimal Round2(decimal dec)
        {
            return decimal.Round(dec, 2);
        }
        protected DateTime NextMonth(DateTime date, int day, int next)
        {
            DateTime tmpdate = date.AddDays(30 * next);
            /* return (date.Day >= day) ? date : new DateTime(date.Year, date.Month,
               Math.Min(day, DateTime.DaysInMonth(date.Year, date.Month)));*/
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
            public decimal principal { get; set;}
            public decimal Interest { get; set; }
            public decimal endBalance { get; set; }
            public decimal totalInterest { get; set; }


        }
    }

}
