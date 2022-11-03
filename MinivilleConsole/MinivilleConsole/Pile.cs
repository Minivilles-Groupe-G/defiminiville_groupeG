using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinivilleConsole
{
    public class Pile
    {
        public void SetupCards(ref List<Cards> carte)
        {
            //On va ajouter les carte avec l'id 0 et 1
            CardsInfo cardsInfo = new CardsInfo();
            carte.Add(cardsInfo.NewCards(0));
            carte.Add(cardsInfo.NewCards(1));
        }

        //Permet d'afficher toute les carte du jeux
        public void PrintCart()
        {
            List<Cards> carte = new List<Cards>();
            CardsInfo newCards = new CardsInfo();

            for (int i = 0; i < 14; i++)
            {
                carte.Add(newCards.NewCards(i));

                //On colore le texte d'un couleur différente selon la couleur de la carte
                if (carte[i].color == "Green") { Console.ForegroundColor = ConsoleColor.Green; }
                else if (carte[i].color == "Red") { Console.ForegroundColor = ConsoleColor.Red; }
                else if (carte[i].color == "Blue") { Console.ForegroundColor = ConsoleColor.Cyan; }
                else if (carte[i].color == "Purple") { Console.ForegroundColor = ConsoleColor.DarkMagenta; }

                Console.WriteLine("[{0}] {1} : {2}. Prix : {3}.\nNombre à faire [{4} ~ {5}]", carte[i].id, carte[i].name, carte[i].effect, carte[i].cost, carte[i].minDice, carte[i].maxDice);
            }
        }

        //Créer une pile de carte
        public List<Cards> PileDeCarte()
        {
            List<Cards> pileDeCarte = new List<Cards>();
            CardsInfo newCards = new CardsInfo();

            for (int i = 0; i < 14; i++)
            {
                pileDeCarte.Add(newCards.NewCards(i));
            }

            return pileDeCarte;
        }
    }
}