using HeladosApp.Servicios;
using HeladosApp.ViewModels;

namespace HeladosApp
{
	public partial class App : Application
	{
		// Propiedades.
		private readonly CarritoViewModel _carritoViewModel;

		// Constructor.
		public App(AutorizacionServicio autorizacionServicio, CarritoViewModel carritoViewModel)
		{
			InitializeComponent();

			autorizacionServicio.Initialize();

			MainPage = new AppShell(autorizacionServicio);
			_carritoViewModel = carritoViewModel;
		}

		// Metodo.
		protected override async void OnStart()
		{
			await _carritoViewModel.InicializarCarritoASync();
		}
	}
}
