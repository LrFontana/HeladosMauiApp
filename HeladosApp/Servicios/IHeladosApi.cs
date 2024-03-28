using HeladosMaui.Base.DTOs;
using Refit;

namespace HeladosApp.Servicios
{
	public interface IHeladosApi
	{
		[Get("/api/helados")]
		Task<HeladoDto[]> ObtenerHeladosAsync();
	}
}
