using HeladosMaui.Api.Data;
using HeladosMaui.Api.Data.Entidades;
using HeladosMaui.Base.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HeladosMaui.Api.Servicios
{
	public class ServicioAutorizacion(DataContext dataContext, ServicioToken servicioToken, ServicioContraseña servicioContraseña)
	{
		// Propiedades.
		private readonly DataContext _dataContext = dataContext;
		private readonly ServicioToken _servicioToken = servicioToken;
		private readonly ServicioContraseña _servicioContraseña = servicioContraseña;

		//Metodos.
		// Iniciar Sesion.
		public async Task<ResultadoConInfoDto<RespuestaAutorizacionDto>> IniciarSesionAsync(RequestIniciarSesionDto requestIniciarSesionDto)
		{
			// obtiene el usuario de la base de datos.
			var usuario = await _dataContext.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Email == requestIniciarSesionDto.Email);

			// verifica si este usuario existe.
			if (usuario == null)
				return ResultadoConInfoDto<RespuestaAutorizacionDto>.Fallido("El usuario no existe");

			// y si existe, verifica si la contraseña es correcta.
			if (!_servicioContraseña.CompararSiSonIguales(requestIniciarSesionDto.Contrseña, usuario.ContraseñaSalt, usuario.ContraseñaHash))
				return ResultadoConInfoDto<RespuestaAutorizacionDto>.Fallido("Contraseña incorrecta");

			// si pasa las verificaciones devuelve la respuesta.
			return GenerarRespuestaDeAutorizacion(usuario);
		}


		// Registrarse
		public async Task<ResultadoConInfoDto<RespuestaAutorizacionDto>> RegistrarseAsync(RequestRegistrarseDto requestRegistrarseDto)
		{
			// verifica si el email ya existe.
			if (await _dataContext.Usuarios.AsNoTracking().AnyAsync(u => u.Email == requestRegistrarseDto.Email))
			{
				return ResultadoConInfoDto<RespuestaAutorizacionDto>.Fallido("El mail ya existe");
			}

			// si el mail no existe, crea un nuevo usuario.
			var usuario = new Usuario
			{
				Email = requestRegistrarseDto.Email,
				Nombre = requestRegistrarseDto.Nombre,
				Direccion = requestRegistrarseDto.Direccion,
			};

			(usuario.ContraseñaSalt, usuario.ContraseñaHash) = _servicioContraseña.GenerarSaltYHash(requestRegistrarseDto.Contrseña);

			try
			{
				// agrega al nuevo usuario a la base de datos.
				await _dataContext.Usuarios.AddAsync(usuario);
				await _dataContext.SaveChangesAsync();
				return GenerarRespuestaDeAutorizacion(usuario);
			}
			catch (Exception ex)
			{
				return ResultadoConInfoDto<RespuestaAutorizacionDto>.Fallido(ex.Message);
			}


		}

		// Generar respuesta de autorizacion
		private ResultadoConInfoDto<RespuestaAutorizacionDto> GenerarRespuestaDeAutorizacion(Usuario usuario)
		{
			// registra al nuevo usuario y crea el token.
			var usuarioRegistrado = new UsuarioRegistrado(usuario.Id, usuario.Nombre, usuario.Email, usuario.Direccion);
			var token = _servicioToken.GenerarJwt(usuarioRegistrado);

			// devuelve la respuesta.
			var respuestaAutorizacion = new RespuestaAutorizacionDto(usuarioRegistrado, token);

			return ResultadoConInfoDto<RespuestaAutorizacionDto>.Exitoso(respuestaAutorizacion);
		}

		// Cambiar Contraseña.
		public async Task<ResultadoDto> CambiarContraseñaAsync(CambiarContraseñaDto dto, Guid usuarioId)
		{
			var usuario = await _dataContext.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioId);
			if (usuario is null)
				return ResultadoDto.Fallido("Solicitud invalida.");

			if (!_servicioContraseña.CompararSiSonIguales(dto.ContraseñaVieja, usuario.ContraseñaSalt, usuario.ContraseñaHash))
			{
				return ResultadoDto.Fallido("Contraseña Incorrecta.");
			}

			(usuario.ContraseñaSalt, usuario.ContraseñaHash) = _servicioContraseña.GenerarSaltYHash(dto.ContraseñaNueva);

			await _dataContext.SaveChangesAsync();
			return ResultadoDto.Exitoso();
		}
	}
}
