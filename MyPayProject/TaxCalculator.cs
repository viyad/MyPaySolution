using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPayProject
{
    /// <summary>
    /// Class name - TaxCalculator
    /// There are two types of tax calculations; one is for residential employee, the other is for working holiday employee 
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public class TaxCalculator
    {
        /// <summary>
        /// Calculate tax for residential employees
        /// </summary>
        /// <param name="gross">Gross value of double type</param>
        /// <returns>
        /// Tax value of double type
        /// </returns>
        public static double CalculateResidentialTax(double gross)
        {
            double rateA = 0, rateB = 0;

            // Calculate tax for resident based on Resident tax scale
            if (gross > -1 && gross <= 72)
            {
                rateA = 0.19;
                rateB = 0.19;
            }
            else if (gross > 72 && gross <= 361)
            {
                rateA = 0.2342;
                rateB = 3.213;
            }
            else if (gross > 361 && gross <= 932)
            {
                rateA = 0.3477;
                rateB = 44.2476;
            }
            else if (gross > 932 && gross <= 1380)
            {
                rateA = 0.345;
                rateB = 41.7311;
            }
            else if (gross > 1380 && gross <= 3111)
            {
                rateA = 0.39;
                rateB = 103.8657;
            }
            else if (gross > 3111 && gross <= 999999)
            {
                rateA = 0.47;
                rateB = 352.7888;
            }

            return Math.Round(((rateA * gross) - rateB), 2);
        }

        /// <summary>
        /// Calculate tax for working holiday employees
        /// </summary>
        /// <param name="gross">Gross value of double type</param>
        /// <param name="yearToDate">A double, the amount earned by the employee up-to-date</param>
        /// <returns>
        /// Tax value of double type
        /// </returns>
        public static double CalculateWorkingHolidayTax(double gross, double yearToDate)
        {
            double rate = 0, totalGross = 0;
            totalGross = gross + yearToDate;

            switch (totalGross)
            {
                case double g when totalGross > -1 && totalGross <= 37000:
                    rate = 0.15;
                    break;
                case double g when totalGross > 37000 && totalGross <= 90000:
                    rate = 0.32;
                    break;
                case double g when totalGross > 90000 && totalGross <= 180000:
                    rate = 0.37;
                    break;
                case double g when totalGross > 180000 && totalGross <= 9999999:
                    rate = 0.45;
                    break;
                default:
                    break;
            }

            return Math.Round((gross * rate), 2);
        }
    }
}
