using System;
using System.Collections.Generic;
using System.Linq;

int wallet = 400; 
bool wantsToPlay = true; 

// Dictionary to hold the card values
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

string startGame = Console.ReadLine();

if (startGame.ToLower() == "start")
{
    while (wallet >= 200 && wantsToPlay)
    {
        wallet -= 200;

        List<KeyValuePair<string, int>> playerHand = new List<KeyValuePair<string, int>>();
        List<KeyValuePair<string, int>> dealerHand = new List<KeyValuePair<string, int>>();

        for (int i = 0; i < 2; i++)
        {
            var playerCard = cardValues.ElementAt(random.Next(cardValues.Count));
            playerHand.Add(playerCard);

            var dealerCard = cardValues.ElementAt(random.Next(cardValues.Count));
            dealerHand.Add(dealerCard);
        }

        // Dealer randomly draws a third card or not
        int dealer_choice = random.Next(1, 3);
        if (dealer_choice == 1)
        {
            var newCardDealer = cardValues.ElementAt(random.Next(cardValues.Count));
            dealerHand.Add(newCardDealer);
        }

        Console.WriteLine("Your hand:");
        foreach (var card in playerHand)
        {
            Console.WriteLine($"- {card.Key}");
        }

        // Pokazujemy jedną kartę dealera
        Console.WriteLine($"Dealer shows: {dealerHand[0].Key}");

        Console.WriteLine("Do you want to choose another card? yes/no");
        string choice = Console.ReadLine();

        if (choice.ToLower() == "yes")
        {
            var newCard = cardValues.ElementAt(random.Next(cardValues.Count));
            playerHand.Add(newCard);

            Console.WriteLine("Your hand:");
            foreach (var card in playerHand)
            {
                Console.WriteLine($"- {card.Key}");
            }
        }

        // Zmiana wartości Asa u gracza, jeśli potrzebne
        if (playerHand.Sum(card => card.Value) > 21)
        {
            var aceIndex = playerHand.FindIndex(card => card.Key == "A" && card.Value == 11);
            if (aceIndex != -1)
            {
                playerHand[aceIndex] = new KeyValuePair<string, int>("A", 1);
            }
        }

        // Zmiana wartości Asa u dealera, jeśli potrzebne
        if (dealerHand.Sum(card => card.Value) > 21)
        {
            var aceIndex = dealerHand.FindIndex(card => card.Key == "A" && card.Value == 11);
            if (aceIndex != -1)
            {
                dealerHand[aceIndex] = new KeyValuePair<string, int>("A", 1);
            }
        }

        int yourcardsum = playerHand.Sum(card => card.Value);
        int dealercardsum = dealerHand.Sum(card => card.Value);

        Console.WriteLine($"Your total: {yourcardsum}");
        Console.WriteLine("Dealer's hand:");
        foreach (var card in dealerHand)
        {
            Console.WriteLine($"- {card.Key}");
        }
        Console.WriteLine($"Dealer's total: {dealercardsum}");

        if (yourcardsum == dealercardsum)
        {
            Console.WriteLine("It's a tie! You get your money back.");
            wallet += 200;
        }
        else if (yourcardsum > 21)
        {
            Console.WriteLine("You busted! You lose 200$.");
        }
        else if (dealercardsum > 21 || yourcardsum > dealercardsum)
        {
            Console.WriteLine("You win! You gain 200$.");
            wallet += 400;
        }
        else
        {
            Console.WriteLine("Dealer wins! You lose 200$.");
        }

        Console.WriteLine($"Your current wallet amount is: {wallet}$");
        Console.WriteLine("Do you want to play again? yes/no");
        string ending = Console.ReadLine();

        if (ending.ToLower() != "yes")
        {
            wantsToPlay = false;
            Console.WriteLine($"Thank you for playing! Your final wallet amount is: {wallet}$");
        }
    }

    Console.ReadKey(intercept: true);
}
