using HeladosApp.Pages;
using HeladosApp.Servicios;

namespace HeladosApp
{
	public partial class AppShell : Shell
	{
		// Propiedades.
		public AutorizacionServicio _autorizacionServicio;

		private readonly static Type[] _tipoDeRutasPaginas =
			[
				typeof(PaginaInicioDeSesion),
				typeof(PaginaRegistro),
				typeof(PaginaOrden),
				typeof(PaginaDetalleDeOrden),
				typeof(PaginaDetalles),
			];

		


		// Constructor.
		public AppShell(AutorizacionServicio autorizacionServicio)
		{
			InitializeComponent();
			RegistrarRutas();
			_autorizacionServicio = autorizacionServicio;
		}

		// Metodos.

		// Registrar Rutas.
		private static void RegistrarRutas()
		{
			foreach (var tipoRutas in _tipoDeRutasPaginas)
			{
				Routing.RegisterRoute(tipoRutas.Name, tipoRutas);
			}
		}

		private async void TapLabelDesarrolladoPor_Tapped(object sender, TappedEventArgs e)
		{
			await Launcher.OpenAsync("https://www.linkedin.com/in/leandro-fontana-40a750234/");
		}

		private async void CerrarSesion_Clicked(object sender, EventArgs e)
		{
			//await Shell.Current.DisplayAlert("Alert", "Click cerrar sesion", "Ok");
			_autorizacionServicio.CerrarSesion();
			await Shell.Current.GoToAsync($"//{nameof(PaginaDeAcople)}");
		}
	}
}
