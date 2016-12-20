using System;
using System.Collections.Generic;
using ModuloEjercicio.API;

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

        private ExerciseService exerciseService = ExerciseService.GetInstance();
		
        public List<Exercise> FindAll()
        {
            return exerciseService.FindAll();
        }

        public Exercise Get(Int32 Id)
        {
            return exerciseService.Get(Id);
        }

        public void Add(Exercise Exercise)
        {
            exerciseService.Add(Exercise);
        }

        public void Update(Exercise Exercise)
        {
            exerciseService.Update(Exercise);
        }

        public void Delete(Int32 Id)
        {
            exerciseService.Delete(Id);
        }

		//fake method
		public List<Exercise> FindAllBetweenDates(DateTime start, DateTime end)
		{
            return FindAll();
		}
	}
}

