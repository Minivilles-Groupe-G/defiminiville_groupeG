using Minivilles_Graphique;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville
{
    public class Pile
    {
        public List<CardsInfo> cards = new List<CardsInfo>();

        public Pile()
        {
            Push(0);
            Push(1);
        }

        public int NeedTwoDice()
        {
            int nbCard6 = 0;
            foreach (CardsInfo card in cards)
            {
                if (card.MinDice > 6)
                {
                    nbCard6++;
                }
            }
            return nbCard6;
        }

        public int GetCardGain(string cardColor, int diceScore)
        {
            int result = 0;

            return result;
        }

        private List<CardsInfo> GetCardByColor(List<CardsInfo> pile, string cardColor)
        {
            List<CardsInfo> result = new List<CardsInfo>();

            for(int i = 0; i < pile.Count; i++)
            {
                if(pile[i].Color == cardColor)
                {
                    result.Add(pile[i]);
                }
            }

            return result;
        }

        private List<CardsInfo> GetCardByID(List<CardsInfo> pile, int cardID)
        {
            List<CardsInfo> result = new List<CardsInfo>();

            for (int i = 0; i < pile.Count; i++)
            {
                if (pile[i].Id == cardID)
                {
                    result.Add(pile[i]);
                }
            }

            return result;
        }

        private List<CardsInfo> GetCardByNumber(List<CardsInfo> pile, int cardNumber)
        {
            List<CardsInfo> result = new List<CardsInfo>();

            for (int i = 0; i < pile.Count; i++)
            {
                if (pile[i].Number == cardNumber)
                {
                    result.Add(pile[i]);
                }
            }

            return result;
        }

        private int GetSpecialEffectCardScore(List<CardsInfo> pile)
        {
            foreach (CardsInfo card in pile)
            {
                if (card.Id == 10)
                {
                    List<CardsInfo> farmAmount = GetCardByID(pile, 1);
                    return 3 * (farmAmount.Count);
                }
                if (card.Id == 11)
                {
                    List<CardsInfo> mineAmount = GetCardByID(pile, 12);
                    List<CardsInfo> forestAmount = GetCardByID(pile, 5);
                    return 3 * (mineAmount.Count + forestAmount.Count);
                }
                if (card.Id == 14)
                {
                    List<CardsInfo> wheatAmount = GetCardByID(pile, 0);
                    List<CardsInfo> orchardAmount = GetCardByID(pile, 13);
                    return 2 * (wheatAmount.Count + orchardAmount.Count);
                }
            }
            return 0;
        }

        public void Push(int Id)
        {
            Cards crds = new Cards();
            cards.Add(crds.GetCard(Id));
        }
    }
}
