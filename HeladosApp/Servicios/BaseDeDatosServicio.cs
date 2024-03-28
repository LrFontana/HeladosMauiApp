using HeladosApp.Data;
using HeladosApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeladosApp.Servicios
{
	public class BaseDeDatosServicio : IAsyncDisposable
	{
		// Propiedades.
		private const string NombreBaseDeDatos = "HeladosAppDb.db3";
			
		private static readonly string _baseDeDatosPath = Path.Combine(FileSystem.AppDataDirectory, NombreBaseDeDatos);

		private SQLiteAsyncConnection? _coneccion;
		private SQLiteAsyncConnection BaseDeDatos => 
			_coneccion ??= new SQLiteAsyncConnection(_baseDeDatosPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);


		// Metodos.

		// Agregar item al carrito.
		public async Task<int> AgregarItemCarrito(EntidadItemCarrito entidad) =>
			await EjecutarAsync(async () => await BaseDeDatos.InsertAsync(entidad));

		// Actualizar item del carrito.
		public async Task ActualizarItemCarrito(EntidadItemCarrito entidad) => 
			await EjecutarAsync(async () => await BaseDeDatos.UpdateAsync(entidad));

		// Eliminar item del carrito.
		public async Task EliminarItemCarrito(EntidadItemCarrito entidad) => 
			await EjecutarAsync(async () => await BaseDeDatos.DeleteAsync(entidad));

		// Obtener item del carrito.
		public async Task<EntidadItemCarrito> ObtenerItemCarritoAsync(int id) => 
			await EjecutarAsync(async () => await BaseDeDatos.GetAsync<EntidadItemCarrito>(id));

		// Obtener todos los items del carrito.
		public async Task<List<EntidadItemCarrito>> ObtenerTodosLosItemCarritoAsync() =>
			await EjecutarAsync(async () => await BaseDeDatos.Table<EntidadItemCarrito>().ToListAsync());

		// Dispose.
		public async ValueTask DisposeAsync() => await _coneccion?.CloseAsync();

		// Ejecutar async.
		private async Task<TResult> EjecutarAsync <TResult>(Func<Task<TResult>> accionDb)
		{
			await BaseDeDatos.CreateTableAsync<EntidadItemCarrito>();
			return await accionDb.Invoke();
		}

		// Limpiar carrito.
		public async Task<int> LimpiarCarritoAsync() =>
			await EjecutarAsync(async () => await BaseDeDatos.DeleteAllAsync<EntidadItemCarrito>());
	}
}
