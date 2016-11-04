using System;
using System.Collections.Generic;
using ModuloEjercicio.API;
using ModuloEjercicio.Persistence.Default;

namespace ModuloEjercicio.Persistence.API
{
	public abstract class ExercisePersistService
	{
		private static ExercisePersistService singleton = null;

		public static ExercisePersistService getInstance()
		{
			if (singleton == null)
			{
				return new DefaultExercisePersistService();
			}
			else
			{
				return singleton;
			}
		}

		public abstract List<Exercise> FindAll();

		public abstract Exercise Get(Int32 Id);

		public abstract void Add(Exercise Exercise);

		public abstract void Update(Exercise Exercise);

		public abstract void Delete(Int32 Id);
	}
}
