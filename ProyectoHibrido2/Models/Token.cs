using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoHibrido2.Models
{
    public class Token
    {
        public string? AccessToken { get; set; }
        public string? TokenType { get; set; }
        public int? UsuarioId { get; set; }
        public string? UsuarioNombre { get; set; }
    }
}
