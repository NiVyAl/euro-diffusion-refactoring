using System;
namespace euroRefactoring
{
	public class Country : IComparable
	{
		City[] cities;
		string Name;
		bool IsComplete;
		int _caseNumber;
		int _countryNumber;

		public string CountryName { get; set; }
		public int Days { get; set; }



		public Country(int caseNumber, int countryNumber)
		{
			_caseNumber = caseNumber;
			_countryNumber = countryNumber;
		}

		public void Parse(string contryDefinition) {
			CountryName = returnCountry(contryDefinition);

			int[] coordinates = returnCoordinates(contryDefinition); // может заменить на x1 y1 x2 y2 ??????
			int xLength = Math.Abs(coordinates[2] - coordinates[0]) + 1; // Может убрать +1 ??????
			int yLength = Math.Abs(coordinates[3] - coordinates[1]) + 1; //
			City[] cities = new City[xLength * yLength];
			for (int i = 0; i < xLength; i++)
			{
				for (int j = 0; j < yLength; j++)
				{
					//coordinates[0] + i; //X
					//coordinates[1] + j; //Y
					cities[i + j] = new City(coordinates[0] + i, coordinates[1] + j, _countryNumber);
				}
			}
		}


		/* мои функции */
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
				throw new Exception($"Can't parse country name, case number: {_caseNumber}, country number: {_countryNumber}");
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
					throw new Exception($"Can't parse coordinates, case number: {_caseNumber}, country name: {CountryName}");
				}
			}

			return coordinates;
		}
	}
}
