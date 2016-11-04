using System;
namespace ModuloCalendario.DataClasses
{
	public class Note
	{


		public Note(int id, string text, int exerciseId, DateTime date)
		{
			this.Id = id;
			this.Text = text;
			this.ExerciseId = exerciseId;
			this.Date = date;
		}

		public Note(int id, string text, int exerciseId) : 
			this(id, text, exerciseId, DateTime.Now) 
		{}

		public int Id
		{
			get;
		}

		public string Text
		{
			get;
			set;
		}

		public int ExerciseId
		{
			get;
		}

		public DateTime Date
		{
			get;
			set;
		}
	}
}
