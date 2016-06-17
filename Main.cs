using System;

namespace Main
{
	public static class Hello
	{
		public static void Main()
		{
			Console.Write("Hello");
		}

		public static char Translator(string MorseCode)
		{

			switch (MorseCode)
			{
				case ".-":
					return 'a';
					//break;
				case "-...":
					return 'b';
					//break;
				case "-.-.":
					return 'c';
					//break;
				case "-..":
					return 'd';
					//break;
				case ".":
					return 'e';
					//break;
				case "..-.":
					return 'f';
					//break;
				case "--.":
					return 'g';
					//break;
				case "....":
					return 'h';
					//break;
				case "..":
					return 'i';
					//break;
				case ".---":
					return 'j';
					//break;
				case "-.-":
					return 'k';
					//break;
				case ".-..":
					return 'l';
					//break;
				case "--":
					return 'm';
					//break;
				case "-.":
					return 'n';
					//break;
				case "---":
					return 'o';
					//break;
				case ".--.":
					return 'p';
					//break;
				case "--.-":
					return 'q';
					//break;
				case ".-.":
					return 'r';
					//break;
				case "...":
					return 's';
					//break;
				case "-":
					return 't';
					//break;
				case "..-":
					return 'u';
					//break;
				case "...-":
					return 'v';
					//break;
				case ".--":
					return 'w';
					//break;
				case "-..-":
					return 'x';
					//break;
				case "-.--":
					return 'y';
					//break;
				case "--..":
					return 'z';
					//break;
				case ".----":
					return '1';
					//break;
				case "..---":
					return '2';
					//break;   
				case "...--":
					return '3';
					//break;
				case "....-":
					return '4';
					//break;
				case ".....":
					return '5';
					//break;
				case "-....":
					return '6';
					//break;
				case "--...":
					return '7';
					//break;
				case "---..":
					return '8';
					//break;
				case "----.":
					return '9';
					//break;
				case "-----":
					return '0';
					//break;
				default:
					return ' ';
			}
		}
	}

}