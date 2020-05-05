using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using TestProject1.Model;
using TestProject1.Utilities;

namespace TestProject1
{
    [TestFixture]
    public class TestWithData
    {
        private RestClient _restClient;
        private RestRequest _restRequest;
        

        [SetUp]
        public void SetUp()
        {
            _restClient = new RestClient("http://api.zippopotam.us/");
        }
        
        
        [TestCase("IN", "110001", HttpStatusCode.OK,TestName ="Check status code for IN with pin code 110001")]
        [TestCase("AU", "2140", HttpStatusCode.OK,TestName ="Check status code for AU with pin code 2140")]
        [TestCase("IN", "1100", HttpStatusCode.NotFound,TestName ="Check status code for IN with pin code 1100")]
        public void TestGetCall_WithData(string ccode,string pinCode,HttpStatusCode expectedStatusCode)
        {
            _restRequest = new RestRequest($"{ccode}/{pinCode}", Method.GET);
            var result = _restClient.Execute(_restRequest);
            Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
        }

        [Test,TestCaseSource("placesTestData")]
        public void TestWithTestCaseSource(string ccode, string pinCode, string placeName)
        {
            _restRequest = new RestRequest($"{ccode}/{pinCode}", Method.GET);
            var result = _restClient.Execute(_restRequest);
            var output = new JsonDeserializer().Deserialize<Zippopotam>(result);
            Console.WriteLine("::::::::: "+output.Places[0].PlaceName+" :::::::::");
            Assert.That(output.Places[0].PlaceName,Is.EqualTo(placeName));
            //var output = Helper.DeserializeResponse(result);
            
          //  Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
        }

        private static IEnumerable<TestCaseData> placesTestData()
        {
            yield return new TestCaseData("au","2140","Homebush").SetName("Check status code for 2140 that has place name as homebush");
        }
    }
}