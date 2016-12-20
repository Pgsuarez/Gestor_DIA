using System;
using ModuloCalendario.DataClasses;
using System.Collections.Generic;
using ProyectoDIA.Core;

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

		private Notas notes;

		private NotesService()
		{
            try
            {
                var diario = new Diario(new Notas());
                this.notes = diario.DiarioLoadToXml();
            }
            catch { }
            if (notes == null)
            {
                this.notes = new Notas();
            }

		}

        ~NotesService()
        {
            var diario = new Diario(this.notes);
            diario.DiarioSaveToXml();
        }

		public List<Nota> FindAllBetweenDates(DateTime start, DateTime end)
		{
            return this.notes.N.FindAll(x => (x.Fecha >= start && x.Fecha <= end));
		}

		public Nota FindById(int id){
            return this.notes.Get(id);
		}

		public void Save(Nota note){
            this.notes.anadir (note);
		}

		public void Update(Nota note){
            this.notes.Update (note);
		}

        public List<Nota> FindAll()
        {
            return this.notes.N;
        }

		public void Remove(Nota note){
            this.notes.Borrar (note.Id);
		}


	}
}
