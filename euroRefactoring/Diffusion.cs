using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace euroRefactoring
{
	public class Diffusion
	{
		Country[] Countries;

		public int NumberOfCountry; // number of countries in each case
		public int CaseNumber = 1;

		public List<City> Cities = new List<City>(); // сдесь все города

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
						Cities.Clear();
						/* получаю количество стран */
						if (!ParseNumberOfCountry(line, lineNumber))
							break;
						/* */

						/* инициализирую каждую страну */
						Countries = new Country[NumberOfCountry];
						for (int i = 0; i < NumberOfCountry; i++)
						{
							lineNumber++;
							Countries[i] = new Country(i, lineNumber, Cities, NumberOfCountry, CaseNumber);
							Countries[i].Parse(sr.ReadLine()); // читаем строку и сразу ее передаем на парсинг
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
			/* определяем соседей каждого города */
			for (int i = 0; i < Cities.Count; i++)
			{
				for (int j = 0; j < Cities.Count; j++)
				{
					if ((i != j) && (Cities[i].X == Cities[j].X) && (Cities[i].Y == Cities[j].Y))
						throw new Exception($"Wrong coordinates (country stay at another country), case number: {CaseNumber}, countries: {Countries[Cities[i].countryIndex].CountryName}, {Countries[Cities[j].countryIndex].CountryName}");

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
				for (int i = 0; i < Cities.Count; i++)
				{
					Cities[i].CalculatePortion();
				}
				for (int i = 0; i < Cities.Count; i++)
				{
					Cities[i].DayPassed();
				}

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



		/* мои методы (удалить либо хз)*/

		bool ParseNumberOfCountry(string line, int lineNumber)
		{
			NumberOfCountry = 0;

			/* parse number of country */
			bool isCanParse = int.TryParse(line, out NumberOfCountry);
			if (!isCanParse)
			{
				Console.WriteLine($"Can't parse number of country, line: {lineNumber}");
				return false;
			}
			if (NumberOfCountry == 0)
				return false;
			if ((NumberOfCountry >=20) || (NumberOfCountry < 0))
				throw new Exception($"number of countries must be (1 < NumberOfCountry < 20), caseNumber: {CaseNumber}");
			return true;
			/* */
		}

		/* */

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