using System;
using System.Collections.Generic;

namespace oop3 {
    class LincolnCardGame {
        // Normal 52 card deck.
        private Deck _deck = new Deck();
        // Stores both player objects.
        private List<Player> _players = new List<Player>();

        private bool _computerOpponent;
        // Used to display information back to the user.
        private string _lastAction = "Game started.";
        // Number of cards each player is dealt.
        private int _handsize;
        // Used to 
        private int _overflowRounds;
        // Keeps track of which player is to play.
        private int _turnSelector;
        // Counts how many rounds there are.
        private int _turnCounter;


        public LincolnCardGame() {
            _handsize = 10;
            _overflowRounds = 0;
            _turnSelector = 0;
            _turnCounter = 1;
        }


        // Overloaded constructor to allow for custom-handsizes games.
        public LincolnCardGame(int handsize) {
            _handsize = handsize;
            _overflowRounds = 0;
            _turnSelector = 0;
            _turnCounter = 1;
        }


        public void StartGame() {
            // Loop whilst user wants to keep playing.
            do {
                ChooseComputerOpponent();

                Player p1 = new Player();
                _players.Add(p1);

                if (_computerOpponent) {
                    Player p2 = new Computer();
                    _players.Add(p2);
                } else {
                    Player p2 = new Player();
                    _players.Add(p2);
                }

                // Shuffle and deal cards to both players.
                DealStartingCards();
                // Ensure player(s) are ready to begin.
                StartScreen();

                while (!WinCondition()) {
                    for (int a = 1; a <= 2; a++) {
                        // Warn users everytime player switches.
                        ChangingPlayerScreen();
                       
                        Player player = _players[_turnSelector];

                        // Once all cards are used it's a 1 card vs 1 card luck game.
                        if (player.GetHandSize() == 0) {
                            _lastAction = "No more cards remaining. Drawing one card from the deck.";
                            Card c = _deck.DealCard();
                            player.AddCardToHand(c);
                        }

                        ChooseCardToPlay(player);

                        _turnSelector = FlipBooleanInt(_turnSelector);
                    }
                    // Give round results.
                    RoundEndingScreen();
                    _turnCounter++;
                }
            } while (GameEndingScreen());  
        }


        // Play 2 cards (or 1 if player is out of cards).
        private void ChooseCardToPlay(Player player) {
            for (int a = 1; a <= 2; a++) {
                // If no more cards, break out of the loop.
                if (player.GetHandSize() == 0) { break; }

                DrawHeader();
                Console.WriteLine($"Round: {_turnCounter}. Player {_turnSelector + 1} to play.\n");
                
                player.PrintHand();
                Card c = player.ChooseCardToPlay();
                _lastAction = $"{c} is your first card.";
            }
        }


        // User input for computer AI or another player opponent.
        private void ChooseComputerOpponent() {
            string UserResponse;

            bool flag1 = true;
            while (flag1) {
                DrawHeader();
                Console.WriteLine("Do you want to play against the computer? (Y/N):");

                try {
                    UserResponse = Console.ReadLine().Trim().ToLower();
                } catch {
                    UserResponse = "-1";
                }

                if (UserResponse == "y") {
                    flag1 = false;
                    _computerOpponent = true;
                } else if (UserResponse == "n") {
                    flag1 = false;
                    _computerOpponent = false;
                } else {
                    _lastAction = "ERROR: Invalid input.";
                }
            }
        }


        // Deal cards to the players according to handsize.
        private void DealStartingCards() {
            _deck.Shuffle();
            for (int a = 0; a < _handsize; a++) {
                foreach (Player player in _players) {
                    Card c = _deck.DealCard();
                    player.AddCardToHand(c);
                }
            }
            _lastAction = $"Each player has been dealt {_handsize} cards.";
        }


        private void StartScreen() {
            DrawHeader();

            if (_computerOpponent) {
                Console.WriteLine("Are you ready to play?");
            } else {
                Console.WriteLine("Are both players ready to play?");
            }

            PressAnyKeyToContinue();
        }


        // Informs the players to switch around (so they cannot see each other's cards).
        private void ChangingPlayerScreen() {
            if (!_computerOpponent) {
                _lastAction = $"Changing to player {_turnSelector + 1}.";
                DrawHeader();
                PressAnyKeyToContinue();
            }
            _lastAction = "Choose two cards to play. Highest pairing wins.";
        }


        // Deals with determining who wins the round and clears up any previous overflow rounds.
        private void RoundEndingScreen() {
            int winningPlayer;

            if (_players[0] > _players[1]) {
                winningPlayer = 1;
            } else if (_players[0] < _players[1]) {
                winningPlayer = 2;
            } else {
                winningPlayer = -1;
            }

            if (winningPlayer > 0) {
                // Assign win to winning player.
                _players[winningPlayer - 1].WinRound();
                _lastAction = $"Player {winningPlayer} won round {_turnCounter}.";

                // Assign overflow rounds to winning player.
                for (int a = 0; a < _overflowRounds; a++) {
                    _players[winningPlayer - 1].WinRound();
                }

                if (_overflowRounds > 0) {
                    _lastAction += $" Player {winningPlayer} also won {_overflowRounds} from previous drawed rounds.";
                    _overflowRounds = 0;
                }
                
            } else {
                // Draw.
                _overflowRounds++;
                _lastAction = "Round was a draw! Next player to win will win this round too!";
            }

            // Show current score and hand of both players for this round.

            DrawHeader();
            Console.WriteLine($"Player 1 wins: {_players[0].NumberOfWins}.");
            Console.WriteLine($"Player 2 wins: {_players[1].NumberOfWins}.\n");

            for (int a = 0; a <= 1; a++) {
                Console.WriteLine($"Player {a + 1}'s hand: ");
                foreach (Card card in _players[a].CurrentRound) {
                    Console.WriteLine("    " + card);
                }
                Console.WriteLine();
            }

            foreach (Player player in _players) {
                player.ClearCurrentRound();
            }

            PressAnyKeyToContinue();
        }


        // Prompt user if they want to replay. Loops at do while in StartGame.
        private bool GameEndingScreen() {
            string UserResponse;
            _lastAction = "Game has ended. Do you want to play again? Y/N: ";
            DrawHeader();

            try {
                UserResponse = Console.ReadLine().Trim().ToLower();
            } catch {
                return false;
            }

            if (UserResponse == "y") {
                _overflowRounds = 0;
                _turnSelector = 0;
                _turnCounter = 1;
                _lastAction = "Game started.";

                foreach (Player player in _players) {
                    player.ResetAll();
                }

                return true;
            }
            return false;
        }


        // Win condition: No more cards left, not drawing.
        private bool WinCondition() {
            if ((_players[0].GetHandSize() == 0) && (_overflowRounds == 0)) {
                if (_players[0].NumberOfWins == _players[1].NumberOfWins) {
                    return false;
                }
                return true;
            }
            return false;
        }


        private void PressAnyKeyToContinue() {
            Console.WriteLine("Press ENTER to continue.");
            Console.Write("> ");
            Console.ReadLine();
        }


        private void DrawHeader() {
            const string MENUHEADER = "\n - - - - - - - - - - - - - - - Lincoln Card Game - - - - - - - - - - - - - - - \n";
            Console.Clear();
            Console.WriteLine(MENUHEADER);
            Console.WriteLine($">>> {_lastAction}\n");
        }


        // 0 --> 1 and 1 --> 0
        private int FlipBooleanInt(int a) {
            return a ^ 1;
        }

    }
}
