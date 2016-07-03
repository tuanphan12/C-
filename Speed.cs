using System;
using System.Linq;
using System.Collections.Generic;

namespace Speed
{
	public class Game 
	{
		// First card to match
		private string _card1;
		// Replaces first card if no match
		private string[] _stack1;
		// Second card to match
		private string _card2;
		// Replaces second card if no match
		private string[] _stack2;
		// All of the cards known to no longer in the game
		private Dictionary<string,string[]> _deck;

		public void InitDeck()
		{
			Dictionary<string, string[]> deck = new Dictionary<string, string[]>();
			string[] cards = {"1","2","3","4","5","6","7","8","9","10","jack","queen","king", "ace"};
			deck.Add("hearts", cards);
			deck.Add("spades", cards);
			deck.Add("clubs", cards);
			deck.Add("diamonds", cards);
			this._deck = deck;
		}

		public void SetUp()
		{
			InitDeck();
			foreach (string suit in this._deck.Keys)
			{
				Console.WriteLine("{0}: {1}", suit, string.Join(", ",this._deck[suit]));
			}
		}
		//Methods to:
		// - Initiate player 1
		// - Initiate computer player
		// - Replace card1 and card2

	}

	public class PlayingGame
	{
		
		public static void Main() 
		{
			Console.WriteLine("Let's build speed!");
			Game g = new Game();
			g.SetUp();
			
		}


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
}