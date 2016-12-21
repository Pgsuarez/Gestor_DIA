using System;
using Gtk;

namespace ModuloMedidas.UserInterface
{
	public partial class MedidasWindow
	{
		private void RecuperarMedidas()
		{
			store = new ListStore(
				typeof(string),
				typeof(float),
				typeof(float)
			);

			lMed.Recuperar ();
			foreach (var m in lMed.getListaMedidas())
			{
				store.AppendValues(m.Fecha.ToString(),m.Peso,m.CircunferenciaAbdominal);
			}
		}

		void OnDialogResponse(object o, ResponseArgs args)
		{
			if (args.ResponseId.Equals(ResponseType.Accept))
			{
				var peso = pesoEntry.Text;
				var circunferenciaAbdominal = circunferenciaAbdominalEntry.Text;
				var fecha = fechaEntry.Date;

				var m = new Medidas(float.Parse(peso), float.Parse(circunferenciaAbdominal), fecha);
				lMed.Add (m);
				store.AppendValues(m.Fecha.ToString(),m.Peso,m.CircunferenciaAbdominal);
			}

			if (args.ResponseId.Equals(ResponseType.Apply))
			{
				TreeIter iter;
				TreeModel model;

				var fecha = fechaEntry;
				var peso =pesoEntry.Text;
				var circunferenciaAbdominal = circunferenciaAbdominalEntry.Text;

				if (treeView.Selection.GetSelected(out model, out iter))
				{
					var selectedMed_Fecha = (string)model.GetValue(iter, (int)Column.Fecha);
					var m = new Medidas(float.Parse(peso), float.Parse(circunferenciaAbdominal), fecha.Date);

					lMed.Borrar (selectedMed_Fecha);
					lMed.Add (m);

					store.Remove(ref iter);
					store.AppendValues(m.Fecha.ToString(),m.Peso,m.CircunferenciaAbdominal);


				}
			}

			//ResponseType.Reject -> Delete exercise
			if (args.ResponseId.Equals(ResponseType.Reject))
			{
				TreeIter iter;
				TreeModel model;
				if (treeView.Selection.GetSelected(out model, out iter))
				{
					var f = (string) store.GetValue(iter, (int)Column.Fecha);
					lMed.Borrar (f);
					store.Remove(ref iter);
				}
			}
		}

		private void OnClose(object sender,EventArgs e) {
			lMed.Guardar ();
			Gtk.Application.Quit();
		}

		 ListaMedidas lMed = new ListaMedidas ();

	}
}