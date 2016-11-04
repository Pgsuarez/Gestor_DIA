using System;
using Gtk;
using System.Collections.Generic;
using ModuloCalendario.DataClasses;
using ModuloCalendario.Services;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MonthNotes : Gtk.VBox
	{
		//State
		public DateTime Month
		{
			get;
			private set;
		}

		//Derived data
		private List<Note> notes;
		ListStore notesListStore;

		private void Fetch()
		{
			var firstDayOfMonth = new DateTime(this.Month.Year, this.Month.Month, 1);
			var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

			this.notes = NotesService.Instance.FindAllBetweenDates(firstDayOfMonth, lastDayOfMonth);

			int counter = 0;

			this.notesListStore.Clear();
			foreach (Note note in this.notes)
			{
				this.notesListStore.AppendValues(counter++, note.ExerciseId.ToString(), note.Text);
			}
		}

		public void ChangeMonth(DateTime month)
		{
			Month = month;
			Fetch();
			RenderState();
		}
	}
}
