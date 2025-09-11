using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerCardGame
{
    internal class Card
    {
        public int rank;
        public string suit;

        public Card(int rank, string suit)
        {
            this.rank = rank;
            this.suit = suit;
        }
    }
}
 