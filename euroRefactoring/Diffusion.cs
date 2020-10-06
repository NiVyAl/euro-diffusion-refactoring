using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace euroRefactoring
{
	public partial class Diffusion
	{
		Country[] _countries;
		bool _IsComplete;

		private int _numberOfCountry; // number of countries in each case
		private int _caseNumber = 1;

		public void Parse(string filename)
		{
			try
			{
				using (StreamReader sr = new StreamReader(filename, System.Text.Encoding.Default))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						bool isCanParse = ParseNumberOfCountry(line);
						if (!isCanParse)
							break;

						_countries = new Country[_numberOfCountry];
						for (int i = 0; i < _numberOfCountry; i++)
						{
							_countries[i] = new Country();
							_countries[i].Parse(sr.ReadLine()); // читаем строку и сразу ее передаем на парсинг
						}

						if (_numberOfCountry > 1) // calculation starts if the number of countries is > 1 (if only 1 country, it complete in 0 days)
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


		/* мои методы (удалить либо перенести в другой файл)*/

		bool ParseNumberOfCountry(string line)
		{
			_numberOfCountry = 0;

			/* parse number of country */
			bool isCanParse = int.TryParse(line, out _numberOfCountry);
			if (!isCanParse)
			{
				Console.WriteLine("Can't parse number of country");
				return false;
			}
			if (_numberOfCountry == 0)
				return false;
			return true;
			/* */
		}

		/* */
	}
}
