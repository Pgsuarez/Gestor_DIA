using System;
using Gtk;
using System.Collections.Generic;
using ModuloCalendario.DataClasses;
using ModuloCalendario.Services;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MonthMeasuresGraph : Gtk.VBox
	{
		public DateTime CurrentMonth
		{
			get;
			private set;
		}

		private List<Measurements> measurements;

		private void InitModel(){
			this.CurrentMonth = DateTime.Now;

			this.measurements = new List<Measurements>();
			this.UpdateMeasures ();
		}

		private void UpdateMeasures()
		{
			var firstDayOfMonth = new DateTime(this.CurrentMonth.Year, this.CurrentMonth.Month, 1);
			var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

			this.measurements = MeasurementsService.Instance.FindAllBetweenDates(firstDayOfMonth, lastDayOfMonth);
		}

		private int MeasuresMonth {
			get {
				return this.measurements.Count;
			}
		}

		private void RefreshView(){
			int counter = 0;
			this.ClearMeasures ();
			foreach (Measurements mea in this.measurements)
			{
				this.ShowMeasure(counter++, mea.Weight, mea.AbdominalCircunference, mea.Date.Day);
			}
		}

		public void ChangeMonth(DateTime month)
		{
			this.CurrentMonth = month;
			this.UpdateMeasures();
			this.RefreshView ();
		}


	}
}
