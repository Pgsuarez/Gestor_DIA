using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ProyectoDIA.Core
{
	public class Diario
	{
		public Diario (Notas N)
		{
			this.N = N;
		}

		public Notas N {
			get;
			set;			
		}
			

		public void DiarioSaveToXml(){
			XDocument document = new XDocument(
				new XDeclaration("1.0", "utf-8", null));

			//Creamos el nodo raiz y lo añadimos al documento
			XElement nodoRaiz = new XElement("Diario");
			document.Add(nodoRaiz);

			XElement x = N.NotasSaveToXml ();

			nodoRaiz.Add (x);

			document.Save("Diario.xml");

		}

		public Notas DiarioLoadToXml(){
			XDocument xdocument = XDocument.Load("Diario.xml");
			IEnumerable<XElement> Diario = xdocument.Elements();
			return N.NotasLoadToXml (Diario.Elements());
		}

	}
}

