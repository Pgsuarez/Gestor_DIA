﻿using System;
using Gtk;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class Calendar : Gtk.VBox
	{
		public DateTime Month
		{
			get;
			set;
		}

		public DateTime Day
		{
			get {
				return this.calendarWidget.Date;
			}
		}
	}
}
