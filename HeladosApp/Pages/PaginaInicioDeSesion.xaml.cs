using HeladosApp.ViewModels;

namespace HeladosApp.Pages;

public partial class PaginaInicioDeSesion : ContentPage
{
	public PaginaInicioDeSesion(AutorizacionViewModel autorizacionViewModel)
	{
		InitializeComponent();
		BindingContext = autorizacionViewModel;
	}

	private async void LabelRegistrarse_Tapped(object sender, TappedEventArgs e)
	{
		// va a la pagina registrarse.
		await Shell.Current.GoToAsync(nameof(PaginaRegistro));
	}


}