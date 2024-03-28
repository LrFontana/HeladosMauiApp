using HeladosMaui.Api.Data;
using HeladosMaui.Base.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HeladosMaui.Api.Servicios
{
	public class ServicioHelado(DataContext dataContext)
	{
		// Propiedades.
		private readonly DataContext _dataContext = dataContext;

		// Metodo.
		public async Task<HeladoDto[]> ObtenerHeladoAsync() => await _dataContext.Helados.AsNoTracking()
			                                                                             .Select(h => new HeladoDto(h.Id,
																													h.Nombre,
																													h.Imagen,
																													h.Precio,
																												    h.OpcionesDeHelados.Select(op =>
																						                            new OpcionesDeHeladoDto(
																													op.Sabor, 
																													op.Agregados)).ToArray()
																													)).ToArrayAsync();
	}
}
