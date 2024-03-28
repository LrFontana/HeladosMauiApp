using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using HeladosApp.Pages;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeladosApp.ViewModels
{
	public partial class BaseViewModel : ObservableObject
	{
		// Propiedades.

		[ObservableProperty]
		private bool _estaDisponible;

		// Metodo
		protected async Task GoToAsync(string url, bool animate = false) =>	
			await Shell.Current.GoToAsync(url, animate);

		protected async Task GoToAsync(string url, bool animate, IDictionary<string, object> parametros) =>	
			await Shell.Current.GoToAsync(url, animate, parametros);
		
		protected async Task ErrorAlertAsync(string errorMensaje) => 
			await Shell.Current.DisplayAlert("Error", errorMensaje, "Ok");

		protected async Task MostrarAlertAsync(string mensaje) => await MostrarAlertAsync("Alerta!.", mensaje);

		protected async Task MostrarAlertAsync(string titulo, string mensaje) =>
			await Shell.Current.DisplayAlert(titulo, mensaje, "Ok");

		protected async Task MostrarToastAsync(string mensaje) => 
			await Toast.Make(mensaje).Show();

		protected async Task<bool> ConfirmarAsync(string titulo, string mensaje) => 
			await Shell.Current.DisplayAlert(titulo, mensaje, "Si", "No");

		protected async Task ManejarExcepcionesApiAsync(ApiException ex, Action? cerrarSesion = null) 
		{
			if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				// el usuario no esta logeado.
				await MostrarAlertAsync("La sesion expiro.");
				cerrarSesion?.Invoke();
				await GoToAsync($"//{nameof(PaginaDeAcople)}");
				return;
			}
		}

	}
}
