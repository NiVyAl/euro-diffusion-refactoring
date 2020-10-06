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

		public Diffusion()
		{

		}


		private int _numberOfCountry; // number of countries in each case
		private int _XMaxCoordinate;
		private int _YMaxCoordinate;
		private int _caseNumber = 1;

		public void Parse(string filename)
		{
			try
			{
				using (StreamReader sr = new StreamReader(filename, System.Text.Encoding.Default))
				{
					Console.WriteLine("");
					Console.WriteLine("");
					Console.WriteLine("");
					Console.WriteLine("start programm -----------------");
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						_numberOfCountry = 0;
						bool isCanParse = int.TryParse(line, out _numberOfCountry);
						if (!isCanParse)
						{
							Console.WriteLine("Can't parse number of country");
							break;
						}
						if (_numberOfCountry == 0)
							break;

						_countries = new Country[_numberOfCountry];
						_XMaxCoordinate = 0; // to determine the dimension of the array of cities (AbstractCity[,] _allCity)
						_YMaxCoordinate = 0; //

						initializeCountries(sr); // comput coordinates of cities of each country

						if (_numberOfCountry > 1) // calculation starts if the number of countries is> 1 (if only 1 country, it complete in 0 days)
							Calculate();
						PrintOutput();
						_caseNumber++;
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

		}

		void PrintOutput()
		{

		}


		/// <summary>
		///		initialize each countries
		/// </summary>
		/// <param name="sr"></param>
		private void initializeCountries(StreamReader sr)
		{
			for (int i = 0; i < _numberOfCountry; i++)
			{
				string line = sr.ReadLine();
				string country = returnCountry(line, i);
				int[] coordinates = returnCoordinates(line, country);
				_countries[i] = new Country(country, x1: coordinates[0], y1: coordinates[1], x2: coordinates[2], y2: coordinates[3], _numberOfCountry);

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
