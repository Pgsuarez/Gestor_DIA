using System;
using Gtk;
using ModuloEjercicio.API;
using ModuloEjercicio.App;

namespace ModuloCalendario.UserInterface
{
	public partial class MainWindow : Gtk.Window
	{
		private void OnDelete(object o, Gtk.DeleteEventArgs args)
		{
			Gtk.Application.Quit();
		}

		void OnDaySelected(object sender, EventArgs e)
		{
			var calendar = (Calendar)sender;
			this.ChangeDay (calendar.Date);
		}

		private void OnViewBuilt(){
			this.ChangeDay (this.calendarComponent.Day);
		}

		private void OnNewNoteClicked(object o, EventArgs e){
			this.CreateNote ();
		}

		private void OnNewExerciseClicked(object o, EventArgs e){
            var dialog = new ExerciseDialog("Add exercise", this);
            Exercise ex = dialog.getResult();
            if (ex != null)
            {
                Services.ExercisesService.Instance.Add(ex);
                ChangeDay(currentDay);
            }
		}

		private void OnNewMeasureClicked(object o, EventArgs e)
		{
			this.CreateMeasure();
		}
	}
}
