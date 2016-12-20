using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ModuloEjercicio.API;
using ModuloEjercicio.Persistence.API;

namespace ModuloEjercicio.Persistence.Default
{
	//Sealed indicates that this class cannot be extended (like final in Java)
	//This class is marked sealed in order to indicate that the virtual method Read is implemented
	//and can be used on the constructor
	public sealed partial class DefaultExercisePersistService: ExercisePersistService
	{
		private Dictionary<int, Exercise> Read()
		{
			var toret = new Dictionary<Int32, Exercise>();
			List<Exercise> list = this.ReadLinqXML();
			foreach (Exercise ex in list)
			{
				toret.Add(ex.Id, ex);
			}
			return toret;
		}

		private void Write(Dictionary<int, Exercise> Exercises)
		{
			var list = new List<Exercise>();
			list.AddRange(Exercises.Values);
			this.WriteLinqXML(list);
		}


		private void WriteXMLParser(List<Exercise> Exercises)
		{
			XmlTextWriter textWriter = new XmlTextWriter("exercises.xml", Encoding.UTF8);
			textWriter.WriteStartDocument();
			textWriter.WriteStartElement("Exercises");

			foreach (Exercise Ex in Exercises)
			{
				textWriter.WriteStartElement("Exercise");

				textWriter.WriteStartAttribute("Id");
				textWriter.WriteValue(Ex.Id);
				textWriter.WriteEndAttribute();

				textWriter.WriteStartElement("Distance");

				textWriter.WriteValue(Ex.Distance);

				textWriter.WriteEndElement();
				textWriter.WriteStartElement("Minutes");

                textWriter.WriteValue(Ex.Minutes);

                textWriter.WriteEndElement();
                textWriter.WriteStartElement("Date");

                textWriter.WriteValue(Ex.Date.ToString());

                textWriter.WriteEndElement();

				textWriter.WriteEndElement();
			}

			textWriter.WriteEndElement();

			textWriter.Close();
		}

		private void WriteLinqXML(List<Exercise> Exercises)
		{
			var raiz = new XElement("Exercises");
			foreach (Exercise Ex in Exercises)
			{
				var Exercise = new XElement("Exercise",
                                    new XElement("Distance", Ex.Distance),
		                            new XElement("Minutes", Ex.Minutes),
                                    new XElement("Date", Ex.Date.ToString()));
				Exercise.Add(new XAttribute("Id", Ex.Id));
				raiz.Add(Exercise);
			}
			raiz.Save("exercises.xml");
		}

		private List<Exercise> ReadXMLDOM()
		{
			var ExList = new List<Exercise>();

			XmlDocument docXml = new XmlDocument();
			docXml.Load("exercises.xml");

			foreach (XmlNode Node in docXml.DocumentElement.ChildNodes)
			{
				Exercise Ex = null;
				Int32 Id = default(Int32);
                Int32 Distance = default(Int32);
				Int32 Minutes = default(Int32);
                DateTime Date = default(DateTime);

				foreach (XmlNode Child in Node.ChildNodes)
				{
					switch (Child.Name)
					{
						case "Distance":
							Distance = Convert.ToInt32(Child.InnerText);
							break;
						case "Minutes":
		                    Minutes = Convert.ToInt32(Child.InnerText);
							break;
                        case "Date":
                            Date = Convert.ToDateTime(Child.InnerText);
                            break;
					}
					Id = Convert.ToInt32(Child.Attributes.GetNamedItem("Id").InnerText);
				}
                Ex = Exercise.createExerciseWithId(Id, Distance, Minutes, Date);

				ExList.Add(Ex);
			}

			return ExList;
		}

		private List<Exercise> ReadLinqXML()
		{
			var ExList = new List<Exercise>();
			XElement raiz = XElement.Load("exercises.xml");
			var Exercises = from Ex in raiz.Elements("Exercise")
							 select new
							 {
								 Id = Convert.ToInt32(Ex.Attribute("Id").Value),
								 Distance = Convert.ToInt32(Ex.Element("Distance").Value),
                                 Minutes = Convert.ToInt32(Ex.Element("Minutes").Value),
                                 Date = Convert.ToDateTime(Ex.Element("Date").Value)
							 };
			foreach (var Ex in Exercises)
			{
				ExList.Add(Exercise.createExerciseWithId(Ex.Id, Ex.Distance, Ex.Minutes, Ex.Date));
			}
			return ExList;
		}
	}
}
