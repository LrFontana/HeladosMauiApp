using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeladosApp.Servicios;
using HeladosMaui.Base.DTOs;
using Refit;

namespace HeladosApp.ViewModels
{
	public partial class CambiarContraseñaViewModel : BaseViewModel
	{
		// Propiedades.
		private readonly AutorizacionServicio _autorizacionServicio;
		private readonly IAutorizacionApi _autorizacionApi;

		[ObservableProperty, NotifyPropertyChangedFor(nameof(PuedeCambiarLaContraseña))]
		private string? _contraseñaVieja;

		[ObservableProperty, NotifyPropertyChangedFor(nameof(PuedeCambiarLaContraseña))]
		private string? _contraseñaNueva;

		[ObservableProperty, NotifyPropertyChangedFor(nameof(PuedeCambiarLaContraseña))]
		private string? _confirmarNuevaContraseña;

		public bool PuedeCambiarLaContraseña => !string.IsNullOrWhiteSpace(ContraseñaVieja)
											&& !string.IsNullOrWhiteSpace(ContraseñaNueva)
											&& !string.IsNullOrWhiteSpace(_confirmarNuevaContraseña);

		// Constructor.
		public CambiarContraseñaViewModel(AutorizacionServicio autorizacionServicio, IAutorizacionApi autorizacionApi)
		{
			_autorizacionServicio = autorizacionServicio;
			_autorizacionApi = autorizacionApi;
		}

		// Comandos.
		[RelayCommand]
		private async Task CambiarContraseñaAsync()
		{
			if (ContraseñaNueva != ConfirmarNuevaContraseña)
			{
				await ErrorAlertAsync("La contraseña nueva y confirmar contraseña no son iguales");
				return;
			}

			EstaDisponible = true;

			try
			{
				var dto = new CambiarContraseñaDto(ContraseñaVieja!, ContraseñaNueva!);
				var resultado = await _autorizacionApi.CambiarContraseñaAsync(dto);
				if (!resultado.EsExitoso)
				{
					await ErrorAlertAsync(resultado.MsjError);
					return;
				}

				await MostrarAlertAsync("Exitoso", "Contraseña cambiada exitosamente.");
				ContraseñaVieja =  ContraseñaNueva = ConfirmarNuevaContraseña = null;
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
	}
}
