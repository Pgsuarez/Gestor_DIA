using System;
using Gtk;
namespace ModuloCalendario.UserInterface.Components
{
	public partial class NoteFormDialog : Gtk.Dialog
	{
		public NoteFormDialog(Gtk.Window parent, Gtk.DialogFlags flags) :
			base("Create note", parent, flags)
		{
			this.BuildDialog ();
		}

		public NoteFormDialog(int noteId, Gtk.Window parent, Gtk.DialogFlags flags) :
		base("Edit note", parent, flags)
		{
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


			var lbl1 = new Label("Titulo");
			var lbl2 = new Label("Cuerpo");
			var lbl3 = new Label("Fecha");


			/*if (edit)
			{
				TreeIter iter;
				TreeModel model;

				if (treeView.Selection.GetSelected(out model, out iter))
				{
					var selectedEx_Titulo = (int)model.GetValue(iter, (int)Column.Titulo);
					var selectedEx_Cuerpo = (int)model.GetValue(iter, (int)Column.Cuerpo);
					var selectedEx_Fecha = (int)model.GetValue(iter, (int)Column.Cuerpo);

					selectedEx_Id = (int)store.GetValue(iter, (int)Column.Id);

					tituloEntry = new Entry(selectedEx_Titulo.ToString());
					cuerpoEntry = new Entry(selectedEx_Cuerpo.ToString());
					fechaEntry = new Entry(selectedEx_Fecha.ToString());

				}
			}
			else
			{*/
				Entry tituloEntry = new Entry();
				Entry cuerpoEntry = new Entry();
				Entry fechaEntry = new Entry();

			//}

			row1.Add(lbl1);
			row1.Add(tituloEntry);

			row2.Add(lbl2);
			row2.Add(cuerpoEntry);

			row3.Add(lbl3);
			row3.Add(fechaEntry);

			/*if (edit)
			{
				this.AddButton(Stock.Save, ResponseType.Apply);
			}
			else
			{*/
				this.AddButton(Stock.Save, ResponseType.Accept);
			//}
			this.AddButton(Stock.Cancel, ResponseType.Cancel);
			this.Response += this.OnDialogResponse;

			this.VBox.ShowAll();
			this.Run();
			this.Destroy();

			this.OnViewBuilt ();
		}
	}
}

