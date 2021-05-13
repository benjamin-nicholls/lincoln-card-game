using System;
using System.Collections.Generic;

namespace oop3 {
    class Player {

        private List<Card> _hand = new List<Card>();
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


        // Adds all cards in current round.
        public int CurrentRoundValue() {
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


        public Card PlayCardFromHand(int index) {
            Card card = _hand[index];
            _hand.RemoveAt(index);
            _currentRound.Add(card);
            return card;
        }


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
    }
}
