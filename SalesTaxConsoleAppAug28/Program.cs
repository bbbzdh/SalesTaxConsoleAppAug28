using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SalesTaxConsoleAppAug28;
using Taxjar;


namespace SalesTaxConsoleAppAug28
{

    public class Program
    {
        static HttpClient client;//= new HttpClient();

      

          static void ShowService(TaxService taxService)
            {
                Console.WriteLine($"Country: {taxService.Country}\tCity: " +
                    $"{taxService.City}\tZip: {taxService.Zip}");
            }

            static async Task<Uri> CreateTaxServiceAsync()
            {
                HttpResponseMessage response = await client.GetAsync("https://developers.taxjar.com/api/reference/#sales-tax-api/");
                response.EnsureSuccessStatusCode();

               return response.Headers.Location;
            }

            static async Task<TaxService> GetTaxServiceAsync(string path)
            {
                TaxService taxService = null;
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    taxService = await response.Content.ReadAsStringAsync("https://developers.taxjar.com/api/reference/#sales-tax-api/");
                }
                return taxService;
            }

            static async Task<TaxService> UpdateProductAsync(TaxService taxService)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(
                    $"api/taxServices/{taxService.Zip}", taxService);
                response.EnsureSuccessStatusCode();

                // Deserialize the updated product from the response body.
                taxService = await response.Content.ReadAsStringAsync<TaxService>();
                return taxService;
            }

            static async Task<HttpStatusCode> DeleteProductAsync(string id)
            {
                HttpResponseMessage response = await client.DeleteAsync(
                    $"api/products/{id}");
                return response.StatusCode;
            }

            static void Main()
            {
                RunAsync().GetAwaiter().GetResult();
            }

            static async Task RunAsync()
            {
                // Update port # in the following line.
                client.BaseAddress = new Uri("http://localhost:64195/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    // Create a new product
                    Product product = new Product
                    {
                        Name = "Gizmo",
                        Price = 100,
                        Category = "Widgets"
                    };

                    var url = await CreateProductAsync(product);
                    Console.WriteLine($"Created at {url}");

                    // Get the product
                    product = await GetProductAsync(url.PathAndQuery);
                    ShowProduct(product);

                    // Update the product
                    Console.WriteLine("Updating price...");
                    product.Price = 80;
                    await UpdateProductAsync(product);

                    // Get the updated product
                    product = await GetProductAsync(url.PathAndQuery);
                    ShowProduct(product);

                    // Delete the product
                    var statusCode = await DeleteProductAsync(product.Id);
                    Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.ReadLine();
            }
        }
    }
