using System;
using System.Linq;
using System.Collections.Generic;

namespace Speed
{
	public static class CommonMethods 
	{
		public static List<Tuple<string,string>> DictionaryToTupleList(Dictionary<string,string[]> deck) 
		{
			List<Tuple<string,string>> cardList = new List<Tuple<string,string>>();	
			foreach(string suit in deck.Keys) 
			{
				foreach(string card in deck[suit])
				{
					var thisTuple = Tuple.Create(suit, card);
					cardList.Add(thisTuple);
				}
			}
			return cardList;
		}
	}
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

		private List<Tuple<string,string>> cardList;

		public void InitDeck()
		{
			Dictionary<string, string[]> deck = new Dictionary<string, string[]>();
			string[] cards = {"1","2","3","4","5","6","7","8","9","10","jack","queen","king", "ace"};
			deck.Add("h", cards);
			deck.Add("s", cards);
			deck.Add("c", cards);
			deck.Add("d", cards);
			this._deck = deck;
			this.cardList = CommonMethods.DictionaryToTupleList(deck);
		}

		public void SetUp()
		{
			InitDeck();
			foreach (string suit in this._deck.Keys)
			{
				Console.WriteLine("{0}: {1}", suit, string.Join(", ",this._deck[suit]));
			}
			foreach (Tuple<string,string> card in this.cardList)
			{
				Console.WriteLine(card.ToString());
			}
			
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