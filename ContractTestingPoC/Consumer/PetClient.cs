using ContractTestingPoC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractTestingPoC.src
{


    public class PetClient
    {

        //public PetClient()
        //{
        //    //string baseUri = "https://petstore.swagger.io/v2";

        //    //Console.WriteLine("Fetching products");
        //    //var consumer = new PetClient();
        //    //List<Pet> result = consumer.GetPet(baseUri).GetAwaiter().GetResult();
        //    //Console.WriteLine(result);
        //}

        //static private void WriteoutArgsUsed(string datetimeArg, string baseUriArg)
        //{
        //    Console.WriteLine($"Running consumer with args: dateTimeToValidate = {datetimeArg}, baseUri = {baseUriArg}");
        //}

        //static private void WriteoutUsageInstructions()
        //{
        //    Console.WriteLine("To use with your own parameters:");
        //    Console.WriteLine("Usage: dotnet run ");
        //    Console.WriteLine("Usage Example: dotnet run 01/01/2018 https://petstore.swagger.io/v2");
        //}
    #nullable enable
        public async Task<Pet> GetPet(string baseUrl, HttpClient? httpClient = null)
        {
            using var client = httpClient == null ? new HttpClient() : httpClient;

            var response = await client.GetAsync(baseUrl + "/pet/1");
            response.EnsureSuccessStatusCode();

            var resp = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Pet>(resp);
        }
    }
}
