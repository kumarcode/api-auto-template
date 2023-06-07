using PactNet.Output.Xunit;
using PactNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit;
using PactNet.Infrastructure.Outputters;
using PactNet.Matchers;
using ContractTestingPoC.src;
using FluentAssertions;
using ContractTestingPoC.Models;

namespace ContractTestingPoC.Tests
{
    public class PetApiConsumerPact
    {
        private IPactBuilderV3 pact;
        // private readonly int port = 9222;

        private readonly List<object> pets;

        public PetApiConsumerPact(ITestOutputHelper output)
        {

            string baseUri = "https://petstore.swagger.io/v2";

            Console.WriteLine("Fetching products");
            var consumer = new PetClient();
            Pet result = consumer.GetPet(baseUri).GetAwaiter().GetResult();
            Console.WriteLine(result);


            pets = new List<object>()
            {
                new { id = 1, name = "Kumar" }
            };

            var Config = new PactConfig
            {
                PactDir = Path.Join("..", "..", "..", "..", "pacts"),
                Outputters = new List<IOutput> { new XunitOutput(output), new ConsoleOutput() },
                LogLevel = PactLogLevel.Debug
            };

            pact = Pact.V3("Consumer", "Provider", Config).WithHttpInteractions();
        }

        [Fact]
        public async Task RetrievePet()
        {
            // Arrange
            pact.UponReceiving("A request to get pet by ID")
                        .Given("products exist")
                        .WithRequest(HttpMethod.Get, "/pet/1")
                    .WillRespond()
                    .WithStatus(HttpStatusCode.OK)
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithJsonBody(new
                    {
                        Id = 1,
                        Name = Match.Type("Kumar"),
                        Status = Match.Type("available")
                    });

            await pact.VerifyAsync(async ctx =>
            {
                // Act
                var consumer = new PetClient();
                Pet result = await consumer.GetPet(ctx.MockServerUri.ToString().TrimEnd('/'));
                // Assert
                result.Should().NotBeNull();
                Assert.Equal(1,result.Id);
                //Assert.Equal("Kumar", result.name);
            });
        }
    }
}
