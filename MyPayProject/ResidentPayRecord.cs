using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPayProject
{
    /// <summary>
    /// Class name - ResidentPayRecord
    /// A child class that derived from PayRecord class
    /// Written by: Viyada Tarapornsin
    /// </summary>
    class ResidentPayRecord : PayRecord
    {
        /// <summary>
        /// The ResidentPayRecord class constructor, using parent class
        /// </summary>
        /// <param name="id">An integer, a unique number that used for identifying the PayRecord object</param>
        /// <param name="hours">An array of double, working hours of the given employee ID</param>
        /// <param name="rates">An array of double, rates of the given employee ID</param>
        public ResidentPayRecord(int id, double[] hours, double[] rates) : base(id, hours, rates)
        {

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
                return TaxCalculator.CalculateResidentialTax(this.Gross);
            }
        }
    }
}
