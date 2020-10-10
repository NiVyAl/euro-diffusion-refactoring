using System;
using System.Collections.Generic;
namespace euroRefactoring
{
	public class Country : IComparable
	{
		//City[] cities;
		public List<City> сities = new List<City>(); // сдесь все города
		string Name;
		bool IsComplete;

		public string CountryName { get; set; }
		public int Days { get; set; }

		int countryIndex;

		public Country(int countryIndex)
		{
			Days = 0;
			this.countryIndex = countryIndex;
		}

		public void Parse(string contryDefinition) {
			CountryName = returnCountry(contryDefinition);

			int[] coordinates = returnCoordinates(contryDefinition); // может заменить на x1 y1 x2 y2 ??????
			int xLength = Math.Abs(coordinates[2] - coordinates[0]) + 1; // Может убрать +1 ??????
			int yLength = Math.Abs(coordinates[3] - coordinates[1]) + 1; //

			for (int i = 0; i < xLength; i++)
			{
				for (int j = 0; j < yLength; j++)
				{
					//coordinates[0] + i; //X
					//coordinates[1] + j; //Y
					Diffusion.Cities.Add(new City(x: coordinates[0] + i, y: coordinates[1] + j, countryIndex));
					сities.Add(Diffusion.Cities[Diffusion.Cities.Count - 1]);
				}
			}
		}


		/* мои функции */
		public void writeAboutYou()
		{
			Console.WriteLine($"Name: {CountryName}, Index: {countryIndex}");
			// ПРОБЛЕМА!!! СТРАНА НЕ ЗНАЕТ КТО ЕЕ ГОРОДА
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

		private string returnCountry(string line)
		{
			string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (words[0] == " ") //		!!! переделать проверку!!!								!!! переделать проверку!!!
				throw new Exception($"Can't parse country name, case number: {Diffusion.CaseNumber}, country number: {countryIndex}");
			return words[0];
		}

		private int[] returnCoordinates(string line)
		{
			int coordinatesCount = 4;
			string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			int[] coordinates = new int[coordinatesCount];
			for (int i = 1; i <= coordinatesCount; i++)
			{
				if (int.TryParse(words[i], out coordinates[i - 1]) == false)
				{
					throw new Exception($"Can't parse coordinates, case number: {Diffusion.CaseNumber}, country name: {CountryName}");
				}
			}

			return coordinates;
		}
	}
}
