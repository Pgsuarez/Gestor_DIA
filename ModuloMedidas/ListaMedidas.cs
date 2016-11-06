using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ModuloMedidas
{
	public class ListaMedidas
	{
		private List<Medidas> listaMedidas=new List<Medidas>();

		public ListaMedidas ()
		{
		}

		public List<Medidas> getListaMedidas(){
			return this.listaMedidas;
		}

		public void setListaMedidas(List<Medidas> lm){
			this.listaMedidas = lm;
		}

		public void Add(Medidas m){
			this.listaMedidas.Add (m);
		}

		public void Guardar(){
			var doc = new XmlTextWriter( "listamedidas.xml", Encoding.UTF8 );
			doc.WriteStartDocument();

			doc.WriteStartElement( "listamedidas" );

			foreach(var m in this.listaMedidas) {
				doc.WriteStartElement( "medidas" );

				doc.WriteStartElement( "fecha" );
				doc.WriteString( m.Fecha.ToString() );
				doc.WriteEndElement();

				doc.WriteStartElement( "peso" );
				doc.WriteString( m.Peso.ToString() );
				doc.WriteEndElement();

				doc.WriteStartElement( "circunferenciaabdominal" );
				doc.WriteString( m.CircunferenciaAbdominal.ToString() );
				doc.WriteEndElement();

				doc.WriteEndElement();
			}

			doc.WriteEndElement();

			doc.WriteEndDocument();
			doc.Close();
		}

		public void Recuperar(){
			var docXml = new XmlDocument( );

			docXml.Load ("listamedidas.xml");
			foreach (XmlNode nodo in docXml.DocumentElement.ChildNodes) {
				if (nodo.Name == "medidas") {
					StringBuilder p=new StringBuilder();
					StringBuilder ca=new StringBuilder();
					StringBuilder f=new StringBuilder();
					foreach (XmlNode child in nodo.ChildNodes) {
						if (child.Name == "peso") {
							p.Append( child.InnerText);
						} else if (child.Name == "circunferenciaabdominal") {
							ca.Append(child.InnerText);
						} else if (child.Name == "fecha") {
							f.Append(child.InnerText);
						}
					}
					this.Add (new Medidas (float.Parse (p.ToString ()), float.Parse (ca.ToString ()), DateTime.Parse (f.ToString ())));
				}
			}
		}

		public override string ToString ()
		{
			StringBuilder toret = new StringBuilder();

			foreach (Medidas m in listaMedidas) {
				toret.Append (m.ToString ());
			}
			return toret.ToString();
		}
	}
}

