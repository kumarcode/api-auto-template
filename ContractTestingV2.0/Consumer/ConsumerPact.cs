using PactNet.Mocks.MockHttpService;
using PactNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractTestingV2._0.Consumer
{
    public class ConsumerPact : IDisposable
    {

        public IPactBuilder PactBuilder { get; private set; }
        public IMockProviderService MockProviderService { get; private set; }

        public int MockServerPort { get { return 9224; } }
        public string MockProviderServiceBaseUri { get { return String.Format("http://localhost:{0}", MockServerPort); } }

        public ConsumerPact()
        {
            //Pact Configuration
            var pactConfig = new PactConfig
            {
                SpecificationVersion = "2.0.0",
                PactDir = @"c:\contractTestingPoC\pacts",
                LogDir = @"c:\contractTestingPoC\logs"
            };

            PactBuilder = new PactBuilder(pactConfig);

            PactBuilder.ServiceConsumer("Service_Consumer").HasPactWith("PetList");

            MockProviderService = PactBuilder.MockService(MockServerPort);
        }

        public void Dispose()
        {
            PactBuilder.Build();
        }
    }

}
