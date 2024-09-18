using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoHibrido2.Models
{
    public class CarritoCompra
    {
        public decimal PrecioUnitario {  get; set; }
        public int Cantidad { get; set; }
        public decimal ValorTotal { get; set; }
        public int ProductoId { get; set; }
        public int ClienteId { get; set; }
    }
}
