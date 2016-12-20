using System;
using System.Collections.Generic;
using Gtk;
using ModuloEjercicio.API;

namespace ModuloEjercicio.App
{
	public partial class MainWindow
	{
		private void LoadExercises()
		{
			store = new ListStore(
				typeof(int),
				typeof(int),
				typeof(int)
			);

			var exs = exerciseService.FindAll();
			foreach (var ex in exs)
			{
				store.AppendValues(ex.Distance, ex.Minutes, ex.Id);
			}
		}

		void OnDialogResponse(object o, ResponseArgs args)
		{/*
			//ResponseType.Accept -> Add new exercise
			if (args.ResponseId.Equals(ResponseType.Accept))
			{
				var distance = Convert.ToInt32(distanceEntry.Text);
				var minutes = Convert.ToInt32(minutesEntry.Text);
				var ex = new Exercise(distance, minutes);
				exerciseService.Add(ex);
				store.AppendValues(ex.Distance, ex.Minutes, ex.Id);
			}

			//ResponseType.Apply -> Edit exercise
			if (args.ResponseId.Equals(ResponseType.Apply))
			{
				var distance = Convert.ToInt32(distanceEntry.Text);
				var minutes = Convert.ToInt32(minutesEntry.Text);

				var ex = exerciseService.Get(selectedEx_Id);
				ex.Distance = distance;
				ex.Minutes = minutes;
				exerciseService.Update(ex);

				TreeIter iter;
				TreeModel model;
				if (treeView.Selection.GetSelected(out model, out iter))
				{
					store.SetValue(iter, (int)Column.Distance, ex.Distance);
					store.SetValue(iter, (int)Column.Minutes, ex.Minutes);
					store.SetValue(iter, (int)Column.Id, ex.Id);
				}
			}
            */
			//ResponseType.Reject -> Delete exercise
			if (args.ResponseId.Equals(ResponseType.Reject))
			{
				TreeIter iter;
				TreeModel model;
				if (treeView.Selection.GetSelected(out model, out iter))
				{
					var id = (int) store.GetValue(iter, (int)Column.Id);
					exerciseService.Delete(id);
					store.Remove(ref iter);
				}
			}
		}

        void onAddButtonClicked()
        {
            var dialog = new ExerciseDialog("Add exercise", this);
            Exercise ex = dialog.getResult();
            if (ex != null)
            {
                exerciseService.Add(ex);
                store.AppendValues(ex.Distance, ex.Minutes, ex.Id);
            }
        }

        void onEditButtonClicked()
        {
            Exercise ex = exerciseService.Get(1);
            var dialog = new ExerciseDialog("Edit exercise", this, ex);
            ex = dialog.getResult();
            exerciseService.Update(ex);

            TreeIter iter;
            TreeModel model;
            if (treeView.Selection.GetSelected(out model, out iter))
            {
                store.SetValue(iter, (int)Column.Distance, ex.Distance);
                store.SetValue(iter, (int)Column.Minutes, ex.Minutes);
                store.SetValue(iter, (int)Column.Id, ex.Id);
            }
        }

		/*
		void OnRowActivated(object sender, RowActivatedArgs args)
		{

			TreeIter iter;
			TreeView view = (TreeView)sender;

			if (view.Model.GetIter(out iter, args.Path))
			{
				string row = view.Model.GetValue(iter, (int)Column.Distance).ToString();
				row += ", " + view.Model.GetValue(iter, (int)Column.Minutes);
				statusbar.Push(0, row);
			}
		}*/

		ExerciseService exerciseService = ExerciseService.getInstance();
	}
}