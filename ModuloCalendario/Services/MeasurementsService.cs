using System;
using ModuloCalendario.DataClasses;
using System.Collections.Generic;

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

		private List<Measurements> measurements;


		private MeasurementsService()
		{
			this.measurements = new List<Measurements>();

			this.measurements.Add(new Measurements(1, 1, 12, new DateTime(2016, 12, 16)));
			this.measurements.Add(new Measurements(2, 3, 30, new DateTime(2016, 12, 17)));
		}

		public List<Measurements> FindAllBetweenDates(DateTime start, DateTime end)
		{
			return this.measurements.FindAll(x => (x.Date >= start && x.Date <= end));
		}

		//fake method
		public Measurements FindById(int id){
			return this.measurements [0];
		}

		//fake method
		public void Save(Measurements mea){
			this.measurements.Add (mea);
		}

		//fake method
		public void Update(Measurements mea){
			this.measurements.Add (mea);
		}

		public void Remove(Measurements mea){
			this.measurements.Remove (mea);
		} 
	}
}
