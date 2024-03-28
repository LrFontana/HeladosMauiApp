using System.Security.Cryptography;
using System.Text;

namespace HeladosMaui.Api.Servicios
{
	public class ServicioContraseña
	{
		// Propiedades.
		private const int TamañoSalt = 10;

		// Metodos.

		// Generar Salt y Hash.
		public (string salt, string contraseñaHash) GenerarSaltYHash(string contraseñaComun)
		{
			// verifica que "contraseñaComun" no este nula o con espacios.
			if(string.IsNullOrWhiteSpace(contraseñaComun))
				throw new ArgumentNullException(nameof(contraseñaComun));

			// crea el salt y lo convierte a string.
			var randonNum = RandomNumberGenerator.GetBytes(TamañoSalt);
			var salt =  Convert.ToBase64String(randonNum);

			var contraseñaHashed = GenerarContraseñaHash(contraseñaComun, salt);

			return (salt, contraseñaHashed);
		}

		// Compara si son iguales las contraseñas que ingresa el usuario y la que hay en la base de datos.
		public bool CompararSiSonIguales(string contraseñaComun, string salt , string contraseñaHash)
		{
			var nuevaContraseñaHash = GenerarContraseñaHash(contraseñaComun, salt);

			return nuevaContraseñaHash == contraseñaHash;
		}

		// Generar Contraseña Hash.
		private static string GenerarContraseñaHash(string contraseñaComun, string salt)
		{
			// crea el hash y lo convierte a string.
			var bytes = Encoding.UTF8.GetBytes(contraseñaComun + salt);
			var hash = SHA256.HashData(bytes);

			return Convert.ToBase64String(hash);
		}
	}
}
