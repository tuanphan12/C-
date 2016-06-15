using System;
using System.Linq;
using System.Threading;

public static class TunaLetter3 
{
	public static void Main()
	{
		TypeWriter("Want to play Category Tag? ");
		string ans = Console.ReadLine();
		if (WantToPlay(ans))
			TypeWriter("Yay!");
		else
			Console.Write("Oh no!");
		string text = @"Too bad cause this code is too broken and long.";
		TypeWriter(text);

	}
	static bool WantToPlay(string a)
	{
		string[] TunaAnswers = {"yes", "yeah", "y", "yup", "yussss", "freaking yeah!!!!"};
		return TunaAnswers.Contains(a.ToLower());
	}

	static void TypeWriter(string text)
	{
		bool isFirst = false;
		string[] words = WordList(text);
		foreach (string word in words)
		{
			if (word == "br") 
			{
				isFirst = true;
				continue;
			}
			foreach (char letter in word)
			{
				if (isFirst && letter != ' ') 
				{
					isFirst = false;
					Console.WriteLine(letter);
					continue;
				}
				Console.Write(letter);
				Random random = new Random();
				int WaitTime;
				char[] punctuation = {'.','!','?'};
				if (punctuation.Contains(letter)) 
				{
					WaitTime = 20;
				}
				else 
				{
					WaitTime = random.Next(0,50);
				}
				Thread.Sleep(WaitTime);

			}
			Console.Write(" ");
			Thread.Sleep(50);
		}
	}
	static string[] WordList(string text)
	{
		return text.Split(' ');
	}
}