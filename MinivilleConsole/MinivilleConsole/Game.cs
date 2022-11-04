using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MinivilleConsole
{
    public class Game
    {
        Pile setup = new Pile();

        //Permet de mettre en place les carte que possède le joueur au début
        List<Cards> mainJouerA = new List<Cards>();
        List<Cards> mainJouerB = new List<Cards>();

        Player playerA;
        Player playerB;

        int choix;

        Dice dice = new Dice(6);
        Pile pile = new Pile();

        int dice1;
        int dice2;

        int difficulty = 1;
        int nbPieceVictoire;

        public void Begin()
        {
            setup.SetupCards(ref mainJouerA);
            setup.SetupCards(ref mainJouerB);

            Console.ForegroundColor = ConsoleColor.White; //Changement de couleur du texte
            Console.WriteLine(".88b  d88. d888888b d8b   db d888888b db    db d888888b db      db      d88888b .d8888. \r\n88'YbdP`88   `88'   888o  88   `88'   88    88   `88'   88      88      88'     88'  YP \r\n88  88  88    88    88V8o 88    88    Y8    8P    88    88      88      88ooooo `8bo.   \r\n88  88  88    88    88 V8o88    88    `8b  d8'    88    88      88      88~~~~~   `Y8b. \r\n88  88  88   .88.   88  V888   .88.    `8bd8'    .88.   88booo. 88booo. 88.     db   8D \r\nYP  YP  YP Y888888P VP   V8P Y888888P    YP    Y888888P Y88888P Y88888P Y88888P `8888Y' \r\n                                                                                        \r\n                                                                                        \r\n\r");

            Console.WriteLine("Quel est votre nom utilisateur ?");
            string name = Console.ReadLine();

            playerA = new Player(name, mainJouerA);
            playerB = new Player("Ordinateur", mainJouerB);

            //Evite les erreur de frappe
            do
            {
                //Affichage
                Console.WriteLine("\nChoisissez votre difficulté :");
                Console.WriteLine("[1] Normal");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[2] Rapide");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("[3] Difficile");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[4] Expert");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.White;
                difficulty = int.Parse(Console.ReadLine());
            }
            while (difficulty < 1 || difficulty > 4);

            //Choix du nb pièce pour la victoire selon la difficulté
            if (difficulty == 1) { nbPieceVictoire = 20; }
            else if (difficulty == 2) { nbPieceVictoire = 10; }
            else if (difficulty == 3) { nbPieceVictoire = 30; }
            else if (difficulty == 4) { nbPieceVictoire = 20; }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nVous commencez avec les cartes \"Champs de blé\" et \"Ferme\"");

            int nbDe;

            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nCombien de dé(s) voulez-vous lancer ? (Il est recommandé de commencer par un seul dé).");
                nbDe = int.Parse(Console.ReadLine());
            }
            while (nbDe < 1 || nbDe > 2);

            _Game(playerA, playerB, nbDe);
        }

        //Manche
        private void _Game(Player joueurA, Player joueurB, int nbDice)
        {
            dice1 = 0;
            dice2 = 0;

            joueurA.AfficherCartePlayer();

            Console.ForegroundColor = ConsoleColor.Yellow;
            //Tour joueur A
            if (nbDice == 1)
            {
                dice1 = dice.Roll();
                Console.WriteLine("\n[{0}] Résultat du dé : {1}\n", joueurA.name, dice1);
            }
            else
            {
                dice1 = dice.Roll();
                dice2 = dice.Roll();
                Console.WriteLine("\n[{0}] Résultat des dés : {1}, {2} : {3}\n", joueurA.name, dice1, dice2, dice1 + dice2);
            }

            //Si joueurB carte bleu ou rouges
            if (joueurB.CheckColor("Blue") || joueurB.CheckColor("Red"))
            {
                //Action
                joueurB.MoneyGain("Blue", dice1 + dice2, joueurA);
                joueurB.MoneyGain("Red", dice1 + dice2, joueurA);
            }

            //Si joueurA carte bleue ou verte ou violet
            if (joueurA.CheckColor("Blue") || joueurA.CheckColor("Green") || joueurA.CheckColor("Purple"))
            {
                //Action
                joueurA.MoneyGain("Blue", dice1 + dice2, joueurB);
                joueurA.MoneyGain("Green", dice1 + dice2, joueurB);
                joueurA.MoneyGain("Purple", dice1 + dice2, joueurB);
            }

            //Phase achat de carte
            //Une seul carte par tour
            pile.PrintCart();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[14] None");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            //Argent du joueurA
            Console.WriteLine("\n[{0}] Argent : {1}",joueurA.name, joueurA.Money);

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nQuelle carte voulez-vous acheter ?");
                choix = int.Parse(Console.ReadLine());
            }
            while (choix < 0 || choix > 15);

            //Vérifie et achète la carte
            playerA.BuyCards(choix);

            //Fonction IA
            if (difficulty == 1 || difficulty == 2)
            {
                IARandom(joueurB, joueurA);
            }
            else
            {
                IA(joueurB, joueurA);
            }

            //Fin de la manche
            GameLoop();
        }

        //Fonction IA
        private void IA(Player Ordi, Player Humain)
        {
            Random rnd = new Random();
            int nbde = 1;
            Pile pile = new Pile();

            foreach (var cpdss in Ordi.cartePosseder)
            {
                if (cpdss.minDice > 6)
                {
                    nbde = 2;
                    break;
                }
            }

            Ordi.AfficherCartePlayer();

            Console.ForegroundColor = ConsoleColor.Yellow;
            if (nbde == 1)
            {
                dice1 = dice.Roll();
                Console.WriteLine("\n[{0}] Résultat du dé : {1}", Ordi.name, dice1);
            }
            else
            {
                dice1 = dice.Roll();
                dice2 = dice.Roll();
                Console.WriteLine("\n[{0}] Résultat des dés : {1}, {2} : {3}\n", Ordi.name, dice1, dice2, dice1 + dice2);
            }

            //Si joueurB carte bleu ou rouges
            if (Humain.CheckColor("Blue") || Humain.CheckColor("Red"))
            {
                //Action
                Humain.MoneyGain("Blue", dice1 + dice2, Ordi);
                Humain.MoneyGain("Red", dice1 + dice2, Ordi);
            }

            //Si joueurA carte bleue ou verte ou violet
            if (Ordi.CheckColor("Blue") || Ordi.CheckColor("Green") || Ordi.CheckColor("Purple"))
            {
                //Action
                Ordi.MoneyGain("Blue", dice1 + dice2, Humain);
                Ordi.MoneyGain("Green", dice1 + dice2, Humain);
                Ordi.MoneyGain("Purple", dice1 + dice2, Humain);
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            //Argent de l'ordi
            Console.WriteLine("\n[{0}] Argent : {1}", Ordi.name, Ordi.Money);

            //buycard

            bool canbotbuycard = true;

            bool canbuypurple = true;
            bool canbuyred = true;
            bool canbuygreen = true;
            bool canbuyblue = true;

            while (canbotbuycard)
            {
                if (difficulty == 4 && !CheckAllCards(Ordi))
                {
                    //Va esquiver le nothing to do
                }
                //nothing to do
                else if (Ordi.Money >= (nbPieceVictoire - 5))
                {
                    canbuypurple = false;
                    canbuyred = false;
                    canbuygreen = false;
                    canbuyblue = false;

                    canbotbuycard = false;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("L'ordi décide de ne pas acheter de carte");
                }

                //nothing can do
                if (!canbuypurple && !canbuyred && !canbuygreen && !canbuyblue)
                {
                    canbotbuycard = false;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("L'ordi décide de ne pas acheter de carte");
                }

                //attack
                if ((Humain.Money >= Ordi.Money) && (canbuypurple || canbuyred))
                {
                    int colorrwchoice = rnd.Next(0, 2);

                    if (Ordi.Money >= 6)
                    {
                        colorrwchoice = 1;
                    }

                    //buy red card
                    if (colorrwchoice == 0 && canbuyred && (AIBuyCard(Ordi, "Red") != null) && canbuyred)
                    {
                        Ordi.BuyCards(AIBuyCard(Ordi, "Red").id);
                        canbuyred = true;
                        break;
                    }
                    else if (AIBuyCard(Ordi, "Red") == null)
                    {
                        canbuyred = false;
                    }

                    //buy purple card
                    if (colorrwchoice == 1 && canbuypurple && (AIBuyCard(Ordi, "Purple") != null) && canbuypurple)
                    {
                        Ordi.BuyCards(AIBuyCard(Ordi, "Purple").id);
                        canbuypurple = true;
                        break;
                    }
                    else if (AIBuyCard(Ordi, "Purple") == null)
                    {
                        canbuypurple = false;
                    }
                }
                else
                {
                    canbuyred = false;
                    canbuypurple = false;
                }

                //defend
                if (!(canbuypurple && canbuyred) && (canbuygreen && canbuyblue))
                {

                    int colorrwchoice = rnd.Next(0, 2);

                    //buy green card
                    if (colorrwchoice == 0 && canbuygreen && (AIBuyCard(Ordi, "Green") != null))
                    {
                        Ordi.BuyCards(AIBuyCard(Ordi, "Green").id);
                        canbuygreen = true;
                        break;
                    }
                    else if (AIBuyCard(Ordi, "Green") == null)
                    {
                        canbuygreen = false;
                    }

                    //buy blue card
                    if (colorrwchoice == 1 && canbuyblue && (AIBuyCard(Ordi, "Blue") != null))
                    {
                        Ordi.BuyCards(AIBuyCard(Ordi, "Blue").id);
                        canbuyblue = true;
                        break;
                    }
                    else if (AIBuyCard(Ordi, "Blue") == null)
                    {
                        canbuyblue = false;
                    }
                }
                else
                {
                    canbuygreen = false;
                    canbuyblue = false;
                }
            }
        }

        //Function for AI
        private Cards AIBuyCard(Player Ordi, string color)
        {
            Random rnd = new Random();
            List<Cards> canbuycolorcards = new List<Cards>();

            foreach (var prpl in ColorCard(color))
            {
                if (prpl.cost <= Ordi.Money)
                {
                    canbuycolorcards.Add(prpl);
                }
            }

            if (canbuycolorcards.Count > 0)
            {
                int cardcolorchoice = rnd.Next(0, canbuycolorcards.Count);
                return canbuycolorcards[cardcolorchoice];
            }
            else
            {
                return null;
            }
        }

        //Function for AI
        private List<Cards> ColorCard(string color)
        {
            List<Cards> result = new List<Cards>();

            foreach(var rdlst in pile.PileDeCarte())
            {
                if(rdlst.color == color)
                {
                    result.Add(rdlst);
                }
            }

            return result;
        }

        private void GameLoop()
        {
            bool haveAWinner = false;

            //Check des score
            if (playerA.Money >= nbPieceVictoire || playerB.Money >= nbPieceVictoire)
            {
                if (difficulty == 4)
                {
                    //Si un des joueur à toute les carte + le pièces nécessaires
                    if (CheckAllCards(playerA) || CheckAllCards(playerB)) { haveAWinner = true; EndGame(); }
                }
                else
                {
                    haveAWinner = true;
                    EndGame();
                }
            }

            //Si victoire d'un des deux, on ne fait rien

            //Sinon on continue
            if (!haveAWinner)
            {
                int nbDe;
                do
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nCombien de dé(s) voulez-vous lancer ?");
                    nbDe = int.Parse(Console.ReadLine());
                }
                while (nbDe < 1 || nbDe > 2);
                _Game(playerA, playerB, nbDe);
            }

        }

        //Permet de savoir si un joueur à toute les carte
        private bool CheckAllCards(Player player)
        {
            bool haveAllCards = false;
            int nbCarte = 13;
            int haveCards = 0;

            for (int i = 0; i < nbCarte; i++)
            {
                foreach (Cards cards in player.cartePosseder)
                {
                    if (cards.id == i)
                    {
                        haveCards++;
                        break;
                    }
                }
            }

            if (haveCards == nbCarte) { haveAllCards = true; }
            
            return haveAllCards;
        }

        //Détermine le gagnant et mes fin à la partie
        private void EndGame()
        {
            //Résultat
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  _____      __                 _   _             _   \r\n |  __ \\    /_/                | | | |           | |  \r\n | |__) |   ___   ___   _   _  | | | |_    __ _  | |_ \r\n |  _  /   / _ \\ / __| | | | | | | | __|  / _` | | __|\r\n | | \\ \\  |  __/ \\__ \\ | |_| | | | | |_  | (_| | | |_ \r\n |_|  \\_\\  \\___| |___/  \\__,_| |_|  \\__|  \\__,_|  \\__|\r\n                                                      \r\n                                                      \r\n\r");

            //Victoire carte + pièces
            if (difficulty == 4)
            {
                if (CheckAllCards(playerA) && CheckAllCards(playerB))
                {
                    //Egalité
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("  ___                 _   _   _         \r\n | __|  __ _   __ _  | | (_) | |_   ___ \r\n | _|  / _` | / _` | | | | | |  _| / -_)\r\n |___| \\__, | \\__,_| |_| |_|  \\__| \\___|\r\n       |___/                            ");
                }
                else if (CheckAllCards(playerA))
                {
                    //Victoire
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" __   __  _        _           _             \r\n \\ \\ / / (_)  __  | |_   ___  (_)  _ _   ___ \r\n  \\ V /  | | / _| |  _| / _ \\ | | | '_| / -_)\r\n   \\_/   |_| \\__|  \\__| \\___/ |_| |_|   \\___|\r\n                                             ");

                }
                else if (CheckAllCards(playerB))
                {
                    //Défaite
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  ___     __    __          _   _         \r\n |   \\   /_/   / _|  __ _  (_) | |_   ___ \r\n | |) | / -_) |  _| / _` | | | |  _| / -_)\r\n |___/  \\___| |_|   \\__,_| |_|  \\__| \\___|\r\n                                          ");
                }
            }
            else
            {
                //Victoire pièce
                if (playerA.Money >= nbPieceVictoire && playerB.Money >= nbPieceVictoire)
                {
                    //Egalité
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("  ___                 _   _   _         \r\n | __|  __ _   __ _  | | (_) | |_   ___ \r\n | _|  / _` | / _` | | | | | |  _| / -_)\r\n |___| \\__, | \\__,_| |_| |_|  \\__| \\___|\r\n       |___/                            ");
                }
                else if (playerA.Money >= nbPieceVictoire)
                {
                    //Victoire
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" __   __  _        _           _             \r\n \\ \\ / / (_)  __  | |_   ___  (_)  _ _   ___ \r\n  \\ V /  | | / _| |  _| / _ \\ | | | '_| / -_)\r\n   \\_/   |_| \\__|  \\__| \\___/ |_| |_|   \\___|\r\n                                             ");
                }
                else if (playerB.Money >= nbPieceVictoire)
                {
                    //Défaite
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  ___     __    __          _   _         \r\n |   \\   /_/   / _|  __ _  (_) | |_   ___ \r\n | |) | / -_) |  _| / _` | | | |  _| / -_)\r\n |___/  \\___| |_|   \\__,_| |_|  \\__| \\___|\r\n                                          ");
                }
            }

            Console.ReadLine(); //Pour ne pas fermer la fenêtre
        }

        //Fonction IA facile
        private void IARandom(Player Ordi, Player Humain)
        {
            Random rnd = new Random();
            Pile pile = new Pile();

            int iawantotopay = 0;

            int nbde = 1;

            //choix du nombre de dé
            foreach (var ordicards in Ordi.cartePosseder)
            {
                if (ordicards.minDice > 6)
                {
                    nbde = 2;
                    break;
                }
            }

            Ordi.AfficherCartePlayer();

            Console.ForegroundColor = ConsoleColor.Yellow;
            //agis en fonction du nombre de dé
            if (nbde == 1)
            {
                dice1 = dice.Roll();
                Console.WriteLine("\n[{0}] Résultat du dé : {1}", Ordi.name, dice1);
            }
            else
            {
                dice1 = dice.Roll();
                dice2 = dice.Roll();
                Console.WriteLine("\n[{0}] Résultat des dés : {1}, {2} : {3}\n", Ordi.name, dice1, dice2, dice1 + dice2);
            }

            //fait l'action des carte de l'adversaire
            if (Humain.CheckColor("Blue") || Humain.CheckColor("Red"))
            {
                //Action
                Humain.MoneyGain("Blue", dice1 + dice2, Ordi);
                Humain.MoneyGain("Red", dice1 + dice2, Ordi);
            }

            //Si joueurA carte bleue ou verte ou violet
            if (Ordi.CheckColor("Blue") || Ordi.CheckColor("Green") || Ordi.CheckColor("Purple"))
            {
                //Action
                Ordi.MoneyGain("Blue", dice1 + dice2, Humain);
                Ordi.MoneyGain("Green", dice1 + dice2, Humain);
                Ordi.MoneyGain("Purple", dice1 + dice2, Humain);
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            //Argent de l'ordi
            Console.WriteLine("\n[{0}] Argent : {1}", Ordi.name, Ordi.Money);

            //si l'ordi à la thune
            if (Ordi.Money > 0 && Ordi.Money < (nbPieceVictoire - 2))
            {
                //décide si l'ordi veut jouer une chance sur deux
                iawantotopay = rnd.Next(0, 2);
                if (iawantotopay == 1)
                {
                    List<Cards> cardiacanbuy = new List<Cards>();

                    foreach (var pldc in pile.PileDeCarte())
                    {
                        if (pldc.cost <= Ordi.Money)
                        {
                            cardiacanbuy.Add(pldc);
                        }
                    }

                    //achat d'une carte random parmis celle qu'il peut acheter
                    int iacardbuy = rnd.Next(0, cardiacanbuy.Count);
                    Ordi.BuyCards(cardiacanbuy[iacardbuy].id);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nL'ordi n'achète pas de carte");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nL'ordi n'achète pas de carte");
            }
        }   
    }
}