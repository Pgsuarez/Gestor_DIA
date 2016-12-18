using System;

namespace ModuloCalendario.DataClasses
{
	public class Exercise
	{
		public Exercise (int id, int distance, int minutes, DateTime date)
		{
			this.Id = id;
			this.Distance = distance;
			this.Minutes = minutes;
			this.Date = date;
		}

		public Int32 Id { get; }
		public Int32 Distance { get;set; }
		public Int32 Minutes { get; set; }
		public DateTime Date { get; set; }

	}
}

