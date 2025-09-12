namespace PokerCardGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("Player");
            Player AI = new Player("AI");
            Deck deck = new Deck();
            deck.Shuffle();
            
            // Najpierw rozdajemy karty na stół
            deck.DealToTable();

            // Kopiujemy karty ze stołu do ręki gracza
            deck.CopyCardsFromTable(AI.Hand);
            deck.CopyCardsFromTable(player.Hand);
            
            // Dodajemy 2 karty z talii do ręki gracza
            deck.DealCardsToPlayer(player.Hand);
            deck.DealCardsToPlayer(AI.Hand);

            // Wyświetlamy karty w ręce gracza
            player.DisplayHand();
            AI.DisplayHand();
        }
    }
}
