using System;
using System.Collections.Generic;
using Gtk;
using ModuloCalendario.DataClasses;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MonthNotes : Gtk.VBox
	{
		enum Columns { Index, Exercise, Note, Date };

		//state

		//components and widgets
		private Label monthLabel;
		private Label notesCounterLabel;
		TreeView notesTreeView;


		public MonthNotes() : base()
		{
			//Init view's state
			this.Month = DateTime.Now;

			this.notes = new List<Note>();
			this.notesListStore = new Gtk.ListStore(typeof(int), typeof(string), typeof(string));

			//Render
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
			Fetch();
			RenderState();
		}

		void AddListColumns()
		{
			CellRendererText rendererText;
			TreeViewColumn column;

			rendererText = new CellRendererText();
			column = new TreeViewColumn("#", rendererText, "text", Columns.Index);
			column.SortColumnId = (int)Columns.Index;
			this.notesTreeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Exercise", rendererText, "text", Columns.Exercise);
			column.SortColumnId = (int)Columns.Exercise;
			this.notesTreeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Note", rendererText, "text", Columns.Note);
			column.SortColumnId = (int)Columns.Note;
			this.notesTreeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Date", rendererText, "text", Columns.Date);
			column.SortColumnId = (int)Columns.Date;
			this.notesTreeView.AppendColumn(column);

		}

		void RenderState()
		{
			this.notesCounterLabel.Text = "  (" + this.notes.Count + ")";
			this.monthLabel.Text = this.Month.Month.ToString() + "/" + this.Month.Year.ToString();
		}
	}
}
