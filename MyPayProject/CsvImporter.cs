using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using LINQtoCSV;
using System.Globalization;
using System.Data;

namespace MyPayProject
{
    /// <summary>
    /// Class name - CsvImporter
    /// Import data from a given csv file
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public class CsvImporter
    {
        /// <summary>
        /// Method name - ImportPayRecords
        /// A static method that read data from a given csv file 
        /// using CVSHelper a reusable component from Nuget.org by by Josh Close
        /// https://www.nuget.org/packages/CsvHelper/
        /// </summary>
        /// <param name="file">A file name</param>
        /// <returns>
        /// A list of PayRecord object
        /// </returns>
        public static List<PayRecord> ImportPayRecords(string file)
        {
            List<PayRecord> payRecord = new List<PayRecord>();

            try
            {
                // Open the .csv file using a CsvReader class from the reusable CSVHelper component
                using (var reader = new StreamReader(file))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    // Declare and initialise variables and data structure
                    int thisEmployee = 0, currentEmployee = 0;
                    string thisVisa = "", thisYearToDate = "";
                    var hours = new List<double>();
                    var rates = new List<double>();

                    while (csv.Read())
                    {

                        // Get the first field of the row and convert the string value to integer type
                        currentEmployee = Convert.ToInt32(csv.GetField<string>(0));

                        // Check if the employee has values and is a record of a new employee
                        if (thisEmployee != 0 && thisEmployee != currentEmployee)
                        {
                            /* Create a pay record depending on the employee type
                               using id, hours, rates, visa type, and year to date earn */
                            PayRecord pay = CreatePayRecord(thisEmployee, hours.ToArray(), rates.ToArray(), thisVisa, thisYearToDate);
                            payRecord.Add(pay);

                            // Clear the values in the hours and rates lists to get ready to store the data of the next employee
                            hours.Clear();
                            rates.Clear();
                        }

                        /* Add hours and rates of the employee from the second and third field of DataTable to the lists
                           Before adding to the lists, convert the string values to double values */
                        hours.Add(Convert.ToDouble(csv.GetField<string>(1)));
                        rates.Add(Convert.ToDouble(csv.GetField<string>(2)));

                        /* Assign the fourth field of the DataTable to visa type variable
                           Assing the fifth field of the DataTable to the year to date earned variable */
                        thisVisa = csv.GetField<string>(3);
                        thisYearToDate = csv.GetField<string>(4);
                        thisEmployee = currentEmployee;
                    }

                    // If it is the last row of the DataType, create the last pay record
                    if (thisEmployee != 0)
                    {
                        PayRecord pay = CreatePayRecord(thisEmployee, hours.ToArray(), rates.ToArray(), thisVisa, thisYearToDate);
                        payRecord.Add(pay);
                    }

                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return payRecord;
        }

        /// <summary>
        /// Method name - CreatePayRecord
        /// A static method that instantiates a PayRecord object based on the values of visa and yearToDate
        /// If both the values of visa and yearToDate are given, instantiate the WorkingHolidayPayRecord object
        /// Otherwise instantiate the ResidentPayRecord
        /// </summary>
        /// <param name="id">An integer, a unique number that used for identifying the PayRecord object</param>
        /// <param name="hours">An array of double, working hours of the given employee ID</param>
        /// <param name="rate">An array of double, rates of the given employee ID</param>
        /// <param name="visa">A string, a visa type held by a working holiday employee</param>
        /// <param name="yearToDate">A string, the amount earned by the employee up-to-date</param>
        /// <returns>
        /// A PayRecord object whether it is a ResidentPayRecord or a WorkingHolidayPayRecord type
        /// </returns>
        public static PayRecord CreatePayRecord(int id, double[] hours, double[] rate, string visa, string yearToDate)
        {
            PayRecord pay;

            // If the employee row does not contain either a value for visa or year to date,
            // instantiates a new instance of a resident pay record
            // Otherwise, instantiates a new instance of a working holiday pay record
            if (visa == "" || yearToDate == "")
                pay = new ResidentPayRecord(id, hours, rate);
            else
                pay = new WorkingHolidayPayRecord(id, hours, rate, Convert.ToInt32(visa), Convert.ToInt32(yearToDate));

            return pay;
        }

        /// <summary>
        /// Method name - ImportPayRecordsV1StreamReader
        /// A static method that reads data from a given csv file 
        /// using a StreamReader class from the reuable System.IO component
        /// </summary>
        /// <param name="file">A file name</param>
        /// <returns>
        /// A list of PayRecord object
        /// </returns>
        public static List<PayRecord> ImportPayRecordsV1StreamReader(string file)
        {
            List<PayRecord> payRecord = new List<PayRecord>();

            // Open the .csv file using a StreamReader class from the reusable System.IO component
            try
            {
                // Open the .csv file using a stream reader.
                using (var sr = new StreamReader(file))
                {
                    // Declare and initialise variables and data structures
                    string currentLine;
                    int thisEmployee = 0, currentEmployee = 0;
                    string thisVisa = "", thisYearToDate = "";
                    var hours = new List<double>();
                    var rates = new List<double>();

                    // Read the header line
                    if (!sr.EndOfStream)
                        currentLine = sr.ReadLine();

                    // Iterates through each line in the file
                    for (currentLine = sr.ReadLine(); currentLine != null; currentLine = sr.ReadLine())
                    {
                        // Seperate each value using a comma delimiter and keep the values in an array
                        string[] values = currentLine.Split(',');

                        // Get the employee Id from the first position of the array
                        currentEmployee = Convert.ToInt32(values[0]);

                        // Check if the employee has values and is a record of a new employee
                        if (thisEmployee != 0 && thisEmployee != currentEmployee)
                        {
                            /* Create a pay record depending on the employee type
                               using id, hours, rates, visa type, and year to date earn */
                            PayRecord pay = CreatePayRecord(thisEmployee, hours.ToArray(), rates.ToArray(), thisVisa, thisYearToDate);
                            payRecord.Add(pay);

                            // Clear the values in the hours and rates lists to get ready to store the data of the next employee
                            hours.Clear();
                            rates.Clear();
                        }

                        /* Add hours and rates of the employee using the values in the second and 
                           third position of the array to the lists
                           Before adding to the lists, convert the string values to double values */
                        hours.Add(Convert.ToDouble(values[1]));
                        rates.Add(Convert.ToDouble(values[2]));

                        /* Assign the value in the fourth position of the array to visa type variable
                           Assing the value in the fifth position of the array to the year to date earned variable */
                        thisVisa = values[3];
                        thisYearToDate = values[4];
                        thisEmployee = currentEmployee;
                    }

                    // If it is the last row of the DataType, create the last pay record
                    if (thisEmployee != 0)
                    {
                        PayRecord pay = CreatePayRecord(thisEmployee, hours.ToArray(), rates.ToArray(), thisVisa, thisYearToDate);
                        payRecord.Add(pay);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return payRecord;
        }

        /// <summary>
        /// Method name - ImportPayRecordsV2CsvHelperDataTable
        /// A static method that read data from a given csv file 
        /// using CVSHelper a reusable component from Nuget.org by by Josh Close
        /// https://www.nuget.org/packages/CsvHelper/
        /// </summary>
        /// <param name="file">A file name</param>
        /// <returns>
        /// A list of PayRecord object
        /// </returns>
        public static List<PayRecord> ImportPayRecordsV2CsvHelperDataTable(string file)
        {
            List<PayRecord> payRecord = new List<PayRecord>();

            try
            {
                // Open the .csv file using a CsvReader class from the reusable CSVHelper component
                using (var reader = new StreamReader(file))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    // Configure tyhe csv reader attribute to expect a header record on the first row
                    csv.Configuration.HasHeaderRecord = true;

                    // Instantiate a CsvDataReader
                    using (var dr = new CsvDataReader(csv))
                    {
                        // Declare and initialise variables and data structure
                        int thisEmployee = 0, currentEmployee = 0;
                        string thisVisa = "", thisYearToDate = "";
                        var hours = new List<double>();
                        var rates = new List<double>();

                        // Instantiate a DataTable and load the data from file into the DataTable
                        var dt = new DataTable();
                        dt.Load(dr);

                        // Iterate each row through the DataTable
                        foreach (System.Data.DataRow row in dt.Rows)
                        {
                            // Get the first field of the row and convert the string value to integer type
                            currentEmployee = Convert.ToInt32(row.Field<string>(0));

                            // Check if the employee has values and is a record of a new employee
                            if (thisEmployee != 0 && thisEmployee != currentEmployee)
                            {
                                /* Create a pay record depending on the employee type
                                   using id, hours, rates, visa type, and year to date earn */
                                PayRecord pay = CreatePayRecord(thisEmployee, hours.ToArray(), rates.ToArray(), thisVisa, thisYearToDate);
                                payRecord.Add(pay);

                                // Clear the values in the hours and rates lists to get ready to store the data of the next employee
                                hours.Clear();
                                rates.Clear();
                            }

                            /* Add hours and rates of the employee from the second and third field of DataTable to the lists
                               Before adding to the lists, convert the string values to double values */
                            hours.Add(Convert.ToDouble(row.Field<string>(1)));
                            rates.Add(Convert.ToDouble(row.Field<string>(2)));

                            /* Assign the fourth field of the DataTable to visa type variable
                               Assing the fifth field of the DataTable to the year to date earned variable */
                            thisVisa = row.Field<string>(3);
                            thisYearToDate = row.Field<string>(4);
                            thisEmployee = currentEmployee;
                        }

                        // If it is the last row of the DataType, create the last pay record
                        if (thisEmployee != 0)
                        {
                            PayRecord pay = CreatePayRecord(thisEmployee, hours.ToArray(), rates.ToArray(), thisVisa, thisYearToDate);
                            payRecord.Add(pay);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return payRecord;
        }

    }
}
