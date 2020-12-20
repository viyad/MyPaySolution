using MyPayProject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyPayNUnitTestProject
{
    public class Tests
    {
        private List<PayRecord> _records;
        [SetUp]
        public void Setup()
        {
            _records = CsvImporter.ImportPayRecords(@"../../../Import/employee-payroll-data.csv");
        }

        [Test]
        public void TestImport()
        {
            Assert.IsNotNull(_records);
            Assert.IsNotEmpty(_records);
            Assert.AreEqual(5, _records.Count);
        }
        
        [Test]
        public void TestGross()
        {
            double[] gross = { 652.00, 418.00, 2202.00, 1104.00, 1797.45 };
            int index = 0;

            foreach (var record in _records)
                Assert.AreEqual(Math.Round(gross[index++], 2), Math.Round(record.Gross, 2));
        }
        
        [Test]
        public void TestTax()
        {
            double[] tax = { 182.45, 133.76, 754.91, 165.60, 597.14 };
            int index = 0;

            foreach (var record in _records)
                Assert.AreEqual(Math.Round(tax[index++], 2), Math.Round(record.Tax, 2));
        }

        [Test]
        public void TestNet()
        {
            double[] net = { 469.55, 284.24, 1447.09, 938.40, 1200.31 };
            int index = 0;

            foreach (var record in _records)
                Assert.AreEqual(Math.Round(net[index++], 2), Math.Round(record.Net, 2));
        }

        [Test]
        public void TestExport()
        {
            string fileName = DateTime.Now.Ticks.ToString() + "-records.csv";
            PayRecordWriter.Write(@"../../../Export/" + fileName, _records, false);
            string filePath = @"../../../Export/" + fileName;
            Assert.IsTrue(File.Exists(filePath));
        }
    }
}
