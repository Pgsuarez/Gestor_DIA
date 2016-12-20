using System;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class Toolbar : Gtk.Toolbar
	{
		public event EventHandler NewNoteClicked;
		public event EventHandler NewExerciseClicked;

		private void OnViewBuilt(){
			this.RefreshView ();
		}

		private void OnNewNoteClicked(object sender, EventArgs e){
			if (this.NewNoteClicked != null) {
				this.NewNoteClicked(sender, e);
			}
		}

		private void OnNewExerciseClicked(object sender, EventArgs e){
			if (this.NewExerciseClicked != null) {
				this.NewExerciseClicked (sender, e);
			}
		}
	}
}

