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
		public DateTime CurrentDay
		{
			get;
			private set;
		}

		//Derived data
		private List<Note> notes;

		private void InitModel(){
			this.CurrentDay = DateTime.Now;
			this.notes = new List<Note>();
			this.UpdateNotes ();
		}

		//Methods
		private void UpdateNotes()
		{
			this.notes = NotesService.Instance.FindAllBetweenDates(this.CurrentDay, this.CurrentDay);
		}

		private int NotesCount {
			get {
				return this.notes.Count;
			}
		}

		private void RefreshView(){
			int counter = 0;

			this.ClearNotes ();
			foreach (Note note in this.notes)
			{
				this.ShowNote (counter++, note.Title, note.Body);
			}


			ShowNotesCounter(this.NotesCount);
			ShowDayLabel (this.CurrentDay.Day.ToString (),
				this.CurrentDay.Month.ToString (),
				this.CurrentDay.Year.ToString ()
			);
		}

		public void ChangeDay(DateTime day)
		{
			CurrentDay = day;
			UpdateNotes();
			RefreshView ();
		}
	}
}
