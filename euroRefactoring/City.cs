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
		public bool IsComplete
		{
			get
			{
				return (countUncompleteCountries == 0);
			}
		}

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
			countUncompleteCountries = Diffusion.NumberOfCountry-1;
		}

		public void DayPassed()
		{
			CalculatePortion();
			for (int i = 0; i < Neighbors.Count; i++)
			{
				Neighbors[i].Transfer(Portion);
			}
		}

		public void CalculatePortion() {
			Console.WriteLine($"City X: {x}, Y: {y}");
			for (int i = 0; i < Diffusion.NumberOfCountry; i++)
			{
				Portion[i] = (int)(Balance[i] * _portionCoins);
				//if (Balance[i] > 0)
				//	Console.WriteLine(Balance[i]);
			}

			for (int i = 0; i < Diffusion.NumberOfCountry; i++)
			{
				Console.WriteLine(Portion[i]);
			}
			Console.WriteLine();
		}

		public void Transfer(int[] takenCoins) {
			for (int i = 0; i < Diffusion.NumberOfCountry; i++)
			{
				int startDayCoins = Portion[i];
				Portion[i] -= Portion[i]; // given coins are deducted
				Portion[i] += takenCoins[i]; // take coins
				if (startDayCoins == 0 && takenCoins[i] > 0) // if there were 0 coins and there are more
				{
					countUncompleteCountries--; // the number of un complete cities decreases
				}
			}
		}
	}
}
