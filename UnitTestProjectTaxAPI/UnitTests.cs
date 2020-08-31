using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ConsoleApptaxHelper;
using Taxjar;
using System.Configuration;

namespace UnitTestProjectTaxAPI
{
    [TestFixture]
    public class UnitTests
    {
        public static string GetKey()
        {
            return ConfigurationManager.AppSettings["TaxjarApiKey"];         
        }          
        
        [Test]
        public void TaxHelper_Gets_TaxRates()
        {
            var client = new TaxjarApi(GetKey());
            decimal TaxRate = TaxHelper.GetRateByZipCode("07001", "US", "Avenel");
            Console.WriteLine(TaxRate);
            Assert.AreEqual(0.06625, TaxRate);
        }
        [Test]
        public void TaxHelperCalculatesSalesTax()
        {
            decimal amount = Convert.ToDecimal("16.5");
            decimal shipping = Convert.ToDecimal("1.5");
            decimal TaxAmount = TaxHelper.calculateTax("US", "07001", "NJ", "Avenel", "305 W Village Dr", "US", "07446", "NJ", "Ramsey", "63 W Main St", amount, shipping);
            Console.WriteLine(TaxAmount);
            Assert.AreEqual(1.093125, TaxAmount);
        }
    }
}
