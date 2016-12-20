using System;
using Gtk;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class DayContent : Gtk.VBox
	{
		private void OnViewBuilt(){
			this.RefreshView ();
		}
	}
}
