using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville
{
    public class Player
    {
        private int pieces;
        private Pile playerCards;

        public Player(int pieces, Pile playerCards)
        {
            this.pieces = pieces;
            this.playerCards = playerCards;
        }

        public void BuyCard(int Id)
        {

        }

        public int Pieces { get; set; }
        public Pile PlayerCards { get; set; }
    }
}
