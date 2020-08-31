using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SalesTaxConsoleAppAug28;
using Taxjar;

namespace SalesTaxConsoleAppAug28
{

    public class rates
    {
        string rate { get; set; };
    }
    public static  class TaxHelper
    {

       // static HttpClient client;//= new HttpClient();

        public static void GetRate()
        {
            var client = new TaxjarApi("9e0cd62a22f451701f29c3bde214");
            var rates = client.RatesForLocation("90404-3370");

            // United States (ZIP w/ Optional Params)
            //var rates = client.RatesForLocation("90404", new
            //{
            //    city = "Santa Monica",
            //    state = "CA",
            //    country = "US"
            //});


        }


    }   
}
