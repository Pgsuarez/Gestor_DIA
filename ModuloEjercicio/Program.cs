using ModuloEjercicio.App;
using Gtk;

namespace ModuloEjercicio
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			RunOnWindow();
		}

		private static void RunOnWindow()
		{
			Application.Init();
			var window = new MainWindow();
			window.ShowAll();
			Application.Run();
		}
	}
}
