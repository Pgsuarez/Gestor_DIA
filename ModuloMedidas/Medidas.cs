using System;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ModuloMedidas
{
	public class Medidas
	{

		public Medidas(float p,float ca, DateTime f){
			this.Peso = p;
			this.CircunferenciaAbdominal = ca;
			this.Fecha = f;
		}

		public DateTime Fecha {
			get;
			set;
		}

		public float Peso {
			get;
			set;
		}

		public float CircunferenciaAbdominal {
			get;
			set;
		}

		public void Guardar(){
			var med = new XElement ("medidas",
				          new XElement ("peso", Peso),
				          new XElement ("circunferenciaabdominal", CircunferenciaAbdominal),
				          new XElement ("fecha", Fecha.ToString ()));
			med.Save ("medidas.xml");
		}

		public void Recuperar(){
			XElement med = XElement.Load ("medidas.xml");
			this.Peso = float.Parse (med.Element ("medidas").Element ("peso").ToString());
			this.CircunferenciaAbdominal = float.Parse (med.Element ("medidas").Element ("circunferenciaabdominal").ToString());
			this.Fecha = DateTime.Parse (med.Element ("medidas").Element ("fecha").ToString());
		}

		public override string ToString ()
		{
			return string.Format ("[Medidas: Fecha={0}, Peso={1}, CircunferenciaAbdominal={2}]\n", Fecha, Peso, CircunferenciaAbdominal);
		}
	}

}

