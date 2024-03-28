using HeladosApp.ViewModels;

namespace HeladosApp.Pages;

public partial class PaginaRegistro : ContentPage
{
	public PaginaRegistro(AutorizacionViewModel autorizacionViewModel)
	{
		InitializeComponent();
		BindingContext = autorizacionViewModel;
	}

	private async void LabelIciarSesion_Tapped(object sender, TappedEventArgs e)
	{
		// va a la pagina iniciar sesion.
		await Shell.Current.GoToAsync(nameof(PaginaInicioDeSesion));
    }
}