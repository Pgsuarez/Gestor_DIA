using System;
using Gtk;

namespace ModuloCalendario.UserInterface
{
	public partial class MainWindow : Gtk.Window
	{
		private void OnDelete(object o, Gtk.DeleteEventArgs args)
		{
			Gtk.Application.Quit();
		}

		void OnDaySelected(object sender, EventArgs e)
		{
			var calendar = (Calendar)sender;
			this.ChangeDay (calendar.Date);
		}
	}
}
