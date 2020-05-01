using System;
using Newtonsoft.Json;

namespace TestProject1.Model
{
   
    
    public class Users
    {
        //Request payload data

  
        public String email { get; set; }
        public String password { get; set; }

        public String token { get; set; }
        public String id { get; set; }



    }
}