﻿using System;
using Gtk;
using ModuloEjercicio.API;

namespace ModuloEjercicio.App
{
	public partial class MainWindow : Window
	{
		public MainWindow() :
				base(WindowType.Toplevel)
		{
			this.Build();
		}

		private void Build()
		{
			this.Title = "Manage exercises";
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

			addButton.Clicked += delegate {
                onAddButtonClicked();
            };
			editButton.Clicked += delegate { 
                onEditButtonClicked();
            };
			deleteButton.Clicked += delegate { ShowDeleteDialog(); };

			butList.PackStart(addButton, false, false, 0);
			butList.PackStart(editButton, false, false, 0);
			butList.PackStart(deleteButton, false, false, 0);


			ScrolledWindow sw = new ScrolledWindow();
			sw.ShadowType = ShadowType.EtchedIn;
			sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
			exList.PackStart(sw, true, true, 0);

			LoadExercises();

			treeView = new TreeView(store);
			treeView.RulesHint = true;
			sw.Add(treeView);

			AddColumns();
		}

		void AddColumns()
		{
			CellRendererText rendererText = new CellRendererText();
			TreeViewColumn column = new TreeViewColumn("Distance", rendererText,
			                                           "text", Column.Distance);
			column.SortColumnId = (int)Column.Distance;
			treeView.AppendColumn(column);

			rendererText = new CellRendererText();
			column = new TreeViewColumn("Minutes", rendererText,
			                            "text", Column.Minutes);
			column.SortColumnId = (int)Column.Minutes;
			treeView.AppendColumn(column);
		}

		void ShowDeleteDialog()
		{
			string title = "Delete exercise";
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
	        Distance,
	        Minutes,
			Id
	    }

		private Button addButton;
		private Button editButton;
		private Button deleteButton;
	}
}
