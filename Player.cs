using System;
using System.Collections.Generic;

namespace oop3 {
    class Player : IEquatable<Player> {
        // All cards a player has access to.
        private List<Card> _hand = new List<Card>();
        // Used for cards currently being played.
        private List<Card> _currentRound = new List<Card>();

        private int _numberOfWins;

        public List<Card> CurrentRound {
            get { return _currentRound; }
        }

        public int NumberOfWins {
            get { return _numberOfWins; }
        }


        public Player() {
            _numberOfWins = 0;
        }


        public int GetHandSize() {
            return _hand.Count;
        }


        public void ResetAll() {
            _hand.Clear();
            _currentRound.Clear();
            _numberOfWins = 0;
        }


        // Adds all card values in current round.
        protected int CurrentRoundValue() {
            int totalValue = 0;
            foreach (Card card in _currentRound) {
                // Ace is worth 14, not 1
                if (card.Value == 1) {
                    totalValue += 14;
                } else {
                    totalValue += card.Value;
                }
            }
            return totalValue;
        }


        public void WinRound() {
            _numberOfWins++;
        }


        public void AddCardToHand(Card card) {
            _hand.Add(card);
        }


        public void ClearCurrentRound() {
            _currentRound.Clear();
        }


        public void PrintHand() {
            for (int a = 0; a < _hand.Count; a++) {
                Console.WriteLine($"{a + 1}. {_hand[a]}.");
            }
        }


        // Removes card at specified index from hand and returns the card object.
        protected Card PlayCardFromHand(int index) {
            Card card = _hand[index];
            _hand.RemoveAt(index);
            _currentRound.Add(card);
            return card;
        }


        // Prompts the user to select a card from their hand.
        public virtual Card ChooseCardToPlay() {
            int UserResponse;
            while (true) {
                Console.WriteLine("\nWhich card do you want to play?");
                Console.Write("> ");

                try {
                    UserResponse = Convert.ToInt16(Console.ReadLine());
                } catch {
                    UserResponse = 0;
                }

                if ((UserResponse > 0) && (UserResponse <= GetHandSize())) {
                    return PlayCardFromHand(UserResponse - 1);
                } else {
                    Console.WriteLine("Please select a card.\n");
                }
            }
        }

        public bool Equals(Player other) {
            if (other == null) { return false; }

            if (this.CurrentRoundValue() == other.CurrentRoundValue()) {
                return true;
            } else {
                return false;
            }
        }

        public override bool Equals(Object obj) {
            if (obj == null) { return false; }
             Player personObj = obj as Player;
            if (personObj == null) {
                return false;
            } else {
                return Equals(personObj);
            }
        }

        public override int GetHashCode() {
            return this.GetHashCode();
        }


        public static bool operator ==(Player p1, Player p2) {
            if (p1.CurrentRoundValue() == p2.CurrentRoundValue()) {
                return true;
            } else {
                return false;
            }
        }

        public static bool operator !=(Player p1, Player p2) {
            return !(p1 == p2);
        }

        public static bool operator >(Player p1, Player p2) {
            if (p1.CurrentRoundValue() > p2.CurrentRoundValue()) {
                return true;
            } else {
                return false;
            }
        }

        public static bool operator <(Player p1, Player p2) {
            if (p1.CurrentRoundValue() < p2.CurrentRoundValue()) {
                return true;
            } else {
                return false;
            }
        }
    }
}

