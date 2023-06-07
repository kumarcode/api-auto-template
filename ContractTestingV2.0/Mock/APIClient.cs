using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ContractTestingV2._0.Mock
{
    class APIClient
    {

        private readonly HttpClient _client;

        public APIClient(string baseUri = null)
        {
            _client = new HttpClient { BaseAddress = new Uri(baseUri ?? "http://localhost:60881") };
        }

        public EmployeeModel GetEmployeeDetails(string id)
        {
            string reasonPhrase;

            var request = new HttpRequestMessage(HttpMethod.Get, "/employee/" + id);
            request.Headers.Add("Accept", "application/json");

            var response = _client.SendAsync(request);

            var content = response.Result.Content.ReadAsStringAsync().Result;
            var status = response.Result.StatusCode;

            reasonPhrase = response.Result.ReasonPhrase; //NOTE: any Pact mock provider errors will be returned here and in the response body

            request.Dispose();
            response.Dispose();

            if (status == HttpStatusCode.OK)
            {
                return !String.IsNullOrEmpty(content) ?
                  JsonConvert.DeserializeObject<EmployeeModel>(content)
                  : null;
            }

            throw new Exception(reasonPhrase);
        }

    }

}
