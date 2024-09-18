
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoHibrido2.Models
{
    public class ImagenPerfil
    {
        public string? UrlImagen { get; set; }

        public string? RutaImagen => AppConfig.BaseUrl + UrlImagen;
    }
}
