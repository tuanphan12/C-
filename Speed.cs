using System;
using System.Linq;
using System.Collections.Generic;

namespace Speed
{
	public static class CommonMethods 
	{
		public static List<Tuple<string, string>> Deal(List<Tuple<string, string>> original, int numCards, HashSet<int> check) 
		{
			Random r = new Random();
			List<Tuple<string, string>> thisStack = new List<Tuple<string, string>>();
			for (Int32 i = 0; i < numCards; i++) 
			{
			    int curValue = r.Next(0, original.Count);
			    while (check.Contains(curValue)) 
			    {
			        curValue = r.Next(0, original.Count);
			    }
			    thisStack.Add(original[curValue]);
			    check.Add(curValue);
			}
			return thisStack;
		}
	}
	public class Game 
	{
		// First card to match
		private string _card1;
		// Replaces first card if no match
		private List<Tuple<string,string>> _stack1;
		// Second card to match
		private string _card2;
		// Replaces second card if no match
		private List<Tuple<string,string>> _stack2;
		// All of the cards in the game
		private List<Tuple<string,string>> cardList;

		private List<Tuple<string,string>> _p1stack;
		private List<Tuple<string,string>> _p2stack;

		public void InitDeck()
		{
			//Makes a tuple for each card (suit,value)
			var cardlist2 = new List<Tuple<string, string>>
			{
				Tuple.Create("h","2"), Tuple.Create("h","3"), Tuple.Create("h","4"), Tuple.Create("h","5"), Tuple.Create("h","6"), Tuple.Create("h","7"), Tuple.Create("h","8"), Tuple.Create("h","9"), Tuple.Create("h","10"), Tuple.Create("h","jack"), Tuple.Create("h","queen"), Tuple.Create("h","king"), Tuple.Create("h","ace"),
				Tuple.Create("s","2"), Tuple.Create("s","3"), Tuple.Create("s","4"), Tuple.Create("s","5"), Tuple.Create("s","6"), Tuple.Create("s","7"), Tuple.Create("s","8"), Tuple.Create("s","9"), Tuple.Create("s","10"), Tuple.Create("s","jack"), Tuple.Create("s","queen"), Tuple.Create("s","king"), Tuple.Create("s","ace"),
				Tuple.Create("c","2"), Tuple.Create("c","3"), Tuple.Create("c","4"), Tuple.Create("c","5"), Tuple.Create("c","6"), Tuple.Create("c","7"), Tuple.Create("c","8"), Tuple.Create("c","9"), Tuple.Create("c","10"), Tuple.Create("c","jack"), Tuple.Create("c","queen"), Tuple.Create("c","king"), Tuple.Create("c","ace"),
				Tuple.Create("d","2"), Tuple.Create("d","3"), Tuple.Create("d","4"), Tuple.Create("d","5"), Tuple.Create("d","6"), Tuple.Create("d","7"), Tuple.Create("d","8"), Tuple.Create("d","9"), Tuple.Create("d","10"), Tuple.Create("d","jack"), Tuple.Create("d","queen"), Tuple.Create("d","king"), Tuple.Create("d","ace")
			};
			this.cardList = cardlist2;

			HashSet<int> check = new HashSet<int>();
			
			this._stack1 = CommonMethods.Deal(this.cardList,6,check);
			this._stack2 = CommonMethods.Deal(this.cardList,6,check);
			this._p1stack = CommonMethods.Deal(this.cardList,20,check);
			this._p2stack = CommonMethods.Deal(this.cardList,20,check);

 
		}

		public void SetUp()
		{
			InitDeck();
			foreach (string suit in this._deck.Keys)
			{
				Console.WriteLine("{0}: {1}", suit, string.Join(", ",this._deck[suit]));
			}
			//foreach (Tuple<string,string> card in this.cardList)
			//{
			//	Console.WriteLine(card.ToString());
			//}
			Console.WriteLine(string.Join(", ",this.cardList));
			Console.WriteLine("Now Stack1: ");
			Console.WriteLine(string.Join(", ",this._stack1));
			Console.WriteLine("Now Stack2: ");
			Console.WriteLine(string.Join(", ",this._stack2));
			Console.WriteLine("Now p1Stack: ");
			Console.WriteLine(string.Join(", ",this._p1stack));
			Console.WriteLine("Now p2Stack: ");
			Console.WriteLine(string.Join(", ",this._p2stack));
			
		}
		//Methods to:
		// - Initiate player 1
		// - Initiate computer player
		// - Replace card1 and card2

	}

	public class Player 
	{
		// Cards in your hand
		private string[] _cards;
		// Cards to replace used cards in your hand 
		private string[] _stack;

		//Methods to:
		// - Place a card
		// - Refill hand
		// - See hand
		// - Declare "have no matches" to get new cards

	}

	public class PlayGame
	{		
		public static void Main() 
		{
			Console.WriteLine("Let's build speed!");
			Game g = new Game();
			g.SetUp();
			
		}

	}
}