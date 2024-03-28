using HeladosMaui.Api.Data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace HeladosMaui.Api.Data
{
	public class DataContext : DbContext
	{
        //Constructor.
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }

        // Db Set.
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Helado> Helados { get; set; }
        public DbSet<OpcionesDeHelado> OpcionesDeHelados { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<DetalleOrden> DetalleOrdenes { get; set; }

		// Metodos.
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<OpcionesDeHelado>()
                        .HasKey(op => new { op.HeladoId, op.Sabor, op.Agregados });

			AgregarDatos(modelBuilder);
		}

        private static void AgregarDatos(ModelBuilder modelBuilder)
        {
			Helado[] helados = [
			new Helado { Id = 1, Nombre = "Vanilla Delight", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_0.jpg", Precio = 60.25 },
			new Helado { Id = 2, Nombre = "ChocoBerry Bliss", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_1.jpg", Precio = 70.5 },
			new Helado { Id = 3, Nombre = "Minty Cookie Crunch", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_2.jpg", Precio = 80.75 },
			new Helado { Id = 4, Nombre = "Strawberry Dream", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_3.jpg", Precio = 60.95 },
			new Helado { Id = 5, Nombre = "Cookie Dough Extravaganza", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_4.jpg", Precio = 90.2 },
			new Helado { Id = 6, Nombre = "Caramel Swirl Symphony", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_5.jpg", Precio = 70.75 },
			new Helado { Id = 7, Nombre = "Peanut Butter Paradise", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_6.jpg", Precio = 80.5 },
			new Helado { Id = 8, Nombre = "Mango Tango Tango", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_7.jpg", Precio = 90.8 },
			new Helado { Id = 9, Nombre = "Hazelnut Heaven", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_8.jpg", Precio = 80.95 },
			new Helado { Id = 10, Nombre = "Blueberry Bliss Bonanza", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_9.jpg", Precio = 70.2 },
			new Helado { Id = 11, Nombre = "Toffee Twist Temptation", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_10.jpg", Precio = 70.95 },
			new Helado { Id = 12, Nombre = "Rocky Road Revelry", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_11.jpg", Precio = 90.5 },
			new Helado { Id = 13, Nombre = "Passionfruit Paradise", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_12.jpg", Precio = 80.75 },
			new Helado { Id = 14, Nombre = "Cotton Candy Carnival", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_13.jpg", Precio = 70.2 },
			new Helado { Id = 15, Nombre = "Lemon Sorbet Serenity", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_14.jpg", Precio = 60.9 },
			new Helado { Id = 16, Nombre = "Maple Pecan Pleasure", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_15.jpg", Precio = 80.25 },
			new Helado { Id = 17, Nombre = "Raspberry Ripple Rhapsody", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_16.jpg", Precio = 70.45 },
			new Helado { Id = 18, Nombre = "Almond Joyful Jubilee", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_17.jpg", Precio = 90.95 },
			new Helado { Id = 19, Nombre = "Blue Lagoon Bliss", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_18.jpg", Precio = 80.5 },
			new Helado { Id = 20, Nombre = "Coconut Caramel Carnival", Imagen = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_19.jpg", Precio = 70.8 }
			];

			OpcionesDeHelado[] opcionesDeHelados = [
			new OpcionesDeHelado { HeladoId = 1, Sabor = "Vanilla", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 1, Sabor = "Default", Agregados = "Chocolate Sauce" },
			new OpcionesDeHelado { HeladoId = 1, Sabor = "Default", Agregados = "Whipped Cream" },
			new OpcionesDeHelado { HeladoId = 2, Sabor = "Chocolate", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 2, Sabor = "Strawberry", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 2, Sabor = "Default", Agregados = "Sprinkles" },
			new OpcionesDeHelado { HeladoId = 3, Sabor = "Mint Chocolate Chip", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 3, Sabor = "Default", Agregados = "Cherries" },
			new OpcionesDeHelado { HeladoId = 3, Sabor = "Default", Agregados = "Chocolate Sauce" },
			new OpcionesDeHelado { HeladoId = 4, Sabor = "Strawberry", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 4, Sabor = "Default", Agregados = "Sprinkles" },
			new OpcionesDeHelado { HeladoId = 4, Sabor = "Default", Agregados = "Whipped Cream" },
			new OpcionesDeHelado { HeladoId = 5, Sabor = "Cookie Dough", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 5, Sabor = "Default", Agregados = "Chocolate Sauce" },
			new OpcionesDeHelado { HeladoId = 5, Sabor = "Default", Agregados = "Cherries" },
			new OpcionesDeHelado { HeladoId = 6, Sabor = "Vanilla", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 6, Sabor = "Default", Agregados = "Chocolate Sauce" },
			new OpcionesDeHelado { HeladoId = 6, Sabor = "Default", Agregados = "Cherries" },
			new OpcionesDeHelado { HeladoId = 7, Sabor = "Chocolate", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 7, Sabor = "Default", Agregados = "Whipped Cream" },
			new OpcionesDeHelado { HeladoId = 7, Sabor = "Default", Agregados = "Sprinkles" },
			new OpcionesDeHelado { HeladoId = 8, Sabor = "Strawberry", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 8, Sabor = "Cookie Dough", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 8, Sabor = "Default", Agregados = "Sprinkles" },
			new OpcionesDeHelado { HeladoId = 9, Sabor = "Mint Chocolate Chip", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 9, Sabor = "Default", Agregados = "Chocolate Sauce" },
			new OpcionesDeHelado { HeladoId = 9, Sabor = "Default", Agregados = "Whipped Cream" },
			new OpcionesDeHelado { HeladoId = 10, Sabor = "Chocolate", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 10, Sabor = "Strawberry", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 10, Sabor = "Default", Agregados = "Cherries" },
			new OpcionesDeHelado { HeladoId = 11, Sabor = "Vanilla", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 11, Sabor = "Default", Agregados = "Whipped Cream" },
			new OpcionesDeHelado { HeladoId = 11, Sabor = "Default", Agregados = "Cherries" },
			new OpcionesDeHelado { HeladoId = 12, Sabor = "Chocolate", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 12, Sabor = "Mint Chocolate Chip", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 12, Sabor = "Default", Agregados = "Chocolate Sauce" },
			new OpcionesDeHelado { HeladoId = 13, Sabor = "Strawberry", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 13, Sabor = "Default", Agregados = "Sprinkles" },
			new OpcionesDeHelado { HeladoId = 13, Sabor = "Default", Agregados = "Whipped Cream" },
			new OpcionesDeHelado { HeladoId = 14, Sabor = "Cookie Dough", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 14, Sabor = "Default", Agregados = "Chocolate Sauce" },
			new OpcionesDeHelado { HeladoId = 14, Sabor = "Default", Agregados = "Cherries" },
			new OpcionesDeHelado { HeladoId = 15, Sabor = "Vanilla", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 15, Sabor = "Strawberry", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 15, Sabor = "Default", Agregados = "Sprinkles" },
			new OpcionesDeHelado { HeladoId = 16, Sabor = "Chocolate", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 16, Sabor = "Mint Chocolate Chip", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 16, Sabor = "Default", Agregados = "Whipped Cream" },
			new OpcionesDeHelado { HeladoId = 17, Sabor = "Strawberry", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 17, Sabor = "Cookie Dough", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 17, Sabor = "Default", Agregados = "Chocolate Sauce" },
			new OpcionesDeHelado { HeladoId = 18, Sabor = "Vanilla", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 18, Sabor = "Default", Agregados = "Sprinkles" },
			new OpcionesDeHelado { HeladoId = 18, Sabor = "Default", Agregados = "Cherries" },
			new OpcionesDeHelado { HeladoId = 19, Sabor = "Chocolate", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 19, Sabor = "Strawberry", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 19, Sabor = "Default", Agregados = "Whipped Cream" },
			new OpcionesDeHelado { HeladoId = 20, Sabor = "Mint Chocolate Chip", Agregados = "Default" },
			new OpcionesDeHelado { HeladoId = 20, Sabor = "Default", Agregados = "Chocolate Sauce" },
			new OpcionesDeHelado { HeladoId = 20, Sabor = "Default", Agregados = "Sprinkles" }
			];


			modelBuilder.Entity<Helado>().HasData(helados);

			modelBuilder.Entity<OpcionesDeHelado>().HasData(opcionesDeHelados);
		}
	}
}
