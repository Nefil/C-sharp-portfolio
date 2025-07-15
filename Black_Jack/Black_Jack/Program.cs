using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        int wallet = 400;
        bool wantsToPlay = true;

        Dictionary<string, int> cardValues = new Dictionary<string, int>()
        {
            { "2", 2 }, { "3", 3 }, { "4", 4 }, { "5", 5 },
            { "6", 6 }, { "7", 7 }, { "8", 8 }, { "9", 9 },
            { "10", 10 }, { "J", 10 }, { "Q", 10 }, { "K", 10 },
            { "A", 11 }
        };

        Random random = new Random();

        Console.WriteLine("Welcome to Blackjack!");
        Console.WriteLine($"Your starting wallet amount is: {wallet}$");
        Console.WriteLine("Good luck and enjoy your game. You need 200$ to start.");
        Console.WriteLine("Type 'start' and press Enter to begin.");

        string startGame = Console.ReadLine()?.Trim();

        if (startGame?.ToLower() == "start")
        {
            while (wallet >= 200 && wantsToPlay)
            {
                wallet -= 200;
                List<KeyValuePair<string, int>> playerHand = new List<KeyValuePair<string, int>>();
                List<KeyValuePair<string, int>> dealerHand = new List<KeyValuePair<string, int>>();

                // Initial deal
                for (int i = 0; i < 2; i++)
                {
                    var playerCard = cardValues.ElementAt(random.Next(cardValues.Count));
                    playerHand.Add(playerCard);

                    var dealerCard = cardValues.ElementAt(random.Next(cardValues.Count));
                    dealerHand.Add(dealerCard);
                }

                // Check for blackjack
                int playerSum = SumHand(playerHand);
                int dealerSum = SumHand(dealerHand);

                bool playerBlackjack = (playerHand.Count == 2 && playerSum == 21);
                bool dealerBlackjack = (dealerHand.Count == 2 && dealerSum == 21);

                Console.WriteLine("\nYour hand:");
                foreach (var card in playerHand)
                    Console.WriteLine($"- {card.Key}");

                Console.WriteLine($"Dealer shows: {dealerHand[0].Key}");

                if (playerBlackjack || dealerBlackjack)
                {
                    if (playerBlackjack && dealerBlackjack)
                    {
                        Console.WriteLine("Both have blackjack! It's a tie! You get your money back.");
                        wallet += 200;
                    }
                    else if (playerBlackjack)
                    {
                        Console.WriteLine("Blackjack! You win and gain 300$.");
                        wallet += 500; // 200$ stake refund + 300$ winnings
                    }
                    else
                    {
                        Console.WriteLine("Dealer has blackjack! You lose 200$.");
                    }
                    ShowWallet(wallet);
                    if (!PlayAgainPrompt(ref wantsToPlay)) break;
                    continue;
                }

                // Player's turn
                bool playerTurn = true;
                while (playerTurn)
                {
                    Console.WriteLine("Do you want to choose another card? yes/no");
                    string choice = Console.ReadLine()?.Trim().ToLower();
                    if (choice == "yes")
                    {
                        var newCard = cardValues.ElementAt(random.Next(cardValues.Count));
                        playerHand.Add(newCard);
                        Console.WriteLine("Your hand:");
                        foreach (var card in playerHand)
                            Console.WriteLine($"- {card.Key}");

                        FixAces(playerHand);
                        playerSum = SumHand(playerHand);

                        if (playerSum > 21)
                        {
                            Console.WriteLine("You busted!");
                            playerTurn = false;
                        }
                    }
                    else if (choice == "no")
                    {
                        playerTurn = false;
                    }
                    else
                    {
                        Console.WriteLine("Please type 'yes' or 'no'.");
                    }
                }

                FixAces(playerHand);
                playerSum = SumHand(playerHand);

                // Dealer's turn (draw until minimum 17)
                FixAces(dealerHand);
                dealerSum = SumHand(dealerHand);
                while (dealerSum < 17)
                {
                    var newCardDealer = cardValues.ElementAt(random.Next(cardValues.Count));
                    dealerHand.Add(newCardDealer);
                    FixAces(dealerHand);
                    dealerSum = SumHand(dealerHand);
                }

                // Show hands
                Console.WriteLine($"\nYour total: {playerSum}");
                Console.WriteLine("Dealer's hand:");
                foreach (var card in dealerHand)
                    Console.WriteLine($"- {card.Key}");
                Console.WriteLine($"Dealer's total: {dealerSum}");

                // Determine the result
                if (playerSum > 21)
                {
                    Console.WriteLine("You busted! You lose 200$.");
                }
                else if (dealerSum > 21)
                {
                    Console.WriteLine("Dealer busted! You win 200$.");
                    wallet += 400;
                }
                else if (playerSum == dealerSum)
                {
                    Console.WriteLine("It's a tie! You get your money back.");
                    wallet += 200;
                }
                else if (playerSum > dealerSum)
                {
                    Console.WriteLine("You win! You gain 200$.");
                    wallet += 400;
                }
                else
                {
                    Console.WriteLine("Dealer wins! You lose 200$.");
                }

                ShowWallet(wallet);

                if (wallet < 200)
                {
                    Console.WriteLine("You don't have enough money to play again. The game ends.");
                    break;
                }

                if (!PlayAgainPrompt(ref wantsToPlay))
                {
                    Console.WriteLine($"Thank you for playing! Your final wallet amount is: {wallet}$");
                    break;
                }
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(intercept: true);
        }
        else
        {
            Console.WriteLine("Game did not start. Please type 'start' next time.");
        }
    }

    static int SumHand(List<KeyValuePair<string, int>> hand)
    {
        return hand.Sum(card => card.Value);
    }

    static void FixAces(List<KeyValuePair<string, int>> hand)
    {
        while (SumHand(hand) > 21 && hand.Any(card => card.Key == "A" && card.Value == 11))
        {
            int aceIndex = hand.FindIndex(card => card.Key == "A" && card.Value == 11);
            if (aceIndex != -1)
                hand[aceIndex] = new KeyValuePair<string, int>("A", 1);
        }
    }

    static void ShowWallet(int wallet)
    {
        Console.WriteLine($"\nYour current wallet amount is: {wallet}$");
    }

    static bool PlayAgainPrompt(ref bool wantsToPlay)
    {
        while (true)
        {
            Console.WriteLine("Do you want to play again? yes/no");
            string ending = Console.ReadLine()?.Trim().ToLower();
            if (ending == "yes")
            {
                wantsToPlay = true;
                return true;
            }
            else if (ending == "no")
            {
                wantsToPlay = false;
                return false;
            }
            else
            {
                Console.WriteLine("Please type 'yes' or 'no'.");
            }
        }
    }
}
