using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VulturDeliverys.Models.ViewModels
{
    public class EnvioDetallesViewModel
    {
        public EnvioModelQuery Envio { get; set; }
        public List<ConexionModelQuery> Conexiones { get; set; }
        public List<TrazabilidadModelQuery> Trazabilidades { get; set; }


    }

    public class EnvioModelQuery
    {
        public int EnvioID { get; set; }
        public string NombreEmisor { get; set; }
        public string NombreReceptor { get; set; }
        public string DireccionOrigen { get; set; }
        public string DireccionDestino { get; set; }
        public string DescripcionPaquete { get; set; }
        public double PesoPaquete { get; set; }
        public decimal ValorEnvio { get; set; }
        public DateTime FechaEnvio { get; set; }
    }

    public class ConexionModelQuery
    {
        public string NombreCiudadOrigen { get; set; }
        public string NombreCiudadDestino { get; set; }
        public DateTime FechaHoraSalida { get; set; }
        public DateTime? FechaHoraLlegada { get; set; }
    }

    public class TrazabilidadModelQuery
    {
        public DateTime FechaHora { get; set; }
        public string Ubicacion { get; set; }
        public string Estado { get; set; }
        public string DetallesAdicionales { get; set; }
    }
}