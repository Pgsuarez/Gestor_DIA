using System;
using System.Collections.Generic;
using ModuloEjercicio.Core;

namespace ModuloEjercicio.API
{
	public abstract class ExerciseService
	{
		private static ExerciseService singleton = null;

		public static ExerciseService getInstance()
		{
			if (singleton == null)
			{
				return new DefaultExerciseService();
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
