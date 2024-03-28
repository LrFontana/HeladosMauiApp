using HeladosApp.ViewModels;

namespace HeladosApp.Pages;

public partial class PaginaPerfiles : ContentPage
{
	// Propiedades.
	private readonly PerfilViewModel _perfilViewModel;

	// Constructor.
	public PaginaPerfiles(PerfilViewModel perfilViewModel)
	{
		InitializeComponent();
		BindingContext = perfilViewModel;
		_perfilViewModel = perfilViewModel;
	}

	// Metodos.

	//OnAppearing
	protected override void OnAppearing()
	{
		_perfilViewModel.Initialize();
	}
}