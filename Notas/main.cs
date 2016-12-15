using System;
using Gtk;
using ProyectoDIA.IU;

namespace ProyectoDIA
{
	public class main
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

