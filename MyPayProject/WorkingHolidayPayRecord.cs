using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPayProject
{
    /// <summary>
    /// Class name - WorkingHolidayPayRecord
    /// A child class that derived from PayRecord class
    /// Written by: Viyada Tarapornsin
    /// </summary>
    class WorkingHolidayPayRecord : PayRecord
    {
        /// <summary>
        /// Store for the Visa property.
        /// </summary>
        private int _visa;

        /// <summary>
        /// Store for the YearToDate property.
        /// </summary>
        private int _yearToDate;

        /// <summary>
        /// The WorkingHolidayPayRecord class constructor.
        /// </summary>
        /// <param name="id">An integer, a unique number that used for identifying the PayRecord object</param>
        /// <param name="hours">An array of double, working hours of the given employee ID</param>
        /// <param name="rates">An array of double, rates of the given employee ID</param>
        /// <param name="visa">A string, a visa type held by a working holiday employee</param>
        /// <param name="yearToDate">A string, the amount earned by the employee up-to-date</param>
        public WorkingHolidayPayRecord(int id, double[] hours, double[] rates,
                        int visa, int yearToDate) : base(id, hours, rates)
        {
            _visa = visa;
            _yearToDate = yearToDate;
        }

        /// <summary>
        /// Visa property - get and set the value of Visa
        /// </summary>
        /// <value>
        /// int
        /// </value>
        public int Visa
        {
            get { return _visa; }
            private set { _visa = value; }
        }

        /// <summary>
        /// YearToDate property - get and set the value of YearToDate
        /// </summary>
        /// <value>
        /// int
        /// </value>
        public int YearToDate
        {
            get { return _yearToDate; }
            private set { _yearToDate = value; }
        }

        /// <summary>
        /// Tax property - an override property that has its own implementation
        /// </summary>
        /// <value>
        /// Double
        /// </value>
        public override double Tax
        {
            get
            {
                // Use a static method of TaxCalculator class to calculate Gross of the employee 
                return TaxCalculator.CalculateWorkingHolidayTax(this.Gross, _yearToDate);
            }
        }

        /// <summary>
        /// To print the details of this employee.
        /// This method is polymorphism which derived from PayRecord class and has its own implementation
        /// </summary>
        /// <returns>
        /// A string of employee's details
        /// </returns>
        public override string GetDetails()
        {
            string detail = "";
            detail = $"----------- EMPLOYEE: {Id} -----------\n";
            detail += $"GROSS:\t{Gross:C2}\n";
            detail += $"NET:\t{Net:C2}\n";
            detail += $"TAX:\t{Tax:C2}\n";
            
            detail += $"VISA:\t{Visa}\n";
            detail += $"TAX:\t{YearToDate + Gross:C2}\n";

            return detail;
        }
    }
}
