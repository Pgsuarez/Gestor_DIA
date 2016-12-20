using System;
using Gtk;
using System.Collections.Generic;
using ModuloCalendario.Services;
using ProyectoDIA.Core;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class DayNotes : Gtk.VBox
	{
		public DateTime CurrentDay
		{
			get;
			private set;
		}

		private List<Nota> notes;

		private void InitModel(){
			this.CurrentDay = DateTime.Now;

            this.notes = NotesService.Instance.FindAll();
			this.UpdateNotes ();
		}

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
			foreach (Nota note in this.notes)
			{
                this.ShowNote(counter++, note.Titulo, note.Cuerpo, note.Fecha.ToString());
			}
		}

		public void ChangeMonth(DateTime month)
		{
			this.CurrentDay = month;
			this.UpdateNotes();
			this.RefreshView ();
		}


	}
}
