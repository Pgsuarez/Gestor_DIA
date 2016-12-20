using System;
using ModuloCalendario.DataClasses;
using System.Collections.Generic;
namespace ModuloCalendario
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


		//fake method
		public List<Measurements> FindAllBetweenDates(DateTime start, DateTime end)
		{
			return this.measurements;
		}
	}
}
