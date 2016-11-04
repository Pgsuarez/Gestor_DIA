using System;
namespace ModuloEjercicio.API
{
	public class Exercise
	{
		private static int maxId = 0;

		public Exercise(Int32 Distance, Int32 Minutes)
		{
			this.Id = maxId++;
			this.Distance = Distance;
			this.Minutes = Minutes;
		}

		private Exercise(Int32 Id, Int32 Distance, Int32 Minutes)
		{
			this.Id = Id;
			this.Distance = Distance;
			this.Minutes = Minutes;
		}

		/**
		 * Do not use this method
		 * This method is meant to able persistence services to create an Exercise out of read data
		 */
		public static Exercise createExerciseWithId(Int32 Id, Int32 Distance, Int32 Minutes)
		{
			if (maxId <= Id)
			{
				maxId = Id + 1;
			}
			return new Exercise(Id, Distance, Minutes);
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

		public override string ToString()
		{
			return String.Format("{0}: {1}m in {2} minutes", Id, Distance, Minutes);
		}
	}
}
