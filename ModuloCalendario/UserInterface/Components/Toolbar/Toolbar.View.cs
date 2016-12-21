using System;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class Toolbar: Gtk.Toolbar
	{
		public Toolbar () : base()
		{
			this.Build ();
		}

		private void Build(){
			this.ToolbarStyle = Gtk.ToolbarStyle.Both;

			//buttons
			var newNoteButton = new Gtk.ToolButton(Gtk.Stock.Add);
			newNoteButton.Label = "New Note";
			newNoteButton.Clicked += this.OnNewNoteClicked;

			var newExerciseButton = new Gtk.ToolButton(Gtk.Stock.Add);
			newExerciseButton.Label = "New Exercise";
			newExerciseButton.Clicked += this.OnNewExerciseClicked;

			var newMeasureButton = new Gtk.ToolButton(Gtk.Stock.Add);
			newMeasureButton.Label = "New Measure";
			newMeasureButton.Clicked += this.OnNewMeasureClicked;

			this.Insert (newNoteButton, 0);
			this.Insert (newExerciseButton, 1);
			this.Insert(newMeasureButton, 2);

			this.OnViewBuilt ();
		}
	}
}

