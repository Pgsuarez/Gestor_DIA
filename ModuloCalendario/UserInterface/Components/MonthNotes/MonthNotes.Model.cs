using System;
using Gtk;
using System.Collections.Generic;
using ModuloCalendario.DataClasses;
using ModuloCalendario.Services;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MonthNotes : Gtk.VBox
	{
		public DateTime CurrentMonth
		{
			get;
			private set;
		}

		private List<Note> notes;

		private void InitModel(){
			this.CurrentMonth = DateTime.Now;

			this.notes = new List<Note>();
			this.UpdateNotes ();
		}

		private void UpdateNotes()
		{
			var firstDayOfMonth = new DateTime(this.CurrentMonth.Year, this.CurrentMonth.Month, 1);
			var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

			this.notes = NotesService.Instance.FindAllBetweenDates(firstDayOfMonth, lastDayOfMonth);
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
				this.ShowNote(counter++, note.Title, note.Body, note.Date.ToString());
			}
		}

		public void ChangeMonth(DateTime month)
		{
			this.CurrentMonth = month;
			this.UpdateNotes();
			this.RefreshView ();
		}


	}
}
