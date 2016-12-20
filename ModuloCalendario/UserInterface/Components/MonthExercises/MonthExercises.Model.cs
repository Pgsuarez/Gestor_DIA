using System;
using Gtk;
using System.Collections.Generic;
using ModuloCalendario.Services;
using ModuloEjercicio.API;
using ModuloEjercicio.App;

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

		//Copy after this
		public void Remove(int index){
			Services.ExercisesService.Instance.Delete (this.exercises [index].Id);
			MainWindow.Instance.SetHasChanged ();

		}

		public void Edit(int index){
			Exercise ex = this.exercises [index];
			var dialog = new ExerciseDialog("Edit exercise", MainWindow.Instance, ex);
			ex = dialog.getResult ();
			if (ex != null)
			{
				Services.ExercisesService.Instance.Update(ex);
				MainWindow.Instance.SetHasChanged ();
			}
		}


	}
}
