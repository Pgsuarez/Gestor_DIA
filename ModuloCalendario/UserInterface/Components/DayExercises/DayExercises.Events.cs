﻿using System;
using Gtk;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class DayExercises : Gtk.VBox
	{
		private void OnViewBuilt(){
			this.RefreshView ();
		}

		//copy after this
		private void OnClickRemove(object o, EventArgs args){
			TreeIter iter;
			TreeModel model;

			if (this.notesTreeView.Selection.GetSelected (out model, out iter)) {
				int selectedIndex = (int)this.exercisesListStore.GetValue (iter, (int)DayExercises.Columns.Index);
				this.Remove (selectedIndex);
			}
		}

		private void OnClickEdit(object o, EventArgs args){
			TreeIter iter;
			TreeModel model;

			if (this.notesTreeView.Selection.GetSelected (out model, out iter)) {
				int selectedIndex = (int)this.exercisesListStore.GetValue (iter, (int)DayExercises.Columns.Index);
				this.Edit (selectedIndex);
			}
		}
	}
}
