using System;
using System.Collections.Generic;
using DIO.Series.Interfaces;

namespace DIO.Series
{
	public class SerieRepositorio : IRepositorio<Serie>
	{
        private List<Serie> listaSerie = new List<Serie>();
		public void Atualiza(int id, Serie serie)
		{
			listaSerie[id] = serie;
		}

		public void Exclui(int id)
		{
			listaSerie.RemoveAt(id);
		}

		public void Insere(Serie serie)
		{
			listaSerie.Add(serie);
		}

		public List<Serie> Lista()
		{
			return listaSerie;
		}

		public Serie RetornaPorId(int id)
		{
			return listaSerie[id];
		}

		public int ProximoId()
		{
			return listaSerie.Count;
		}
	}
}