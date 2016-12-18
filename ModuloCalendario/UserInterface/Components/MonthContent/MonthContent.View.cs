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

		public MonthContent() : base()
		{
			//Init view's state
			this.InitModel();

			//Build
			this.Build();
		}

		private void Build()
		{
			this.monthNotesComponent = new MonthNotes ();
			this.monthExercisesComponent = new MonthExercises ();

			Gtk.VBox vbox = new Gtk.VBox ();

			vbox.PackStart (this.monthNotesComponent);
			vbox.PackStart (this.monthExercisesComponent);

			this.PackStart (vbox);

			//Update state and render
			this.OnViewBuilt();
		}
	}
}
