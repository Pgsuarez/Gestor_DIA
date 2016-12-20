using System;
using System.Collections.Generic;
using Gtk;
using ModuloCalendario.DataClasses;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class DayContent : Gtk.VBox
	{

		DayNotes dayNotesComponent;
		DayExercises dayExercisesComponent;
		Gtk.Label titleLabel;

		public DayContent() : base()
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

			this.dayNotesComponent = new DayNotes ();
			this.dayExercisesComponent = new DayExercises ();

			var nbLibro = new Gtk.Notebook();

			nbLibro.AppendPage(
				this.dayNotesComponent,
				new Gtk.Label( "Notes" )
			);

			nbLibro.AppendPage(
				this.dayExercisesComponent,
				new Gtk.Label( "Exercises" )
			);


			this.Add( nbLibro );

			//Update state and render
			this.OnViewBuilt();
		}

		private void ShowCurrentDay(string day, string month, string year){
			this.titleLabel.Text = String.Format ("{0}/{1}/{2}", day, month, year);
		}
	}
}
