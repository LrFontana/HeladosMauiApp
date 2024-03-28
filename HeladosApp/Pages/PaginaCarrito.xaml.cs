using HeladosApp.ViewModels;

namespace HeladosApp.Pages;

public partial class PaginaCarrito : ContentPage
{
	
	// Constructor.
	public PaginaCarrito(CarritoViewModel carritoViewModel)
	{
		InitializeComponent();
		BindingContext = carritoViewModel;
	}
}