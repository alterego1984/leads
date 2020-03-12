using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using leads.Models;
using System.Threading;

namespace leads.Controllers
{
    [RoutePrefix("leads/login")]
    public class LoginController : ApiController
    {
        //prueba de conexión al controlador
        [HttpGet]
        [Route("test")]
        public IHttpActionResult Test()
        {
            return Ok(true);
        }
        
        //autenticacion
        [HttpPost]
        [Route("authenticate")]
        public LoginResponse Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var response = new LoginResponse();
            DBLeadsEntities _db = new DBLeadsEntities();
            //valido los datos suministrados
            clientes cliente = _db.clientes.Where(c => c.cli_usuario.Equals(login.usuario) && c.cli_password.Equals(login.password) && c.cli_id.Equals(login.id_cliente)).FirstOrDefault();

            if (cliente==null)
            {
                response.message = "Datos inválidos";
                response.token = "";
                response.status = "FAIL";
                return response;
            }
            else
            {
                //si los datos son correctos se genera un token y se almacena en la BD
                DateTime fecha = DateTime.Now;
                string token=TokenGenerator.GenerateToken(fecha.ToString("ddMMyyyy hhmmss") + "-" + login.id_cliente);
                cliente.cli_token = token;
                _db.SaveChanges();
                response.message = "Token generado con éxito";
                response.token = token;
                response.status = "OK";
                return response;
            }
            
        }


    }
}
