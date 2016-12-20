using System;
using Gtk;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MonthContent : Gtk.VBox
	{
		private void OnViewBuilt(){
			this.RefreshView ();
		}
	}
}
