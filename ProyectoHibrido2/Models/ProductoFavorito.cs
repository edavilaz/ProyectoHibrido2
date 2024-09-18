using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoHibrido2.Models
{
    public class ProductoFavorito
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int ProductoId { get; set; }

        public string? Nombre { get; set; }
        public string? Detalle { get; set; }
        public decimal Precio { get; set; }
        public string? ImagenUrl { get; set; }

        public bool IsFavorito { get; set; }


    }
}
