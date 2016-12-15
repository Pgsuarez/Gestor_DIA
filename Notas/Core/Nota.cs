using System;
using System.Xml.Linq;
using System.Collections.Generic;

namespace ProyectoDIA.Core
{
	public class Nota
	{
		public Nota (int Id, string t, string c, DateTime d)
		{
			Titulo = t;
			Cuerpo = c;
			Fecha = d;
			this.Id = Id;
		}

		public int Id  {
			get;
			set;
		}

		public DateTime Fecha {
			get;
			set;
		}

		public string Titulo {
			get;
			set;
		}

		public string Cuerpo {
			get;
			set;
		}

		public XElement SaveToXml(){
			XElement raiz = new XElement( "nota",
				new XElement( "Id", Id ),
				new XElement( "Titulo", Titulo ),
				new XElement( "Cuerpo", Cuerpo ),
				new XElement( "Fecha", Fecha)
			);
			return raiz;
		}

		public override string ToString ()
		{
			return string.Format ("[Nota: Titulo={0}, Cuerpo={1}, Fecha={2}, Id={3}]", Titulo, Cuerpo, Fecha,Id);
		}

	
		public Notas LoadToXml(IEnumerable<XElement> Notas){
			Notas n2 = new ProyectoDIA.Core.Notas ();
			foreach (var n in Notas)
			{
				Id = Convert.ToInt32(n.Element ("Id").Value);
				Titulo = n.Element ("Titulo").Value;
				Cuerpo = n.Element ("Cuerpo").Value;
				Fecha = DateTime.Parse(n.Element ("Fecha").Value);
				n2.anadir( new Nota (Id,Titulo, Cuerpo, Fecha));
			}

			return n2;
		}
	}
}

