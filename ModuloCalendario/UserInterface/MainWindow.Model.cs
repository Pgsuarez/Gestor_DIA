using System;
using Gtk;

namespace ModuloCalendario.UserInterface
{
	public partial class MainWindow : Gtk.Window
	{
		private DateTime currentDay;

		public void RefreshView(){

		}

		public void ChangeDay(DateTime day){
			this.currentDay = day;
			this.monthContentComponent.ChangeMonth (day);
			this.dayNotesComponent.ChangeDay (day);
			this.RefreshView ();
		}
	}
}
