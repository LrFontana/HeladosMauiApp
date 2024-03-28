using HeladosApp.ViewModels;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using UIKit;

namespace HeladosApp;

public class TabbarRenderer : ShellRenderer
{
	// Metodos.
	protected override IShellTabBarAppearanceTracker CreateTabBarAppearanceTracker()
	{
		return new BadgeShellTabbarAppearanceTracker();
	}

	// Clase.
	class BadgeShellTabbarAppearanceTracker : ShellTabBarAppearanceTracker
	{
		// Propiedad.
		private UITabBarItem? _tabbarItemCarrito;
		// Metodo.
		public override void UpdateLayout(UITabBarController controller)
		{
			base.UpdateLayout(controller);

			if(_tabbarItemCarrito is null)
			{
				const int IndexItemCarritoTabbar = 1;

				_tabbarItemCarrito = controller.TabBar.Items?[IndexItemCarritoTabbar];
				ActualizarBadge(CarritoViewModel.ContadorTotalCarrito);
				CarritoViewModel.ContadorTotalCarritoCambio += CarritoViewModel_ContadorTotalCarritoCambio;
			}			
		}

		private void CarritoViewModel_ContadorTotalCarritoCambio(object? sender, int nuevoContador) => ActualizarBadge(nuevoContador);

		private void ActualizarBadge(int contador)
		{
			if (_tabbarItemCarrito is not null)
			{
				if (contador <= 0)
				{
					_tabbarItemCarrito.BadgeValue = null;
				}
				else 
				{ 
					_tabbarItemCarrito.BadgeValue = contador.ToString();
					_tabbarItemCarrito.BadgeColor = Colors.DeepPink.ToPlatform();
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			CarritoViewModel.ContadorTotalCarritoCambio -= CarritoViewModel_ContadorTotalCarritoCambio;
			base.Dispose(disposing);			
		}
	}
}
