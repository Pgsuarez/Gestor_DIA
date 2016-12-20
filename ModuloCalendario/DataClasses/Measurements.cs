using System;

namespace ModuloCalendario.DataClasses
{
	public class Measurements
	{
		public Measurements (int id, int weight, int abdominalCircunference, DateTime date)
		{
			this.Id = id;
			this.Weight = weight;
			this.AbdominalCircunference = abdominalCircunference;
			this.Date = date;
		}

		public Int32 Id { get;set; }
		public Int32 Weight { get;set; }
		public Int32 AbdominalCircunference { get; set; }
		public DateTime Date { get; set; }
	}
}

