using System;
using Gtk;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class Calendar : Gtk.VBox
	{
		public event EventHandler DaySelected;

		private void OnDaySelected(object sender, EventArgs e)
		{
			if (DaySelected != null)
			{
				DaySelected(sender, e);
			}
		}
	}
}
