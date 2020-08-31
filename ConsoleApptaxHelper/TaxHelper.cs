using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxjar;
using System.Configuration;

namespace ConsoleApptaxHelper
{
    public static class TaxHelper
    {
        public static string GetKey()
        {
            return ConfigurationManager.AppSettings["TaxjarApiKey"];
        }
        public static decimal GetRateByZipCode(string zipCode, string countryCode, string city)
        {
            var client = new TaxjarApi(GetKey());
            var rates = client.RatesForLocation(zipCode, new
            {
                country = countryCode,
                city = city,
                
            });
            return rates.CombinedRate;
        }
                     
        public static decimal calculateTax

            (string from_country, string from_zip, string from_state, string from_city, string from_street, string to_country,
            string to_zip, string to_state, string to_city, string to_street, decimal amount, decimal shipping)
        {

            var client = new TaxjarApi(GetKey());

            var tax = client.TaxForOrder(new
            {
                from_country = from_country,
                from_zip = from_zip,
                from_state = from_state,
                from_city = from_city,
                from_street = from_street,
                to_country = to_country,
                to_zip = to_zip,
                to_state = to_state,
                to_city = to_city,
                to_street = to_street,
                amount = amount,
                shipping = shipping
            });

            decimal amt = Convert.ToDecimal(amount);

            var myRate = tax.Rate;
            var calculatedTax = myRate * amt;
            return calculatedTax;
        }
    }

}




    

