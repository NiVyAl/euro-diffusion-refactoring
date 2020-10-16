using System;
using System.Collections.Generic;

namespace euroRefactoring
{
	public class City
	{
		public int X, Y;
		public List<City> Neighbors = new List<City>();
		int[] Balance;
		int[] Portion;
		public bool IsComplete
		{
			get
			{
				return (countUncompleteCountries == 0);
			}
		}

		private int countUncompleteCountries;
		public int countryIndex;
		private const int _initiallyCountCoins = 1000000;
		private const double _portionCoins = 0.001;
		int NumberOfCountry;

		public City(int x, int y, int countryIndex, int numberOfCountry)
		{
			this.X = x;
			this.Y = y;
			this.countryIndex = countryIndex;
			NumberOfCountry = numberOfCountry;
			Balance = new int[NumberOfCountry];
			Portion = new int[NumberOfCountry];
			City[] neighbours = new City[4];

			for (int i = 0; i < NumberOfCountry; i++)
			{
				Balance[i] = 0;
				Portion[i] = 0;
			}
			Balance[countryIndex] = _initiallyCountCoins;
			countUncompleteCountries = NumberOfCountry-1;
		}

		public void DayPassed()
		{
			for (int i = 0; i < Neighbors.Count; i++)
			{
				Neighbors[i].Transfer(Portion);
			}
		}

		public void CalculatePortion() {
			for (int i = 0; i < NumberOfCountry; i++)
			{
				Portion[i] = (int)(Balance[i] * _portionCoins);
			}
		}

		public void Transfer(int[] takenCoins) {
			for (int i = 0; i < NumberOfCountry; i++)
			{
				int startDayCoins = Balance[i];
				Balance[i] -= Portion[i]; // given coins are deducted
				Balance[i] += takenCoins[i]; // take coins
				if (startDayCoins == 0 && takenCoins[i] > 0) // if there were 0 coins and there are more
				{
					countUncompleteCountries--; // the number of un complete cities decreases
				}
			}
		}
	}
}