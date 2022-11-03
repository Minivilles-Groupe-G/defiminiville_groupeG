using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinivilleConsole
{
    public class CardsInfo
    {
        private List<int> limit = new List<int> { 6, 6, 6, 6, 6, 6, 4, 4, 6, 6, 6, 6, 6, 6 };

        //Renvoie une carte avec les bonne valeur selon l'id
        public Cards NewCards(int id)
        {
            List<Cards> cards = new List<Cards>();

            //Champs de blé #0
            cards.Add(new Cards(0, limit[0], "Blue", "Champs", 1, "Champs de blé", "Pendant le tour de n'importe quel joueur, recevez 1 pièce de la banque", 1, 1, 1));

            //Ferme #1
            cards.Add(new Cards(1, limit[1], "Blue", "Bétail", 1, "Ferme", "Pendant le tour de n'importe quel joueur, recevez 1 pièce de la banque", 2, 2, 1));

            //Boulangerie #2
            cards.Add(new Cards(2, limit[2], "Green", "Brioche", 1, "Boulangerie", "Pendant votre tour uniquement, recevez 1 pièce de la banque", 2, 3, 1));

            //Café #3
            cards.Add(new Cards(3, limit[3], "Red", "Tasse", 2, "Café", "Recevez une pièce du joueur qui a lancé les dés", 3, 3, 1));

            //Supérette #4
            cards.Add(new Cards(4, limit[4], "Green", "Brioche", 2, "Supérette", "Pendant votre tour uniquement, recevez 3 pièces de la banque", 4, 4, 3));

            //Forêt #5
            cards.Add(new Cards(5, limit[5], "Blue", "Engrenage", 3, "Forêt", "Pendant votre tour uniquement, recevez 1 pièce de la banque", 5, 5, 1));

            //Stade #6
            cards.Add(new Cards(6, limit[6], "Purple", "Tour", 3, "Stade", "Pendant votre tour uniquement, recevez 2 pièces de la part de chaque joueur", 6, 6, 2));

            //Chaine de télévision #7
            cards.Add(new Cards(7, limit[7], "Purple", "Tour", 7, "Chaine de télévision", "Pendant votre tour uniquement, recevez 5 pièces de joueur adverse", 6, 6, 5));

            //Fromagerie #8 ////////////////////////////////////
            cards.Add(new Cards(8, limit[8], "Green", "Usine", 5, "Fromagerie", "Pendant votre tour uniquement, revevez 3 pièces de la banque pour chaque type (Bétail) que vous possédez", 7, 7, 3));

            //Fabrique de meubles #9 ////////////////////////////////////
            cards.Add(new Cards(9, limit[9], "Blue", "Usine", 3, "Fabrique de meubles", "Pendant votre tour uniquement, recevez 3 pièces de la banque pour chaque établissement de type (Engrenage) que vous possédez", 8, 8, 3));

            //Mine #10
            cards.Add(new Cards(10, limit[10], "Blue", "Engrenage", 6, "Mine", "Pendant le tour de n'importe quel joueur, recevez 5 pièces de la banque", 9, 9, 5));

            //Restaurant #11
            cards.Add(new Cards(11, limit[11], "Red", "Tasse", 3, "Restaurant", "Recevez 2 pièces du joueur qui a lancé les dés", 9, 10, 2));

            //Verger #12
            cards.Add(new Cards(12, limit[12], "Blue", "Champs", 3, "Verger", "Pendant le tour de n'importe quel joueur, rcevez 3 pièces de la banque", 10, 10, 3));

            //Marché de fruit et légumes #13 ////////////////////////////////////
            cards.Add(new Cards(13, limit[13], "Green", "Bourse", 2, "Marché de fruit et légumes", "Pendant votre tour uniquement, recevez 2 pièces de la banque pour chaque établissement de type (Champs) que vous possédez", 11, 12, 2));


            foreach (Cards carte in cards)
            {
                if (carte.id == id)
                {
                    return carte;
                }
            }

            return null;
        }

        public void UpdateCardsLimit(int id)
        {
            limit[id] = limit[id] - 1;
        }
    }
}