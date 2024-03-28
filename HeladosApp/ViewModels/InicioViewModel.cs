using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeladosApp.Pages;
using HeladosApp.Servicios;
using HeladosMaui.Base.DTOs;

namespace HeladosApp.ViewModels
{
	public partial class InicioViewModel(IHeladosApi heladosApi, AutorizacionServicio autorizacionServicio) : BaseViewModel
	{
		// Propiedades.
		[ObservableProperty]
		private HeladoDto[] _heladoDtos = [];

		[ObservableProperty]
		private string _nombreUsuario = string.Empty;

		private readonly IHeladosApi _heladosApi = heladosApi;
		private readonly AutorizacionServicio _autorizacionServicio = autorizacionServicio;

		private bool _estaInicializado;		


		// Metodos.

		// Inicializado.
		public async Task InicializadoASync()
		{
			NombreUsuario = _autorizacionServicio.UsuarioRegistrado!.Nombre;

			// verifica si el viewModel esta inicializado.
			if (_estaInicializado)
				return;

			EstaDisponible = true;
			try
			{
				// llama a la api para buscar "helados".
				_estaInicializado = true;
				HeladoDtos = await _heladosApi.ObtenerHeladosAsync();
			}
			catch (Exception ex)
			{
				_estaInicializado = false;
				await ErrorAlertAsync(ex.Message);
			}
			finally
			{
				EstaDisponible = false;
			}
		}

		// Ir a la paginad de detalles
		[RelayCommand]
		private async Task IrAPaginaDetalleAsync(HeladoDto heladoDto) 
		{
			var parametro = new Dictionary<string, object>
			{
				[nameof(DetallesViewModel.Helado)] = heladoDto
			};
			await GoToAsync(nameof(PaginaDetalles), animate: true, parametro);
		}			
	}
}
