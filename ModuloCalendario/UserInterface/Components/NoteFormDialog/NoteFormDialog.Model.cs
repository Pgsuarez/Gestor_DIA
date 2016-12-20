using System;
using ModuloCalendario.Services;
using ProyectoDIA.Core;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class NoteFormDialog : Gtk.Dialog
	{
		Int32 noteId = -1;
		Nota note;

		private void RefreshView(){
            this.ShowTitle (this.note.Titulo);
            this.ShowBody (this.note.Cuerpo);
            this.ShowDate (this.note.Fecha);
		}

		private void SaveNote(string title, string body, DateTime date){
			
            this.note.Titulo = title;
            this.note.Cuerpo = body;
            this.note.Fecha = date;

			if (this.noteId == -1) {
				NotesService.Instance.Save (this.note);
			} else {
				NotesService.Instance.Update (this.note);
			}

			this.Destroy ();
		}

		private void FetchNote(){
			if (this.noteId != -1) {
				this.note = NotesService.Instance.FindById (this.noteId);
			}
		}
	}
}

	