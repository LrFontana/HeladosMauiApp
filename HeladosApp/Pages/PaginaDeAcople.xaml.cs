using HeladosApp.Servicios;

namespace HeladosApp.Pages;

public partial class PaginaDeAcople : ContentPage
{
	// Propiedades. 
	private readonly AutorizacionServicio _autorizacionServicio;

	// Constructor.
	public PaginaDeAcople(AutorizacionServicio autorizacionServicio)
	{
		InitializeComponent();
		_autorizacionServicio = autorizacionServicio;
	}

	// Metodos

	protected async override void OnAppearing()
	{
		// verifiva si el usuario ya esta logeado y va a la pagina de inicio.
		if(_autorizacionServicio.UsuarioRegistrado is not null 
		   && _autorizacionServicio.UsuarioRegistrado.Id != default
		   && !string.IsNullOrWhiteSpace(_autorizacionServicio.Token))
		{
			await Shell.Current.GoToAsync($"//{nameof(PaginaInicio)}");
		}
	}

	// Boton Iniciar Sesion.
	private async void IniciarSesion_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(PaginaInicioDeSesion));
	}

	// Boton Registrarse.
	private async void Registrarse_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(PaginaRegistro));
	}
}