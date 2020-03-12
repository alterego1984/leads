using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace leads.Models
{
    public class SaveRequest
    {
        public string token { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public int id_cliente { get; set; }
    }

    public class SaveResponse
    {
        public string message { get; set; }
        public string status { get; set; }
    }

}