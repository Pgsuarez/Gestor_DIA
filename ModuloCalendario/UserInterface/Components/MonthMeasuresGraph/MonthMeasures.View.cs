using System;
using System.Collections.Generic;
using Gtk;
using Charts;
using Charts.Data;
using ModuloCalendario.DataClasses;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MonthMeasuresGraph : Gtk.VBox
	{
		

		LineChart lc;
		static GLib.SList group = null;
		private RadioButton rbWeight;
		private RadioButton rbAC;




		public MonthMeasuresGraph() : base()
		{
			//Init view's state
			this.InitModel();

			//Build
			Build();
		}

		private void Build()
		{
			var mainVox = new Gtk.VBox();

			//List
			this.lc = new LineChart("Weigth");
			lc.XLabel = "Day";

			lc.YLabel = "Kg";

			lc.MinXValue = 1;
			lc.MaxXValue = 30;
			lc.XResolution = 1;




			rbWeight = new RadioButton(null, "Weight");
			group = rbWeight.Group;
			rbWeight.Active = true;
			rbAC = new RadioButton(rbWeight, "Abc. Circ.");
			rbWeight.Clicked += (sender, e) => ShowWeightGraphic();
			rbAC.Clicked += (sender, e) => ShowACGraphic();

			HBox cont = new HBox();
			cont.PackStart(rbWeight);
			cont.Add(rbAC);

			mainVox.PackStart(cont, false, false, 0);
			mainVox.Add(lc);


			//Wrap
			PackStart(mainVox, true, true, 0);

			//Update state and render
			this.OnViewBuilt();
		}



		private void ClearMeasures(){
			this.lc.Clear();
		}

		private void ChangeToWeight()
		{
			lc.Title = "Weight";
			lc.XLabel = "Day";

			lc.YLabel = "Kg";

			lc.MinXValue = 1;
			lc.MaxXValue = 30;
			lc.XResolution = 1;

		}

		private void ChangeToAC()
		{
			lc.Title = "Abc. Circ.";
			lc.XLabel = "Day";

			lc.YLabel = "cm";

			lc.MinXValue = 1;
			lc.MaxXValue = 30;
			lc.XResolution = 1;
		}

		private void ShowMeasure(int index, int w, int ac, int day){
			if (weightGraph)
			{
				Console.WriteLine("peso");
				this.lc.AddData(new LineData(w, day), "Weight");
			}
			else
			{
				Console.WriteLine("abc");
				this.lc.AddData(new LineData(ac, day), "Abd. Circ.");
			}
		}
	}
}
