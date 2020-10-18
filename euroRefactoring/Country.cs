using System;
using System.Collections.Generic;
namespace euroRefactoring
{
	public class Country : IComparable
	{
		int[] Сities; // index of cities in this country
		int NumberOfCountry;
		int CaseNumber;
		List<City> AllCities; // all cities
		int СountryIndex;
		int LineNumber;

		public bool IsComplete { get; set; }
		public string CountryName { get; set; }
		public int Days { get; set; }
		public bool[] Neighbors;
		public int[] Coordinates = new int[4];

		public Country(int countryIndex, int lineNumber, List<City> allCities, int numberOfCountry, int caseNumber)
		{
			СountryIndex = countryIndex;
			LineNumber = lineNumber;
			NumberOfCountry = numberOfCountry;
			Neighbors = new bool[NumberOfCountry];
			AllCities = allCities;
			CaseNumber = caseNumber;
		}

		/// <summary>
		///		Parse line with country name and coordinates
		/// </summary>
		/// <param name="contryDefinition"></param>
		public void Parse(string contryDefinition) {
			string[] words = contryDefinition.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (words.Length != 5)
				throw new Exception($"Сountry entered incorrectly, case number: {CaseNumber}, line: {LineNumber}");
			CountryName = words[0].Replace("\t", String.Empty);
			if (CountryName.Length >= 25)
				throw new Exception($"Сountry name can be at most 25 characters, case number: {CaseNumber}, line: {LineNumber}");

			for (int i = 1; i <= Coordinates.Length; i++)
			{
				if (int.TryParse(words[i], out Coordinates[i - 1]) == false)
				{
					throw new Exception($"Can't parse coordinates, case number: {CaseNumber}, country name: {CountryName}, line: {LineNumber}");
				}
			}

			InitializeCities();
		}

		private void InitializeCities()
		{
			int xLength = Math.Abs(Coordinates[2] - Coordinates[0]);
			int yLength = Math.Abs(Coordinates[3] - Coordinates[1]);
			int citiesCount = (xLength + 1) * (yLength + 1);
			Сities = new int[citiesCount];

			int k = 0;
			for (int i = 0; i <= xLength; i++)
			{
				for (int j = 0; j <= yLength; j++)
				{
					AllCities.Add(new City(x: Coordinates[0] + i, y: Coordinates[1] + j, СountryIndex, NumberOfCountry));
					Сities[k] = AllCities.Count - 1;
					k++;
				}
			}
		}

		public void CheckIsComplete()
		{
			for (int i = 0; i < Сities.Length; i++)
			{
				if (AllCities[Сities[i]].IsComplete)
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
	}
}