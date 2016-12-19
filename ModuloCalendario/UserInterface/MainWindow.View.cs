using System;
using Gtk;

namespace ModuloCalendario.UserInterface
{
	public partial class MainWindow : Gtk.Window
	{

		private Components.Calendar calendarComponent;
		private Components.DayContent dayContentComponent;
		private Components.MonthContent monthContentComponent;
		private Components.Toolbar toolbarComponent;


		public MainWindow() : base(Gtk.WindowType.Toplevel)
		{
			SetPosition(Gtk.WindowPosition.Center);
			DeleteEvent += OnDelete;
			Build();
			ShowAll();
		}

		private void Build()
		{
			var mainVbox = new VBox (false, 0);
			var mainHbox = new HBox(true, 5);
			var leftVox = new VBox(true, 5);

			this.calendarComponent = new Components.Calendar();
			this.dayContentComponent = new Components.DayContent();
			this.monthContentComponent = new Components.MonthContent();
			this.toolbarComponent = new Components.Toolbar ();

			this.toolbarComponent.NewNoteClicked += this.OnNewNoteClicked;
			this.toolbarComponent.NewExerciseClicked += this.OnNewExerciseClicked;


			this.calendarComponent.DaySelected += OnDaySelected;

			//left-top
			leftVox.PackStart(this.calendarComponent, true, true, 5);

			//left-bottom
			leftVox.PackStart(this.dayContentComponent, true, true, 5);

			//left
			mainHbox.PackStart(leftVox, true, true, 5);

			//right
			mainHbox.PackStart(this.monthContentComponent, true, true, 5);

			mainVbox.PackStart (this.toolbarComponent, false, false, 0);
			mainVbox.Add (mainHbox);
			this.Add(mainVbox);

			this.OnViewBuilt ();
		}

		private void ShowNoteForm(){
			var noteForm = new Components.NoteFormDialog (this, Gtk.DialogFlags.DestroyWithParent);
		}
	}
}
