using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerCardGame
{
    internal class Deck
    {
        public Random random = new Random();
        private List<Card> cards;
        private List<Card> Table;

        public Deck()
        {
            cards = new List<Card>();
            Table = new List<Card>(); 
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };

            foreach (string suit in suits)
            {
                for (int rank = 2; rank <= 14; rank++)
                {
                    cards.Add(new Card(rank, suit));
                }
            }
        }

        public void Shuffle()
        {
            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);

                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;

            }

        }

        public void DealToTable()
        {      
            for (int i = 0; i < 3; i++)
            {   
                Table.Add(cards[0]);
                cards.RemoveAt(0);
            }

            Console.WriteLine("Cards on deck:");
            foreach (Card card in Table)
            {
                Console.WriteLine($"{card.rank} of {card.suit}");
            }
        }
    }
}
