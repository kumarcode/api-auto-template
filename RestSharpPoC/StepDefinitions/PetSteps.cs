using NUnit.Framework;
using RestSharp;
using RestSharpPoC.Models;
using RestSharpPoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Newtonsoft.Json;

namespace RestSharpPoC.StepDefinitions
{
    [Binding]
    public class PetSteps
    {
        private readonly PetService _petService;
        private Pet _addedPet;
        private RestResponse _postResponse;
        private RestResponse _getResponse;



        public PetSteps()
        {
            _petService = new PetService("https://petstore.swagger.io/v2");
        }

        [Given(@"I add the pet using the POST Pet API")]
        public void GivenIAddThePetUsingThePOSTPetAPI()
        {
            _postResponse = _petService.AddPet("TestData/PetData.json");
        }

        [Given(@"the pet is added successfully")]
        public void GivenThePetIsAddedSuccessfully()
        {
            // Assert the response status code
            Assert.AreEqual("OK", _postResponse.StatusCode.ToString());
        }

        [When(@"I retrieve the pet by ID using the GET Pet API")]
        public void WhenIRetrieveThePetByIDUsingTheGETPetAPI()
        {
            // Deserialize the response body to a Pet object
            var postPet = JsonConvert.DeserializeObject<Pet>(_postResponse.Content);
            _getResponse = _petService.GetPetById(postPet.Id);
        }

        [Then(@"the pet is returned successfully with correct pet details")]
        public void ThenThePetIsReturnedSuccessfullyWithCorrectPetDetails()
        {
            // Deserialize the response body to a Pet object
            var getPet = JsonConvert.DeserializeObject<Pet>(_getResponse.Content);


            Assert.AreEqual("Kumar", getPet.Name, "The retrieved pet has a different name than the added pet");

        }
    }
}
