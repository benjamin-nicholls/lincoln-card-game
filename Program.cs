using System;
using System.Collections.Generic;

namespace oop3 {
    class Program {
        static void Main() {

            // Allows for custom handsizes.
            int UserResponse = GetWantedHandsize();

            LincolnCardGame Lincoln;
            if (UserResponse == 0) {
                Lincoln = new LincolnCardGame();
            } else {
                Lincoln = new LincolnCardGame(UserResponse);
            }
            
            Lincoln.StartGame();

        }


        public static int GetWantedHandsize() {
            int UserResponse;
            while (true) {
                Console.WriteLine("How many cards do you want each player to have? (1-26, 0 for default)");

                try {
                    UserResponse = Convert.ToInt16(Console.ReadLine());
                    if ((UserResponse >= 0) && (UserResponse < 27)) {
                        break;
                    }
                    Console.WriteLine("Invalid input.\n");
                } catch (FormatException e) {
                    Console.WriteLine($"Invalid input. {e}\n");
                } catch {
                    Console.WriteLine("Invalid input.\n");
                }
            }
            return UserResponse;
        }
                    
    }
}