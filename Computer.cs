using System;

namespace oop3 {
    class Computer : Player {

        // Play first card in the computer's hand.
        public override Card ChooseCardToPlay() {
            return PlayCardFromHand(0);
        }

    }
}
