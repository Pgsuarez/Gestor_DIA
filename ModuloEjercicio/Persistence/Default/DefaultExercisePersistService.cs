using System;
using System.Collections.Generic;
using ModuloEjercicio.API;
using ModuloEjercicio.Persistence.API;

namespace ModuloEjercicio.Persistence.Default
{
	//Sealed indicates that this class cannot be extended (like final in Java)
	//This class is marked sealed in order to indicate that the virtual method Read is implemented
	//and can be used on the constructor
	public sealed partial class DefaultExercisePersistService: ExercisePersistService
	{
		private Dictionary<Int32, Exercise> exercises;

		public DefaultExercisePersistService()
		{
			try
			{
				exercises = Read();
			}
			catch
			{
				exercises = new Dictionary<Int32, Exercise>();
			}
		}

		~DefaultExercisePersistService()
		{
			Write(exercises);
		}

		public override List<Exercise> FindAll()
		{
			var list = new List<Exercise>();
			list.AddRange(exercises.Values);
			return list;
		}
		
		public override Exercise Get(int Id)
		{
			Exercise ex;
			exercises.TryGetValue(Id, out ex);
			return ex;
		}

		public override void Add(Exercise Exercise)
		{
			exercises.Add(Exercise.Id, Exercise);
		}

		public override void Update(Exercise Exercise)
		{
			exercises[Exercise.Id] = Exercise;
		}

		public override void Delete(int Id)
		{
			exercises.Remove(Id);
		}
	}
}
