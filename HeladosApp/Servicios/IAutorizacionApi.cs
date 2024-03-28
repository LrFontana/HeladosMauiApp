using HeladosMaui.Base.DTOs;
using Refit;

namespace HeladosApp.Servicios
{
	public interface IAutorizacionApi
	{
		// Metodos.
		[Post("/api/aut/signin")]
		Task<ResultadoConInfoDto<RespuestaAutorizacionDto>> IniciarSesionAsync(RequestIniciarSesionDto requestIniciarSesionDto);

		[Post("/api/aut/signup")]
		Task<ResultadoConInfoDto<RespuestaAutorizacionDto>> RegistrarseAsync(RequestRegistrarseDto requestRegistrarseDto);

		[Headers("Authorization: Bearer")]
		[Post("/api/aut/cambiar-contraseña")]
		Task<ResultadoDto> CambiarContraseñaAsync(CambiarContraseñaDto dto);
	}
}
