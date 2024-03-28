using HeladosMaui.Api.Data;
using HeladosMaui.Api.Data.Entidades;
using HeladosMaui.Base.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HeladosMaui.Api.Servicios
{
	public class ServicioOrden(DataContext dataContext)
	{
		// Propiedades.
		private readonly DataContext _dataContext = dataContext;

		// Metodos.

		// Establecer orden.
		public async Task<ResultadoDto> EstablecerOrdenAsync(EstablecerOrdenDto dto, Guid clienteId)
		{
			var cliente = await _dataContext.Usuarios.FirstOrDefaultAsync(u => u.Id == clienteId);
			if (cliente is null)
				return ResultadoDto.Fallido("El cliente no existe");

			var ordenItems = dto.Items.Select(i =>
				new DetalleOrden
				{
					Sabor = i.Sabor,
					HeladoId = i.HeladoId,
					Nombre = i.Nombre,
					Precio = i.Precio,
					Cantidad = i.Cantidad,
					Agregados = i.Agegado,
					PrecioTotal = i.PrecioTotal
				});

			var orden = new Orden
			{
				ClienteId = clienteId,
				ClienteDireccion = cliente.Direccion,
				ClienteEmail = cliente.Email,
				ClienteNombre = cliente.Nombre,
				OrdenFecha = DateTime.Now,
				PrecioTotal = ordenItems.Sum(o => o .PrecioTotal),
				DetalleOrdenes = ordenItems.ToArray()
			};

			try
			{
				await _dataContext.Ordenes.AddAsync(orden);
				await _dataContext.SaveChangesAsync();
				return ResultadoDto.Exitoso();
			}
			catch (Exception ex)
			{
				return ResultadoDto.Fallido(ex.Message);
			}
		}

		// Obtener orden usuario.
		public async Task<OrdenDto[]> ObtenerOrdenUsuarioAsync(Guid usuarioId) =>
			await _dataContext.Ordenes
						.Where(o => o.ClienteId == usuarioId)
						.Select(o => new OrdenDto(o.Id, o.OrdenFecha, o.PrecioTotal, o.DetalleOrdenes.Count)).ToArrayAsync();

		// Obtener los itens de la orden del usuario.
		public async Task<OrdenItemDto[]> ObtenerItemsOrdenUsuarioAsync(long ordenId, Guid usuarioId) =>
			await _dataContext.DetalleOrdenes
					   .Where(oi => oi.OrdenId == ordenId && oi.Orden.ClienteId == usuarioId)
					   .Select(oi => new OrdenItemDto(oi.Id, oi.HeladoId, oi.Cantidad, oi.Precio, oi.Nombre, oi.Sabor, oi.Agregados)).ToArrayAsync();
	}
}
