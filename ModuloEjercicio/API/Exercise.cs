using System;
namespace ModuloEjercicio.API
{
	public class Exercise
	{
		private static int maxId = 0;

        public Exercise(Int32 Distance, Int32 Minutes, DateTime Date)
		{
			this.Id = maxId++;
			this.Distance = Distance;
			this.Minutes = Minutes;
            this.Date = Date;
		}

		private Exercise(Int32 Id, Int32 Distance, Int32 Minutes, DateTime Date)
		{
			this.Id = Id;
			this.Distance = Distance;
			this.Minutes = Minutes;
            this.Date = Date;
		}

		/**
		 * Do not use this method
		 * This method is meant to able persistence services to create an Exercise out of read data
		 */
		public static Exercise createExerciseWithId(Int32 Id, Int32 Distance, Int32 Minutes, DateTime Date)
		{
			if (maxId <= Id)
			{
				maxId = Id + 1;
			}
			return new Exercise(Id, Distance, Minutes, Date);
		}

		public Int32 Id
		{
			get; private set;
		}

		public Int32 Minutes
		{
			get; set;
		}

		public Int32 Distance
        {
            get; set;
        }

        public DateTime Date
        {
            get; set;
        }

		public override string ToString()
		{
            return String.Format("{0} (ID:{1}): {2}m in {3} minutes", Date, Id, Distance, Minutes);
		}
	}
}
