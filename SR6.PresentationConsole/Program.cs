using SR6.DataLayer;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SR6.PresentationConsole
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static void Main()
        {
            //Console.WriteLine("Hello World!");
            client.BaseAddress = new Uri("http://localhost:2131/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));
            // Call http get
            GetBrandAsync().Wait();
            Console.WriteLine("Finish");
            Console.ReadLine();

        }
        static async Task GetBrandAsync()
        {
            HttpResponseMessage response = await client.GetAsync("api/brands");
            if (response.IsSuccessStatusCode)
            {
                var brands = await response.Content.ReadAsAsync<List<Brand>>();
                if(brands != null)
                {
                    foreach(var item in brands)
                    {
                        Console.WriteLine("Brand Name:{0}", item.BrandName);
                    }
                }
                else
                {
                    Console.WriteLine("No record");
                }
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
    }
}
