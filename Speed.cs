using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Timers;

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

		/// Views hand as dictionary to make it easier to see
		public static void ViewHand(Player player, bool debugging = false) 
		{
			// Gets dictionary of cards
			var original = player.Hand;
			Dictionary<string, List<int>> asDictionary = AsDictionary(original);
			// Debugging mode shows the cards in the player's stack as well as their hand
			if (debugging) 
			{
				var deck = player.Stack;
				Dictionary<string, List<int>> asDictionary2 = AsDictionary(deck);
				foreach (string suit in asDictionary.Keys) { Console.WriteLine("{0}: {1} ({2})", suit, string.Join(", ",asDictionary[suit]), string.Join(", ",asDictionary2[suit])); }
				return;
			}
			// Prints out just hand to console
			foreach (string suit in asDictionary.Keys) { Console.WriteLine("{0}: {1}", suit, string.Join(", ",asDictionary[suit])); }
		}

		// Views the 2 cards in play
		public static void ViewCards(Tuple<string, int> card1, Tuple<string, int> card2)
		{
			Console.WriteLine("\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
			Console.WriteLine("~~~~                            ~~~~");
			Console.WriteLine("~~~~     {0}     {1}      ~~~~",card1.ToString(),card2.ToString());
			Console.WriteLine("~~~~                            ~~~~");
			Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
		}

		// Gives players time before new cards are placed
		public static void Countdown()
		{
			Type("New cards in 3");
			Console.Write("...");
			Thread.Sleep(1000);
			Console.Write("2...");
			Thread.Sleep(1000);
			Console.WriteLine("1...");
			Thread.Sleep(1000);
			return;
		}

		// Cause typing looks better
		public static void Type(string text)
		{
			string[] words = text.Split(' ');
			foreach (string word in words)
			{
				if (word == "br") 
				{
					Console.WriteLine(" ");
					continue;
				}
				foreach (char letter in word)
				{
					Console.Write(letter);
					Random random = new Random();
					int WaitTime;
					char[] punctuation = {'.','!','?', ','};
					if (punctuation.Contains(letter)) 
					{
						WaitTime = 200;
					}
					else 
					{
						WaitTime = random.Next(20,90);
					}
					Thread.Sleep(WaitTime);

				}
				if (word != words[words.Length-1])
				{
					Console.Write(" ");
				}
				Thread.Sleep(50);
			}
		}
	}

	public class Game 
	{
		// First card to match
		private Tuple<string, int> _card1;
		public Tuple<string, int> Card1
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
		private Tuple<string, int> _card2;
		public Tuple<string, int> Card2
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
		// Just a deck of cards
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
		// Game is over
		public bool _gameOver = false;

		
		//--METHODS--------------------

		
		// Makes a deck of cards as tuples and deals some out to each player and each stack
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
			// Deals 6 cards to each stack
			Stack1 = CommonMethods.Deal(Deck,6,check);
			Stack2 = CommonMethods.Deal(Deck,6,check);
			// Deals 20 cards to each player, from which 4 are dealt to each players hand
			Player1.Stack = CommonMethods.Deal(Deck,20,check); Player1.InitHand();
			Player2.Stack = CommonMethods.Deal(Deck,20,check); Player2.InitHand();
		}

		// Draws new cards from deck when game is starting or there are no matches from both players
		public void NewCards() 
		{
			// If there are no cards left to draw from, the game ends
			if (Stack1.Count == 0) { CommonMethods.Type("\n\nNo more cards! Let's see who won...\n\n"); this._gameOver = true; return; }
			
			// Reset no matches for each player because there are new cards
			Player1._noMatches = false; Player2._noMatches = false;
			
			// Draw new card from each stack
			HashSet<int> check1 = new HashSet<int>(); HashSet<int> check2 = new HashSet<int>();
			Card1 = CommonMethods.Deal(Stack1,1,check1)[0]; Card2 = CommonMethods.Deal(Stack2,1,check2)[0];
			Stack1.Remove(Card1); Stack2.Remove(Card2);
			
			// Viewing sequence with new cards
			CommonMethods.Countdown();
			CommonMethods.ViewCards(Card1,Card2);
			CommonMethods.ViewHand(Player1);
		}

		// This gets called whenever timer goes off, lets Player 2 have turn
		public void Player2Turn(object source, ElapsedEventArgs e) 
		{
			Player2.ComputerTurn(this);
			
			CommonMethods.ViewCards(Card1, Card2);
			CommonMethods.ViewHand(Player1);
		}

		// Called when game is over
		public void EndGame()
		{
			string winner;
			int p1CardsLeft = Player1.Hand.Count + Player1.Stack.Count;
			int p2CardsLeft = Player2.Hand.Count + Player2.Stack.Count;

			// Decide a winner
			if (Player1._finished || (p1CardsLeft < p2CardsLeft)) { winner = Player1.Name; }
			else { winner = Player2.Name; }

			// Print winner and stats
			CommonMethods.Type("Looks like our winner is.....");CommonMethods.Type(winner);CommonMethods.Type("!!!");
			Console.WriteLine("\n\n ----End game stats--- ");
			
			Console.WriteLine("{0}'s remaining cards: ",Player1.Name); CommonMethods.ViewHand(Player1, true);
			Console.WriteLine("{0}'s remaining cards: ",Player2.Name); CommonMethods.ViewHand(Player2, true);
		}

		// Where each game takes place
		public void NewGame()
		{
			//Initializing players and game
			Player player1 = new Player(); Player player2 = new Player();
			Player1 = player1; Player2 = player2;
			InitDeck();
			CommonMethods.Type("Hello contestant! What's your name?: ");
			var name = Console.ReadLine();
			player1.Name = name;
			CommonMethods.Type("Now what do you want your opponent to be called?: ");
			var name2 = Console.ReadLine();
			player2.Name = name2;
			CommonMethods.Type("Alright, ");CommonMethods.Type(player1.Name);CommonMethods.Type(", here is your hand!\n"); Thread.Sleep(500);
			CommonMethods.ViewHand(player1); Thread.Sleep(500);
			NewCards();

			// Initialize timer for player2
			System.Timers.Timer timer = new System.Timers.Timer();
			timer.Elapsed += new ElapsedEventHandler(Player2Turn);
			timer.Interval = 1500;
			timer.Enabled = true;

			
			while (true) 
			{
				var key = Console.ReadKey();

				// Press a to refill your hand
				if (key.KeyChar == 'a') 
				{ 
					timer.Stop();
					player1.RefillHand();

					timer.Start();
					CommonMethods.ViewCards(Card1, Card2);
					CommonMethods.ViewHand(player1);
				}
				//Press suit letter to get rid of a card
				if (key.KeyChar == 'h' || key.KeyChar == 's' || key.KeyChar == 'c' || key.KeyChar == 'd')
				{
					timer.Stop();
					player1.PlayCard(key.KeyChar, this);

					timer.Start();
					CommonMethods.ViewCards(Card1, Card2);
					CommonMethods.ViewHand(player1);

				}
				// Press p to ask for new cards
				if (key.KeyChar == 'p') 
				{ 
					timer.Stop();
					player1.AdmitDefeat();

					timer.Start();
					CommonMethods.ViewCards(Card1, Card2);
					CommonMethods.ViewHand(player1);
				}

				// If players both need card, get new cards
				if (player1._noMatches == true && _player2._noMatches == true)
				{
					timer.Stop();
					NewCards();
					timer.Start();
				}

				//If cards run out, game over
				if (player1._finished || player2._finished || _gameOver)
				{
					timer.Stop();
					break;
				}
			}
			EndGame();
		}

	}

	public class Player 
	{
		// Cards in your hand
		private List<Tuple<string,int>> _hand;
		public List<Tuple<string,int>>  Hand
		{
			get { return this._hand; }
			set { this._hand = value; }
		}
		// Cards to replace used cards in your hand 
		private List<Tuple<string,int>> _stack;
		public List<Tuple<string,int>>  Stack
		{
			get { return this._stack; }
			set { this._stack = value; }
		}
		// Name of player
		private string _name;
		public string Name
		{
			get { return this._name; }
			set { this._name = value; }
		}

		// Flag to say player wants to draw new card from stack
		public bool _noMatches = false;

		//Flag to say player has used all of their cards
		public bool _finished = false;


		//--METHODS--------------------

		
		//Draws first hand from deck
		public void InitHand() 
		{
			HashSet<int> check = new HashSet<int>();	
			this._hand = CommonMethods.Deal(Stack,4,check);
			foreach (Tuple<string, int> card in Hand) { Stack.Remove(card); }
		}

		//No cards that match
		public void AdmitDefeat() 
		{
			Console.WriteLine("\n\n{0} wants to see a new card!", Name);
			this._noMatches = true;
		}

		//Replaces cards that are missing in hand
		public bool RefillHand()
		{
			HashSet<int> check = new HashSet<int>();
			List<Tuple<string, int>> newHand = new  List<Tuple<string, int>>();
			if (Hand.Count < 4)
			{
				if (Stack.Count == 0)
				{
					CommonMethods.Type(Name);CommonMethods.Type(" has finished their cards!");
					this._finished = true;
					return false;
				}
				else if (Stack.Count < Hand.Count && Stack.Count < 4) { newHand = CommonMethods.Deal(Stack,(4-Stack.Count),check); }
				else { newHand = CommonMethods.Deal(Stack,(4 - Hand.Count),check); }
				
				foreach (Tuple<string, int> card in newHand) { Stack.Remove(card); }

				Hand.AddRange(newHand);
				Hand.OrderBy(v => v).ToList();
			}
			else { Console.WriteLine("\n\nYour hand is already full!"); return false; }
			return true;
		}

		//Tries to put a card in play
		public void PlayCard(char suit_char, Game game)
		{
			// Are there even any cards in your hand you can play?
			if (Hand.Count == 0) { Console.WriteLine("\nYou don't have any cards, draw new ones!"); return; }
			
			// Checks if suit is valid
			string suit = suit_char.ToString();
			Dictionary<string, List<int>> allCards = CommonMethods.AsDictionary(Hand);

			if (allCards[suit].Count == 0) { Console.WriteLine("\n\nYou don't have that card!"); return; }

			//Checks if value is valid
			Console.Write("\n\nSuit: {0}, Value: ", suit);
			string value_str = Console.ReadLine();
			int value = Convert.ToInt32(value_str);

			if (!allCards[suit].Contains(value)) { Console.WriteLine("\n\nYou don't have that card!"); return; }

			// Sees if there is a match
			CheckIfMatch(suit,value,game);			
		}

		//Helper for seeing if there is a match with played card
		public void CheckIfMatch(string suit, int value, Game game)
		{
			Tuple<string, int> card = new Tuple<string, int>(suit, value);

			var compare1 = game.Card1.Item2;
			var compare2 = game.Card2.Item2;

			// Edge cases (13 and 1) have to be dealt with seperately
			if (value == 13)
			{
				if (compare1 == 1 || compare1 == 12) { game.Card1 = card; }
				else if (compare2 == 1 || compare2 == 12) { game.Card2 = card; } 
				else { Console.WriteLine("\n\nThat card isn't a match!"); return; }	
			}
			else if (value == 1)
			{
				if (compare1 == 13 || compare1 == 2) { game.Card1 = card; }
				else if (compare2 == 13 || compare2 == 2) { game.Card2 = card; } 
				else { Console.WriteLine("\n\nThat card isn't a match!"); return; }	
			}
			else 
			{
				if (value == compare1 + 1 || value == compare1 - 1) { game.Card1 = card; }
				else if (value == compare2 + 1 || value == compare2 - 1) { game.Card2 = card; } 
				else { Console.WriteLine("\n\nThat card isn't a match!"); return; }
			}
			Hand.Remove(card);
		}

		//Code controlled Player 2 sequence
		public void ComputerTurn(Game game) 
		{
			// Checks if player 2 is already done
			if (this._finished) { game._gameOver = true; return; }

			var compare1 = game.Card1.Item2;
			var compare2 = game.Card2.Item2;

			string chosenSuit = "none";
			int chosenValue = 0;
			//1. See if you can play a card
			foreach (Tuple<string, int> card in Hand)
			{
				string suit = card.Item1;
				int value = card.Item2;
				if (value == 13)
				{
					if (compare1 == 1 || compare1 == 12) { chosenSuit = suit; chosenValue = value; break; }
					else if (compare2 == 1 || compare2 == 12) { chosenSuit = suit; chosenValue = value; break; } 
					else { continue; }	
				}
				else if (value == 1)
				{
					if (compare1 == 13 || compare1 == 2) { chosenSuit = suit; chosenValue = value; break; }
					else if (compare2 == 13 || compare2 == 2) { chosenSuit = suit; chosenValue = value; break; } 
					else { continue; }	
				}
				else 
				{
					if (value == compare1 + 1 || value == compare1 - 1) { chosenSuit = suit; chosenValue = value; break; }
					else if (value == compare2 + 1 || value == compare2 - 1) { chosenSuit = suit; chosenValue = value; break; } 
					else { continue; }
				}
			}

			if (chosenSuit != "none") 
			{
				Console.WriteLine("\n\n{0} has placed a card!", Name);
				CheckIfMatch(chosenSuit, chosenValue, game);
				return;
			}

			//2. If you can't play a card, try refilling hand
			if (RefillHand()) { Console.WriteLine("\n\n{0} has refilled their hand!", Name); return; }
			//3. If you can't do either, AdmitDefeat
			else { AdmitDefeat(); }
		}	

	}


	public class PlayGame
	{		
		public static void Main() 
		{
			CommonMethods.Type("Do you want to play Speed?(y/n): ");
			var ans = Console.ReadLine();
			while (ans == "y") 
			{
				Game game = new Game();
				game.NewGame();
				CommonMethods.Type("\nWant to play again?(y/n): ");
				ans = Console.ReadLine();
			}
			
			
		}

	}
}