using CommunityToolkit.Mvvm.Input;
using HeladosApp.Models;
using HeladosApp.Pages;
using HeladosApp.Servicios;
using HeladosMaui.Base.DTOs;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeladosApp.ViewModels
{
	public partial class CarritoViewModel : BaseViewModel
	{
        // Propiedades.
        public ObservableCollection<ItemCarrito> ItemsCarrito { get; set; } = [];
        public static int ContadorTotalCarrito { get; set; }
		

		public static event EventHandler<int>? ContadorTotalCarritoCambio;

        private readonly BaseDeDatosServicio _baseDeDatosServicio;
		private readonly IOrdenApi _ordenApi;
		private readonly AutorizacionServicio _autorizacionServicio;

		// Constructor.
		public CarritoViewModel(BaseDeDatosServicio baseDeDatosServicio, IOrdenApi ordenApi, AutorizacionServicio autorizacionServicio)
        {
			_baseDeDatosServicio = baseDeDatosServicio;
			_ordenApi = ordenApi;
			_autorizacionServicio = autorizacionServicio;
		}


        // Metodos.

        // Agregar item.
        public async void AgregarItemAlCarrito(HeladoDto heladoDto, int cantidad, string sabor, string agregado)
		{
			var itemExiste = ItemsCarrito.FirstOrDefault(ic => ic.HeladoId == heladoDto.Id);

			if (itemExiste is not null)
			{
				var dbItemCarrito = await _baseDeDatosServicio.ObtenerItemCarritoAsync(itemExiste.Id);

				if (cantidad <= 0)
				{
					// el usuario quiere eliminar este item del carrito.

					await _baseDeDatosServicio.EliminarItemCarrito(dbItemCarrito);
					ItemsCarrito.Remove(itemExiste);
					await MostrarToastAsync("Helado eliminado del carrito.");
				}
				else
				{
					dbItemCarrito.Cantidad = cantidad;
					await _baseDeDatosServicio.ActualizarItemCarrito(dbItemCarrito);

					itemExiste.Cantidad = cantidad;
					await MostrarToastAsync("La cantidad en el carrito a sido actualizada.");
				}
			}
			else
			{
				// Crea el nuevo Item.
				var itemCarrito = new ItemCarrito
				{
					NombreSabor = sabor,
					HeladoId = heladoDto.Id,
					Nombre = heladoDto.Nombre,
					Precio = heladoDto.Precio,
					Cantidad = cantidad,
					NombreAgregado = agregado
				};

				var entidad = new Data.EntidadItemCarrito(itemCarrito);
				await _baseDeDatosServicio.AgregarItemCarrito(entidad);

				itemCarrito.Id = entidad.Id;

				ItemsCarrito.Add(itemCarrito);

				await MostrarToastAsync("Helado agregado al carrito.");
			}

			NotificarContadorCarritoCambio();
		}

		// Notificar cambio del contador del carrito.
		private void NotificarContadorCarritoCambio()
		{
			ContadorTotalCarrito = ItemsCarrito.Sum(i => i.Cantidad);
			ContadorTotalCarritoCambio?.Invoke(null, ContadorTotalCarrito);
		}

		// Obtener la cantidad de items en el carrito.
		public int ObtenerCantidadDeItemCarrito(int heladoId)
        {
            var itemExistente = ItemsCarrito.FirstOrDefault(i => i.HeladoId == heladoId);
            return itemExistente?.Cantidad ?? 0;

		}

        // Inicializar carrito.
        public async Task InicializarCarritoASync()
        {
            var dbItems = await _baseDeDatosServicio.ObtenerTodosLosItemCarritoAsync();

            foreach (var dbItem in dbItems)
            {
                ItemsCarrito.Add(dbItem.ConvertirAItemCarritoModel());
            }
			NotificarContadorCarritoCambio();

		}

		// Limpiar/Eliminar todos los items del carrito.
		[RelayCommand]
		private async Task LimpiarCarritoAsync()
		{
			await ConfirmarLimpiarCarritoAsync(desdeEstablecerOrden: false);
		}

		//  Confirmar limpiar el carrito
		private async Task ConfirmarLimpiarCarritoAsync(bool desdeEstablecerOrden)
		{
			if (!desdeEstablecerOrden && ItemsCarrito.Count == 0)
			{
				await MostrarAlertAsync("Carrito Vacio.", "No hay helados en el carrito.");
				return;
			}

			if (desdeEstablecerOrden || await ConfirmarAsync("Limpiar Carrito?", "Esta seguro que ud quiere eliminar todos los helados del carrito?"))
			{
				await _baseDeDatosServicio.LimpiarCarritoAsync();
				ItemsCarrito.Clear();

				if (!desdeEstablecerOrden)
					await MostrarToastAsync("Carrito Limpio.");

				NotificarContadorCarritoCambio();
			}
		}

		// Eliminar item del carrito por id.
		[RelayCommand]
		private async Task EliminarItemCarritoAsync(int itemCarritoId)
		{
			if (await ConfirmarAsync("Eliminar Helado Del Carrito?", "Esta seguro que ud quiere eliminar este helado del carrito?"))
			{
				var heladoExistente = ItemsCarrito.FirstOrDefault(i => i.Id == itemCarritoId);
				if (heladoExistente is null)
					return;

				ItemsCarrito.Remove(heladoExistente);

				var carritoItemDb = await _baseDeDatosServicio.ObtenerItemCarritoAsync(itemCarritoId);
				if(carritoItemDb is null)
					return;

				await _baseDeDatosServicio.EliminarItemCarrito(carritoItemDb);
				
				await MostrarToastAsync("Helado Eliminado del carrito.");
				NotificarContadorCarritoCambio();
			}
		}

		// Establecer/Armar la orden.
		[RelayCommand]
		private async Task EstablecerOdernAsync()
		{
			if (ItemsCarrito.Count == 0)
			{
				await MostrarAlertAsync("Carrito Vacio.", "Por favor seleccione algun helado antes de establecer la orden.");
				return;
			}

			EstaDisponible = true;

			try
			{
				var ordern = new OrdenDto(0, DateTime.Now, ItemsCarrito.Sum(i => i.PrecioTotal));
				var itemsOrden = ItemsCarrito.Select(i => new OrdenItemDto(0, i.HeladoId, i.Cantidad, i.Precio, i.Nombre, i.NombreSabor, i.NombreAgregado)).ToArray();
				var establecerOrdenDto = new EstablecerOrdenDto(ordern, itemsOrden);

				var resultado = await _ordenApi.EstablecerOrdenAsync(establecerOrdenDto);
				if(!resultado.EsExitoso)
				{
					await MostrarAlertAsync(resultado.MsjError!);
					return;
				}

				
				await MostrarToastAsync("Orden Confirmada.");
				await ConfirmarLimpiarCarritoAsync(desdeEstablecerOrden: true);
			}
			catch (ApiException ex)
			{
				await ManejarExcepcionesApiAsync(ex, () => _autorizacionServicio.CerrarSesion());
			}
			finally
			{
				EstaDisponible = false;
			}
		}
    }
}
