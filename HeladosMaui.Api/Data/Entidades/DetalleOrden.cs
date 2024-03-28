using System.ComponentModel.DataAnnotations;

namespace HeladosMaui.Api.Data.Entidades
{
	public class DetalleOrden
    {
        // Propiedades.

        [Key]
        public long Id { get; set; }
        public long OrdenId { get; set; }
        public int HeladoId { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; }

        [Range(0.1, double.MaxValue)]
        public double Precio { get; set; }

        [Range(0.1, 100)]
        public int Cantidad { get; set; }

		[Required, MaxLength(50)]
		public string Sabor { get; set; }

		[Required, MaxLength(50)]
		public string Agregados { get; set; }

        public double PrecioTotal { get; set; } 


		public virtual Orden Orden { get; set; }

    }
}
