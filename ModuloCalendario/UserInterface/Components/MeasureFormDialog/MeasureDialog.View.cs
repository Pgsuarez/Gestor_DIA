using System;
using Gtk;
namespace ModuloCalendario.UserInterface.Components
{
	public partial class MeasureFormDialog : Gtk.Dialog
	{
		private Entry weightEntry;
		private Entry acEntry;
		private Gtk.Calendar dateEntry;

		public MeasureFormDialog(Gtk.Window parent, Gtk.DialogFlags flags) :
			base("Insert measure", parent, flags)
		{
			this.OnCreate ();
			this.BuildDialog ();
		}

		public MeasureFormDialog(int measureId, Gtk.Window parent, Gtk.DialogFlags flags) :
		base("Edit Measure"+measureId, parent, flags)
		{
			this.OnCreate ();
			this.OnReceivedMeasured(measureId);

			this.BuildDialog();
		}

		private void BuildDialog(){

			this.Modal = true;

			var row1 = new HBox(false, 5);
			var row2 = new HBox(false, 5);
			var row3 = new HBox(false, 5);

			this.VBox.PackStart(row1);
			this.VBox.Add(row2);
			this.VBox.PackEnd(row3);

			var lbl1 = new Label("Weight");
			var lbl2 = new Label("Barriga Perimeter");
			var lbl3 = new Label("Date");

			this.weightEntry = new Entry();
			this.acEntry = new Entry();
			this.dateEntry = new Gtk.Calendar();

			row1.PackStart(lbl1,false,false,5);
			row1.Add(this.weightEntry);

			row2.PackStart(lbl2, false, false, 5);
			row2.Add(this.acEntry);

			row3.PackStart(lbl3, false, false, 5);
			row3.Add(this.dateEntry);

			this.AddButton(Stock.Save, ResponseType.Accept);
			this.AddButton(Stock.Cancel, ResponseType.Cancel);

			this.Response += this.OnDialogResponse;

			this.VBox.ShowAll();

			this.OnViewBuilt ();


			this.Run();
			this.Destroy();
		}

		private void ShowWeight(int w){
			this.weightEntry.Text = w.ToString();
			Console.WriteLine("Añadiendo peso : " + w);
		}

		private void ShowAC(int ac){
			this.acEntry.Text = ac.ToString();

			Console.WriteLine("Añadiendo Circunference : " + ac);
		}

		private void ShowDate(DateTime date){
			this.dateEntry.Date = date;

			Console.WriteLine("Añadiendo Fecha : " + date.ToString());
		}
	}
}

