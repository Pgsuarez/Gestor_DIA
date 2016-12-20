﻿using System;
using System.Collections.Generic;
using Gtk;
using ModuloCalendario.DataClasses;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class DayExercises : Gtk.VBox
	{
		enum Columns { Index, Distance, Minutes, Date };

		TreeView notesTreeView;
		ListStore exercisesListStore;
	


		public DayExercises() : base()
		{
			//Init view's state
			this.InitModel();

			this.exercisesListStore = new Gtk.ListStore(typeof(int), typeof(int), typeof(int), typeof(string));

			//Build
			Build();
		}

		private void Build()
		{
			var mainVox = new Gtk.VBox();

			var removeButton = new Gtk.Button (Gtk.Stock.Remove);
			var editButton = new Gtk.Button (Gtk.Stock.Edit);

			removeButton.Clicked += this.OnClickRemove;
			editButton.Clicked += this.OnClickEdit;

			var hBox = new Gtk.HBox();

			hBox.PackStart (removeButton, false, false, 0);
			hBox.PackStart (editButton, false, false, 0);

			mainVox.PackStart (hBox, false, false, 10);

			//List
			this.notesTreeView = new TreeView(this.exercisesListStore);

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
			column = new TreeViewColumn("Distance", rendererText, "text", Columns.Distance);
			column.SortColumnId = (int)Columns.Distance;
			this.notesTreeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Minutes", rendererText, "text", Columns.Minutes);
			column.SortColumnId = (int)Columns.Minutes;
			this.notesTreeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Date", rendererText, "text", Columns.Date);
			column.SortColumnId = (int)Columns.Date;
			this.notesTreeView.AppendColumn(column);

		}

		private void ClearExercises(){
			this.exercisesListStore.Clear ();
		}

		private void ShowExercise(int index, int distance, int minutes, string date){
			this.exercisesListStore.AppendValues (index, distance, minutes, date);
		}
	}
}
