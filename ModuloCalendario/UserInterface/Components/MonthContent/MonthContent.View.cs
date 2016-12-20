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

			var nbLibro = new Gtk.Notebook();

			nbLibro.AppendPage(
				this.monthNotesComponent,
				new Gtk.Label( "Notes" )
			);

			nbLibro.AppendPage(
				this.monthExercisesComponent,
				new Gtk.Label( "Exercises" )
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
