using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using TestProject1.Model;
using TestProject1.Utilities;

namespace TestProject1
{
    public class TestWithPost
    {
        private RestClient _restClient;
        private RestRequest _restRequest;
        private RestClient _restClient1;
       
        [SetUp]
        public void SetUp()
        {
            _restClient = new RestClient("http://dummy.restapiexample.com");
            _restClient1 = new RestClient("https://reqres.in/");

        }
        
        [Test]
        public void TestWithPostCall()
        {
            _restRequest = new RestRequest("api/users" , Method.POST);
            _restRequest.AddJsonBody(new {name = "morpheus"});
            _restRequest.AddJsonBody(new {job = "leader" });
            var result = _restClient1.Execute(_restRequest);
            var rResult = Helper.DeserializeResponse(result);
            var output = rResult["name"];
            Assert.That(output, Is.EqualTo("morpheus"));
        }

        [Test]
        public void PostCallDummyApi()
        {
            _restRequest = new RestRequest("/api/v1/create", Method.POST);
           _restRequest.AddHeader("Content-Type", "application/json;charset=utf-8");
          // _restRequest.AddJsonBody(clsObj);
           _restRequest.AddJsonBody(new {name = "sam",salary = "1000",age = "26"});
          // _restRequest.AddJsonBody(new {salary = "1000"});
           //_restRequest.AddJsonBody(new {age = "26"});
           var resp = _restClient.Execute(_restRequest);
           var jsonObject = Helper.DeserializeResponse(resp);
           //String jObj = Helper.DeserializeUsingJObject(resp, "data");
           Console.WriteLine(jsonObject["data"]);
           JObject dataFields = JObject.Parse(jsonObject["data"]);
           String name = dataFields["name"]?.ToString();
           Assert.That(resp.StatusCode.ToString(),Is.EqualTo("OK"));
           Assert.That(name,Is.EqualTo("sam"));

        }

        [Test]
        public void TestPostWithTypeClass()
        {
            _restRequest = new RestRequest("api/register",Method.POST);
            _restRequest.AddJsonBody(new Users() {email = "eve.holt@reqres.in", password = "pistol"});
            var response = _restClient1.Execute<Users>(_restRequest);
            Console.WriteLine(response.Data.token);
            Assert.That(response.Data.token,Is.EqualTo("QpwL5tke4Pnpja7X4"));

        }
        
        [Test]
        public void PostCallDummyApi2()
        {
            _restRequest = new RestRequest("/api/v1/create", Method.POST);
            _restRequest.AddHeader("Content-Type", "application/json;charset=utf-8");
            // _restRequest.AddJsonBody(clsObj);
            
           // _restRequest.AddJsonBody(new {Data.name = "sam",salary = "1000",age = "26"});
            // _restRequest.AddJsonBody(new {salary = "1000"});
            //_restRequest.AddJsonBody(new {age = "26"});
            var resp = _restClient.Execute(_restRequest);
            var jsonObject = Helper.DeserializeResponse(resp);
            //String jObj = Helper.DeserializeUsingJObject(resp, "data");
            Console.WriteLine(jsonObject["data"]);
            JObject dataFields = JObject.Parse(jsonObject["data"]);
            String name = dataFields["name"]?.ToString();
            Assert.That(resp.StatusCode.ToString(),Is.EqualTo("OK"));
            Assert.That(name,Is.EqualTo("sam"));

        }
    }
}