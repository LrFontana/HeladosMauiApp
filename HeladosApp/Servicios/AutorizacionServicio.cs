using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HeladosApp.Servicios
{
	public class AutorizacionServicio
	{
		// Propiedades.
		private const string LlaveDeAutorizacion = "LlaveDeAutorizacion";
		public  UsuarioRegistrado? UsuarioRegistrado { get;  private set; }
        public string? Token { get; set; }


		// Metodos.

		// Iniciar sesion.
		public void IniciarSesion(RespuestaAutorizacionDto respuestaAutorizacionDto)
		{
			// serializacion del dto y almacenamiento de la informacion en las preferencias.
			var serializacion = JsonSerializer.Serialize(respuestaAutorizacionDto);
			Preferences.Default.Set(LlaveDeAutorizacion, serializacion);

			// establece el usuario y token en el dto.
			(UsuarioRegistrado, Token) = respuestaAutorizacionDto;
		}

		// Initialize
		public void Initialize()
		{
			// verifica si la llave de autorizacion existe en las preferencias.
			if(Preferences.Default.ContainsKey(LlaveDeAutorizacion))
			{
				// valida la "LlaveDeAutorizacion" y sino la elimina
				var serializacion = Preferences.Default.Get<string?> (LlaveDeAutorizacion, null);
				if(string.IsNullOrEmpty(serializacion))
				{
					Preferences.Default.Remove(LlaveDeAutorizacion);
				}
				else
				{
					(UsuarioRegistrado, Token) = JsonSerializer.Deserialize<RespuestaAutorizacionDto>(serializacion)!;
				}
			}
		}

		// Cerrar Sesion.
		public void CerrarSesion()
		{
			// Elimina las preferencias y establece los valores en null.
			Preferences.Default.Remove(LlaveDeAutorizacion);
			(UsuarioRegistrado, Token) = (null, null);
		}
    }
}
