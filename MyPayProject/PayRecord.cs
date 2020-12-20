using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MyPayProject
{
    /// <summary>
    /// Class name - PayRecord
    /// An abstract class that holds information of an employee
    /// The general information includes:
    ///     _id which is the employee's id. This is a privite field and is available to the employee object
    ///     _hour which is an array of hours worked by the employee
    ///     _rates which is an array of rates earned by the employee
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public abstract class PayRecord
    {
        /// <summary>
        /// Store for the Id property.
        /// </summary>
        private int _id;

        /// <summary>
        /// Store for the _hour field.
        /// </summary>
        protected double[] _hour;

        /// <summary>
        /// Store for the _rates field.
        /// </summary>
        protected double[] _rates;

        /// <summary>
        /// Id property - get and set the value of the employee's id
        /// </summary>
        /// <value>
        /// Integer
        /// </value>
        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        /// <summary>
        /// Gross property - calculate gross of the employee based on hours worked and rates earned
        /// </summary>
        /// <value>
        /// Double
        /// </value>
        public double Gross
        {
            get
            {
                // Declare and initialise the gross variable
                double gross = 0;

                /* Iterate through the array of _hour to get hour value and rate value
                   The rates and hour arrays are parrallel array so they have the same index */
                for (int index = 0; index < _hour.Length; index++)
                {
                    // Calculate gross by summing up the multiplication of hour and rate
                    gross += _hour[index] * _rates[index];
                }
                return Math.Round(gross, 2);
            }
        }

        /// <summary>
        /// Tax property procedure - an abstract property, the implementation is in each child class
        /// </summary>
        /// <value>
        /// Double
        /// </value>
        public abstract double Tax
        {
            get;
        }

        /// <summary>
        /// Net property - calculate Net by using Gross and Tax
        /// </summary>
        /// <value>
        /// Double
        /// </value>
        public double Net
        {
            get
            {
                // Net is the result of the deduction of tax from gross
                return Math.Round((Gross - Tax), 2);
            }
        }

        /// <summary>
        /// The PayRecord class constructor.
        /// </summary>
        /// <param name="id">An integer, a unique number that used for identifying the PayRecord object</param>
        /// <param name="hours">An array of double, working hours of the given employee ID</param>
        /// <param name="rates">An array of double, rates of the given employee ID</param>
        public PayRecord(int id, double[] hours, double[] rates)
        {
            _id = id;
            _hour = hours;
            _rates = rates;
        }

        /// <summary>
        /// To print the details of this employee.
        /// This method is polymorphism which allows its children to implement their own GetDetails method, if there is any diffences
        /// </summary>
        /// <returns>
        /// A string of employee's details
        /// </returns>
        public virtual string GetDetails()
        {
            string detail = "";
            detail = $"----------- EMPLOYEE: {Id} -----------\n";
            detail += $"GROSS:\t{Gross:C2}\n";
            detail += $"NET:\t{Net:C2}\n";
            detail += $"TAX:\t{Tax:C2}\n";

            return detail;
        }
    }
}
