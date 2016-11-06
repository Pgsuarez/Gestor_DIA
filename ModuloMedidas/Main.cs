using System;

namespace ModuloMedidas
{
	public class MainClass
	{
		public static void Main(String[] args){
			ListaMedidas lmed=new ListaMedidas();
			lmed.Add(new Medidas(5,13,DateTime.Now));
			lmed.Add(new Medidas(3,73,DateTime.Now));
			lmed.Add(new Medidas(6,23,DateTime.Now));
			lmed.Add(new Medidas(4,3,DateTime.Now));

			lmed.Guardar ();

			ListaMedidas lmed2 = new ListaMedidas ();
			lmed2.Recuperar ();

			Console.WriteLine (lmed2.ToString ());
		}
	}
}

