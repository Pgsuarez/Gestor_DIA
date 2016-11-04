using System;
using System.Collections.Generic;
using ModuloEjercicio.API;
using ModuloEjercicio.App;
using Gtk;
using System.Threading;

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

		private static void RunOnConsole()
		{
			ExerciseService exerciseService = ExerciseService.getInstance();
			List<Exercise> exercises;

			Boolean Exit = false;
			while (!Exit)
			{
				exercises = exerciseService.FindAll();
				UI.MostrarLista(exercises, "ejercicios");

				Int16 op = UI.Menu();
				var Switch = new Dictionary<Int32, System.Action>
				{
					{1, () => exerciseService.Add(UI.NuevoEjercicio())},
					{2, () => exerciseService.Delete(exercises[UI.LeerNumEjercicio()].Id)},
					{3, () => {
							var ex = exercises[UI.LeerNumEjercicio()];
							ex = UI.ModificarEjercicio(ex);
							exerciseService.Update(ex);
							  }
					},
					{4, () => Exit = true }
				};
				Switch[op]();
			}
		}

		private static void RunOnBoth()
		{
			// Create the thread object, passing in the Alpha.Beta method
			// via a ThreadStart delegate. This does not start the thread.
			var windowThread = new Thread(new ThreadStart(RunOnWindow));
			var consoleThread = new Thread(new ThreadStart(RunOnConsole));

			// Start the thread
			windowThread.Start();
			consoleThread.Start();

			// Wait until oThread finishes. Join also has overloads
			// that take a millisecond interval or a TimeSpan object.
			windowThread.Join();
			consoleThread.Join();
		}
	}
}
