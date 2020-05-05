using System;
using Newtonsoft.Json;
using RestSharp;

namespace TestProject1.Model
{
    public class AddUserDummyApi
    {
        
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("salary")]
        public string Salary { get; set; }

        [JsonProperty("age")]
        public string Age { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }
    }
}