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
    [RoutePrefix("leads/register")]
    public class LeadsController : ApiController
    {
        //prueba de conexión al controlador
        [HttpGet]
        [Route("test")]
        public IHttpActionResult Test()
        {
            return Ok(true);
        }

        //
        [HttpPost]
        [Route("save")]
        public SaveResponse SaveLead(SaveRequest request)
        {
            if (request == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var response = new SaveResponse();
            DBLeadsEntities _db = new DBLeadsEntities();

            //valido el token y el id de cliente suministrado
            clientes cliente = _db.clientes.Where(c => c.cli_id.Equals(request.id_cliente) && c.cli_token.Equals(request.token) && c.cli_token != "" && c.cli_token != null).FirstOrDefault();

            if (cliente == null)
            {
                response.message = "No autorizado";
                response.status = "FAIL";
                return response;
            }
            else
            {
                //validacion de telefono
                if (String.IsNullOrEmpty(request.telefono))
                {
                    response.message = "El valor de telefono es obligatorio";
                    response.status = "FAIL";
                    return response;
                }

                if (request.telefono.Length!=10)
                {
                    response.message = "La longitud de telefono debe ser de 10";
                    response.status = "FAIL";
                    return response;
                }

                //validacion de intervalo entre registros
                DateTime fecha = DateTime.Now;
                var ultimo_registro = _db.leads.Where(m => m.cli_id == request.id_cliente && m.lea_telefono.Equals(request.telefono)).OrderByDescending(m => m.lea_fecha).FirstOrDefault();

                if (ultimo_registro!=null)
                {
                    DateTime fecha_ultimo_registro = ultimo_registro.lea_fecha;
                    TimeSpan span = fecha.Subtract(fecha_ultimo_registro);
                    double minutos_transcurridos = span.TotalMinutes;

                    if (minutos_transcurridos < cliente.cli_tiempo)
                    {
                        response.message = "El telefono " + request.telefono + " fue registrado hace menos de " + cliente.cli_tiempo.ToString() + " minutos.";
                        response.status = "FAIL";
                        return response;
                    }
                }
                

                //validacion de dominio
                if (!request.correo.EndsWith(cliente.cli_dominio, StringComparison.InvariantCultureIgnoreCase))
                {
                    response.message = "Se esperaba un correo con dominio " + cliente.cli_dominio;
                    response.status = "FAIL";
                    return response;
                }

                //almaceno el lead
                var lead = new leads.Models.leads();
                lead.lea_nombre = request.nombre;
                lead.lea_correo = request.correo;
                lead.lea_telefono = request.telefono;
                lead.cli_id = request.id_cliente;
                lead.lea_fecha = fecha;
                
                _db.leads.Add(lead);
                _db.SaveChanges();
                response.message = "Registro almacenado con éxito";
                response.status = "OK";
                return response;
            }

        }
    }
}
