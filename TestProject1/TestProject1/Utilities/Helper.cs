using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Serialization.Json;

namespace TestProject1.Utilities
{
    public static class Helper
    {
        public static Dictionary<string, string> DeserializeResponse(IRestResponse response)
        {
            var jsonDeserializer = new JsonDeserializer();
            var jsonObj = jsonDeserializer.Deserialize<Dictionary<String,String>>(response);
            return jsonObj;
        }
        
        
        public static String DeserializeUsingJObject(IRestResponse response, string keyName)
        {
            JObject jsonObj = JObject.Parse(response.Content);
            
            return jsonObj[keyName]?.ToString();

        }
        
        
    }
}