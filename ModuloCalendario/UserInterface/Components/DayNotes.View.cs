using System;
using Gtk;
using System.Collections.Generic;
using ModuloCalendario.DataClasses;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class DayNotes : Gtk.VBox
	{

		enum Columns { Index, Exercise, Note };

		//state

		//components and widgets
		private Label dayLabel;
		private Label notesCounterLabel;
		TreeView notesTreeView;


		public DayNotes() : base()
		{
			//Init view's state
			this.Day = DateTime.Now;

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
			Fetch();
			RenderState();
		}

		void AddListColumns()
		{
			CellRendererText rendererText;
			TreeViewColumn column;

			rendererText = new CellRendererText();
			column = new TreeViewColumn("#", rendererText, "text", Columns.Index);
			column.SortColumnId = (int) Columns.Index;
			this.notesTreeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Exercise", rendererText, "text", Columns.Exercise);
			column.SortColumnId = (int) Columns.Exercise;
			this.notesTreeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Note", rendererText, "text", Columns.Note);
			column.SortColumnId = (int) Columns.Note;
			this.notesTreeView.AppendColumn(column);
		}

		void RenderState()
		{
			this.notesCounterLabel.Text = "  (" + this.notes.Count + ")";
			this.dayLabel.Text = this.Day.Day.ToString() + "/" + this.Day.Month.ToString() + "/" + this.Day.Year.ToString();
		}
	}
}
