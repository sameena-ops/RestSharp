using System;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp.Authenticators;
using TestProject1.Model;
using TestProject1.Utilities;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        private RestClient _restClient;
        private RestRequest _restRequest;
        [SetUp]
        public void SetUp()
        {
            _restClient = new RestClient("https://reqres.in/");
            
        }
        [Test]
        public void getUserDetails()
        {
            _restRequest = new RestRequest("/api/users/{user}",Method.GET);
            _restRequest.AddUrlSegment("user", 2);
            var response = _restClient.Execute(_restRequest).Content;
            Console.WriteLine("Response is : "+ response);
            
        }

        [Test]
        public void verifyTotalNumOfUsers()
        {
            int i = 0;
            _restRequest = new RestRequest("/api/users?page=2", Method.GET);
            var response = _restClient.Execute(_restRequest);
            //Inbuilt Deserializer part of rest sharp framework
            var output = Helper.DeserializeResponse(response);
            var total = output["total"];
            JArray obj = JArray.Parse(output["data"]);
            while (i < obj.Count)
            {
                string email = (string) obj[i]["email"];
                Console.WriteLine(email);
                i++;
            }
            Console.WriteLine("This total is from Inbuilt Deserialization :" + total);
            //Jobject -Newtonsoft
            var resultNewJson = Helper.DeserializeUsingJObject(response, "total");
            Console.WriteLine("NewJsonObj deserialized value is "+resultNewJson);
            Assert.That(resultNewJson?.ToString(),Is.EqualTo("12"),"Total record doesn't match");
        }
        
        [Test]
        public void GetRequestWithAuthentication()
        {
            _restClient.Authenticator = new HttpBasicAuthenticator("eve.holt@reqres.in", "cityslicka");
            _restRequest = new RestRequest("https://reqres.in/api/login", Method.GET);
            var response = _restClient.Execute(_restRequest);
            //  Console.WriteLine("Status Code: " + response.Content);
            //Console.WriteLine("Status Code: " + (int) response.StatusCode);
            Assert.That((int)response.StatusCode, Is.EqualTo(200));
        }
        
        [Test]
        public void verifyTotalNumOfUsersUsingPoco()
        { 
            _restRequest = new RestRequest("/api/users?page=2", Method.GET);
            var response = _restClient.Execute<ListOfUsersReqRes>(_restRequest);
            string adValue = response.Data.Ad.Company;
            Console.WriteLine(adValue);
            foreach (var value in response.Data.Data)
            {
                Console.WriteLine(value.Email);
            }
        }

        [TearDown]
        public void TearDown()
        {
         Console.WriteLine("Execution completed");   
        }
    }
}