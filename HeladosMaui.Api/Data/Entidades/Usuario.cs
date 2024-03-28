using System.ComponentModel.DataAnnotations;

namespace HeladosMaui.Api.Data.Entidades
{
	public class Usuario
	{
        // Propiedades.
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(15)]
        public string Nombre { get; set; }

        [Required, MaxLength(25)]
        public string Email { get; set; }

        [Required, MaxLength(40)]
        public string Direccion { get; set; }

        [Required, MaxLength(20)]
        public string ContraseñaSalt { get; set; }

        [Required, MaxLength(180)]
        public string ContraseñaHash { get; set; }
    }
}
