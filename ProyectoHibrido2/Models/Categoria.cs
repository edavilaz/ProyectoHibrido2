﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoHibrido2.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? UrlImagen { get; set; }
        public string? RutaImagen => AppConfig.UrlImages + UrlImagen;
    }
}
