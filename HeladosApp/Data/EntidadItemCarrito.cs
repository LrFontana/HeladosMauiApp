using HeladosApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeladosApp.Data
{
    public class EntidadItemCarrito
    {
        // Propiedades
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int HeladoId { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public string Nombre { get; set; }
        public string NombreSabor { get; set; }
        public string NombreAgregado { get; set; }

        // Constructores.
        public EntidadItemCarrito(ItemCarrito itemCarrito)
        {
            HeladoId = itemCarrito.HeladoId;
            Nombre = itemCarrito.Nombre;
            Precio = itemCarrito.Precio;
            NombreSabor = itemCarrito.NombreSabor;
            NombreAgregado = itemCarrito.NombreAgregado;
            Cantidad = itemCarrito.Cantidad;
        }

        public EntidadItemCarrito()
        {

        }


        // Metodo.
        public ItemCarrito ConvertirAItemCarritoModel() =>
            new()
            {
				Id = Id,
                HeladoId = HeladoId,
			    Nombre = Nombre,
			    Precio = Precio,
			    NombreSabor = NombreSabor,
			    NombreAgregado = NombreAgregado,
			    Cantidad = Cantidad
            };
        
    }
}
