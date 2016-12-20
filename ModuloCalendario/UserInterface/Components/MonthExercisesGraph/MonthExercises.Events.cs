using System;
using Gtk;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MonthExercisesGraph : Gtk.VBox
	{
		private void OnViewBuilt(){
			this.RefreshView ();
		}
	}
}
