namespace PokerCardGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Poker Card Game!\nWhats your name?");
            string playerName = Console.ReadLine();

            Player player = new Player(playerName);
            player.Wallet = 1000; // Startowy portfel 1000
            

            LogicAI AI = new LogicAI("AI");
            
            bool continuePlaying = true;
            
            while (continuePlaying && player.Wallet > 0)
            {
                Console.WriteLine($"\n--- New Deal ---\nYour wallet: {player.Wallet} $");
                Console.WriteLine("Stawka: 200 zł");
                
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

                // Sprawdzamy, kto wygrał
                int playerHandValue = GameLogic.CheckHand(player.Hand);
                int aiHandValue = GameLogic.CheckHand(AI.Hand);

                // Pobieramy nazwy układów
                string playerHandName = GameLogic.GetHandName(playerHandValue);
                string aiHandName = GameLogic.GetHandName(aiHandValue);

                bool playerWon = false;

                if (playerHandValue > aiHandValue)
                {
                    Console.WriteLine($"Wygrałeś! Twój układ jest lepszy: {playerHandName}");
                    Console.WriteLine($"AI miał: {aiHandName}");
                    playerWon = true;
                }
                else if (playerHandValue < aiHandValue)
                {
                    Console.WriteLine($"Przegrałeś. AI ma lepszy układ: {aiHandName}");
                    Console.WriteLine($"Ty miałeś: {playerHandName}");
                    playerWon = false;
                }
                else // Jeśli układy są równoważne, sprawdź kickers
                {
                    Console.WriteLine($"Obaj macie ten sam układ: {playerHandName}");
                    
                    // Porównaj kickers - sortujemy karty malejąco po wartości
                    var playerSortedCards = player.Hand.OrderByDescending(c => c.rank).ToList();
                    var aiSortedCards = AI.Hand.OrderByDescending(c => c.rank).ToList();
                    
                    bool isDraw = true;
                    
                    // Porównujemy kolejno karty od najwyższej
                    for (int i = 0; i < playerSortedCards.Count; i++)
                    {
                        if (playerSortedCards[i].rank > aiSortedCards[i].rank)
                        {
                            Console.WriteLine($"Wygrałeś! Masz wyższą kartę: {GameLogic.GetCardName(playerSortedCards[i].rank)}");
                            isDraw = false;
                            playerWon = true;
                            break;
                        }
                        else if (playerSortedCards[i].rank < aiSortedCards[i].rank)
                        {
                            Console.WriteLine($"Przegrałeś. AI ma wyższą kartę: {GameLogic.GetCardName(aiSortedCards[i].rank)}");
                            isDraw = false;
                            playerWon = false;
                            break;
                        }
                    }
                    
                    if (isDraw)
                    {
                        Console.WriteLine("Remis! Wasze układy i wszystkie karty są identyczne.");
                        playerWon = true; // W remisie gracz nie przegrywa pieniędzy
                    }
                }

                // Aktualizacja portfela
                if (!playerWon)
                {
                    player.Wallet -= 200;
                    Console.WriteLine($"Przegrałeś 200 zł. Twój portfel: {player.Wallet} zł");
                }
                else
                {
                    player.Wallet += 200;
                    Console.WriteLine($"Wygrałeś 200 zł! Twój portfel: {player.Wallet} zł");
                }

                // Czyszczenie rąk przed nową grą
                player.Hand.Clear();
                AI.Hand.Clear();

                // Sprawdzenie czy gracz chce kontynuować
                if (player.Wallet <= 0)
                {
                    Console.WriteLine("Twój portfel jest pusty! Koniec gry.");
                    break;
                }

                Console.WriteLine("Czy chcesz zakończyć grę? (y/n)");
                string response = Console.ReadLine().ToLower();
                continuePlaying = response != "y";
            }

            Console.WriteLine("Dziękujemy za grę!");
        }
    }
}
