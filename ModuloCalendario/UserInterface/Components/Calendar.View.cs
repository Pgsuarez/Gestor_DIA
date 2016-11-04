using System;
using Gtk;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class Calendar : Gtk.VBox
	{

		private Gtk.Calendar calendarWidget;
		private Gtk.Label calendarTitle;

		public Calendar() : base()
		{
			Build();
		}

		private void Build()
		{
			this.calendarTitle = new Gtk.Label("Calendar");
			this.calendarWidget = new Gtk.Calendar();

			this.calendarWidget.DaySelected += OnDaySelected;

			PackStart(this.calendarTitle, false, true, 5);
			PackStart(this.calendarWidget, true, true, 5);
		}


	}
}
