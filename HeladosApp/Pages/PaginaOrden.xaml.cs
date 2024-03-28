using HeladosApp.ViewModels;

namespace HeladosApp.Pages;

public partial class PaginaOrden : ContentPage
{
	// Propiedades.
	private readonly OrdenViewModel _ordenViewModel;

	// Constructor.
	public PaginaOrden(OrdenViewModel ordenViewModel)
	{
		InitializeComponent();
		_ordenViewModel = ordenViewModel;
		BindingContext = _ordenViewModel;
	}

	// Metodos.

	protected override async void OnAppearing()
	{
		await _ordenViewModel.InitializeAsync();
	}
}