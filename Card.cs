using System;
using System.Collections.Generic;
using System.Linq;

namespace oop3 {
    class Card {

        private string _Suit;
        private int _Value;
        private Dictionary<int, string> _CardNames = new Dictionary<int, string>() {
            { 1, "Ace" }, { 2, "Two" }, { 3, "Three"}, { 4, "Four" }, { 5, "Five" }, { 6, "Six" }, { 7, "Seven" },
            { 8, "Eight" }, { 9, "Nine" }, { 10, "Ten" }, { 11, "Jack" }, { 12, "Queen" }, { 13, "King" } };
        
        public int Value {
            get { return _Value; }
        }


        public Card(string Suit, int Value) {
            _Suit = Suit;
            _Value = Value;
        }


        // Return a display name e.g. "Two of Clubs".
        public string GetDisplayName() {
            return $"{_CardNames.ElementAt(_Value - 1).Value} of {_Suit}";
        }

    }
}