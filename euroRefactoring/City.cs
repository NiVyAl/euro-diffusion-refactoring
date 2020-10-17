using System;
using System.Collections.Generic;

namespace euroRefactoring
{
	public class City
	{
		const int InitiallyCountCoins = 1000000;
		const double PortionCoins = 0.001;
		int NumberOfCountry;
		int CountUncompleteCountries;
		int[] Balance;
		int[] Portion;

		public int X, Y;
		public List<City> Neighbors = new List<City>(); // ???может лучше массив из 4 элементов???
		public int CountryIndex;
		public bool IsComplete
		{
			get
			{
				return (CountUncompleteCountries == 0);
			}
		}

		public City(int x, int y, int countryIndex, int numberOfCountry)
		{
			X = x;
			Y = y;
			CountryIndex = countryIndex;
			NumberOfCountry = numberOfCountry;
			Balance = new int[NumberOfCountry];
			Portion = new int[NumberOfCountry];

			for (int i = 0; i < NumberOfCountry; i++)
			{
				Balance[i] = 0;
				Portion[i] = 0;
			}
			Balance[CountryIndex] = InitiallyCountCoins;
			CountUncompleteCountries = NumberOfCountry-1;
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
				Portion[i] = (int)(Balance[i] * PortionCoins);
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
					CountUncompleteCountries--; // the number of un complete cities decreases
				}
			}
		}
	}
}