using System;
using System.Linq;
using System.Collections.Generic;

namespace Speed
{
	class Game 
	{
		// Who is in the game
		private Player _player1;
		public Player Player1
		{
			get 
			{
				return _player1;
			}
			set 
			{
				_player1 = value;
			}

		}
		// First card to match
		private string _card1;
		// Replaces first card if no match
		private string[] _stack1;
		// Second card to match
		private string _card2;
		// Replaces second card if no match
		private string[] _stack2;
		// All of the cards known to no longer in the game
		public Dictionary<string,string[]> _deck;
		Dictionary<string,string[]> _deck = new Dictionary<string,string[]>();
		string[] cards = {"1","2","3","4","5","6","7","8","9","10","jack","queen","king", "ace"};
		_deck.Add("hearts", cards);
		_deck.Add("spades", cards);
		_deck.Add("clovers", cards);
		_deck.Add("diamonds", cards);

		//Methods to:
		// - Initiate player 1
		// - Initiate computer player
		// - Replace card1 and card2


		public static void Main() 
		{
			Console.WriteLine("Let's build speed!");
			foreach (string suit in _deck.Keys)
			{
			    Console.WriteLine("{0}: {1}", suit, string.Join(", ",_deck[suit]));
			}
		}


	}

	class Player 
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