using HeladosMaui.Base.DTOs;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeladosApp.Servicios
{
	[Headers("Authorization: Bearer")]
    public interface IOrdenApi
    {
		[Post("/api/ordenes/armar-orden")]
		Task<ResultadoDto> EstablecerOrdenAsync(EstablecerOrdenDto dto);

		[Get("/api/ordenes")]
		Task<OrdenDto[]> ObtenerMiOrdenAsync();

		[Get("/api/ordenes/{ordenId}/items")]
		Task<OrdenItemDto[]> ObtenerItemOrdenAsync(long ordenId);
	}
}
