using System;
using Gtk;
using System.Collections.Generic;
using ModuloCalendario.DataClasses;
using ModuloCalendario.Services;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MonthContent : Gtk.VBox
	{

		DateTime currentMonth;

		private void InitModel(){
			this.currentMonth = DateTime.Now;
		}

		private void RefreshView(){
			this.ShowCurrentMonth (this.currentMonth.Month.ToString (), 
				this.currentMonth.Year.ToString ());
		}

		public void ChangeMonth(DateTime month){
			this.currentMonth = month;
			this.monthNotesComponent.ChangeMonth (month);

			this.monthExercisesComponent.ChangeMonth (month);
			this.monthExercisesComponentG.ChangeMonth(month);


			this.monthMeasuresComponent.ChangeMonth(month);
			this.monthMeasuresComponentG.ChangeMonth(month);

			this.RefreshView ();
		}

	}
}
