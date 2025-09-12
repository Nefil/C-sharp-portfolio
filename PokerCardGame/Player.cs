using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerCardGame
{
    internal class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }

        public Player(string name)
        {
            Name = name;
            Hand = new List<Card>();
        }

        public void DisplayHand()
        {
            Console.WriteLine($"Ostatnie 2 karty w ręce gracza {Name}:");
            
            if (Hand.Count == 0)
            {
                Console.WriteLine("Gracz nie ma żadnych kart.");
                return;
            }
            
            // Wyświetl tylko ostatnie 2 karty (lub mniej, jeśli nie ma 2 kart)
            int startIndex = Math.Max(0, Hand.Count - 2);
            for (int i = startIndex; i < Hand.Count; i++)
            {
                Card card = Hand[i];
                Console.WriteLine($"{card.rank} of {card.suit}");
            }
        }
    }
}
