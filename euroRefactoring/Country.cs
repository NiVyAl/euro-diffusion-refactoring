using System;
using System.Collections.Generic;
namespace euroRefactoring
{
	public class Country : IComparable
	{
		int[] cities; // сдесь индексы городов принадлежащих стране
		public string Name;
		public bool IsComplete { get; set; }

		public string CountryName { get; set; }
		public int Days { get; set; }

		int NumberOfCountry;
		int CaseNumber;
		List<City> AllCities;
		int countryIndex;
		int numberUncompleteCities;
		int lineNumber;
		public bool[] Neighbors;
		public int[] Coordinates = new int[4];

		public Country(int countryIndex, int lineNumber, List<City> allCities, int numberOfCountry, int CaseNumber)
		{
			this.countryIndex = countryIndex;
			this.lineNumber = lineNumber;
			Neighbors = new bool[numberOfCountry];
			this.AllCities = allCities;
			this.CaseNumber = CaseNumber;
			this.NumberOfCountry = numberOfCountry;
		}

		public void Parse(string contryDefinition) {
			Coordinates = returnCoordinates(contryDefinition); // может заменить на x1 y1 x2 y2 ??????
			int xLength = Math.Abs(Coordinates[2] - Coordinates[0]);
			int yLength = Math.Abs(Coordinates[3] - Coordinates[1]);
			int citiesCount = (xLength+1) * (yLength+1);
			cities = new int[citiesCount];
			numberUncompleteCities = citiesCount;

			int k = 0;
			for (int i = 0; i <= xLength; i++)
			{
				for (int j = 0; j <= yLength; j++)
				{
					AllCities.Add(new City(x: Coordinates[0] + i, y: Coordinates[1] + j, countryIndex, NumberOfCountry));
					cities[k] = AllCities.Count - 1;
					k++;
				}
			}
		}


		/* мои функции */
		public void CheckIsComplete()
		{
			for (int i = 0; i < cities.Length; i++)
			{
				if (AllCities[cities[i]].IsComplete)
				{
					IsComplete = true;
				} else
				{
					IsComplete = false;
					break;
				}
					
			}
		}

		private int[] returnCoordinates(string line)
		{
			int coordinatesCount = 4;
			string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (words.Length != 5)
				throw new Exception($"Сountry entered incorrectly, case number: {CaseNumber}, line: {lineNumber}");

			CountryName = words[0].Replace("\t", String.Empty);
			if (CountryName.Length >= 25)
				throw new Exception($"Сountry name can be at most 25 characters, case number: {CaseNumber}, line: {lineNumber}");
			int[] coordinates = new int[coordinatesCount];
			for (int i = 1; i <= coordinatesCount; i++)
			{
				if (int.TryParse(words[i], out coordinates[i - 1]) == false)
				{
					throw new Exception($"Can't parse coordinates, case number: {CaseNumber}, country name: {CountryName}, line: {lineNumber}");
				}
			}

			return coordinates;
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
	}
}