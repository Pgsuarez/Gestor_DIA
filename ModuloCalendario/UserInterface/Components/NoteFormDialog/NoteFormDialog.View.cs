using System;
using Gtk;
namespace ModuloCalendario.UserInterface.Components
{
	public partial class NoteFormDialog : Gtk.Dialog
	{
		private Entry titleEntry;
		private Entry bodyEntry;
		private Gtk.Calendar dateEntry;

		public NoteFormDialog(Gtk.Window parent, Gtk.DialogFlags flags) :
			base("Create note", parent, flags)
		{
			this.OnCreate ();
			this.BuildDialog ();
		}

		public NoteFormDialog(int noteId, Gtk.Window parent, Gtk.DialogFlags flags) :
		base("Edit note", parent, flags)
		{
			this.OnCreate ();
			this.OnReceivedNoteId (noteId);
			this.BuildDialog ();
		}

		private void BuildDialog(){

			this.Modal = true;

			var row1 = new HBox(true, 5);
			var row2 = new HBox(true, 5);
			var row3 = new HBox(true, 5);

			this.VBox.Add(row1);
			this.VBox.Add(row2);
			this.VBox.Add(row3);

			var lbl1 = new Label("Title");
			var lbl2 = new Label("Body");
			var lbl3 = new Label("Date");

			this.titleEntry = new Entry();
			this.bodyEntry = new Entry();
			this.dateEntry = new Gtk.Calendar();

			row1.Add(lbl1);
			row1.Add(this.titleEntry);

			row2.Add(lbl2);
			row2.Add(this.bodyEntry);

			row3.Add(lbl3);
			row3.Add(this.dateEntry);

			this.AddButton(Stock.Save, ResponseType.Accept);
			this.AddButton(Stock.Cancel, ResponseType.Cancel);

			this.Response += this.OnDialogResponse;

			this.VBox.ShowAll();
			this.Run();
			this.Destroy();

			this.OnViewBuilt ();
		}

		private void ShowTitle(string title){
			this.titleEntry.Text = title;
		}

		private void ShowBody(string body){
			this.bodyEntry.Text = body;
		}

		private void ShowDate(DateTime date){
			this.dateEntry.Date = date;
		}
	}
}

