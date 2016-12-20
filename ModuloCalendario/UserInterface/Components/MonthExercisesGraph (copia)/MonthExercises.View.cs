using System;
using System.Collections.Generic;
using Gtk;
using Charts;
using Charts.Data;
using ModuloCalendario.DataClasses;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MonthExercisesGraph : Gtk.VBox
	{
		

		BarChart lc;

		public MonthExercisesGraph() : base()
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
			this.lc = new BarChart("Exercices");
			lc.ValueLabel = "";


			ScrolledWindow sw = new ScrolledWindow();
			sw.ShadowType = ShadowType.EtchedIn;
			sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
			//sw.Add(lc);

			mainVox.PackStart(lc, true, true, 0);


			//Wrap
			PackStart(mainVox, true, true, 0);

			//Update state and render
			this.OnViewBuilt();
		}

		private void ClearExercises(){
			this.lc.Clear();
		}

		private void ShowExercise(int index, int distance, int minutes, string day){
			this.lc.AddData(new BarData("Distance", distance),day);
			this.lc.AddData(new BarData("Duration", minutes),day);
		}
	}
}
