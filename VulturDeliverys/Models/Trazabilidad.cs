//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VulturDeliverys.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Trazabilidad
    {
        public int TrazabilidadID { get; set; }
        public Nullable<int> EnvioID { get; set; }
        public Nullable<System.DateTime> FechaHora { get; set; }
        public string Ubicacion { get; set; }
        public string Estado { get; set; }
        public string DetallesAdicionales { get; set; }
    
        public virtual Envio Envio { get; set; }
    }
}