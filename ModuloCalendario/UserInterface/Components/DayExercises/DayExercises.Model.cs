using System;
using Gtk;
using System.Collections.Generic;
using ModuloCalendario.DataClasses;
using ModuloCalendario.Services;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class DayExercises : Gtk.VBox
	{
		public DateTime CurrentDay
		{
			get;
			private set;
		}

		private List<Exercise> exercises;

		private void InitModel(){
			this.CurrentDay = DateTime.Now;

			this.exercises = new List<Exercise>();
			this.UpdateExercises ();
		}

		private void UpdateExercises()
		{
			this.exercises = ExercisesService.Instance.FindAllBetweenDates(this.CurrentDay, this.CurrentDay);
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
			this.CurrentDay = month;
			this.UpdateExercises();
			this.RefreshView ();
		}


	}
}
