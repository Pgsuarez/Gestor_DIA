using System;
using ModuloCalendario.DataClasses;
using ProyectoDIA.Core;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MeasureFormDialog : Gtk.Dialog
	{
		private void OnViewBuilt(){
			this.RefreshView ();
		}

		private void OnCreate(){
			this.measure = new Measurements (-1, 0, 0, DateTime.Now);
		}

		private void OnReceivedMeasured(int measureId){
			this.measureId = measureId;
			this.FetchMeasure ();
			Console.WriteLine("recibido valor : " + measureId);
		}

		private void OnDialogResponse(object o, Gtk.ResponseArgs args){
			if (args.ResponseId.Equals (Gtk.ResponseType.Accept) || args.ResponseId.Equals (Gtk.ResponseType.Apply)) {
				this.SaveMeasure (Int32.Parse( this.weightEntry.Text),
				                  Int32.Parse(this.acEntry.Text),
					this.dateEntry.Date);
			} else if (args.ResponseId.Equals(Gtk.ResponseType.Reject)) {
				this.Destroy ();
			}
		}
	}
}

