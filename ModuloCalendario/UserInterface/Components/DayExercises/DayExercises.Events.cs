using System;
using Gtk;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class DayExercises : Gtk.VBox
	{
		private void OnViewBuilt(){
			this.RefreshView ();
		}
	}
}
