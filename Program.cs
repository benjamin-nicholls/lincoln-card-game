using System;
using System.Collections.Generic;

namespace oop3 {
    class Program {
        static void Main() {
            Deck deck = new Deck();
            string LastAction = "Program started.";
            string UserResponse;
            int UserResponseInt;
            int TurnCounter = 1;
            int TurnSelector = 0;
            int OverflowRounds = 0;
            bool ComputerOpponent = false;
            const int HANDSIZE = 10;

            List<Player> Players = new List<Player>();

            bool flag1 = true;
            while (flag1) { 
                Player Player1 = new Player();
                Players.Add(Player1);

                DrawHeader(LastAction);
                Console.WriteLine("Do you want to play against the computer? (Y/N):");

                try {
                    UserResponse = Console.ReadLine().Trim().ToLower();
                } catch {
                    UserResponse = "0";
                }

                if (UserResponse == "y") {
                    // later change this to computer
                    Player Player2 = new Player();
                    Players.Add(Player2);
                    flag1 = false;
                    ComputerOpponent = true;
                } else if (UserResponse == "n") {
                    Player Player2 = new Player();
                    Players.Add(Player2);
                    flag1 = false;
                } else {
                    LastAction = "ERROR: Invalid input.";
                }

            }

            Console.WriteLine(deck.Shuffle());
            for (int a = 0; a < HANDSIZE; a++) {
                foreach (Player player in Players) {
                    player.AddCardToHand(deck.DealCard());
                }
            }
            LastAction = $"Each player has been dealt {HANDSIZE} cards.";

            DrawHeader(LastAction);
            if (ComputerOpponent) {
                Console.WriteLine("Are you ready to play?");
            } else {
                Console.WriteLine("Are both players ready to play?");
            }
            Console.WriteLine("Press any key to continue.");
            Console.Write("> ");
            Console.ReadKey();
            //shuffle deck
            //each player receives 10 cards
            //each player plays 2 cards
            // highest wins
            //can play 2 cards at the same time or play one at a time
            while (TurnCounter <= HANDSIZE / 2) {
                if (!ComputerOpponent) {
                    LastAction = $"Changing to player {TurnSelector + 1}.";
                    DrawHeader(LastAction);
                    Console.WriteLine("Press any key to continue.");
                    Console.Write("> ");
                    Console.ReadKey();
                }
                LastAction = "Choose two cards to play. Highest pairing wins.";

                for (int a = 1; a <= 2; a++) {
                    DrawHeader(LastAction);
                    Console.WriteLine($"Round: {TurnCounter}. Player {TurnSelector + 1} to play.\n");
                    Players[TurnSelector].PrintHand();

                    flag1 = true;
                    while (flag1) {
                        Console.WriteLine("\nWhich card do you want to play?");
                        Console.Write("> ");

                        try {
                            UserResponseInt = Convert.ToInt16(Console.ReadLine());
                        } catch {
                            UserResponseInt = 0;
                        }

                        if (UserResponseInt > 0 && UserResponseInt <= Players[TurnSelector].GetHandSize()) {
                            LastAction = $"{Players[TurnSelector].PlayCardFromHand(UserResponseInt - 1).GetDisplayName()} is your first card.";
                            flag1 = false;
                        } else {
                            Console.WriteLine("Please select a card.\n");
                        }
                    }
                }
                
                if (TurnSelector == 1) {

                    if (Players[0].CurrentRoundValue() == Players[1].CurrentRoundValue()) {
                        // Draw.
                        OverflowRounds++;
                        LastAction = "Round was a draw! Next player to win will win this round too!";

                        if (TurnCounter > HANDSIZE / 2) {
                            Console.WriteLine("Round draw - no more cards remaining. Drawing one card from the deck each.");
                            foreach (Player player in Players) {
                                player.AddCardToHand(deck.DealCard());
                            }

                        }

                    } else if (Players[0].CurrentRoundValue() > Players[1].CurrentRoundValue()) {
                        // Player 1 wins
                        Players[0].WinRound();
                        LastAction = $"Player 1 won round {TurnCounter}.";
                        for (int a = 0; a < OverflowRounds; a++) {
                            Players[0].WinRound();
                        }
                        if (OverflowRounds > 0) {
                            LastAction += $" Player 1 also won {OverflowRounds} from previous drawed rounds.";
                        }
                        

                    } else {
                        // Player 2 wins.
                        Players[1].WinRound();
                        LastAction = $"Player 2 won round {TurnCounter}.";
                        for (int a = 0; a < OverflowRounds; a++) {
                            Players[1].WinRound();
                        }
                        if (OverflowRounds > 0) {
                            LastAction += $" Player 2 also won {OverflowRounds} from previous drawed rounds.";
                        }

                    }

                    DrawHeader(LastAction);
                    Console.WriteLine($"Player 1 wins: {Players[0].NumberOfWins}.");
                    Console.WriteLine($"Player 2 wins: {Players[1].NumberOfWins}.");
                    Console.WriteLine();

                    for (int a = 0; a <= 1; a++) {
                        Console.WriteLine($"Player {a + 1}'s hand: ");
                        foreach (Card card in Players[a].CurrentRound) {
                            Console.WriteLine("    " + card.GetDisplayName());
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine("\nPress any key to continue.");
                    Console.Write("> ");
                    Console.ReadKey();

                    foreach (Player player in Players) {
                        player.ClearCurrentRound();
                    }

                    TurnCounter++;
                }
                TurnSelector = FlipBooleanInt(TurnSelector);
            }
        }

        private static void DrawHeader(string LastAction) {
            const string MENUHEADER = "\n - - - - - - - - - - - - - - - Lincoln Game - - - - - - - - - - - - - - - \n";
            Console.Clear();
            Console.WriteLine(MENUHEADER);
            Console.WriteLine($">>> {LastAction}\n");
        }


        // 0 --> 1 and 1 --> 0
        private static int FlipBooleanInt(int a) {
            return a ^ 1;
        }
    }
}


// NEED TO MAKE IT REPEAT OR CLOSE AT THE END

// method/operator overloading
// use an interface
// inheritance
// custom exceptions
// method overriding
// virtual/abstract methods
// protected access use in classes