using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeladosMaui.Base.DTOs
{
	public record OrdenItemDto(long Id, int HeladoId, int Cantidad, double Precio, string Nombre, string Sabor, string Agegado)
	{
		// Propiedad.
		public double PrecioTotal => Cantidad * Precio;
    }

	public record class OrdenDto(long Id, DateTime FechaOrden, double PrecioTotal, int CatidadTotalItems = 0)
	{
		public string MostrarCantidadDeItems => CatidadTotalItems + (CatidadTotalItems > 1 ? " Helados" : " Helado"); 
	}

	public record EstablecerOrdenDto(OrdenDto Orden, OrdenItemDto[] Items);

	
}
