using System;
using System.Collections.Generic;

namespace oop3 {
    class Hand {
        //entire class currently unused
        private List<Card> _hand;

        public Hand() {
            _hand = new List<Card>();


        }

        public int GetHandSize() {
            return _hand.Count;
        }



    }
}
