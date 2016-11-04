using System;
using Gtk;
using System.Collections.Generic;
using ModuloCalendario.DataClasses;
using ModuloCalendario.Services;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class DayNotes : Gtk.VBox
	{
		//State
		public DateTime Day
		{
			get;
			private set;
		}

		//Derived data
		private List<Note> notes;
		ListStore notesListStore;

		//Methods

		private void Fetch()
		{
			this.notes = NotesService.Instance.FindAllBetweenDates(this.Day, this.Day);

			int counter = 0;

			this.notesListStore.Clear();
			foreach (Note note in this.notes)
			{
				this.notesListStore.AppendValues(counter++, note.ExerciseId.ToString(), note.Text);
			}
		}

		public void ChangeDay(DateTime day)
		{
			Day = day;
			Fetch();
			RenderState();
		}
	}
}
