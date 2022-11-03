using System;
using System.Collections.Generic;
using System.Text;

namespace Minivilles_Graphique
{
    class PlayerPile
    {
        public List<CardsInfo> cards = new List<CardsInfo>();

        // Initialise la pile avec les cartes de base
        public PlayerPile()
        {
            Push(0);
            Push(2);
        }

        // Retourne le gain en prenant la couleur et le score des dés
        public int GetCardGain(string cardColor, int diceScore)
        {
            int totalGain = 0;
            List<CardsInfo> validCards = new List<CardsInfo>();
            validCards = GetCardByColor(cards, cardColor);
            validCards = GetCardByNumber(validCards, diceScore);

            // Ajoute le score des cartes valides
            foreach (CardsInfo card in validCards)
                totalGain += card.Gain;
            // Ajoute le score des cartes spéciales valides
            totalGain += GetSpecialEffectCardScore(validCards);
            return totalGain;
        }

        // Retourne les cartes de la pile ayant la couleur passée en argument
        private List<CardsInfo> GetCardByColor(List<CardsInfo> pile, string cardColor)
        {
            List<CardsInfo> cards = new List<CardsInfo>();
            foreach (CardsInfo card in pile)
                if (card.Color == cardColor)
                    cards.Add(card);
            return cards;
        }

        // Retourne les cartes de la pile s'activant avec le score passé en argument
        private List<CardsInfo> GetCardByNumber(List<CardsInfo> pile, int nbr)
        {
            List<CardsInfo> cards = new List<CardsInfo>();
            foreach (CardsInfo card in pile)
                if (card.MinDice <= nbr && card.MaxDice >= nbr)
                    cards.Add(card);
            return cards;
        }

        // Retourne les cartes de la pile ayant l'ID passé en argument
        private List<CardsInfo> GetCardByID(List<CardsInfo> pile, int cardId)
        {
            List<CardsInfo> cards = new List<CardsInfo>();
            foreach (CardsInfo card in pile)
                if (card.Id == cardId)
                    cards.Add(card);
            return cards;
        }

        // Retourne true si une carte de la pile demande un score de dé de plus de 6.
        public int needTwoDice()
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

        public int IsCardId(int Id)
        {
            int x = 0;
            foreach(CardsInfo card in cards)
            {
                if(card.Id == Id)
                {
                    x++;
                }
            }
            return x;
        }

        // Retourne les cartes à effet spéciaux
        private int GetSpecialEffectCardScore(List<CardsInfo> pile)
        {
            foreach (CardsInfo card in pile)
            {
                if (card.Id == 10)
                {
                    List<CardsInfo> farmAmount = GetCardByID(pile, 1); // Get Farm
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

        // Ajoute une carte a la pile
        public void Push(int Id)
        {
            cards.Add(Card.GetCard(Id));
        }
    }
}

