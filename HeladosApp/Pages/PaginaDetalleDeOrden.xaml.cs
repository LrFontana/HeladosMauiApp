using HeladosApp.ViewModels;

namespace HeladosApp.Pages;

public partial class PaginaDetalleDeOrden : ContentPage
{
	// Constructor.
	public PaginaDetalleDeOrden(DetallesOrdenViewModel detallesOrdenViewModel)
	{
		InitializeComponent();
		BindingContext = detallesOrdenViewModel;
	}
}