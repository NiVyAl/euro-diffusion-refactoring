using System;
namespace euroRefactoring
{
	public class Country
	{
		City[] cities;
		string Name;
		bool IsComplete;

		public Country()
		{

		}

		public void Parse(string contryDefinition) {
			Console.WriteLine(contryDefinition);
		}
	}
}
