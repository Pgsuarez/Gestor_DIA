using System;
using ModuloCalendario.Services;
using ModuloCalendario.DataClasses;
using ProyectoDIA.Core;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MeasureFormDialog : Gtk.Dialog
	{
		Int32 measureId = -1;
		Measurements measure;

		private void RefreshView(){
            this.ShowWeight(this.measure.Weight);
            this.ShowAC (this.measure.AbdominalCircunference);
            this.ShowDate (this.measure.Date);
		}

		private void SaveMeasure(int w, int ac, DateTime date){
			
            this.measure.Weight = w;
            this.measure.AbdominalCircunference = ac;
            this.measure.Date = date;

			if (this.measureId == -1) {
				MeasurementsService.Instance.Save (this.measure);
			} else {
				MeasurementsService.Instance.Update (this.measure);
			}

			this.Destroy ();
		}

		private void FetchMeasure(){
			if (this.measureId != -1) {
				this.measure = MeasurementsService.Instance.FindById (this.measureId);
				Console.WriteLine("refrescando"+measure.Weight);
			}
		}
	}
}

	