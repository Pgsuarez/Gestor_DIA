using System;
using System.Collections.Generic;
using Gtk;
using ModuloEjercicio.API;

namespace ModuloEjercicio.App
{
	public partial class ExerciseDialog
	{
		void OnDialogResponse(object o, ResponseArgs args)
		{
			//ResponseType.Accept -> Add new exercise
			if (args.ResponseId.Equals(ResponseType.Accept))
			{
				var distance = Convert.ToInt32(distanceEntry.Text);
				var minutes = Convert.ToInt32(minutesEntry.Text);
                exToRet = new Exercise(distance, minutes);
			}

			//ResponseType.Apply -> Edit exercise
			if (args.ResponseId.Equals(ResponseType.Apply))
			{
				var distance = Convert.ToInt32(distanceEntry.Text);
				var minutes = Convert.ToInt32(minutesEntry.Text);

				exToRet.Distance = distance;
				exToRet.Minutes = minutes;
			}
		}
	}
}