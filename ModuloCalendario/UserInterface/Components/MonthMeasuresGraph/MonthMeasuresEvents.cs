using System;
using Gtk;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MonthMeasuresGraph : Gtk.VBox
	{
		Boolean weightGraph;

		private void OnViewBuilt(){
			ShowWeightGraphic();
			this.RefreshView ();
		}

		private void ShowACGraphic()
		{
			weightGraph = false;
			ChangeToAC();
			this.RefreshView();
		}

		private void ShowWeightGraphic()
		{
			weightGraph = true;
			ChangeToWeight();
			this.RefreshView();
		}
	}
}
