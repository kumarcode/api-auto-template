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

        public RestResponse AddPet(string filePath)
        {
            // Load the request payload from file
            var json = File.ReadAllText(filePath);
            var pet = JsonConvert.DeserializeObject<Pet>(json);

            var request = new RestRequest("/pet", Method.Post);


            request.AddJsonBody(pet);
            return _restClient.Execute(request);
        }

        public RestResponse GetPetById(long petId)
        {
            var request = new RestRequest($"/pet/{petId}", Method.Get);
            return _restClient.Execute(request);
        }
    }
}
