using System;
using Gtk;
using System.Collections.Generic;
using ModuloCalendario.DataClasses;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class DayNotes : Gtk.VBox
	{

		enum Columns { Index, Title, Body };

		//state

		//components and widgets
		private Label dayLabel;
		private Label notesCounterLabel;
		TreeView notesTreeView;
		ListStore notesListStore;



		public DayNotes() : base()
		{
			
			//Init view's state
			this.InitModel();
			this.notesListStore = new Gtk.ListStore(typeof(int), typeof(string), typeof(string));

			//Render
			Build();
		}

		private void Build()
		{
			var mainVox = new Gtk.VBox();

			//Header
			var headerHBox = new Gtk.HBox();

			var headerFirstLabel = new Gtk.Label("Notes of day ");
			headerHBox.PackStart(headerFirstLabel, false, false, 0);

			this.dayLabel = new Gtk.Label();
			headerHBox.PackStart(this.dayLabel, false, true, 0);

			this.notesCounterLabel = new Gtk.Label();
			headerHBox.PackStart(this.notesCounterLabel, false, false, 0);

			mainVox.PackStart(headerHBox, false, false, 5);


			//List
			this.notesTreeView = new TreeView(this.notesListStore);

			ScrolledWindow sw = new ScrolledWindow();
			sw.ShadowType = ShadowType.EtchedIn;
			sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
			sw.Add(notesTreeView);

			this.AddListColumns();

			mainVox.PackStart(sw, true, true, 0);


			//Wrap
			PackStart(mainVox, true, true, 0);

			//Update state and render
			this.OnViewBuilt();
		}

		private void AddListColumns()
		{
			CellRendererText rendererText;
			TreeViewColumn column;

			rendererText = new CellRendererText();
			column = new TreeViewColumn("#", rendererText, "text", Columns.Index);
			column.SortColumnId = (int) Columns.Index;
			this.notesTreeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Title", rendererText, "text", Columns.Title);
			column.SortColumnId = (int) Columns.Title;
			this.notesTreeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Body", rendererText, "text", Columns.Body);
			column.SortColumnId = (int) Columns.Body;
			this.notesTreeView.AppendColumn(column);
		}

		private void ShowNotesCounter(int count)
		{
			this.notesCounterLabel.Text = "  (" + count + ")";
		}

		private void ShowDayLabel(String day, String month, String year){
			this.dayLabel.Text = day + "/" + month + "/" + year;
		}
			

		public void ClearNotes(){
			this.notesListStore.Clear ();
		}

		public void ShowNote(int index, string title, string body) {
			this.notesListStore.AppendValues (index, title, body);
		}
	}
}
