using System;
using ModuloCalendario.DataClasses;

using System.Collections;
using System.Collections.Generic;

using ModuloMedidas;

namespace ModuloCalendario.Services
{
	public class MeasurementsService
	{

		private static MeasurementsService instance = null;
		public static MeasurementsService Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new MeasurementsService();
				}
				return instance;
			}
		}

		private ListaMedidas measurements;


		private MeasurementsService()
		{
			this.measurements = new ListaMedidas();
		}

		public List<Measurements> FindAllBetweenDates(DateTime start, DateTime end)
		{
			
			return Adapter(this.measurements.getListaMedidas().FindAll(x => (x.Fecha >= start && x.Fecha <= end)));
		}

		private List<Measurements> Adapter(List<Medidas> list)
		{
			List<Measurements> toret = new List<Measurements>();

			foreach (Medidas m in list)
				toret.Add(new Measurements(m));

			return toret;
		}


		//fake method
		public Measurements FindById(int id){
			int index = this.measurements.getListaMedidas().FindIndex(x => x.Id == id);
			return ((index != -1)? new Measurements( this.measurements.getListaMedidas()[index]) : null);
		}

		//fake method
		public void Save(Measurements mea){
			mea.Id = measurements.getListaMedidas().Count;
			measurements.Add(new Medidas(mea.Id, mea.Weight, mea.AbdominalCircunference, mea.Date));
		}

		//fake method
		public void Update(Measurements mea){

			if (mea.Id >= 0)
			{
				this.measurements.Remove(mea.Id);
				this.measurements.Add(new Medidas(mea.Id, mea.Weight, mea.AbdominalCircunference, mea.Date));
			}
		}

		public void Remove(Measurements mea){
			if(mea.Id >= 0)
			this.measurements.Remove (mea.Id);
		} 
	}
}
