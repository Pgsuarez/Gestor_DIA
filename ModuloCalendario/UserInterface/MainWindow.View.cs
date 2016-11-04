using System;
using Gtk;

namespace ModuloCalendario.UserInterface
{
	public partial class MainWindow : Gtk.Window
	{

		private Components.Calendar calendarComponent;
		private Components.DayNotes dayNotesComponent;
		private Components.MonthNotes monthNotesComponent;

		public MainWindow() : base(Gtk.WindowType.Toplevel)
		{
			SetPosition(Gtk.WindowPosition.Center);
			DeleteEvent += OnDelete;
			Build();
			ShowAll();
		}

		private void Build()
		{
			var mainHbox = new HBox(true, 5);
			var leftVox = new VBox(true, 5);

			this.calendarComponent = new Components.Calendar();
			this.dayNotesComponent = new Components.DayNotes();
			this.monthNotesComponent = new Components.MonthNotes();

			this.calendarComponent.DaySelected += OnDaySelected;
			//left-top
			leftVox.PackStart(calendarComponent, true, true, 5);

			//left-bottom
			leftVox.PackStart(dayNotesComponent, true, true, 5);

			//left
			mainHbox.PackStart(leftVox, true, true, 5);

			//right
			mainHbox.PackStart(this.monthNotesComponent, true, true, 5);

			Add(mainHbox);
		}
	}
}
