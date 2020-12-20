using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper;
using LINQtoCSV;

namespace MyPayProject
{
    /// <summary>
    /// Class name - PayRecordWriter
    /// This calss is to write a list of PayRecord into a given file
    /// With an option to display in the console
    /// Written by: Viyada Tarapornsin
    /// </summary>
    public class PayRecordWriter
    {
        /// <summary>
        /// Method name - Write
        /// A static method that uses a reusable component called CsvHelper by Josh Close
        /// https://www.nuget.org/packages/CsvHelper/
        /// </summary>
        /// <param name="file">A file name</param>
        /// <param name="records">A list of PayRecord object</param>
        /// <param name="writeToConsole">A flag or bool to indicate that it will be displayed in the console</param>
        public static void Write(string file, List<PayRecord> records, bool writeToConsole)
        {
            // Instantiate the StreamWriter and CsvWriter to write the records into file
            using (var writer = new StreamWriter(file))
            using (var csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }

            // If the writeToConsole is true, loop through the PayRecord and display in the console
            if (writeToConsole)
            {
                foreach (PayRecord pay in records)
                {
                    Console.WriteLine(pay.GetDetails());
                }
            }
        }

        /// <summary>
        /// Method name - WriteWithStreamWriter
        /// A static method that is to write a list of PayRecord into a given file using StreamWriter
        /// </summary>
        /// <param name="file">A file name</param>
        /// <param name="records">A list of PayRecord object</param>
        /// <param name="writeToConsole">A flag or bool to indicate that it will be displayed in the console</param>
        public static void WriteWithStreamWriter(string file, List<PayRecord> records, bool writeToConsole)
        {
            // Instantiate a StreamWriter to open a file and write into it
            TextWriter sw = new StreamWriter(file);
            sw.WriteLine("Id,Gross,Net,Tax");

            // Iterate through PayRecord list
            // Write ID, Gross, Net, Tax to file
            foreach (PayRecord pay in records)
            {
                sw.WriteLine("{0},{1},{2},{3}", pay.Id, pay.Gross, pay.Net, pay.Tax);
            }

            sw.Close();

            // If the writeToConsole is true, loop through the PayRecord and display in the console
            if (writeToConsole)
            {
                foreach (PayRecord pay in records)
                {
                    Console.WriteLine(pay.GetDetails());
                }
            }
        }

        /// <summary>
        /// Method name - WriteLINQtoCSV
        /// A static method that uses a reusable component called LINQtoCSV by Matt Perdeck
        /// https://www.nuget.org/packages/LinqToCsv/
        /// </summary>
        /// <param name="file">A file name</param>
        /// <param name="records">A list of PayRecord object</param>
        /// <param name="writeToConsole">A flag or bool to indicate that it will be displayed in the console</param>
        public static void WriteLINQtoCSV(string file, List<PayRecord> records, bool writeToConsole)
        {
            /* Configure the csv file descript that it uses commas as delimiters
               and there is a header row on the first row */
            CsvFileDescription outputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',', // comma delimited
                FirstLineHasColumnNames = true, 
             };

            // Instantiate a CsvContext object and write the records into the file
            CsvContext cc = new CsvContext();
            cc.Write(
                records,
                file,
                outputFileDescription);

            // If the writeToConsole is true, loop through the PayRecord and display in the console
            if (writeToConsole)
            {
                foreach (PayRecord pay in records)
                {
                    Console.WriteLine(pay.GetDetails());
                }
            }
        }
    }
}
