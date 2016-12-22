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
            if (!args.ResponseId.Equals(ResponseType.Cancel) && !args.ResponseId.Equals(ResponseType.DeleteEvent))
            {
                var distance = Convert.ToInt32(distanceEntry.Text);
                var minutes = Convert.ToInt32(minutesEntry.Text);
                var date = dateEntry.Date;

                //ResponseType.Accept -> Add new exercise
                if (args.ResponseId.Equals(ResponseType.Accept))
                {
                    ex = new Exercise(distance, minutes, date);
                }

                //ResponseType.Apply -> Edit exercise
                if (args.ResponseId.Equals(ResponseType.Apply))
                {
                    ex.Distance = distance;
                    ex.Minutes = minutes;
                    ex.Date = date;
    			}
            }
		}
	}
}