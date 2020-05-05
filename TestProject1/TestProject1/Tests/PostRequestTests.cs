using System;
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
           _restRequest.AddJsonBody(new {name = "sam",salary = "1000",age = "26"});
           var resp = _restClient.Execute(_restRequest);
           var de_resp = Helper.DeserializeResponse(resp);
           //String jObj = Helper.DeserializeUsingJObject(resp, "data");
           Console.WriteLine(de_resp["data"]);
          // JObject dataFields = JObject.Parse(jsonObject["data"]);
           JObject dataFields = JObject.Parse(de_resp["data"]);
           string name = (string) dataFields["name"];
           //String name = dataFields["name"]?.ToString();
           Assert.That(resp.StatusCode.ToString(),Is.EqualTo("OK"));
           Assert.That(name,Is.EqualTo("sam"));
        }

        [Test]
        public void TestPostWithTypeClass()
        {
            _restRequest = new RestRequest("api/register",Method.POST);
            _restRequest.AddJsonBody(new Users {email = "eve.holt@reqres.in", password = "pistol"});
            var response = _restClient1.Execute<Users>(_restRequest);
            Console.WriteLine(response.Data.token);
            Assert.That(response.Data.token,Is.EqualTo("QpwL5tke4Pnpja7X4"));

        }
        
        [Test]
        public void RetriveDataWithTypeClass()
        {
            _restRequest = new RestRequest("api/v1/create", Method.POST);
            _restRequest.AddJsonBody(obj: new {name = "samee",age="25",salary = "2000"});
            var response = _restClient.Execute<AddUserDummyApi>(_restRequest);
            var output = new JsonDeserializer().Deserialize<AddUserDummyApi>(response);
            Console.WriteLine(output.Data.Name);
        } 
      
    }
}