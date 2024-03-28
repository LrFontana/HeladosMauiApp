using HeladosMaui.Api.Data;
using HeladosMaui.Api.EndPoints;
using HeladosMaui.Api.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Helado");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

// Habilita autenticacion y autorizacion.
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(jwtOptions =>
	{
		jwtOptions.TokenValidationParameters = ServicioToken.ObtenerParametroValidacionToken(builder.Configuration);
	});
builder.Services.AddAuthorization();

// Servicios.
builder.Services.AddTransient<ServicioToken>()
				.AddTransient<ServicioContraseña>()
				.AddTransient<ServicioAutorizacion>()
				.AddTransient<ServicioHelado>()
				.AddTransient<ServicioOrden>();

var app = builder.Build();

#if DEBUG
MigracionesALaBaseDeDatos(app.Services);
#endif

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapaEndPoints();


app.Run("http://127.0.0.1:5039");

// Migracion Db.
static void MigracionesALaBaseDeDatos(IServiceProvider services)
{
	var scope = services.CreateScope();
	var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
	if (dataContext.Database.GetPendingMigrations().Any())
		dataContext.Database.Migrate();

}


