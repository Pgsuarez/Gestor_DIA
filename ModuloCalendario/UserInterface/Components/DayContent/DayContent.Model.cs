using System;
using Gtk;
using System.Collections.Generic;
using ModuloCalendario.DataClasses;
using ModuloCalendario.Services;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class DayContent : Gtk.VBox
	{

		DateTime currentDay;

		private void InitModel(){
			this.currentDay = DateTime.Now;
		}

		private void RefreshView(){
			this.ShowCurrentDay (
				this.currentDay.Day.ToString(),
				this.currentDay.Month.ToString (), 
				this.currentDay.Year.ToString ());
		}

		public void ChangeDay(DateTime month){
			this.currentDay = month;
			this.dayNotesComponent.ChangeMonth (month);
			this.dayExercisesComponent.ChangeMonth (month);
			this.RefreshView ();
		}

	}
}
