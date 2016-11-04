using System;

namespace DIAGraficos {
	public class Ppal {
		public static void Main(string[] args) {
			
			Gtk.Application.Init();
			new Ventana("Prueba");
			Gtk.Application.Run();
		}
	}
}
