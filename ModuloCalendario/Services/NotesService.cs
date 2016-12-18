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

			this.notes.Add(new Note(1, "Molestia", "Tengo una molestia en el hombro", new DateTime(2016,12,16)));
			this.notes.Add(new Note(2, "Muy bien", "Estoy muy bien", new DateTime(2016,12,17)));
		}

		//fake method
		public List<Note> FindAllBetweenDates(DateTime start, DateTime end)
		{
			return this.notes;
		}


	}
}
