using System;
using Gtk;
using Charts;
using Charts.Data;
namespace DIAGraficos
{
	public class Ventana : Window
	{
		public Ventana(String title) 
			: base(title)
		{
			InitilizeComponent();

			Builder();

			ShowAll();
		}

		void InitilizeComponent()
		{
			lc = new LineChart("Grafico Molon");

			lc.AddData(new LineData(90, 1));
			lc.AddData(new LineData(70, 3));
			lc.AddData(new LineData(50, 15));
			lc.AddData(new LineData(40, 20));
			lc.AddData(new LineData(20, 25));


			lc.XResolution = 10;
			lc.YResolution = 10;

			lc.XLabel = "Peso";
			lc.YLabel = "Repeticiones";

			//lc = new LineChart("Grafico Molon 2");

			//lc.AddData(new LineData(90, 1), "Press Banca Barra");
			//lc.AddData(new LineData(70, 3), "Press Banca Barra");
			//lc.AddData(new LineData(50, 15), "Press Banca Barra");
			//lc.AddData(new LineData(40, 20), "Press Banca Barra");
			//lc.AddData(new LineData(20, 25), "Press Banca Barra");

			//lc.AddData(new LineData(90, 5), "Press Banca Manc.");
			//lc.AddData(new LineData(70, 7), "Press Banca Manc.");
			//lc.AddData(new LineData(50, 10), "Press Banca Manc.");
			//lc.AddData(new LineData(40, 15), "Press Banca Manc.");
			//lc.AddData(new LineData(44, 12), "Press Banca Manc.");
			//lc.AddData(new LineData(20, 20), "Press Banca Manc.");

			//lc.XResolution = 10;
			//lc.YResolution = 10;

			//lc.XLabel = "Peso";
			//lc.YLabel = "Repeticiones";

			//lb = new BarChart("Grafico Molon3");

			//lb.AddData(new BarData("Press Banca 1", 90), "Lunes");
			//lb.AddData(new BarData("Press Banca 2", 70), "Lunes");
			//lb.AddData(new BarData("Press Banca 3", 50), "Lunes");
			//lb.AddData(new BarData("Press Banca 4", 40), "Lunes");
			//lb.AddData(new BarData("Press Banca 5", 20), "Lunes");

			//lb.AddData(new BarData("Press Banca 1", 90), "Martes");
			//lb.AddData(new BarData("Press Banca 2", 70), "Martes");
			//lb.AddData(new BarData("Press Banca 3", 50), "Martes");
			//lb.AddData(new BarData("Press Banca 4", 40), "Martes");
			//lb.AddData(new BarData("Press Banca 5", 20), "Martes");


			//lb.ValueResolution = 10;

			//lb.ValueLabel = "Max Peso";

			//lb = new BarChart("Grafico Molon 5");

			//lb.AddData(new BarData("Press Banca 5", 90));
			//lb.AddData(new BarData("Press Banca 2", 70));
			//lb.AddData(new BarData("Press Banca 3", 50));
			//lb.AddData(new BarData("Press Banca 4", 40));
			//lb.AddData(new BarData("Press Banca 1", 20));


			//lb.ValueResolution = 10;

			//lb.ValueLabel = "Max Peso";

		}

		void Builder()
		{
			this.Add(lc);
			this.Add(lb);
		}

		private LineChart lc;
		private BarChart lb;
	}

}
