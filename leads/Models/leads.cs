//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace leads.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class leads
    {
        public int lea_id { get; set; }
        public string lea_nombre { get; set; }
        public string lea_correo { get; set; }
        public string lea_telefono { get; set; }
        public int cli_id { get; set; }
        public System.DateTime lea_fecha { get; set; }
    
        public virtual clientes clientes { get; set; }
    }
}
