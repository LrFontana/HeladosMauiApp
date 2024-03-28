using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeladosApp.Pages;
using HeladosApp.Servicios;
using HeladosMaui.Base.DTOs;
using Refit;

namespace HeladosApp.ViewModels
{
	public partial class OrdenViewModel : BaseViewModel
	{
		// Propiedades.
		private readonly IOrdenApi _ordenApi;
		private readonly AutorizacionServicio _autorizacionServicio;

		[ObservableProperty]
		private OrdenDto[] _ordenes = [];


		// Constructor.
		public OrdenViewModel(AutorizacionServicio autorizacionServicio, IOrdenApi ordenApi)
		{
			_ordenApi = ordenApi;
			_autorizacionServicio = autorizacionServicio;
		}

		// Metodos.

		// Initialize.
		public async Task InitializeAsync() => await CarganOrdenAsync();

		// Comnandos.
		// Cargar orden.
		[RelayCommand]
		private async Task CarganOrdenAsync()
		{
			EstaDisponible = true;
			try
			{
				Ordenes = await _ordenApi.ObtenerMiOrdenAsync();
				if (Ordenes.Length == 0)
				{
					await MostrarToastAsync("No se encontro ninguna orden.");
				}
			}
			catch (ApiException ex)
			{
				await ManejarExcepcionesApiAsync(ex, () => _autorizacionServicio.CerrarSesion());
			}
			finally
			{
				EstaDisponible = false;
			}
		}

		// Ir a la pagina detalles de orden/pedido.
		[RelayCommand]
		private async Task IrAPaginaDetalleDeOrdenAsync(long ordenId)
		{
			var parametro = new Dictionary<string, object>
			{
				[nameof(DetallesOrdenViewModel.OrdenId)] = ordenId
			};

			await GoToAsync(nameof(PaginaDetalleDeOrden), animate: true, parametro);
		}
	}
}
