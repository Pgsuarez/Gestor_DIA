using System;

namespace ModuloCalendario.UserInterface.Components
{
	public interface IComponent
	{
		void RefreshView();
		void OnViewBuilt();
	}
}

