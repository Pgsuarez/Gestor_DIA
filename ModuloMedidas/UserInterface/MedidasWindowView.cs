using System;
using Gtk;

namespace ModuloMedidas.UserInterface
{
	public partial class MedidasWindow: Gtk.Window
	{
		public MedidasWindow()
			:base( Gtk.WindowType.Toplevel )
		{
			this.Build ();
		}


		private void Build()
		{
			this.Title = "Manage Medidas";
			SetPosition(WindowPosition.Center);
			SetDefaultSize(500, 250);

			DeleteEvent += delegate { Application.Quit(); };

			var vbMain = new HBox(false, 0);
			Add(vbMain);

			var medList = new VBox(false, 5);
			var botones = new VBox(false, 5);
			var vAlign = new Alignment(1, 0, 0, 0);

			vAlign.Add(botones);

			vbMain.PackStart(medList, true, true, 5);
			vbMain.PackEnd(vAlign, false, false, 5);

			addButton = new Button(Stock.Add);
			editButton = new Button(Stock.Edit);
			deleteButton = new Button(Stock.Delete);

			addButton.Clicked += delegate { ShowDialog(false); };
			editButton.Clicked += delegate { ShowDialog(true); };
			deleteButton.Clicked += delegate { ShowDeleteDialog(); };

			botones.PackStart(addButton, false, false, 0);
			botones.PackStart(editButton, false, false, 0);
			botones.PackStart(deleteButton, false, false, 0);

			ScrolledWindow sw = new ScrolledWindow();
			sw.ShadowType = ShadowType.EtchedIn;
			sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
			medList.PackStart(sw, true, true, 0);

			RecuperarMedidas();
			//AppDomain.CurrentDomain.ProcessExit += new EventHandler (OnClose );

			treeView = new TreeView(store);
			treeView.RulesHint = true;
			sw.Add(treeView);

			AddColumns();

			this.DeleteEvent += OnClose;
		}

		void AddColumns()
		{
			CellRendererText rendererText = new CellRendererText();

			TreeViewColumn column = new TreeViewColumn("Fecha", rendererText,
				"text", Column.Fecha);
			column.SortColumnId = (int)Column.Fecha;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Peso", rendererText,
				"text", Column.Peso);
			column.SortColumnId = (int)Column.Peso;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Circunferencia Abdominal", rendererText,
				"text", Column.CircunferenciaAbdominal);
			column.SortColumnId = (int)Column.CircunferenciaAbdominal;
			treeView.AppendColumn(column);
		}

		void ShowDialog(bool edit)
		{
			string title;
			if (edit)
			{
				title = "Modificar Medidas";
			}
			else
			{
				title = "Añadir Medidas";
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


			var lbl1 = new Label("Fecha");
			var lbl2 = new Label("Peso");
			var lbl3 = new Label("Circunferencia Abdominal");

			fechaEntry = new Calendar();
			if (edit)
			{
				TreeIter iter;
				TreeModel model;

				if (treeView.Selection.GetSelected(out model, out iter))
				{
					var selectedMed_Fecha = (string)model.GetValue(iter, (int)Column.Fecha);
					var selectedMed_Peso = (float)model.GetValue(iter, (int)Column.Peso);
					var selectedMed_CircunferenciaAbdominal = (float)model.GetValue(iter, (int)Column.CircunferenciaAbdominal);


					fechaEntry.Date = Convert.ToDateTime(selectedMed_Fecha);
					pesoEntry = new Entry(selectedMed_Peso.ToString());
					circunferenciaAbdominalEntry = new Entry(selectedMed_CircunferenciaAbdominal.ToString());

				}
			}
			else
			{
				pesoEntry = new Entry();
				circunferenciaAbdominalEntry = new Entry();

			}

			row1.Add(lbl1);
			row1.Add(fechaEntry);

			row2.Add(lbl2);
			row2.Add(pesoEntry);

			row3.Add(lbl3);
			row3.Add(circunferenciaAbdominalEntry);

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
			string title = "Borrar Medidas";
			var dialog = new Dialog
				(title, this, Gtk.DialogFlags.DestroyWithParent);
			dialog.Modal = true;

			var lbl = new Label("¿Esta seguro de que desea eliminar las medidas seleccionadas?");
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
			Fecha,
			Peso,
			CircunferenciaAbdominal
		}

		private Entry pesoEntry;
		private Entry circunferenciaAbdominalEntry;
		private Calendar fechaEntry;

		private Button addButton;
		private Button editButton;
		private Button deleteButton;
	}
}

