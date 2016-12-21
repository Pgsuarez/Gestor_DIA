using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace ModuloMedidas
{
	public class ListaMedidas
	{
		private List<Medidas> listaMedidas;

		public ListaMedidas ()
		{
			listaMedidas = new List<Medidas>();
			this.Recuperar();
		}

		public List<Medidas> getListaMedidas(){
			return this.listaMedidas;
		}

		public void setListaMedidas(List<Medidas> lm){
			this.listaMedidas = lm;
			Guardar();
		}

		public void Add(Medidas m){
			this.listaMedidas.Add (m);
			Guardar();
		}

		public void Guardar(){
			var doc = new XmlTextWriter( "listamedidas.xml", Encoding.UTF8 );
			doc.WriteStartDocument();

			doc.WriteStartElement( "listamedidas" );

			foreach(var m in this.listaMedidas) {
				doc.WriteStartElement( "medidas" );

				doc.WriteStartElement("id");
				doc.WriteString(m.Id.ToString());
				doc.WriteEndElement();
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

			try
			{
				docXml.Load("listamedidas.xml");
				foreach (XmlNode nodo in docXml.DocumentElement.ChildNodes)
				{
					if (nodo.Name == "medidas")
					{
						StringBuilder p = new StringBuilder();
						StringBuilder ca = new StringBuilder();
						StringBuilder f = new StringBuilder();
						StringBuilder id = new StringBuilder();

						foreach (XmlNode child in nodo.ChildNodes)
						{
							if (child.Name == "peso")
							{
								p.Append(child.InnerText);
							}
							else if (child.Name == "circunferenciaabdominal")
							{
								ca.Append(child.InnerText);
							}
							else if (child.Name == "fecha")
							{
								f.Append(child.InnerText);
							}
							else if (child.Name == "id")
							{
								id.Append(child.InnerText);
							}
						}
						this.Add(new Medidas(int.Parse(id.ToString()), float.Parse(p.ToString()), float.Parse(ca.ToString()), DateTime.Parse(f.ToString())));
					}
				}

			}
			catch (FileNotFoundException e)
			{
			}
		}

		public void Remove(int id)
		{
			this.listaMedidas.RemoveAt(id);
			this.Guardar();
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

