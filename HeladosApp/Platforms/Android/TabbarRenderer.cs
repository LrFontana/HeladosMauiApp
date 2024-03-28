using Google.Android.Material.Badge;
using Google.Android.Material.BottomNavigation;
using HeladosApp.ViewModels;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;

namespace HeladosApp
{
	public class TabbarRenderer : ShellRenderer
	{
		// Propiedades.

		// Metodos.
		protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
		{
			return new BagShellBottomNavViewAppearanceTracker(this, shellItem);
		}


		// Clase.
		class BagShellBottomNavViewAppearanceTracker : ShellBottomNavViewAppearanceTracker
		{
			// Propiedad.
			private BadgeDrawable _badgeDrawable;

			// Constructor.
			public BagShellBottomNavViewAppearanceTracker(IShellContext shellContext, ShellItem shellItem) : base(shellContext, shellItem)
			{

			}

			// Metodos.
			public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
			{
				base.SetAppearance(bottomView, appearance);

				if (_badgeDrawable is null)
				{
					const int IndexItemCarritoTabbar = 1;

					_badgeDrawable = bottomView.GetOrCreateBadge(IndexItemCarritoTabbar);
					ActualizarBadge(CarritoViewModel.ContadorTotalCarrito);
					CarritoViewModel.ContadorTotalCarritoCambio += CarritoViewModel_ContadorTotalCarritoCambio;
				}
			}

			private void CarritoViewModel_ContadorTotalCarritoCambio(object? sender, int nuevoContador) => ActualizarBadge(nuevoContador);

			private void ActualizarBadge(int contador)
			{
				if (contador <= 0)
				{
					_badgeDrawable.SetVisible(false);
				}
				else
				{
					_badgeDrawable.Number = contador;
					_badgeDrawable.BackgroundColor = Colors.DeepPink.ToPlatform();
					_badgeDrawable.BadgeTextColor = Colors.White.ToPlatform();
					_badgeDrawable.SetVisible(true);
				}
			}
			protected override void Dispose(bool disposing)
			{
				CarritoViewModel.ContadorTotalCarritoCambio -= CarritoViewModel_ContadorTotalCarritoCambio;
				base.Dispose(disposing);
			}
		}
	}
}
