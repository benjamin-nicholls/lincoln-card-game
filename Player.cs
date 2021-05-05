using System;
using System.Collections.Generic;

namespace oop3 {
    class Player {


        private List<Card> _Hand = new List<Card>();
        private List<Card> _CurrentRound = new List<Card>();
        public List<Card> CurrentRound {
            get { return _CurrentRound; }
        }
        private int _NumberOfWins;
        public int NumberOfWins {
            get { return _NumberOfWins; }
        }
        public Player() {
            _NumberOfWins = 0;
        }

        public int CurrentRoundValue() {
            int totalValue = 0;
            foreach (Card card in _CurrentRound) {
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
            _NumberOfWins++;
        }

        public void AddCardToHand(Card card) {
            _Hand.Add(card);
        }

        public void ClearCurrentRound() {
            _CurrentRound.Clear();
        }

        public void PrintHand() {
            for (int a = 0; a < _Hand.Count; a++) {
                Console.WriteLine($"{a + 1}. {_Hand[a].GetDisplayName()}.");
            }
        }



        public Card PlayCardFromHand(int index) {
            Card card = _Hand[index];
            _Hand.RemoveAt(index);
            _CurrentRound.Add(card);
            return card;
        }


        public int GetHandSize() {
            return _Hand.Count;
        }

    }
}
