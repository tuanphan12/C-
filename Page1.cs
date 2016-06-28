using System;

namespace Page1
{
	class ClassName
	{
		static void Main() 
		{
			string rawData = "#2104967205729<LF>";
			Console.WriteLine("Number of Digits: {0}", rawData[1])
			var numDigits = Convert.ToDouble(rawData[1]);
			Console.WriteLine(numDigits);
		}
	}
	
}