using System;
using System.Collections.Generic;
using Gtk;
using ModuloCalendario.DataClasses;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class DayNotes : Gtk.VBox
	{
		enum Columns { Index, Title, Body, Date };


		//components and widgets
		TreeView notesTreeView;
		ListStore notesListStore;
	


		public DayNotes() : base()
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
	}
}
