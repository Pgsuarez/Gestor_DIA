using System;
using Gtk;
using ProyectoDIA.Core;

namespace ProyectoDIA.IU
{
	public partial class MainWindow
	{
		private void LoadNotas()
		{
			store = new ListStore(
				typeof(string),
				typeof(string),
				typeof(DateTime)
			);

			diario.DiarioLoadToXml ();
			var notas = diario.N.N;
			foreach (var n in notas)
			{
				store.AppendValues(n.Titulo, n.Cuerpo, n.Fecha);
			}
		}

		void OnDialogResponse(object o, ResponseArgs args)
		{
			if (args.ResponseId.Equals(ResponseType.Accept))
			{
				var titulo =tituloEntry.Text;
				var cuerpo = cuerpoEntry.Text;
				var fecha = Convert.ToDateTime(fechaEntry.Text);

				var n = new Nota(diario.N.count,titulo, cuerpo, fecha);
				diario.N.anadir(n);
				store.AppendValues(n.Titulo, n.Cuerpo, n.Fecha);
			}
				
			if (args.ResponseId.Equals(ResponseType.Apply))
			{
				var titulo =tituloEntry.Text;
				var cuerpo = cuerpoEntry.Text;
				var fecha = Convert.ToDateTime(fechaEntry.Text);

				var n = diario.N.Get(selectedEx_Id);
				n.Titulo = titulo;
				n.Cuerpo = cuerpo;
				n.Fecha = fecha;
				diario.N.Update(n);

				TreeIter iter;
				TreeModel model;
				if (treeView.Selection.GetSelected(out model, out iter))
				{
					store.SetValue(iter, (int)Column.Titulo, n.Titulo);
					store.SetValue(iter, (int)Column.Cuerpo, n.Cuerpo);
					store.SetValue(iter, (int)Column.Fecha, n.Fecha);

					store.SetValue(iter, (int)Column.Id, n.Id);
				}
			}

			//ResponseType.Reject -> Delete exercise
			if (args.ResponseId.Equals(ResponseType.Reject))
			{
				TreeIter iter;
				TreeModel model;
				if (treeView.Selection.GetSelected(out model, out iter))
				{
					var id = (int) store.GetValue(iter, (int)Column.Id);
					diario.N.Borrar(id);
					store.Remove(ref iter);
				}
			}
		}

		Diario diario = new Diario(new Notas());

	}
}

