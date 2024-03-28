using System.ComponentModel.DataAnnotations;

namespace HeladosMaui.Api.Data.Entidades
{
	public class Orden
    {
        // Propiedades.

        [Key]
        public long Id { get; set; }
        public DateTime OrdenFecha { get; set; } = DateTime.Now;    
        public Guid ClienteId { get; set; }

		[Required, MaxLength(15)]
		public string ClienteNombre{ get; set; }

		[Required, MaxLength(25)]
		public string ClienteEmail { get; set; }

		[Required, MaxLength(40)]
		public string ClienteDireccion{ get; set; }

        [Range(0.1, double.MaxValue)]
        public double PrecioTotal { get; set; }

        public virtual ICollection<DetalleOrden> DetalleOrdenes { get; set; }
    }
}
