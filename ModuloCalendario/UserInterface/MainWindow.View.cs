using System;
using Gtk;

namespace ModuloCalendario.UserInterface
{
	public partial class MainWindow : Gtk.Window
	{

		private Components.Calendar calendarComponent;
		private Components.DayContent dayContentComponent;
		private Components.MonthContent monthContentComponent;


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
			this.dayContentComponent = new Components.DayContent();
			this.monthContentComponent = new Components.MonthContent();


			this.calendarComponent.DaySelected += OnDaySelected;

			//left-top
			leftVox.PackStart(this.calendarComponent, true, true, 5);

			//left-bottom
			leftVox.PackStart(this.dayContentComponent, true, true, 5);

			//left
			mainHbox.PackStart(leftVox, true, true, 5);

			//right
			mainHbox.PackStart(this.monthContentComponent, true, true, 5);

			Add(mainHbox);
		}
	}
}
