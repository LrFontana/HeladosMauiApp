using HeladosApp.ViewModels;

namespace HeladosApp.Pages;

public partial class PaginaInicio : ContentPage
{
	// Propiedades.
	private readonly InicioViewModel _inicioViewModel;


	// Constructor.
	public PaginaInicio(InicioViewModel inicioViewModel)
	{
		InitializeComponent();
		_inicioViewModel = inicioViewModel;
		BindingContext = _inicioViewModel;
	}


	// Metodos.
	protected async override void OnAppearing()
	{
		await _inicioViewModel.InicializadoASync();
	}
}