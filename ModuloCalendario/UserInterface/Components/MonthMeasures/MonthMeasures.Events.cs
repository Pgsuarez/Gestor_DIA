using System;
using Gtk;

namespace ModuloCalendario.UserInterface.Components
{
	public partial class MonthMeasures : Gtk.VBox
	{
		private void OnViewBuilt(){
			this.RefreshView ();
		}

		//copy after this
		private void OnClickRemove(object o, EventArgs args){
			TreeIter iter;
			TreeModel model;

			if (this.notesTreeView.Selection.GetSelected (out model, out iter)) {
				int selectedIndex = (int)this.measurementsListStore.GetValue (iter, (int)MonthMeasures.Columns.Index);
				this.Remove (selectedIndex);
			}
		}

		private void OnClickEdit(object o, EventArgs args){
			TreeIter iter;
			TreeModel model;

			if (this.notesTreeView.Selection.GetSelected (out model, out iter)) {
				int selectedIndex = (int)this.measurementsListStore.GetValue (iter, (int)MonthMeasures.Columns.Index);
				this.Edit (selectedIndex);
			}
		}
	}
}
