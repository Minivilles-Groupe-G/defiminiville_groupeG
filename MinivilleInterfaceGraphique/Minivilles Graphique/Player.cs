using System;

namespace Minivilles_Graphique
{
    class Player
    {
        public int Pieces { get; set; }
        public PlayerPile PlayerCards { get; set; }

        public Player()
        {
            PlayerCards = new PlayerPile();
        }

        public void BuyCard(int Id)
        {
            if(Pieces >= Card.GetCard(Id).Cost && Card.GetCard(Id).Number > 0)
            {
                // Decreasing the card number
                int cardNumber = Card.CardShop[Id].Number;
                Card.CardShop[Id] = new CardsInfo(Id, Card.CardShop[Id].Color, Card.CardShop[Id].Cost, Card.CardShop[Id].Name, cardNumber-1, Card.CardShop[Id].Effect, Card.CardShop[Id].MinDice, Card.CardShop[Id].MaxDice, Card.CardShop[Id].Gain);

                // Adding the card to player's pile.
                PlayerCards.Push(Id);

                // Removing player's gold
                Pieces -= Card.GetCard(Id).Cost;
            }
        }
    }
}
