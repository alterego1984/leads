using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace leads.Models
{
    public class LoginRequest
    {
        public string usuario { get; set; }
        public string password { get; set; }
        public int id_cliente { get; set; }
    }

    public class LoginResponse
    {
        public string token { get; set; }
        public string message { get; set; }
        public string status { get; set; }
    }

}