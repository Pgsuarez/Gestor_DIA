using System;
using ModuloCalendario.DataClasses;
namespace ModuloCalendario.UserInterface.Components
{
	public partial class NoteFormDialog : Gtk.Dialog
	{
		private void OnViewBuilt(){
			this.RefreshView ();
		}

		private void OnCreate(){
			this.note = new Note (-1, "", "", DateTime.Now);
		}

		private void OnReceivedNoteId(int noteId){
			this.noteId = noteId;
			this.FetchNote ();
		}

		private void OnDialogResponse(object o, Gtk.ResponseArgs args){
			if (args.ResponseId.Equals (Gtk.ResponseType.Accept) || args.ResponseId.Equals (Gtk.ResponseType.Apply)) {
				this.SaveNote (this.titleEntry.Text,
					this.bodyEntry.Text,
					this.dateEntry.Date);
			} else if (args.ResponseId.Equals(Gtk.ResponseType.Reject)) {
				this.Destroy ();
			}
		}
	}
}

