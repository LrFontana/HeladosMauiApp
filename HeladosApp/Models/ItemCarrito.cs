using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeladosApp.Models
{
	public partial class ItemCarrito : ObservableObject
	{
        // Propiedades.
        public int Id { get; set; }
        public int HeladoId { get; set; }
        public double Precio { get; set; }        
        public string Nombre { get; set; }
        public string NombreSabor { get; set; }
        public string NombreAgregado { get; set; }

        [ObservableProperty, NotifyPropertyChangedFor(nameof(PrecioTotal))]
        private int _cantidad;

		public double PrecioTotal => Precio * Cantidad;
	}
}
