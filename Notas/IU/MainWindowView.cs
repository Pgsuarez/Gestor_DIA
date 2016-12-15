using System;
using Gtk;
using ProyectoDIA.Core;


namespace ProyectoDIA.IU
{
	public partial class MainWindow: Gtk.Window
	{
		public MainWindow ()
			:base( Gtk.WindowType.Toplevel )
		{
			this.Build();
		}

		private void Build()
		{
			this.Title = "Manage Notas";
			SetPosition(WindowPosition.Center);
			SetDefaultSize(350, 250);

			DeleteEvent += delegate { Application.Quit(); };

			var container = new HBox(false, 0);
			Add(container);

			var exList = new VBox(true, 5);
			var butList = new VBox(true, 5);
			var vAlign = new Alignment(1, 0, 0, 0);

			vAlign.Add(butList);

			container.PackStart(exList, true, true, 5);
			container.PackEnd(vAlign, false, false, 5);

			addButton = new Button(Stock.Add);
			editButton = new Button(Stock.Edit);
			deleteButton = new Button(Stock.Delete);

			addButton.Clicked += delegate { ShowDialog(false); };
			editButton.Clicked += delegate { ShowDialog(true); };
			deleteButton.Clicked += delegate { ShowDeleteDialog(); };

			butList.PackStart(addButton, false, false, 0);
			butList.PackStart(editButton, false, false, 0);
			butList.PackStart(deleteButton, false, false, 0);


			ScrolledWindow sw = new ScrolledWindow();
			sw.ShadowType = ShadowType.EtchedIn;
			sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
			exList.PackStart(sw, true, true, 0);

			LoadNotas();

			treeView = new TreeView(store);
			treeView.RulesHint = true;
			sw.Add(treeView);

			AddColumns();
		}

		void AddColumns()
		{
			CellRendererText rendererText = new CellRendererText();
			TreeViewColumn column = new TreeViewColumn("Titulo", rendererText,
				"text", Column.Titulo);
			column.SortColumnId = (int)Column.Titulo;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Cuerpo", rendererText,
				"text", Column.Cuerpo);
			column.SortColumnId = (int)Column.Cuerpo;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Fecha", rendererText,
				"text", Column.Fecha);
			column.SortColumnId = (int)Column.Fecha;
			treeView.AppendColumn(column);
		}

		void ShowDialog(bool edit)
		{
			string title;
			if (edit)
			{
				title = "Edit note";
			}
			else
			{
				title = "Add note";
			}
			var dialog = new Dialog
				(title, this, Gtk.DialogFlags.DestroyWithParent);
			dialog.Modal = true;

			var row1 = new HBox(true, 5);
			var row2 = new HBox(true, 5);
			var row3 = new HBox(true, 5);

			dialog.VBox.Add(row1);
			dialog.VBox.Add(row2);
			dialog.VBox.Add(row3);


			var lbl1 = new Label("Titulo");
			var lbl2 = new Label("Cuerpo");
			var lbl3 = new Label("Fecha");


			if (edit)
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
			{
				tituloEntry = new Entry();
				cuerpoEntry = new Entry();
				fechaEntry = new Entry();

			}

			row1.Add(lbl1);
			row1.Add(tituloEntry);

			row2.Add(lbl2);
			row2.Add(cuerpoEntry);

			row3.Add(lbl3);
			row3.Add(fechaEntry);

			if (edit)
			{
				dialog.AddButton(Stock.Save, ResponseType.Apply);
			}
			else
			{
				dialog.AddButton(Stock.Save, ResponseType.Accept);
			}
			dialog.AddButton(Stock.Cancel, ResponseType.Cancel);
			dialog.Response += OnDialogResponse;
			dialog.VBox.ShowAll();
			dialog.Run();
			dialog.Destroy();
		}

		void ShowDeleteDialog()
		{
			string title = "Delete note";
			var dialog = new Dialog
				(title, this, Gtk.DialogFlags.DestroyWithParent);
			dialog.Modal = true;

			var lbl = new Label("Are you sure?");
			dialog.VBox.Add(lbl);

			dialog.AddButton(Stock.Yes, ResponseType.Reject);
			dialog.AddButton(Stock.No, ResponseType.Cancel);

			dialog.Response += OnDialogResponse;
			dialog.VBox.ShowAll();
			dialog.Run();
			dialog.Destroy();
		}

		TreeView treeView;
		private ListStore store;

		private enum Column
		{
			Titulo,
			Cuerpo,
			Fecha,
			Id
		}

		private Entry tituloEntry;
		private Entry cuerpoEntry;
		private Entry fechaEntry;
		private int selectedEx_Id;

		private Button addButton;
		private Button editButton;
		private Button deleteButton;
	}
}

