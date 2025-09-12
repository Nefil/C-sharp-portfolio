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
        public List<Card> Table;

        // Konstruktor Talii
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

        // Metoda tasowania talii
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
            // Używamy statycznej metody AddCard do dodania 3 kart na stół
            AddCard(cards, cards, Table, 3);
            
            Console.WriteLine("Cards on table:");
            foreach (Card card in Table)
            {
                Console.WriteLine($"{card.rank} of {card.suit}");
            }
        }

        // Metoda do kopiowania 3 kart ze stołu do ręki gracza (bez usuwania ze stołu)
        public void CopyCardsFromTable(List<Card> playerHand)
        {
            if (Table == null || playerHand == null || Table.Count < 3)
            {
                Console.WriteLine("Nie można skopiować kart - niewystarczająca liczba kart na stole lub błędne listy.");
                return;
            }

            for (int i = 0; i < 3; i++)
            {
                playerHand.Add(Table[i]);
            }
            
        }

        // Metoda do dodania 2 kart z talii do ręki gracza
        public void DealCardsToPlayer(List<Card> playerHand)
        {
            // Używamy metody AddCard do dodania 2 kart z talii do ręki gracza
            AddCard(cards, cards, playerHand, 2);
        }

        public static void AddCard(List<Card> someList, List<Card> deckList, List<Card> Hand, int ammount)
        {
            if (someList == null || Hand == null || someList.Count < ammount)
            {
                Console.WriteLine("Nie można dodać kart - niewystarczająca liczba kart lub błędne listy.");
                return;
            }

            for (int i = 0; i < ammount; i++)
            {
                Hand.Add(someList[0]);
                deckList.RemoveAt(0);
            }
           
        }
    }
}
