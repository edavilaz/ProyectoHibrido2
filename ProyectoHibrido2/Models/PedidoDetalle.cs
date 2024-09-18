using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoHibrido2.Models
{
    public class PedidoDetalle
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public string? ProductoNombre { get; set; }
        public string? ProductoImagen { get; set; }
        public string RutaImagen => AppConfig.UrlImages + ProductoImagen;
        public decimal ProductoPrecio { get; set; }


    }
}
