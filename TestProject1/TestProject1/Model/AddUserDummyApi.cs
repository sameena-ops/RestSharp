using System;
using RestSharp;

namespace TestProject1.Model
{
    public class AddUserDummyApi
    {
        
        public Data data { get; set; }
        public string status { get; set; }
        public string id { get; set; }
    }

    public class Data
    {
        public string name { get; set; }
        public string salary { get; set;}
        public string age { get; set; }
        
    }
}