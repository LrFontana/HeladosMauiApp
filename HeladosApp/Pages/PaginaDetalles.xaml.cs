using HeladosApp.ViewModels;

namespace HeladosApp.Pages;

public partial class PaginaDetalles : ContentPage
{
	public PaginaDetalles(DetallesViewModel detallesViewMdodel)
	{
		InitializeComponent();
		BindingContext = detallesViewMdodel;
	}
}