using System.ComponentModel.DataAnnotations;

namespace HeladosMaui.Api.Data.Entidades
{
	public class Helado
    {
        // Propiedades.

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; }

        [Range(0.1, double.MaxValue)]
        public double Precio { get; set; }

        [Required, MaxLength(180)]
        public string Imagen { get; set; }

        public virtual ICollection<OpcionesDeHelado> OpcionesDeHelados { get; set; }
    }
}
