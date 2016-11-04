using System;
using ModuloCalendario.DataClasses;
using System.Collections.Generic;

namespace ModuloCalendario.Services
{
	public class NotesService
	{
		private static NotesService instance = null;
		public static NotesService Instance
		{
			get {
				if (instance == null)
				{
					instance = new NotesService();
				}
				return instance;
			}
		}

		private List<Note> notes;

		private NotesService()
		{
			this.notes = new List<Note>();

			this.notes.Add(new Note(0, "This exercise is supeasy", 0, DateTime.Now));
			this.notes.Add(new Note(1, "This exercise is easy", 1, DateTime.Now));
			this.notes.Add(new Note(2, "This exercise is hard", 2, DateTime.Now));
		}

		//fake method
		public List<Note> FindAllBetweenDates(DateTime start, DateTime end)
		{
			return this.notes;
		}


	}
}
