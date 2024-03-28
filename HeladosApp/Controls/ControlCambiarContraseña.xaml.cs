using CommunityToolkit.Maui.Views;
using HeladosApp.ViewModels;

namespace HeladosApp.Controls;

public partial class ControlCambiarContraseña : Popup
{
	// Constructor.
	public ControlCambiarContraseña(CambiarContraseñaViewModel cambiarContraseñaViewModel)
	{
		InitializeComponent();
		BindingContext = cambiarContraseñaViewModel;
	}

	// Metodos.
	private async void CerrarPopup_Tapped(object sender, EventArgs e) => await CloseAsync();
}