using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxjar;
using ConsoleApptaxHelper;

namespace ConsoleApptaxHelper
{
    class Program
    {

        static void Main(string[] args)
        {
           decimal TaxRate = TaxHelper.GetRateByZipCode("07001", "US", "Avenel");
           //decimal TaxRate = TaxHelper.GetRateByZipCode("M2J1X7", "CA", "TORONTO");

            Console.WriteLine("Tax Rate: " + TaxRate) ;

            decimal amount = Convert.ToDecimal("16.5");
            decimal shipping = Convert.ToDecimal("1.5");
            decimal TaxAmount = TaxHelper.calculateTax("US", "07001", "NJ", "Avenel", "305 W Village Dr", "US", "07446", "NJ", "Ramsey", "63 W Main St", amount, shipping);
            Console.WriteLine("Tax Amount: " + TaxAmount);
            Console.ReadLine();

        }
    }
   

}
