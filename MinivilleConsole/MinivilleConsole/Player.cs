using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinivilleConsole
{
    public class Player
    {
        public string name;
        private int money = 0;

        public List<Cards> cartePosseder = new List<Cards>();

        //Constructeur
        public Player(string name, List<Cards> cartePosseder)
        {
            this.name = name;
            this.cartePosseder = cartePosseder;
        }

        //Ajoute une nouvelle carte à la liste
        public void NewCards(int id)
        {
            CardsInfo newCards = new CardsInfo();
            cartePosseder.Add(newCards.NewCards(id));
            newCards.UpdateCardsLimit(id);
        }

        //Acheter un nouvelle carte
        public void BuyCards(int id)
        {
            if (id == 14)
            {
                return;
            }

            Pile newPile = new Pile();
            List<Cards> pileDeCarte = newPile.PileDeCarte();

            foreach (Cards card in pileDeCarte)
            {
                //Recpuère la carte avec l'id choisit
                if (card.id == id)
                {
                    if (card.cost <= money && card.limit > 0)
                    {
                        NewCards(id);
                        money -= card.cost;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n[{0}] La carte {1} a été achetée avec succès", name, card.name);

                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        //Argent du joueur
                        Console.WriteLine("\n[{0}] Argent : {1}", name, money);
                    }
                    else if (card.limit <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n[{0}] Il n'y a plus de carte \"{1}\"", name, card.name);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n[{0}] Vous n'avez pas assez d'argent", name);
                    }
                }
            }
        }

        //Renvoie si une des couleur est présente ou non
        public bool CheckColor(string colorA)
        {
            bool haveTheColor = false;

            foreach (Cards card in cartePosseder)
            {
                if (card.color == colorA) { haveTheColor = true; }
            }

            return haveTheColor;
        }

        //Gain d'argent
        public void MoneyGain(string colorA, int resultDe, Player opponent)
        {
            foreach (Cards card in cartePosseder)
            {
                if (CheckColor(colorA))
                {
                    if (card.id == 8 || card.id == 9 || card.id == 13)
                    {
                        //Va regarder toute les carte que le joueur possède et va ajouter le gain si un certain type est détecter
                        if (card.id == 8 && resultDe >= card.minDice && resultDe <= card.maxDice)
                        {
                            foreach (Cards card2 in cartePosseder)
                            {
                                if (card2.type == "Bétail") { money += card.gain; }
                            }
                        }
                        else if (card.id == 9 && resultDe >= card.minDice && resultDe <= card.maxDice)
                        {
                            foreach (Cards card2 in cartePosseder)
                            {
                                if (card2.type == "Engrenage") { money += card.gain; }
                            }
                        }
                        else if (card.id == 13 && resultDe >= card.minDice && resultDe <= card.maxDice)
                        {
                            foreach (Cards card2 in cartePosseder)
                            {
                                if (card2.type == "Champs") { money += card.gain; }
                            }
                        }
                    }
                    else if (CheckColor("Blue") || CheckColor("Green"))
                    {
                        if (resultDe >= card.minDice && resultDe <= card.maxDice)
                        {
                            money += card.gain;
                        }
                    }
                    else if (CheckColor("Red") || CheckColor("Purple"))
                    {
                        if (resultDe >= card.minDice && resultDe <= card.maxDice)
                        {
                            money += card.gain;
                            opponent.Money -= card.gain;
                        }
                    }
                }
            }
        }

        public void AfficherCartePlayer()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nCarte possédée : ");

            foreach (Cards carte in cartePosseder)
            {
                //On colore le texte d'un couleur différente selon la couleur de la carte
                if (carte.color == "Green") { Console.ForegroundColor = ConsoleColor.Green; }
                else if (carte.color == "Red") { Console.ForegroundColor = ConsoleColor.Red; }
                else if (carte.color == "Blue") { Console.ForegroundColor = ConsoleColor.Cyan; }
                else if (carte.color == "Purple") { Console.ForegroundColor = ConsoleColor.DarkMagenta; }

                Console.WriteLine("{0} : {1}. Nombre à faire [{2} ~ {3}]", carte.name, carte.effect, carte.minDice, carte.maxDice);
            }
        }

        public int Money
        {
            get { return money; }
            set { money = value; }
        }
    }
}