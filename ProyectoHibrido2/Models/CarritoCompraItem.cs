using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoHibrido2.Models
{
    public class CarritoCompraItem: INotifyPropertyChanged
    {

        public int Id { get; set; }
        public decimal Precio { get; set; }
        public decimal ValorTotal { get; set; }
        private int cantidad;
        public int Cantidad
        {
            get { return cantidad; }
            set
            {
                if (cantidad != value)
                {
                    cantidad = value;
                    OnPropertyChanged();
                }
            }
        }
        public int ProductoId { get; set; }
        public string? ProductoNombre {  get; set; }
        public string? UrlImagen {  get; set; }
        
        public string? RutaImagen => AppConfig.UrlImages + UrlImagen;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
