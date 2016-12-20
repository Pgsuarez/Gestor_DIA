using System;
using System.Collections.Generic;
using Gtk;
using ModuloCalendario.DataClasses;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MonthContent : Gtk.VBox
	{

		MonthNotes monthNotesComponent;
		MonthExercises monthExercisesComponent;
		MonthMeasures monthMeasuresComponent;

		MonthExercisesGraph monthExercisesComponentG;
		MonthMeasuresGraph monthMeasuresComponentG;

		Gtk.Label titleLabel;

		public MonthContent() : base()
		{
			//Init view's state
			this.InitModel();

			//Build
			this.Build();
		}

		private void Build()
		{
			this.titleLabel = new Label ("");
			var fontDescription = new Pango.FontDescription ();
			fontDescription.Size = Convert.ToInt32(17 * Pango.Scale.PangoScale);
			this.titleLabel.ModifyFont (fontDescription);
			this.PackStart (this.titleLabel, false, true, 10);

			this.monthNotesComponent = new MonthNotes ();
			this.monthExercisesComponent = new MonthExercises ();
			this.monthMeasuresComponent = new MonthMeasures();

			this.monthExercisesComponentG = new MonthExercisesGraph();
			this.monthMeasuresComponentG = new MonthMeasuresGraph();

			var nbLibro = new Gtk.Notebook();
			var nbEjercicio = new Gtk.Notebook();
			var nbMedidas = new Gtk.Notebook();

			nbEjercicio.AppendPage(
				this.monthExercisesComponent,
				new Gtk.Label("List")
			);
			nbEjercicio.AppendPage(
				this.monthExercisesComponentG,
				new Gtk.Label("Graphic")
			);


			nbMedidas.AppendPage(
				this.monthMeasuresComponent,
				new Gtk.Label("List")
			);
			nbMedidas.AppendPage(
				this.monthMeasuresComponentG,
				new Gtk.Label("Graphic")
			);

			nbLibro.AppendPage(
				this.monthNotesComponent,
				new Gtk.Label( "Notes" )
			);

			VBox ex = new VBox();
			ex.Add(nbEjercicio);
			nbLibro.AppendPage(
				ex,
				new Gtk.Label( "Exercises" )
			);

			VBox ey = new VBox();
			ey.Add(nbMedidas);
			nbLibro.AppendPage(
				ey,
				new Gtk.Label("Measures")
			);


			this.Add( nbLibro );

			//Update state and render
			this.OnViewBuilt();
		}

		private void ShowCurrentMonth(string month, string year){
			this.titleLabel.Text = String.Format ("{0}/{1}", month, year);
		}
	}
}
