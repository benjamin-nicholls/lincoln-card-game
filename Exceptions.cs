using System;

namespace oop3 {
    public class DeckEmptyException : Exception {

        private static string _message = "The deck is empty.No card can be dealt from it.";


        public DeckEmptyException() {
        }


        public DeckEmptyException(string message)
            : base(_message) {
        }


        public DeckEmptyException(Exception inner)
            : base(_message, inner) {
        }
    }

    public class CurrentRoundException : Exception {

        private static string _message = "A card in the deck is corrupt. A total cannot be calculated.";


        public CurrentRoundException() {
        }


        public CurrentRoundException(string message)
            : base(_message) {
        }


        public CurrentRoundException(Exception inner)
            : base(_message, inner) {
        }
    }
}
