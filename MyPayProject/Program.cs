using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPayProject
{
    /// <summary>
    /// Class name - Program
    /// The main program that will invoke classes
    /// Written by: Viyada Tarapornsin
    /// </summary>
    class Program
    {
        /// <summary>
        /// Method name - Main
        /// To invoke static methods of CsvImporter and PayRecordWriter classes
        /// and start the whole process from getting data from a given csv file,
        /// calculate all values and save data into a csv file
        /// with an optional display the result in the console window
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            /* Call the ImportPayRecords static method of the CsvImporter class
               to get a list of pay records from the given csv file */
            List<PayRecord> lPayRecord = CsvImporter.ImportPayRecords("../../Import/employee-payroll-data.csv");

            /* Get the time stamptime and pass a new csv file using the timestamp prefix 
               to the Write static method of the PayRecordWriter class
               The second parameter (true or fale) is the option 
               whether the result will be displayed in the console window */
            string fileName = DateTime.Now.Ticks.ToString() + "-records.csv";
            PayRecordWriter.Write("../../Export/" + fileName, lPayRecord, true);

            Console.ReadLine();
            
        }
    }
}
