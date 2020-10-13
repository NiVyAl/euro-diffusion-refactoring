using System;
using System.Collections.Generic;
namespace euroRefactoring
{
	public class Country : IComparable
	{
		List<int> сities = new List<int>(); // сдесь индексы городов принадлежащих стране
		string Name;
		public bool IsComplete { get; set; }

		public string CountryName { get; set; }
		public int Days { get; set; }

		int countryIndex;
		int numberUncompleteCities;
		int lineNumber;

		public Country(int countryIndex, int lineNumber)
		{
			this.countryIndex = countryIndex;
			this.lineNumber = lineNumber;
		}

		public void Parse(string contryDefinition) {
			int[] coordinates = returnCoordinates(contryDefinition); // может заменить на x1 y1 x2 y2 ??????
			int xLength = Math.Abs(coordinates[2] - coordinates[0]);
			int yLength = Math.Abs(coordinates[3] - coordinates[1]);

			for (int i = 0; i <= xLength; i++)
			{
				for (int j = 0; j <= yLength; j++)
				{
					Diffusion.Cities.Add(new City(x: coordinates[0] + i, y: coordinates[1] + j, countryIndex));
					сities.Add(Diffusion.Cities.Count - 1);
				}
			}
			numberUncompleteCities = сities.Count;
		}


		/* мои функции */
		public void CheckIsComplete()
		{
			for (int i = 0; i < сities.Count; i++)
			{
				if (Diffusion.Cities[сities[i]].IsComplete)
				{
					IsComplete = true;
				} else
				{
					IsComplete = false;
					break;
				}
					
			}
		}

		public int CompareTo(object obj)
		{
			int res;
			if (obj == null) return 1;

			Country otherCityDays = obj as Country;
			if (otherCityDays == null)
				throw new ArgumentException("Object is not a Temperature");

			res = this.Days.CompareTo(otherCityDays.Days);  // sort by number of days

			if (res == 0) // or if number is same
				res = this.CountryName.CompareTo(otherCityDays.CountryName); // sort alphabetically
			return res;

		}

		private int[] returnCoordinates(string line)
		{
			int coordinatesCount = 4;
			string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (words.Length != 5)
				throw new Exception($"Сountry entered incorrectly, case number: {Diffusion.CaseNumber}, line: {lineNumber}");

			CountryName = words[0].Replace("\t", String.Empty);
			int[] coordinates = new int[coordinatesCount];
			for (int i = 1; i <= coordinatesCount; i++)
			{
				if (int.TryParse(words[i], out coordinates[i - 1]) == false)
				{
					throw new Exception($"Can't parse coordinates, case number: {Diffusion.CaseNumber}, country name: {CountryName}, line: {lineNumber}");
				}
			}

			return coordinates;
		}
	}
}
