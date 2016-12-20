using System;
namespace ModuloCalendario.DataClasses
{
	public class Note
	{


		public Note(int id, string title, string body, DateTime date)
		{
			this.Id = id;
			this.Title = title;
			this.Body = body;
			this.Date = date;
		}

		public Note(int id, string title, string body) : 
			this(id, title, body, DateTime.Now) 
		{}

		public int Id
		{
			get;
		}

		public string Title
		{
			get;
			set;
		}

		public string Body {
			get;
			set;
		}

		public DateTime Date
		{
			get;
			set;
		}
	}
}
