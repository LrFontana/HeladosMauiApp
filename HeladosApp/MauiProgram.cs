using CommunityToolkit.Maui;
using HeladosApp.Pages;
using HeladosApp.Servicios;
using HeladosApp.ViewModels;
using Microsoft.Extensions.Logging;
using Refit;

#if ANDROID
using System.Net.Security;
using Xamarin.Android.Net;
#elif IOS
using Security;
#endif

namespace HeladosApp
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				})
				.UseMauiCommunityToolkit()
				.ConfigureMauiHandlers(h =>
				{
#if ANDROID || IOS
					h.AddHandler<Shell, TabbarRenderer>();
#endif
				});

#if DEBUG
			builder.Logging.AddDebug();
#endif

			// Servicios.
			builder.Services.AddSingleton<BaseDeDatosServicio>();

			builder.Services.AddTransient<AutorizacionViewModel>()
							.AddTransient<PaginaRegistro>()
							.AddTransient<PaginaInicioDeSesion>();

			builder.Services.AddSingleton<AutorizacionServicio>();

			builder.Services.AddTransient<PaginaDeAcople>();

			builder.Services.AddSingleton<InicioViewModel>()
							.AddSingleton<PaginaInicio>();

			builder.Services.AddTransient<DetallesViewModel>()
							.AddTransient<PaginaDetalles>();

			builder.Services.AddSingleton<CarritoViewModel>()
							.AddTransient<PaginaCarrito>();

			builder.Services.AddTransient<PerfilViewModel>()
							.AddTransient<PaginaPerfiles>();

			builder.Services.AddTransient<OrdenViewModel>()
							.AddTransient<PaginaOrden>();

			builder.Services.AddTransient<DetallesOrdenViewModel>()
							.AddTransient<PaginaDetalleDeOrden>();

			builder.Services.AddTransient<CambiarContraseñaViewModel>();

			ConfigurarRefit(builder.Services);

			return builder.Build();
		}

		// Metodo.

		// Configuracion Refit.
		private static void ConfigurarRefit(IServiceCollection services)
		{
			
			services.AddRefitClient<IAutorizacionApi>(ObtenerRefitSettings)
				    .ConfigureHttpClient(EstablecerHttpClient
					);
			services.AddRefitClient<IHeladosApi>(ObtenerRefitSettings)
					.ConfigureHttpClient(EstablecerHttpClient);

			services.AddRefitClient<IOrdenApi>(ObtenerRefitSettings)
					.ConfigureHttpClient(EstablecerHttpClient);

			// Metodo Configuracion de http client.
			static void EstablecerHttpClient(HttpClient httpClient)
			{
				// verifica si es un dispositovo android genera la url de la api y la establece y sino lo corre en local.
				var urlBase = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7116" : "https://localhost/7116";

				// verifica si es un dispositivo fisico y crea un dev tunnel.
				if(DeviceInfo.DeviceType == DeviceType.Physical)
				{
					urlBase = "https://7lcsjg6p-7116.brs.devtunnels.ms";
				}
				httpClient.BaseAddress = new Uri(urlBase);
			}

			static RefitSettings ObtenerRefitSettings(IServiceProvider servicesProvider)
			{
				var autorizacionServicio = servicesProvider.GetRequiredService<AutorizacionServicio>();

				// configuracion del objeto refit.
				var configuracionesRefit = new RefitSettings
				{
					HttpMessageHandlerFactory = () =>
					{
						// devuelve HttpMessageHandlerFactory.
#if ANDROID
					return new AndroidMessageHandler()
					{
						ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, chain, sslPolicyErrors) =>
						{							
							return certificate?.Issuer == "CN=localhost" ||  sslPolicyErrors == SslPolicyErrors.None;
						}
					};
#elif IOS
					return new NSUrlSessionHandler
					{
						TrustOverrideForUrl = (NSUrlSessionHandler sender, string url, SecTrust trust) =>
							url.StartsWith("https://localhost")
					};
#endif
						return null;
					},
					AuthorizationHeaderValueGetter = (_, __) => 
						Task.FromResult(autorizacionServicio.Token ?? string.Empty) // devuelve jwt.
				};
				return configuracionesRefit;
			}
		} 
	}
}
