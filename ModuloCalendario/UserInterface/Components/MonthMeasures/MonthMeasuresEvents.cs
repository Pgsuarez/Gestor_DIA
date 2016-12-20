using System;
using Gtk;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MonthMeasures : Gtk.VBox
	{
		private void OnViewBuilt(){
			this.RefreshView ();
		}
	}
}
