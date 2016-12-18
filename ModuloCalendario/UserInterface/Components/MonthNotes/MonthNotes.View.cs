using System;
using System.Collections.Generic;
using Gtk;
using ModuloCalendario.DataClasses;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MonthNotes : Gtk.VBox
	{
		enum Columns { Index, Title, Body, Date };


		//components and widgets
		private Label monthLabel;
		private Label notesCounterLabel;
		TreeView notesTreeView;
		ListStore notesListStore;
	


		public MonthNotes() : base()
		{
			//Init view's state
			this.InitModel();

			this.notesListStore = new Gtk.ListStore(typeof(int), typeof(string), typeof(string), typeof(string));

			//Build
			Build();
		}

		private void Build()
		{
			var mainVox = new Gtk.VBox();

			//Header
			var headerHBox = new Gtk.HBox();

			var headerFirstLabel = new Gtk.Label("Notes of month ");
			headerHBox.PackStart(headerFirstLabel, false, false, 0);

			this.monthLabel = new Gtk.Label();
			headerHBox.PackStart(this.monthLabel, false, true, 0);

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
			column.SortColumnId = (int)Columns.Index;
			this.notesTreeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Title", rendererText, "text", Columns.Title);
			column.SortColumnId = (int)Columns.Title;
			this.notesTreeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Body", rendererText, "text", Columns.Body);
			column.SortColumnId = (int)Columns.Body;
			this.notesTreeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Date", rendererText, "text", Columns.Date);
			column.SortColumnId = (int)Columns.Date;
			this.notesTreeView.AppendColumn(column);

		}

		private void ClearNotes(){
			this.notesListStore.Clear ();
		}

		private void ShowNote(int index, string title, string body, string date){
			this.notesListStore.AppendValues (index, title, body, date);
		}
			
		private void ShowMonthLabel(string month, string year){
			this.monthLabel.Text = month + "/" + year;
		}

		private void ShowNotesCounterLabel(int count){
			this.notesCounterLabel.Text = "  (" + count + ")";
		}
	}
}
