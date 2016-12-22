using System;
using Gtk;
using System.Collections.Generic;
using ModuloCalendario.Services;
using ProyectoDIA.Core;
using ModuloEjercicio.App;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MonthNotes : Gtk.VBox
	{
		public DateTime CurrentMonth
		{
			get;
			private set;
		}

		private List<Nota> notes;

		private void InitModel(){
			this.CurrentMonth = DateTime.Now;

            this.notes = NotesService.Instance.FindAll();
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
			foreach (Nota note in this.notes)
			{
                this.ShowNote(counter++, note.Titulo, note.Cuerpo, note.Fecha.ToString());
			}
		}

		public void ChangeMonth(DateTime month)
		{
			this.CurrentMonth = month;
			this.UpdateNotes();
			this.RefreshView ();
		}


		//Copy after this
		public void Remove(int index){
			Services.NotesService.Instance.Remove (this.notes [index]);
			MainWindow.Instance.SetHasChanged ();
		}

		public void Edit(int index){
            Nota nota = this.notes [index];
			new NoteFormDialog (nota.Id, MainWindow.Instance, DialogFlags.DestroyWithParent);
			MainWindow.Instance.SetHasChanged ();
		}


	}
}
