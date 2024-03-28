    using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeladosApp.Controls;
using HeladosApp.Pages;
using HeladosApp.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeladosApp.ViewModels
{
	public partial class PerfilViewModel : BaseViewModel
	{
        // Propiedades.
        private readonly AutorizacionServicio _autorizacionServicio;
        private readonly CambiarContraseñaViewModel _controlCambiarContrasea;


		[ObservableProperty, NotifyPropertyChangedFor(nameof(Iniciales))]
        private string _nombre = "";

        public string Iniciales
        {
            get
            {
				// Leandro fontana -> nombreSeparado[0] = Leandro     nombreSeparado[1] = Fontana
				var nombreSeparado = Nombre.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                if(nombreSeparado.Length > 1 ) 
                {
                    return $"{nombreSeparado[0][0]}{nombreSeparado[1][0]}".ToUpper(); // -> LF
                }

                return Nombre.Length > 1 ? Nombre[..1].ToUpper() : Nombre.ToUpper();
			}
        }

        // Constructor.
        public PerfilViewModel(AutorizacionServicio autorizacionServicio, CambiarContraseñaViewModel controlCambiarContraseña)
        {
            _autorizacionServicio = autorizacionServicio;
            _controlCambiarContrasea = controlCambiarContraseña;

		}

		// Comandos.
		[RelayCommand]
		private async Task CerrarSesionAsync()
		{
			_autorizacionServicio.CerrarSesion();
			await GoToAsync($"//{nameof(PaginaDeAcople)}");
		}

        [RelayCommand]
        private async Task IrAMisOrdenesAsync() => await GoToAsync(nameof(PaginaOrden), animate: true);

        [RelayCommand]
        private async Task CambiarContraseñaAsync()
        {
            await Shell.Current.CurrentPage.ShowPopupAsync(new ControlCambiarContraseña(_controlCambiarContrasea));
        }

		// Metodos.

		// Initialize
		public void Initialize()
        {
            Nombre = _autorizacionServicio.UsuarioRegistrado!.Nombre;
        }
    }
}
