using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoHibrido2.Models
{
    // deben ser los mismos nombres de la entidad pedido de la webapi para evitar errores en la serialización
    public class Pedido
    {
        public string? Direccion { get; set; }
        public decimal ValorTotal { get; set; }
        public int UsuarioId { get; set; }
    }
}
