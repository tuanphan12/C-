using System;

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
		private string[] _deck;


		public static void Main() 
		{
			Console.WriteLine("Let's build speed!");
		}


	}

	class Player 
	{
		// Cards in your hand
		private string[] _cards;
		// Cards to replace used cards in your hand 
		private string[] _stack;

	}
}