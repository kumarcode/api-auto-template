using System.IO;
using Newtonsoft.Json;
using RestSharp;
using RestSharpPoC.Models;

namespace RestSharpPoc.Services
{
    public class PetService
    {
        private readonly RestClient _restClient;

        public PetService(string baseUrl)
        {
            _restClient = new RestClient(baseUrl);
        }

        // Add authentication header here
        private void AddAuthenticationHeader(RestRequest request)
        {
            // Replace 'YOUR_API_KEY' with your actual API key
            string apiKey = "IM_A_TEST_API_KEY";
            request.AddHeader("Authorization", $"Bearer {apiKey}");
        }

        public RestResponse AddPet(string filePath)
        {
            // Load the request payload from file
            var json = File.ReadAllText(filePath);
            var pet = JsonConvert.DeserializeObject<Pet>(json);

            var request = new RestRequest("/pet", Method.Post);

            request.AddJsonBody(pet);

            // Add authentication header
            AddAuthenticationHeader(request);

            return _restClient.Execute(request);
        }

        public RestResponse GetPetById(long petId)
        {
            var request = new RestRequest($"/pet/{petId}", Method.Get);

            // Add authentication header
            AddAuthenticationHeader(request);

            return _restClient.Execute(request);
        }
    }
}
