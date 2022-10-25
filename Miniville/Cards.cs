using Minivilles_Graphique;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville
{
    public class Cards
    {
        public static Dictionary<int, CardsInfo> CardShop = new Dictionary<int, CardsInfo>();

        public static void AddCard(int Id, string Color, int Cost, string Name, int Number, string Effect, int MinDice, int MaxDice, int Gain)
        {
            CardShop.Add(Id, new CardsInfo(Id, Color, Cost, Name, Number, Effect, MinDice, MaxDice, Gain));
        }

        public static List<int> GetCardsCost()
        {
            List<int> result = new List<int>();

            for(int i = 0; i < CardShop.Count; i++)
            {
                result.Add(CardShop[i].Cost);
            }

            return result;
        }

        public static void CreateDeck()
        {
            AddCard(0, "Blue", 1, "Wheat field", 6, "Get 1 coin-", 1, 1, 1);
            AddCard(1, "Blue", 1, "Farm", 6, "Get 1 coin-", 2, 2, 1);
            AddCard(2, "Green", 1, "Baker's shop", 6, "Get 2 coins-", 2, 3, 1);
            AddCard(3, "Red", 2, "Café", 6, "Get 1 coin from the player-that rolled the die", 3, 3, 1);
            AddCard(4, "Green", 2, "Grocer's shop", 6, "Get 3 coins-", 4, 4, 3);
            AddCard(5, "Blue", 3, "Forest", 6, "Get 1 coin-", 5, 5, 1);
            AddCard(6, "Red", 3, "Restaurant", 6, "Get 2 coins from the player-that rolled the die", 9, 10, 2);
            AddCard(7, "Blue", 3, "Stadium", 4, "Get 4 coins-", 6, 6, 2);
            //AddCard(8, "Green", 8, "Business center", 4, "Get 6 coins-", 6, 6, 6);
            AddCard(9, "Red", 7, "TV station", 4, "Get 5 coins from the player-that rolled the die", 6, 6, 5);
            AddCard(10, "Green", 5, "Dairy", 6, "Get 3 coins foreach-shop that you have", 7, 7, 0);
            AddCard(11, "Green", 3, "Furniture factory", 6, "Get 3 coins foreach forest-or mine that you have", 8, 8, 0);
            AddCard(12, "Blue", 6, "Mine", 6, "Get 5 coins-", 9, 9, 5);
            AddCard(13, "Blue", 3, "Orchard", 6, "Get 3 coins-", 10, 10, 3);
            AddCard(14, "Green", 2, "Greengrocer's", 6, "Get 2 coins foreach wheat field-and orchard that you have", 11, 12, 0);
        }
    }
}
