using RestSharp;
using Newtonsoft.Json;
using RestSharpPoc.Services;
using System.IO;
using RestSharpPoC.Models;
using NUnit.Framework;

namespace RestSharpPoc.Test
{
    [TestFixture]
    public class PetServiceTests
    {
        private PetService _petService;

        [SetUp]
        public void Setup()
        {
            _petService = new PetService("https://petstore.swagger.io/v2");
        }

        [Test]
        public void PostPet()
        {
            // Make the API request
            var response = _petService.AddPet("TestData/PetData.json");

            // Assert the response status code
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Test]
        public void GetPetById()
        {
            // Make the API request
            var response = _petService.GetPetById(1);

            // Deserialize the response body to a Pet object
            var pet = JsonConvert.DeserializeObject<Pet>(response.Content);

            // Assert the response status code and pet name
            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.AreEqual("Kumar", pet.Name);
        }
    }
}
