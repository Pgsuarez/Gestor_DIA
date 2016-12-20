using System;
using Gtk;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MonthExercises : Gtk.VBox
	{
		private void OnViewBuilt(){
			this.RefreshView ();
		}
	}
}
