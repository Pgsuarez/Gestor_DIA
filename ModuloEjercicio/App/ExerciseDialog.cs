using System;
using Gtk;
using ModuloEjercicio.API;

namespace ModuloEjercicio.App
{
	public partial class ExerciseDialog : Dialog
	{
        public ExerciseDialog(String title, Window parent, Exercise ex = null) :
                base(title, parent, Gtk.DialogFlags.DestroyWithParent)
        {
            this.ex = ex;
            this.Build();
        }

		private void Build()
		{
            this.Modal = true;

            var row1 = new HBox(true, 5);
            var row2 = new HBox(true, 5);
            var row3 = new HBox(true, 5);

            this.VBox.Add(row1);
            this.VBox.Add(row2);
            this.VBox.Add(row3);

            var lbl1 = new Label("Distance");
            var lbl2 = new Label("Minutes");

            if (ex != null)
            {
                var selectedEx_Distance = ex.Distance;
                var selectedEx_Minutes = ex.Minutes;
                var selectedEx_Date = ex.Date;
                var selectedEx_Id = ex.Id;

                distanceEntry = new Entry(selectedEx_Distance.ToString());
                minutesEntry = new Entry(selectedEx_Minutes.ToString());
                dateEntry = new Calendar();
                dateEntry.Date = selectedEx_Date;
            }
            else
            {
                distanceEntry = new Entry("0");
                minutesEntry = new Entry("0");
                dateEntry = new Calendar();
            }

            row1.Add(lbl1);
            row1.Add(distanceEntry);

            row2.Add(lbl2);
            row2.Add(minutesEntry);

            row3.Add(dateEntry);

            if (ex != null)
            {
                //ResponseType.Apply -> Edit exercise
                this.AddButton(Stock.Save, ResponseType.Apply);
            }
            else
            {
                //ResponseType.Accept -> Add new exercise
                this.AddButton(Stock.Save, ResponseType.Accept);
            }
            this.AddButton(Stock.Cancel, ResponseType.Cancel);
            this.Response += OnDialogResponse;
            this.VBox.ShowAll();
            this.Run();
            this.Destroy();
		}

        public Exercise getResult()
        {
            return ex;
        }

        private Exercise ex;

		private Entry minutesEntry;
        private Entry distanceEntry;
        private Calendar dateEntry;
	}
}
