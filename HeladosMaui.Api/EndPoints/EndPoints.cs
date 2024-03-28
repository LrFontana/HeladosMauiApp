using HeladosMaui.Api.Servicios;
using HeladosMaui.Base.DTOs;
using System.Security.Claims;

namespace HeladosMaui.Api.EndPoints
{
	public static class EndPoints
	{
		// Metodos.

		private static Guid ObtenerIdUsuario(this ClaimsPrincipal principal) => 
			Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier)!);

		// End points
		public static IEndpointRouteBuilder MapaEndPoints(this IEndpointRouteBuilder app)
		{
			app.MapPost("/api/aut/signin", async (RequestIniciarSesionDto iniciarSesionDto, ServicioAutorizacion servicioAutorizacion) =>
				TypedResults.Ok(await servicioAutorizacion.IniciarSesionAsync(iniciarSesionDto)));
			
			
			app.MapPost("/api/aut/signup", async (RequestRegistrarseDto registrarseDto, ServicioAutorizacion servicioAutorizacion) =>
				TypedResults.Ok(await servicioAutorizacion.RegistrarseAsync(registrarseDto)));

			app.MapPost("/api/aut/cambiar-contraseña",
				async (CambiarContraseñaDto dto, ClaimsPrincipal principal, ServicioAutorizacion servicioAutorizacion) =>
				TypedResults.Ok(await servicioAutorizacion.CambiarContraseñaAsync(dto, principal.ObtenerIdUsuario())))
				.RequireAuthorization();

			app.MapGet("/api/helados", async(ServicioHelado servicioHelado) => 
				TypedResults.Ok(await servicioHelado.ObtenerHeladoAsync()));


			var grupoOrden = app.MapGroup("/api/ordenes").RequireAuthorization();

			grupoOrden.MapPost("/armar-orden",
				async(EstablecerOrdenDto dto, ClaimsPrincipal principal, ServicioOrden servicioOrden) =>
					await servicioOrden.EstablecerOrdenAsync(dto, principal.ObtenerIdUsuario()));

			grupoOrden.MapGet("", 
				async (ClaimsPrincipal principal, ServicioOrden servicioOrden) =>
			       TypedResults.Ok(await servicioOrden.ObtenerOrdenUsuarioAsync(principal.ObtenerIdUsuario())));

			grupoOrden.MapGet("/{ordenId:long}/items",
				async (long ordenId, ClaimsPrincipal principal, ServicioOrden servicioOrden) =>
					TypedResults.Ok(await servicioOrden.ObtenerItemsOrdenUsuarioAsync(ordenId, principal.ObtenerIdUsuario())));			

			return app;
		}
			
	}
}
