using System;
using ProyectoDIA.Core;
using System.Collections.Generic;

namespace ProyectoDIA.IU
{
	public class IU
	{
		public static void MostrarLista<T>(List<T> Lista, String Nombre)
		{
			int i = 0;
			Console.WriteLine("------------Lista de {0}------------", Nombre);
			foreach (T elem in Lista)
			{
				Console.WriteLine(i++ + ": " + elem);
			}
			Console.WriteLine("--------------------------------------");
			Console.WriteLine();
		}

		public static Int16 Menu()
		{
			Int16 op;
			do
			{
				Console.WriteLine("Bienvenido a la gestión de ejercicios");
				Console.WriteLine("¿Qué quiere hacer?");
				Console.WriteLine("1: Añadir ejercicio");
				Console.WriteLine("2: Eliminar ejercicio");
				Console.WriteLine("3: Modificar ejercicio");
				Console.WriteLine("4: Salir");
				op = Convert.ToInt16(Console.ReadLine());
				if (op < 0 || op > 4)
					Console.WriteLine("Número incorrecto");
			} while (op < 0 || op > 4);
			return op;
		}

	


		public static Int32 LeerNumEjercicio()
		{
			Console.WriteLine("Inserte el nº del ejercicio");
			return Convert.ToInt32(Console.ReadLine());
		}
	}
}