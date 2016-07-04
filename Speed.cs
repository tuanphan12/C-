using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace Speed
{
	public static class CommonMethods 
	{

		// Deals a given amount or cards randomly from a given original deck
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

		// Converts card list to dictionary and returns
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

		// Views card list as dictionary to make it easier to see
		public static void ViewCards(List<Tuple<string, int>> original) 
		{
			// Initialize variables to use later
			Dictionary<string, List<int>> asDictionary = AsDictionary(original);
			
			// Print out cards to console
			foreach (string suit in asDictionary.Keys)
			{
				Console.WriteLine("{0}: {1}", suit, string.Join(", ",asDictionary[suit]));
			}
		}
	}

	public class Game 
	{
		// First card to match
		private string _card1;
		// Replaces first card if no match
		private List<Tuple<string,int>> _stack1;
		// Second card to match
		private string _card2;
		// Replaces second card if no match
		private List<Tuple<string,int>> _stack2;
		// All of the cards in the game
		private List<Tuple<string,int>> cardList;
		// Player
		public Player _player1;
		// Computer
		public Player _player2;


		public void InitDeck()
		{
			//Makes a tuple for each card (suit,value)
			var cardlist2 = new List<Tuple<string, int>>
			{
				Tuple.Create("h", 1), Tuple.Create("h", 2), Tuple.Create("h", 3), Tuple.Create("h", 4), Tuple.Create("h", 5), Tuple.Create("h", 6), Tuple.Create("h", 7), Tuple.Create("h", 8), Tuple.Create("h", 9), Tuple.Create("h", 10), Tuple.Create("h", 11), Tuple.Create("h", 12), Tuple.Create("h", 13),
				Tuple.Create("s", 1), Tuple.Create("s", 2), Tuple.Create("s", 3), Tuple.Create("s", 4), Tuple.Create("s", 5), Tuple.Create("s", 6), Tuple.Create("s", 7), Tuple.Create("s", 8), Tuple.Create("s", 9), Tuple.Create("s", 10), Tuple.Create("s", 11), Tuple.Create("s", 12), Tuple.Create("s", 13),
				Tuple.Create("c", 1), Tuple.Create("c", 2), Tuple.Create("c", 3), Tuple.Create("c", 4), Tuple.Create("c", 5), Tuple.Create("c", 6), Tuple.Create("c", 7), Tuple.Create("c", 8), Tuple.Create("c", 9), Tuple.Create("c", 10), Tuple.Create("c", 11), Tuple.Create("c", 12), Tuple.Create("c", 13),
				Tuple.Create("d", 1), Tuple.Create("d", 2), Tuple.Create("d", 3), Tuple.Create("d", 4), Tuple.Create("d", 5), Tuple.Create("d", 6), Tuple.Create("d", 7), Tuple.Create("d", 8), Tuple.Create("d", 9), Tuple.Create("d", 10), Tuple.Create("d", 11), Tuple.Create("d", 12), Tuple.Create("d", 13)
			};
			this.cardList = cardlist2;

			HashSet<int> check = new HashSet<int>();
			
			this._stack1 = CommonMethods.Deal(this.cardList,6,check);
			this._stack2 = CommonMethods.Deal(this.cardList,6,check);
			this._player1.Stack = CommonMethods.Deal(this.cardList,20,check);
			this._player1.InitHand();
			this._player2.Stack = CommonMethods.Deal(this.cardList,20,check);
			this._player2.InitHand();

 
		}

		public void SetUp()
		{
			InitDeck();
			CommonMethods.ViewCards(this.cardList);
			Console.WriteLine("Now Stack1: ");
			CommonMethods.ViewCards(this._stack1);
			Console.WriteLine("Now Stack2: ");
			CommonMethods.ViewCards(this._stack2);

			
		}
		//Methods to:
		// - Initiate player 1
		// - Initiate computer player
		// - Replace card1 and card2

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
		//Methods to:
		// - Place a card
		// - Refill hand
		// - See hand
		// - Declare "have no matches" to get new cards
		public void InitHand() 
		{
			HashSet<int> check = new HashSet<int>();	
			this._hand = CommonMethods.Deal(Stack,4,check);
			foreach (Tuple<string, int> card in Hand) 
			{
				Stack.Remove(card);
			}
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
				Console.WriteLine("Your hand is already full!");
			}
		}	
		public void PlayCard()
		{
			Dictionary<string, List<int>> allCards = CommonMethods.AsDictionary(Hand);
			var key1 = Console.ReadKey();
			List<string> suits = new List<string>(new string[] {"h", "s", "c", "d"}); 
			string suit = Char.ToString(key1.KeyChar);
			if (!suits.Contains(suit))
			{
				Console.WriteLine("You don't have that card!");
				return;
			}
			int value = new int();
			var key2 = Console.ReadKey();
			var key3 = Console.ReadKey();

			if (key2.KeyChar == '1') 
			{
				if (key3.KeyChar == '0') { value = 10; }
				else if (key3.KeyChar == '1') { value = 11; }
				else if (key3.KeyChar == '2') { value = 12; }
				else if (key3.KeyChar == '3') { value = 13; }
			}
			else if (key2.KeyChar == '0')
			{
				value = (int)Char.GetNumericValue(key3.KeyChar);
			}
			Tuple<string, int> choice = new Tuple<string, int>(suit, value);
			if (allCards[suit].Contains(value)) 
			{
				Hand.Remove(choice);
			}
			else
			{
				Console.WriteLine("You don't have that card!");
				return;
			}
			return;
		}	

	}

	public class PlayGame
	{		
		public static void Main() 
		{
			Console.WriteLine("Let's play speed!");
			Player player1 = new Player();
			Player player2 = new Player();
			Game game = new Game();
			game._player1 = player1;
			game._player2 = player2;
			game.SetUp();
			Console.WriteLine("What's your name?: ");
			var name = Console.ReadLine();
			player1.Name = name;
			Console.WriteLine("Hello, {0}, here is your deck!",player1.Name);
			CommonMethods.ViewCards(player1.Stack);
			Console.WriteLine("And this is your hand!");
			CommonMethods.ViewCards(player1.Hand);
			while (true) 
			{
				var key = Console.ReadKey();
				if (key.KeyChar == 'a') 
				{ 
					player1.RefillHand();

					Console.WriteLine("\nHand: ");
					CommonMethods.ViewCards(player1.Hand);
					Console.WriteLine("Taken from this stack: ");
					CommonMethods.ViewCards(player1.Stack);
				}
				if (key.KeyChar == 's')
				{
					player1.PlayCard();

					Console.WriteLine("\nHand: ");
					CommonMethods.ViewCards(player1.Hand);
					Console.WriteLine("Taken from this stack: ");
					CommonMethods.ViewCards(player1.Stack);
				}
			}
			
		}

	}
}