using System;
using ModuloCalendario.DataClasses;
using System.Collections.Generic;

namespace ModuloCalendario
{
	public class ExercisesService
	{
		private static ExercisesService instance = null;
		public static ExercisesService Instance
		{
			get {
				if (instance == null)
				{
					instance = new ExercisesService();
				}
				return instance;
			}
		}

		private List<Exercise> exercises;

		private ExercisesService()
		{
			this.exercises = new List<Exercise>();

			this.exercises.Add(new Exercise(1, 1, 12, new DateTime(2016, 12, 16)));
			this.exercises.Add(new Exercise(2, 3, 30, new DateTime(2016, 12, 17)));
		}

		//fake method
		public List<Exercise> FindAllBetweenDates(DateTime start, DateTime end)
		{
			return this.exercises;
		}
	}
}

