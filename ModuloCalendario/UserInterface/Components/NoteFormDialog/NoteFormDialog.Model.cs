using System;
using ModuloCalendario.DataClasses;
using ModuloCalendario.Services;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class NoteFormDialog : Gtk.Dialog
	{
		Int32 noteId = -1;
		Note note;

		private void RefreshView(){
			this.ShowTitle (this.note.Title);
			this.ShowBody (this.note.Body);
			this.ShowDate (this.note.Date);
		}

		private void SaveNote(string title, string body, DateTime date){
			
			this.note.Title = title;
			this.note.Body = body;
			this.note.Date = date;

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

	