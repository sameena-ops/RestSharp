using System;
using Newtonsoft.Json;

namespace TestProject1.Model
{
    public class Users
    {
        //Request payload data
        public string email { get; set; }
        public string password { get; set; }

        public string token { get; set; }
        public string id { get; set; }



    }
}