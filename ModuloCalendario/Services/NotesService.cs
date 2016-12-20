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

		public List<Note> FindAllBetweenDates(DateTime start, DateTime end)
		{
			return this.notes.FindAll(x => (x.Date >= start && x.Date <= end));
		}

		//fake method
		public Note FindById(int id){
			return this.notes [0];
		}

		//fake method
		public void Save(Note note){
			this.notes.Add (note);
		}

		//fake method
		public void Update(Note note){
			this.notes.Add (note);
		}


	}
}
