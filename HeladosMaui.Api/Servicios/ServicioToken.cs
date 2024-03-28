using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HeladosMaui.Api.Servicios
{
	public class ServicioToken(IConfiguration configuration)
	{
        // Propiedades.
        private readonly IConfiguration _configuracion = configuration;

		//Metodos.

		// Generar Token Jwt
		public string GenerarJwt(UsuarioRegistrado usuario)
        {
            // obtiene securitykey
            var securityKey = ObtenerSecurityKey(_configuracion);

			// genera las credenciales de inicio.
			var credenciales = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // obtiene el issuer, expire y genera el token.
            var issuer = _configuracion["Jwt:Issuer"];
            var expiraEnMinutos= Convert.ToInt32(_configuracion["Jwt:ExpireMinute"]);

            Claim[] claims = [
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name,usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.StreetAddress, usuario.Direccion),
                ];


            var token = new JwtSecurityToken(
                issuer: issuer, 
                audience:"*",
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiraEnMinutos),
                signingCredentials: credenciales);

            // convierte el objeto jwt  a string.
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        // Obtener parameetro de validacion
        public static TokenValidationParameters ObtenerParametroValidacionToken(IConfiguration configuration) =>
            new()
            {
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                IssuerSigningKey = ObtenerSecurityKey(configuration)
			};

        // Obtener Security Key
        private static SymmetricSecurityKey ObtenerSecurityKey(IConfiguration configuration)
        {
			// obtiene y genera la llave.
			var secretKey = configuration["Jwt:SecretKey"];
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            return securityKey;
		}
    }
}
