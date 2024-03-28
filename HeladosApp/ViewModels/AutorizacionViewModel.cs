using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeladosApp.Pages;
using HeladosApp.Servicios;
using HeladosMaui.Base.DTOs;

namespace HeladosApp.ViewModels
{
	public  partial class AutorizacionViewModel(IAutorizacionApi autorizacionApi, AutorizacionServicio autorizacionServicio) : BaseViewModel
	{
        // Propiedades.
		private readonly IAutorizacionApi _api = autorizacionApi;
		private readonly AutorizacionServicio _autorizacionServicio = autorizacionServicio;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(PuedeRegistrarse))]
        private string? _nombre;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(PuedeIniciarSesion)), NotifyPropertyChangedFor(nameof(PuedeRegistrarse))]
        private string? _email;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(PuedeIniciarSesion)), NotifyPropertyChangedFor(nameof(PuedeRegistrarse))]
        private string? _contraseña;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(PuedeRegistrarse))]
        private string? _direccion;		


		// verifica que los campos no esten nulos o vacios.
		public bool PuedeIniciarSesion => !string.IsNullOrEmpty(Email)
									   && !string.IsNullOrEmpty(Contraseña);

		// verifica que los campos no esten nulos o vacios.
		public bool PuedeRegistrarse => PuedeIniciarSesion
			                         && !string.IsNullOrEmpty(Contraseña)
			                         && !string.IsNullOrEmpty(Direccion);

		// Metodos.

		// Iniciar sesion.
		[RelayCommand]
		private async Task IniciarSesionAsync()
		{
			EstaDisponible = true;

			try
			{
				// genera el nuevo dto
				var iniciarSesionDto = new RequestIniciarSesionDto(Email, Contraseña);

				// lama a la api
				var resultado = await _api.IniciarSesionAsync(iniciarSesionDto);

				if (resultado.EsExitoso)
				{
					_autorizacionServicio.IniciarSesion(resultado.Data);

					// va a la pagina de inicio.
					await GoToAsync($"//{nameof(PaginaInicio)}", animate: true);
				}
				else
				{
					await ErrorAlertAsync(resultado.MsjError ?? "Error desconocido al intentar iniciar sesion");
				}
			}
			catch (Exception ex)
			{
				await ErrorAlertAsync(ex.Message);
			}
			finally
			{
				EstaDisponible = false;
			}
		}

		// Registrarse.
		[RelayCommand]
		private async Task RegistrarseAsync()
        {
			EstaDisponible = true;

			try
			{
				// genera el nuevo dto
				var registrarseDto = new RequestRegistrarseDto(Nombre, Email, Contraseña, Direccion);

				// lama a la api
				var resultado = await _api.RegistrarseAsync(registrarseDto);
				
				if (resultado.EsExitoso)
				{
					_autorizacionServicio.IniciarSesion(resultado.Data);

					// va a la pagina de inicio.
					await GoToAsync($"//{nameof(PaginaInicio)}", animate: true);
				}
				else
				{
					await ErrorAlertAsync(resultado.MsjError?? "Error desconocido al intentar registrarse");
				}
			}
			catch (Exception ex)
			{
				await ErrorAlertAsync(ex.Message);
			}
			finally
			{
				EstaDisponible = false;
			}
        }
    }
}
