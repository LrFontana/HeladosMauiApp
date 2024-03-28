using CommunityToolkit.Mvvm.ComponentModel;
using HeladosApp.Servicios;
using HeladosMaui.Base.DTOs;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeladosApp.ViewModels
{
    [QueryProperty(nameof(OrdenId), nameof(OrdenId))]
	public partial class DetallesOrdenViewModel : BaseViewModel
	{
        // Propiedades.
        private readonly AutorizacionServicio _autorizacionServicio;
        private readonly IOrdenApi _ordenApi;

        [ObservableProperty]
        private long _ordenId;

        [ObservableProperty]
        private OrdenItemDto[] _ordenItems = [];

        [ObservableProperty]
        private string _titulo = "Helados Pedidos";

        
        // Constructor
        public DetallesOrdenViewModel(AutorizacionServicio autorizacionServicio, IOrdenApi ordenApi)
        {
            _autorizacionServicio = autorizacionServicio;
            _ordenApi = ordenApi;
        }

		// Metodos.

		// override OnOrdenIdChanged
		partial void OnOrdenIdChanged(long value)
		{
            Titulo = $"Pedido N°: {value}";
            CargarLosItemsDeOrdenAsync(value);
		}

        // Cargar los items de orden.
        private async Task CargarLosItemsDeOrdenAsync(long ordenId)
        {
            EstaDisponible = true;

            try
            {
                OrdenItems = await _ordenApi.ObtenerItemOrdenAsync(ordenId);
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
