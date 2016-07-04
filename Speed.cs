using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace Speed
{
	public static class CommonMethods 
	{

		/// Deals a given amount or cards randomly from a given original deck
		public static List<Tuple<string, int>> Deal(List<Tuple<string, int>> original, int numCards, HashSet<int> check) 
		{
			Random r = new Random();
			List<Tuple<string, int>> thisStack = new List<Tuple<string, int>>();
			for (int i = 0; i < numCards; i++) 
			{
				// Deals out given amount of cards, ensuring there are no repeats
			    int curValue = r.Next(0, original.Count);
			    while (check.Contains(curValue)) { curValue = r.Next(0, original.Count); }    
			    
			    thisStack.Add(original[curValue]);
			    check.Add(curValue);
			}
			return thisStack;
		}

		/// Converts card list to dictionary and returns
		public static Dictionary<string, List<int>> AsDictionary(List<Tuple<string, int>> original)
		{
			// Initialize variables to use later
			Dictionary<string, List<int>> asDictionary = new Dictionary<string, List<int>>();
			var hearts = new List<int>();
			var spades = new List<int>();
			var clubs = new List<int>();
			var diamonds = new List<int>();
			
			// Group cards by their suits (hearts,spades,clubs,diamonds)
			foreach (Tuple<string,int> card in original)
			{
				if (card.Item1 == "h") { hearts.Add(card.Item2); } 
				else if (card.Item1 == "s") { spades.Add(card.Item2); } 
				else if (card.Item1 == "c") { clubs.Add(card.Item2); } 
				else if (card.Item1 == "d") { diamonds.Add(card.Item2); }
			}
			// Order the values in ascending order so they're easier to see
			asDictionary.Add("h", hearts.OrderBy(v => v).ToList());
			asDictionary.Add("s", spades.OrderBy(v => v).ToList());
			asDictionary.Add("c", clubs.OrderBy(v => v).ToList());
			asDictionary.Add("d", diamonds.OrderBy(v => v).ToList());
			return asDictionary;

		}

		/// Views card list as dictionary to make it easier to see
		public static void ViewCards(List<Tuple<string, int>> original) 
		{
			// Gets dictionary of cards
			Dictionary<string, List<int>> asDictionary = AsDictionary(original);
			
			// Print out cards to console
			foreach (string suit in asDictionary.Keys) { Console.WriteLine("{0}: {1}", suit, string.Join(", ",asDictionary[suit])); }
		}
	}

	public class Game 
	{
		// First card to match
		private string _card1;
		public string Card1
		{
			get { return this._card1; }
			set { this._card1 = value; }
		}
		// Replaces first card if no match
		private List<Tuple<string, int>> _stack1;
		public List<Tuple<string, int>> Stack1
		{
			get { return this._stack1; }
			set { this._stack1 = value; }
		}
		// Second card to match
		private string _card2;
		public string Card2
		{
			get { return this._card2; }
			set { this._card2 = value; }
		}
		// Replaces second card if no match
		private List<Tuple<string, int>> _stack2;
		public List<Tuple<string, int>> Stack2
		{
			get { return this._stack2; }
			set { this._stack2 = value; }
		}
		// All of the cards in the game
		private List<Tuple<string, int>> _deck;
		public List<Tuple<string, int>> Deck
		{
			get { return this._deck; }
			set { this._deck = value; }
		}
		// Player
		private Player _player1;
		public Player Player1
		{
			get { return this._player1; }
			set { this._player1 = value; }
		}
		// Computer
		private Player _player2;
		public Player Player2
		{
			get { return this._player2; }
			set { this._player2 = value; }
		}


		public void InitDeck()
		{
			//Makes a tuple for each card (suit,value)
			var cardlist = new List<Tuple<string, int>>
			{
				Tuple.Create("h", 1), Tuple.Create("h", 2), Tuple.Create("h", 3), Tuple.Create("h", 4), Tuple.Create("h", 5), Tuple.Create("h", 6), Tuple.Create("h", 7), Tuple.Create("h", 8), Tuple.Create("h", 9), Tuple.Create("h", 10), Tuple.Create("h", 11), Tuple.Create("h", 12), Tuple.Create("h", 13),
				Tuple.Create("s", 1), Tuple.Create("s", 2), Tuple.Create("s", 3), Tuple.Create("s", 4), Tuple.Create("s", 5), Tuple.Create("s", 6), Tuple.Create("s", 7), Tuple.Create("s", 8), Tuple.Create("s", 9), Tuple.Create("s", 10), Tuple.Create("s", 11), Tuple.Create("s", 12), Tuple.Create("s", 13),
				Tuple.Create("c", 1), Tuple.Create("c", 2), Tuple.Create("c", 3), Tuple.Create("c", 4), Tuple.Create("c", 5), Tuple.Create("c", 6), Tuple.Create("c", 7), Tuple.Create("c", 8), Tuple.Create("c", 9), Tuple.Create("c", 10), Tuple.Create("c", 11), Tuple.Create("c", 12), Tuple.Create("c", 13),
				Tuple.Create("d", 1), Tuple.Create("d", 2), Tuple.Create("d", 3), Tuple.Create("d", 4), Tuple.Create("d", 5), Tuple.Create("d", 6), Tuple.Create("d", 7), Tuple.Create("d", 8), Tuple.Create("d", 9), Tuple.Create("d", 10), Tuple.Create("d", 11), Tuple.Create("d", 12), Tuple.Create("d", 13)
			};
			Deck = cardlist;

			HashSet<int> check = new HashSet<int>();
			
			Stack1 = CommonMethods.Deal(Deck,6,check);
			Stack2 = CommonMethods.Deal(Deck,6,check);
			Player1.Stack = CommonMethods.Deal(Deck,20,check);
			Player1.InitHand();
			Player2.Stack = CommonMethods.Deal(Deck,20,check);
			Player2.InitHand();

 
		}

		public void NewGame()
		{
			Player player1 = new Player();
			Player player2 = new Player();
			Player1 = player1;
			Player2 = player2;
			InitDeck();
			Console.WriteLine("Hello contestant! What's your name?: ");
			var name = Console.ReadLine();
			player1.Name = name;
			Console.WriteLine("Hello, {0}, here is your hand!",player1.Name);
			CommonMethods.ViewCards(player1.Hand);
			
			while (true) 
			{
				// Press a to refill your hand
				var key = Console.ReadKey();
				if (key.KeyChar == 'a') 
				{ 
					player1.RefillHand();

					Console.WriteLine("\nHand: ");
					player1.SeeHand();
				}
				//Press s to get rid of a card
				if (key.KeyChar == 's')
				{
					player1.PlayCard();

					Console.WriteLine("\nHand: ");
					player1.SeeHand();
				}
			}
		}

	}

	public class Player 
	{
		// Cards in your hand
		private List<Tuple<string,int>> _hand;
		public List<Tuple<string,int>>  Hand
		{
			get { return this._hand; }
		}
		// Cards to replace used cards in your hand 
		private List<Tuple<string,int>> _stack;
		public List<Tuple<string,int>>  Stack
		{
			get { return this._stack; }
			set { this._stack = value; }
		}
		// Name
		private string _name;
		public string Name
		{
			get { return this._name; }
			set { this._name = value; }
		}

		// Flag to say player has no playable cards
		private bool _noMatches;
		public bool NoMatches
		{
			get { return this._noMatches; }
			set { this._noMatches = value; }
		}

		//--METHODS--------------------

		public void InitHand() 
		{
			HashSet<int> check = new HashSet<int>();	
			this._hand = CommonMethods.Deal(Stack,4,check);
			foreach (Tuple<string, int> card in Hand) 
			{
				Stack.Remove(card);
			}
		}

		public void SeeHand() { CommonMethods.ViewCards(Hand); }

		public void AdmitDefeat() 
		{
			Console.WriteLine("\n\n{0} wants to see a new card!");
			NoMatches = true;
		}

		public void RefillHand()
		{
			Random r = new Random();
			if (Hand.Count < 4)
			{
				int ind = r.Next(0,4);
				var card = Stack[ind];
				Hand.Add(card);
				Stack.Remove(card);
			}
			else 
			{
				Console.WriteLine("\n\nYour hand is already full!");
			}
			return;
		}	
		public void PlayCard()
		{
			Dictionary<string, List<int>> allCards = CommonMethods.AsDictionary(Hand);
			Console.Write("\n\nSuit: ");
			var suit_str = Console.ReadKey();
			var suit = suit_str.KeyChar.ToString();
			List<string> suits = new List<string>(new string[] {"h", "s", "c", "d"}); 
			if (!suits.Contains(suit))
			{
				Console.WriteLine("\n\nYou don't have that card!");
				return;
			}
			Console.Write("\nValue: ");
			var value_str = Console.ReadLine();
			var value = Convert.ToInt32(value_str);
			Tuple<string, int> card = new Tuple<string, int>(suit, value);
			if (allCards[suit].Contains(value))
			{
				Hand.Remove(card);
			}
			else 
			{
				Console.WriteLine("\n\nYou don't have that card!");
			}
			return;
		}	

	}

	public class PlayGame
	{		
		public static void Main() 
		{
			Console.WriteLine("Do you want to play Speed?:(y/n) ");
			var ans = Console.ReadLine();
			if (ans == "y") 
			{
				Game game = new Game();
				game.NewGame();
			}
			
			
		}

	}
}