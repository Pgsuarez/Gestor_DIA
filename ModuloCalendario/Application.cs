using System;
namespace ModuloCalendario
{
	public class Application
	{
		public static void Main(String[] args)
		{
			Gtk.Application.Init();
			var window = new UserInterface.MainWindow();
			Gtk.Application.Run();
		}
	}
}
