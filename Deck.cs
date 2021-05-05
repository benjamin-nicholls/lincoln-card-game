using System;
using System.Collections.Generic;

namespace oop3 {
    class Deck {
        // Index 0 is the card on the bottom of the deck.
        private readonly List<Card> _DeckOfCards = new List<Card>();
        // Index 0 is the oldest card. List currently unused but here for future implementation.
        private readonly List<Card> _DiscardPileOfCards = new List<Card>();

        private readonly string[] _Suits = { "Diamonds", "Clubs", "Hearts", "Spades" };


        public Deck() {
            RepopulateDeck();
        }


        // Empty deck method.
        private void PurgeDeck() {
            _DeckOfCards.Clear();
        }


        // Empty deck and then re-add cards.
        public string RepopulateDeck() {
            PurgeDeck();
            foreach (string suit in _Suits) {
                for (int i = 1; i < 14; i++) {
                    Card c = new Card(suit, i);
                    _DeckOfCards.Add(c);
                }
            }
            return "Deck has been reset.";
        }


        private bool IsEmpty() {
            if (_DeckOfCards.Count > 0) { return false; }
            return true;
        }


        // Actually shuffle the deck rather than randomising which card is dealt to simulate an actual deck of cards.
        public string Shuffle() {
            if (IsEmpty()) { return "No more cards in the deck!"; }
            // Swap each card with a random card within the deck.
            Random r = new Random();
            for (int a = 0; a < _DeckOfCards.Count; a++) {
                int b = r.Next(0, _DeckOfCards.Count - 1);
                Card TempCard = _DeckOfCards[a];
                _DeckOfCards[a] = _DeckOfCards[b];
                _DeckOfCards[b] = TempCard;
            }
            return "The deck has been shuffled.";
        }


        // Returns card (as a string) at the top of the 'stack' of cards.
        public string Deal() {
            if (IsEmpty()) { return "No more cards in the deck!"; }
            _DiscardPileOfCards.Add(_DeckOfCards[^1]);
            string CardDealt = _DeckOfCards[^1].GetDisplayName();
            _DeckOfCards.RemoveAt(_DeckOfCards.Count - 1);
            return CardDealt;
        }


        // Returns card (as the Card object) at the top of the 'stack' of cards.
        public Card DealCard() {
            if (IsEmpty()) { return null; }
            _DiscardPileOfCards.Add(_DeckOfCards[^1]);
            Card CardDealt = _DeckOfCards[^1];
            _DeckOfCards.RemoveAt(_DeckOfCards.Count - 1);
            return CardDealt;
        }


        // Used for visual representation.
        public void PrintCurrentDeckBySuit() {
            for (int i = 1; i <= 13; i++) {
                foreach (string suit in _Suits) {
                    Card TempCard = new Card(suit, i);
                    string TempName = "";
                    foreach (Card card in _DeckOfCards) {
                        if (TempCard.GetDisplayName() == card.GetDisplayName()) {
                            TempName = card.GetDisplayName();
                        }
                    }
                    while (TempName.Length < 20) { TempName += " "; }
                    Console.Write(TempName);
                }
                Console.Write("\n");
            }
        }
    }
}
