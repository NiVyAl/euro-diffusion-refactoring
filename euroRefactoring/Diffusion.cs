using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace euroRefactoring
{
	public class Diffusion
	{
		int NumberOfCountry; // number of countries in each case
		int CaseNumber = 1;
		List<City> Cities = new List<City>(); // все города
		Country[] Countries;

		public void Parse(string filename)
		{
			try
			{
				using (StreamReader sr = new StreamReader(filename, System.Text.Encoding.Default))
				{
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine();
					Console.WriteLine("Start............");
					string line;
					int lineNumber = 1;
					while ((line = sr.ReadLine()) != null)
					{
						if (!TryParseNumberOfCountry(line, lineNumber))
							break;

						/* Initialize countries */
						Cities.Clear();
						Countries = new Country[NumberOfCountry];
						for (int i = 0; i < NumberOfCountry; i++)
						{
							lineNumber++;
							Countries[i] = new Country(i, lineNumber, Cities, NumberOfCountry, CaseNumber);
							Countries[i].Parse(sr.ReadLine());
						}
						checkCorrectData();
						/* */

						if (NumberOfCountry > 1) // calculation starts if the number of countries is > 1 (if only 1 country, it complete in 0 days)
							Calculate();

						PrintOutput();
						lineNumber++;
						CaseNumber++;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		void Calculate()
		{
			/* Define neighbors to each city */
			for (int i = 0; i < Cities.Count; i++)
			{
				for (int j = 0; j < Cities.Count; j++)
				{
					if ((i != j) && (Cities[i].X == Cities[j].X) && (Cities[i].Y == Cities[j].Y))
						throw new Exception($"Wrong coordinates (country stay at another country), case number: {CaseNumber}, countries: {Countries[Cities[i].CountryIndex].CountryName}, {Countries[Cities[j].CountryIndex].CountryName}");

					if (!Cities[i].Neighbors.Contains(Cities[j]))
					{
						if ((((Cities[i].X == Cities[j].X + 1) || (Cities[i].X == Cities[j].X - 1)) && (Cities[i].Y == Cities[j].Y)) || (((Cities[i].Y == Cities[j].Y + 1) || (Cities[i].Y == Cities[j].Y - 1)) && (Cities[i].X == Cities[j].X))) {
							Cities[i].Neighbors.Add(Cities[j]);
							Cities[j].Neighbors.Add(Cities[i]);
						}
					}
				}
			}
			/* */

			int days = 1;
			int numberUncompleteCountries = NumberOfCountry;

			while (numberUncompleteCountries > 0)
			{
				Cities.ForEach(a => a.CalculatePortion());
				Cities.ForEach(a => a.DayPassed());

				for (int i = 0; i < NumberOfCountry; i++)
				{
					if (!Countries[i].IsComplete)
					{
						Countries[i].CheckIsComplete();
						if (Countries[i].IsComplete)
						{
							Countries[i].Days = days;
							numberUncompleteCountries--;
						}
					}
				}
				days++;
			}
		}

		void PrintOutput()
		{
			Console.WriteLine($"Case Number {CaseNumber}");

			var q =
				from t in Countries
				orderby t
				select t;

			var result = q.ToList();
			foreach (Country i in result)
			{
				Console.WriteLine($"	{i.CountryName}: {i.Days}");
			}
		}

		bool TryParseNumberOfCountry(string line, int lineNumber)
		{
			NumberOfCountry = 0;

			bool isCanParse = int.TryParse(line, out NumberOfCountry);
			if (!isCanParse)
				throw new Exception($"Can't parse number of country, line: {lineNumber}");
			if (NumberOfCountry == 0)
				return false;
			if ((NumberOfCountry >=20) || (NumberOfCountry < 0))
				throw new Exception($"number of countries must be (1 < NumberOfCountry < 20), caseNumber: {CaseNumber}");

			return true;
		}

		/// <summary>
		///		check is countries border each other
		/// </summary>
		private void checkCorrectData()
		{
			/* define the neighbors of each country */
			for (int i = 0; i < NumberOfCountry; i++)
			{
				for (int j = 0; j < NumberOfCountry; j++)
				{
					if (Countries[i].Neighbors[j] == true || i == j)
						continue;

					for (int m = 0; m < 2; m++)
					{
						if (Countries[i].Coordinates[m] - 1 == Countries[j].Coordinates[m + 2] || Countries[i].Coordinates[m + 2] + 1 == Countries[j].Coordinates[m]) // check right/left neighbors
						{
							int n = m;
							if (m == 1)
								n = m - 2;
							if ((Countries[j].Coordinates[n + 1] >= Countries[i].Coordinates[n + 1] && Countries[j].Coordinates[n + 1] <= Countries[i].Coordinates[n + 3]) || (Countries[j].Coordinates[n + 3] >= Countries[i].Coordinates[n + 1] && Countries[j].Coordinates[n + 3] <= Countries[i].Coordinates[n + 3])) // check top/bottom neighbors
							{
								Countries[i].Neighbors[j] = true;
								Countries[j].Neighbors[i] = true;
								break;
							}
						}
					}
				}
			}
			/* */

			/* define if all countries are connected to each other */
			bool[] correctCountries = new bool[NumberOfCountry];
			bool[] queueCheck = new bool[NumberOfCountry];
			queueCheck[0] = true;

			bool isCanRun = true;
			while (isCanRun)
			{
				isCanRun = false;
				for (int k = 0; k < NumberOfCountry; k++)
				{
					if (queueCheck[k] == true && correctCountries[k] == false)
					{
						isCanRun = true;
						queueCheck[k] = false;
						correctCountries[k] = true;

						for (int j = 0; j < NumberOfCountry; j++)
						{
							if (Countries[k].Neighbors[j])
							{
								queueCheck[j] = true;
							}
						}
					}
				}
			}

			for (int k = 0; k < NumberOfCountry; k++)
			{
				if (correctCountries[k] == false)
					throw new Exception($"Wrong coordinates, cities don't border (caseNumber: {CaseNumber}, country: {Countries[k].CountryName})");
			}
			/* */
		}
	}
}