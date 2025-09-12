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
        public int Wallet { get; set; } // Dodana właściwość Wallet

        public Player(string name)
        {
            Name = name;
            Hand = new List<Card>();
            Wallet = 0; // Domyślna wartość, będzie nadpisana w Main
        }

        public void DisplayHand()
        {
            Console.WriteLine($"{Name}'s Hand:");
            
            // Wyświetl tylko ostatnie 2 karty (lub mniej, jeśli nie ma 2 kart)
            int startIndex = Math.Max(0, Hand.Count - 2);
            for (int i = startIndex; i < Hand.Count; i++)
            {
                Card card = Hand[i];
                string rankName = GetCardRankName(card.rank);
                Console.WriteLine($"{rankName} of {card.suit}");
            }
        }

        private string GetCardRankName(int rank)
        {
            switch (rank)
            {
                case 14: return "AS";
                case 13: return "King";
                case 12: return "Queen";
                case 11: return "Jack";
                default: return rank.ToString();
            }
        }
    }
}
