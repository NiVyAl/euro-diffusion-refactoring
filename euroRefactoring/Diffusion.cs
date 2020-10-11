using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace euroRefactoring
{
	public class Diffusion
	{
		Country[] _countries;
		bool _IsComplete;

		public static int NumberOfCountry; // number of countries in each case
		public static int CaseNumber = 1;

		public static List<City> Cities = new List<City>(); // сдесь все города

		public void Parse(string filename)
		{
			try
			{
				using (StreamReader sr = new StreamReader(filename, System.Text.Encoding.Default))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						Cities.Clear();
						/* получаю количество стран */
						bool isCanParse = ParseNumberOfCountry(line);
						if (!isCanParse)
							break;
						/* */

						/* инициализирую каждую страну */
						_countries = new Country[NumberOfCountry];
						for (int i = 0; i < NumberOfCountry; i++)
						{
							_countries[i] = new Country(i);
							_countries[i].Parse(sr.ReadLine()); // читаем строку и сразу ее передаем на парсинг
						}
						/* */

						if (NumberOfCountry > 1) // calculation starts if the number of countries is > 1 (if only 1 country, it complete in 0 days)
							Calculate();

						PrintOutput();
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
				for (int j = 1; j < Cities.Count; j++)
				{
					if (!Cities[i].Neighbors.Contains(Cities[j]))
					{
						if ((Cities[i].x == Cities[j].x + 1) || (Cities[i].x == Cities[j].x - 1) || (Cities[i].y == Cities[j].y + 1) || (Cities[i].y == Cities[j].y - 1) )
						{
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
					Cities[i].DayPassed();
				}

				for (int i = 0; i < NumberOfCountry; i++)
				{
					if (!_countries[i].IsComplete)
					{
						_countries[i].CheckIsComplete();
						if (_countries[i].IsComplete)
						{
							_countries[i].Days = days;
							numberUncompleteCountries--;
						}
					}
				}
			}
		}

		void PrintOutput()
		{
			Console.WriteLine($"Case Number {CaseNumber}");

			var q =
				from t in _countries
				orderby t
				select t;

			var result = q.ToList();
			foreach (Country i in result)
			{
				Console.WriteLine($"	{i.CountryName}: {i.Days}");
			}
		}


		/* мои методы (удалить либо перенести в другой файл)*/

		bool ParseNumberOfCountry(string line)
		{
			NumberOfCountry = 0;

			/* parse number of country */
			bool isCanParse = int.TryParse(line, out NumberOfCountry);
			if (!isCanParse)
			{
				Console.WriteLine("Can't parse number of country");
				return false;
			}
			if (NumberOfCountry == 0)
				return false;
			return true;
			/* */
		}

		/* */
	}
}
