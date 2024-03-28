using System.ComponentModel.DataAnnotations;

namespace HeladosMaui.Api.Data.Entidades
{
	public class OpcionesDeHelado
    {
        // Propiedades.
        public int HeladoId { get; set; }

        [Required,MaxLength(50)]
        public string Sabor { get; set; }

        [Required,MaxLength(50)]
        public string Agregados { get; set; }
        public virtual Helado Helado{ get; set; }

    }
}
