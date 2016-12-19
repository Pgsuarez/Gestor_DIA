using System;
using Gtk;
using System.Collections.Generic;
using ModuloCalendario.DataClasses;
using ModuloCalendario.Services;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MonthExercises : Gtk.VBox
	{
		public DateTime CurrentMonth
		{
			get;
			private set;
		}

		private List<Exercise> exercises;

		private void InitModel(){
			this.CurrentMonth = DateTime.Now;

			this.exercises = new List<Exercise>();
			this.UpdateExercises ();
		}

		private void UpdateExercises()
		{
			var firstDayOfMonth = new DateTime(this.CurrentMonth.Year, this.CurrentMonth.Month, 1);
			var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

			this.exercises = ExercisesService.Instance.FindAllBetweenDates(firstDayOfMonth, lastDayOfMonth);
		}

		private int ExercisesCount {
			get {
				return this.exercises.Count;
			}
		}

		private void RefreshView(){
			int counter = 0;
			this.ClearExercises ();
			foreach (Exercise exercise in this.exercises)
			{
				this.ShowExercise(counter++, exercise.Distance, exercise.Minutes, exercise.Date.ToString());
			}
		}

		public void ChangeMonth(DateTime month)
		{
			this.CurrentMonth = month;
			this.UpdateExercises();
			this.RefreshView ();
		}


	}
}
