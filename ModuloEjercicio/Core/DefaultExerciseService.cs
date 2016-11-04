using System;
using System.Collections.Generic;
using ModuloEjercicio.API;
using ModuloEjercicio.Persistence.API;

namespace ModuloEjercicio.Core
{
	public class DefaultExerciseService : ExerciseService
	{
		private readonly ExercisePersistService ExercisePersistService;

		public DefaultExerciseService()
		{
			ExercisePersistService = ExercisePersistService.getInstance();
		}

		public override List<Exercise> FindAll()
		{
			return ExercisePersistService.FindAll();
		}
		
		public override Exercise Get(int Id)
		{
			return ExercisePersistService.Get(Id);
		}

		public override void Add(Exercise Exercise)
		{
			ExercisePersistService.Add(Exercise);
		}

		public override void Update(Exercise Exercise)
		{
			ExercisePersistService.Update(Exercise);
		}

		public override void Delete(int Id)
		{
			ExercisePersistService.Delete(Id);
		}
	}
}
