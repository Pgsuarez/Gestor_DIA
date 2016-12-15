using System;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;


namespace ProyectoDIA.Core
{
	public class Notas
	{
		public Notas(){
			N = new List<Nota> ();
			count = 0;
		}

		public List<Nota> N {
			get;
			set;
		}
		public int count {
			get;
			set;
		}
		public void anadir (Nota n){
			N.Add (n);
			count++;
		}

		public Nota Get(int id){
			return N.Find(x => x.Id.Equals(id));
		}

		public void Update(Nota n){
			Nota n1 = N.Find(x => x.Id.Equals(n.Id));
			N.Remove (n1);
			N.Add (n);
		}

		public void Borrar(int id){
			Nota n1 = N.Find(x => x.Id.Equals(id));
			N.Remove (n1);
		}

		public XElement NotasSaveToXml(){
			XElement ArrayElement=new XElement("Notas");
			foreach (Nota no in N) {
				ArrayElement.Add(no.SaveToXml ());
			}
			return ArrayElement;
		}

		public Notas NotasLoadToXml(IEnumerable<XElement> Diario){
			Notas n1= new Notas();
			Nota n = new Nota (0,null, null, DateTime.Parse ("2013-07-04"));
			foreach (var Notas in Diario)
			{
				n1= n.LoadToXml (Notas.Elements());

			}

			return n1;

		}


		public override string ToString ()
		{
			string toret = "";
			foreach (Nota no in N) {
				toret += no.ToString ();
			}
			return toret;
		}

	}
}
