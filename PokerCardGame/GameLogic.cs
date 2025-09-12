using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerCardGame
{
    internal class GameLogic
    {
        public static int CheckHand(List<Card> hand)
        {
            if (royalFlush(hand))
                return 10;
            else if (straightFlush(hand))
                return 9;
            else if (fourOfAKind(hand))
                return 8;
            else if (fullHouse(hand))
                return 7;
            else if (flush(hand))
                return 6;
            else if (straight(hand))
                return 5;
            else if (threeOfAKind(hand))
                return 4;
            else if (twoPair(hand))
                return 3;
            else if (onePair(hand))
                return 2;
            else
                return 1; // high card
        }

        // Metoda pomocnicza do uzyskania nazwy układu kart
        public static string GetHandName(int handValue)
        {
            switch (handValue)
            {
                case 10: return "Royal Flush";
                case 9: return "Straight Flush";
                case 8: return "Four of a Kind";
                case 7: return "Full House";
                case 6: return "Flush";
                case 5: return "Straight";
                case 4: return "Three of a Kind";
                case 3: return "Two Pair";
                case 2: return "One Pair";
                default: return "High Card";
            }
        }

        // Metoda pomocnicza do uzyskania nazwy karty
        public static string GetCardName(int cardValue)
        {
            switch (cardValue)
            {
                case 14: return "As";
                case 13: return "King";
                case 12: return "Queen";
                case 11: return "Jack";
                default: return cardValue.ToString();
            }
        }

        public static bool royalFlush(List<Card> hand)
        {
            // Poker królewski: 10, J, Q, K, A tego samego koloru
            if (!flush(hand)) return false;
            
            var ranks = hand.Select(c => c.rank).OrderBy(r => r).ToList();
            return ranks.SequenceEqual(new[] { 10, 11, 12, 13, 14 });
        }

        public static bool straightFlush(List<Card> hand)
        {
            // Poker: 5 kolejnych kart tego samego koloru
            return flush(hand) && straight(hand);
        }

        public static bool fourOfAKind(List<Card> hand)
        {
            // Kareta: 4 karty tej samej wartości
            return hand.GroupBy(c => c.rank)
                      .Any(g => g.Count() == 4);
        }

        public static bool fullHouse(List<Card> hand)
        {
            // Full: 3 karty jednej wartości i 2 karty innej wartości
            var groups = hand.GroupBy(c => c.rank).ToList();
            return groups.Count == 2 && groups.Any(g => g.Count() == 3);
        }

        public static bool flush(List<Card> hand)
        {
            // Kolor: 5 kart tego samego koloru
            return hand.GroupBy(c => c.suit).Count() == 1;
        }

        public static bool straight(List<Card> hand)
        {
            // Strit: 5 kolejnych kart
            var orderedRanks = hand.Select(c => c.rank).OrderBy(r => r).ToList();
            
            // Sprawdź normalny strit
            bool isNormalStraight = true;
            for (int i = 0; i < orderedRanks.Count - 1; i++)
            {
                if (orderedRanks[i] + 1 != orderedRanks[i + 1])
                {
                    isNormalStraight = false;
                    break;
                }
            }
            
            // Sprawdź strit z Asem na dole (A-2-3-4-5)
            bool isAceLowStraight = orderedRanks.Contains(14) && // As
                                    orderedRanks.Contains(2) &&
                                    orderedRanks.Contains(3) &&
                                    orderedRanks.Contains(4) &&
                                    orderedRanks.Contains(5);
            
            return isNormalStraight || isAceLowStraight;
        }

        public static bool threeOfAKind(List<Card> hand)
        {
            // Trójka: 3 karty tej samej wartości
            return hand.GroupBy(c => c.rank)
                      .Any(g => g.Count() == 3);
        }

        public static bool twoPair(List<Card> hand)
        {
            // Dwie pary: 2 karty jednej wartości i 2 karty innej wartości
            return hand.GroupBy(c => c.rank)
                      .Count(g => g.Count() == 2) == 2;
        }

        public static bool onePair(List<Card> hand)
        {
            // Para: 2 karty tej samej wartości
            return hand.GroupBy(c => c.rank)
                      .Any(g => g.Count() == 2);
        }

        public static int highCard(List<Card> hand)
        {
            // Zwraca wartość najwyższej karty w ręce
            if (hand == null || hand.Count == 0)
                return 0;
                
            return hand.Max(c => c.rank);
        }
        
        // Metoda pomocnicza do uzyskania wartości najwyższej karty w dowolnej ręce
        public int GetHighCardValue(List<Card> hand)
        {
            return highCard(hand);
        }
    }
}
