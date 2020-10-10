using System;
using System.Collections.Generic;

namespace euroRefactoring
{
	public class City
	{
		public int x, y;
		public List<City> Neighbors = new List<City>();
		int[] Balance;
		int[] Portion;
		bool IsComplete;

		private int countUncompleteCountries;
		private int countryIndex;
		private const int _initiallyCountCoins = 1000000;
		private const double _portionCoins = 0.001;

		public City(int x, int y, int countryIndex)
		{
			this.x = x;
			this.y = y;
			this.countryIndex = countryIndex;
			Balance = new int[Diffusion.NumberOfCountry];
			Portion = new int[Diffusion.NumberOfCountry];
			City[] neighbours = new City[4];

			for (int i = 0; i < Diffusion.NumberOfCountry; i++)
			{
				Balance[i] = 0;
				Portion[i] = 0;
			}
			Balance[countryIndex] = _initiallyCountCoins;
		}

		void CalculatePortion() {

		}

		void Transfer() {

		}
	}
}
