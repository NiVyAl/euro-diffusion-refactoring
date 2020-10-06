using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace euroRefactoring
{
	public partial class Diffusion
	{
		private int _XMaxCoordinate;
		private int _YMaxCoordinate;

		/// <summary>
		///		initialize each countries
		/// </summary>
		/// <param name="sr"></param>
		private void initializeCountries(StreamReader sr)
		{
			_XMaxCoordinate = 0; // to determine the dimension of the array of cities (AbstractCity[,] _allCity)
			_YMaxCoordinate = 0;

			for (int i = 0; i < _numberOfCountry; i++)
			{
				string line = sr.ReadLine();
				string country = returnCountry(line, i);
				int[] coordinates = returnCoordinates(line, country);
				//_countries[i] = new Country(country, x1: coordinates[0], y1: coordinates[1], x2: coordinates[2], y2: coordinates[3], _numberOfCountry);

				/* find max x and y coordinates (for initialization array of city) */
				if (coordinates[2] > _XMaxCoordinate)
					_XMaxCoordinate = coordinates[2];
				if (coordinates[3] > _YMaxCoordinate)
					_YMaxCoordinate = coordinates[3];
				/* */
			}
			//checkCorrectData();
		}

		/// <summary>
		///		return country name from file
		/// </summary>
		/// <param name="line"></param>
		/// <returns>country name</returns>
		private string returnCountry(string line, int countryNumber)
		{
			string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (words[0] == " ") //		!!! переделать проверку!!!								!!! переделать проверку!!!
				throw new Exception($"Can't parse country name, case number: {_caseNumber}, country number: {countryNumber}");
			return words[0];
		}

		/// <summary>
		///		return coordinates of country from file
		/// </summary>
		/// <param name="line"></param>
		/// <returns>int[] coordinates</returns>
		private int[] returnCoordinates(string line, string countryName)
		{
			int coordinatesCount = 4;
			string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			int[] coordinates = new int[coordinatesCount];
			for (int i = 1; i <= coordinatesCount; i++)
			{
				if (int.TryParse(words[i], out coordinates[i - 1]) == false)
				{
					throw new Exception($"Can't parse coordinates, case number: {_caseNumber}, country name: {countryName}");
				}
			}

			return coordinates;
		}
	}
}
