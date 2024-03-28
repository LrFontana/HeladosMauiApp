using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeladosApp.Models;
using HeladosMaui.Base.DTOs;

namespace HeladosApp.ViewModels
{
	[QueryProperty(nameof(Helado), nameof(Helado))]
	public partial class DetallesViewModel : BaseViewModel
	{
		// Propiedades.
		private readonly CarritoViewModel _carritoViewModel;

		[ObservableProperty]
		private HeladoDto? _helado;

		[ObservableProperty]
		private int _cantidad;

		[ObservableProperty]
		private OpcionesDeHelado[] _opcionesHelado = [];

        // Constructor.
        public DetallesViewModel(CarritoViewModel carritoViewModel)
        {
            _carritoViewModel = carritoViewModel;
        }

        // Metodo.

        partial void OnHeladoChanged(HeladoDto? value)
		{
			OpcionesHelado = [];

			if (value is null)
				return;

			OpcionesHelado = value.OpcionesDeHeladoDtos.Select(op => new OpcionesDeHelado
			{
				Sabor = op.Sabor,
				Agregado = op.Agregado,
				EstaSeleccionado = false
			}).ToArray();

			Cantidad = _carritoViewModel.ObtenerCantidadDeItemCarrito(value.Id);
		}

		// Comandos.
		[RelayCommand]
		private void IncrementarCantidad() => Cantidad++;

		[RelayCommand]
		private void DisminuirCantidad()
		{
			if (Cantidad > 0)
			{
				Cantidad--;
			}
		}

		[RelayCommand]
		private async Task IrAtrasAsync() => await GoToAsync("..", animate: true); // va hacia la pagina anterior/previa

		[RelayCommand]
		private void SeleccionarOpciones(OpcionesDeHelado nuevaOpcion)
		{
			var nuevaOpcionSeleccionada = !nuevaOpcion.EstaSeleccionado;

			// elimina todas las opciones anteriores
			OpcionesHelado = [..OpcionesHelado.Select(o => { o.EstaSeleccionado = false; return o; })];

			nuevaOpcion.EstaSeleccionado = nuevaOpcionSeleccionada;
		}

		[RelayCommand]
		private async Task AgregarAlCarritoAsync()
		{
			var seleccionarOpciones = OpcionesHelado.FirstOrDefault(op => op.EstaSeleccionado) ?? OpcionesHelado[0];
			_carritoViewModel.AgregarItemAlCarrito(Helado!, Cantidad, seleccionarOpciones.Sabor, seleccionarOpciones.Agregado);
			await IrAtrasAsync();
		}
	}
}
