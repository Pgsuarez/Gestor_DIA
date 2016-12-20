using System;
namespace ModuloCalendario
{
	public class Application
	{
		public static void Main(String[] args)
		{
			Gtk.Application.Init();
			new UserInterface.MainWindow();
			Gtk.Application.Run();
		}
	}
}
