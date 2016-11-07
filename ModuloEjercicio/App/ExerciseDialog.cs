using System;
using Gtk;
using ModuloEjercicio.API;

namespace ModuloEjercicio.App
{
	public partial class ExerciseDialog : Dialog
	{
        /// <summary>
        /// Creates a new exercise from user input
        /// </summary>
		public ExerciseDialog(String title, Window parent, out Exercise ex) :
                base(title, parent, Gtk.DialogFlags.DestroyWithParent)
        {
            ex = exToRet = new Exercise(0, 0);
            edit = false;
            this.Build();
        }

        /// <summary>
        /// Updates exercise from user input
        /// param name="ex" The exercise to update
        /// </summary>
        public ExerciseDialog(String title, Window parent, ref Exercise ex) :
                base(title, parent, Gtk.DialogFlags.DestroyWithParent)
        {
            exToRet = ex;
            edit = true;
            this.Build();
        }

		private void Build()
		{
            this.Modal = true;

            var row1 = new HBox(true, 5);
            var row2 = new HBox(true, 5);

            this.VBox.Add(row1);
            this.VBox.Add(row2);

            var lbl1 = new Label("Distance");
            var lbl2 = new Label("Minutes");

            if (exToRet != null)
            {
                var selectedEx_Distance = exToRet.Distance;
                var selectedEx_Minutes = exToRet.Minutes;
                var selectedEx_Id = exToRet.Id;

                distanceEntry = new Entry(selectedEx_Distance.ToString());
                minutesEntry = new Entry(selectedEx_Minutes.ToString());
            }
            else
            {
                distanceEntry = new Entry();
                minutesEntry = new Entry();
            }

            row1.Add(lbl1);
            row1.Add(distanceEntry);

            row2.Add(lbl2);
            row2.Add(minutesEntry);

            if (exToRet != null)
            {
                this.AddButton(Stock.Save, ResponseType.Apply);
            }
            else
            {
                this.AddButton(Stock.Save, ResponseType.Accept);
            }
            this.AddButton(Stock.Cancel, ResponseType.Cancel);
            this.Response += OnDialogResponse;
            this.VBox.ShowAll();
            this.Run();
            this.Destroy();
		}

        private Exercise exToRet;
        private bool edit;

		private Entry minutesEntry;
		private Entry distanceEntry;
	}
}
