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
			this.dayContentComponent.ChangeDay (day);
			this.RefreshView ();
		}

		private void CreateNote(){
			this.ShowNoteForm ();
			this.ChangeDay (this.currentDay);
		}

		private void CreateMeasure()
		{
			this.ShowMeasureForm();
			this.ChangeDay(this.currentDay);
		}

		public void SetHasChanged(){
			this.ChangeDay (this.currentDay);
		}
	}
}
