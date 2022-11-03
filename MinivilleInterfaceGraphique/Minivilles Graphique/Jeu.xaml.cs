using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;

namespace Minivilles_Graphique
{
    /// <summary>
    /// Logique d'interaction pour Jeu.xaml
    /// </summary>
    public partial class Jeu : Page
    {
        //Definition des Bitmaps
        ImageSource playerTextTourSource = new BitmapImage(new Uri("https://i.imgur.com/DgUgJAN.png"));
        ImageSource aiTextTourSource = new BitmapImage(new Uri("https://i.imgur.com/fUTjMXk.png"));

        ImageSource choiceBtnLeftEnterSource = new BitmapImage(new Uri("https://i.imgur.com/OOaya6P.png"));
        ImageSource choiceBtnLeftLeaveSource = new BitmapImage(new Uri("https://i.imgur.com/uS527IT.png"));
        ImageSource choiceBtnRightEnterSource = new BitmapImage(new Uri("https://i.imgur.com/HvUY6de.png"));
        ImageSource choiceBtnRightLeaveSource = new BitmapImage(new Uri("https://i.imgur.com/tnESWpF.png"));

        ImageSource choiceBtnLeftEnterShopSource = new BitmapImage(new Uri("https://i.imgur.com/A4vO3GR.png"));
        ImageSource choiceBtnLeftLeaveShopSource = new BitmapImage(new Uri("https://i.imgur.com/sFCuC2F.png"));
        ImageSource choiceBtnRightEnterShopSource = new BitmapImage(new Uri("https://i.imgur.com/I9IHY2t.png"));
        ImageSource choiceBtnRightLeaveShopSource = new BitmapImage(new Uri("https://i.imgur.com/Is6IEUR.png"));

        ImageSource btnRetourShopEnterSource = new BitmapImage(new Uri("https://i.imgur.com/9iZyq1R.png"));
        ImageSource btnRetourShopLeaveSource = new BitmapImage(new Uri("https://i.imgur.com/FWPwYOw.png"));

        ImageSource die1Source = new BitmapImage(new Uri("https://i.imgur.com/cqmCCLI.png"));
        ImageSource die2Source = new BitmapImage(new Uri("https://i.imgur.com/a154v26.png"));
        ImageSource die3Source = new BitmapImage(new Uri("https://i.imgur.com/BEjc0XI.png"));
        ImageSource die4Source = new BitmapImage(new Uri("https://i.imgur.com/M0zi7Vv.png"));
        ImageSource die5Source = new BitmapImage(new Uri("https://i.imgur.com/KURgRtP.png"));
        ImageSource die6Source = new BitmapImage(new Uri("https://i.imgur.com/2aoeeQT.png"));

        Dictionary<string, ImageSource> imgSource = new Dictionary<string, ImageSource>()
        {
            {"0", new BitmapImage(new Uri("https://i.imgur.com/OsnW6Xa.png"))},
            {"1", new BitmapImage(new Uri("https://i.imgur.com/BVC3VU0.png"))},
            {"2", new BitmapImage(new Uri("https://i.imgur.com/5iZdN83.png"))},
            {"3", new BitmapImage(new Uri("https://i.imgur.com/pOVW6QK.png"))},
            {"4", new BitmapImage(new Uri("https://i.imgur.com/NljEkZx.png"))},
            {"5", new BitmapImage(new Uri("https://i.imgur.com/hPHCeng.png"))},
            {"6", new BitmapImage(new Uri("https://i.imgur.com/feFxZ3p.png"))},
            {"7", new BitmapImage(new Uri("https://i.imgur.com/udjnxWy.png"))},
            {"9", new BitmapImage(new Uri("https://i.imgur.com/JAuhM2b.png"))},
            {"10", new BitmapImage(new Uri("https://i.imgur.com/ErICwL8.png"))},
            {"11", new BitmapImage(new Uri("https://i.imgur.com/twES25P.png"))},
            {"12", new BitmapImage(new Uri("https://i.imgur.com/gl85BAs.png"))},
            {"13", new BitmapImage(new Uri("https://i.imgur.com/4t8yIqb.png"))},
            {"14", new BitmapImage(new Uri("https://i.imgur.com/KoaT5jc.png"))}
        };

        ImageSource playerWonText = new BitmapImage(new Uri("https://i.imgur.com/wgjPrGb.png"));
        ImageSource AIWonText = new BitmapImage(new Uri("https://i.imgur.com/YCq8LRt.png"));
        ImageSource TieText = new BitmapImage(new Uri("https://i.imgur.com/W4D6hEX.png"));

        Random rng = new Random();

        List<ImageSource> diceSprites = new List<ImageSource>();

        Player player = new Player();
        Player ordi = new Player();

        List<int> canAIBuy = new List<int>();

        protected int dieResult;
        protected int die2Result;

        bool endGame = false;

        public Jeu()
        {
            InitializeComponent();

            diceSprites.Add(die1Source);
            diceSprites.Add(die2Source);
            diceSprites.Add(die3Source);
            diceSprites.Add(die4Source);
            diceSprites.Add(die5Source);
            diceSprites.Add(die6Source);

            //Affichage des cartes
            DisplayPlayerCards();
            DisplayAICards();
            player.Pieces = 3;
            ordi.Pieces = 3;
            playerGold.Content = player.Pieces;
            aiGold.Content = ordi.Pieces;
            UpdateStyle();
        }

        public void carteSpeciale(int Id)
        {
            if (textTour.Source == playerTextTourSource && Id == 7)
            {
                for(int x = 0; x<2; x++)
                {
                    if(ordi.Pieces > 0)
                    {
                        ordi.Pieces -= 1;
                        player.Pieces += 1;
                    }
                }
                if (ordi.Pieces < 0) ordi.Pieces = 0;
            }
            else if(Id == 7)
            {
                for (int x = 0; x < 2; x++)
                {
                    if (player.Pieces > 0)
                    {
                        player.Pieces -= 1;
                        ordi.Pieces += 1;
                    }
                }
                if (player.Pieces < 0) player.Pieces = 0;
            }

            if (textTour.Source == playerTextTourSource && Id == 9)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (ordi.Pieces > 0)
                    {
                        ordi.Pieces -= 1;
                        player.Pieces += 1;
                    }
                }
                if (ordi.Pieces < 0) ordi.Pieces = 0;
            }
            else if (Id == 9)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (player.Pieces > 0)
                    {
                        player.Pieces -= 1;
                        ordi.Pieces += 1;
                    }
                }
                if (player.Pieces < 0) player.Pieces = 0;
            }

        }

        public void UpdateStyle()
        {
            if(aiGold.Content.ToString().Length == 1)
            {
                aiGold.Margin = new Thickness(1054,90,0,0);
            }
            else
            {
                aiGold.Margin = new Thickness(1035, 90, 0, 0); 
            }

            if (nbAiCards.Content.ToString().Length == 1)
            {
                nbAiCards.Margin = new Thickness(1169, 90, 0, 0);
            }
            else
            {
                nbAiCards.Margin = new Thickness(1150, 90, 0, 0); 
            }
        }

        public void UpdateShop()
        {
            DisplayPlayerCards();
            DisplayAICards();
            UpdateStyle();
            playerGold.Content = player.Pieces;
            aiGold.Content = ordi.Pieces;
            playerGoldShop.Content = player.Pieces;

        }


        // Tour du joueur
        private async void DisplayPlayerDiceThrow()
        {
            bool isTwoDices = false;
            if (ThrowDie2.Visibility == Visibility.Visible) 
                isTwoDices = true;

            die2Result = -1;

            for (int x = 0; x < rng.Next(20, 60); x++)
            {
                await Task.Delay(75);

                dieResult = rng.Next(0, 6);
                ThrowDie.Source = diceSprites[dieResult];

                if (isTwoDices){
                    die2Result = rng.Next(0, 6);
                    ThrowDie2.Source = diceSprites[die2Result];
                }
            }

            choiceButtonsBar.Visibility = Visibility.Collapsed;
            choiceBtnLeft.Visibility = Visibility.Collapsed;
            choiceBtnRight.Visibility = Visibility.Collapsed;
            DiceQuestion.Visibility = Visibility.Collapsed;

            playerDie.Source = diceSprites[dieResult];

            if (isTwoDices) {
                playerDie2.Visibility = Visibility.Visible;
                playerDie2.Source = diceSprites[die2Result]; 
            }
            else if (!isTwoDices) playerDie2.Visibility = Visibility.Collapsed;

            diceResult.Content = $"Vous avez fait un {dieResult+1 + die2Result+1} !";
            diceResult.Visibility = Visibility.Visible;


            // Calcul des gains
            ordi.Pieces += ordi.PlayerCards.GetCardGain("Blue", dieResult+1 + die2Result+1);
            if (player.Pieces < ordi.PlayerCards.GetCardGain("Red", dieResult + 1 + die2Result + 1))
                ordi.Pieces += player.Pieces;
            else
                ordi.Pieces += ordi.PlayerCards.GetCardGain("Red", dieResult + 1 + die2Result + 1);

            player.Pieces += player.PlayerCards.GetCardGain("Blue", dieResult+1 + die2Result+1);
            player.Pieces += player.PlayerCards.GetCardGain("Green", dieResult+1 + die2Result+1);
            player.Pieces -= ordi.PlayerCards.GetCardGain("Red", dieResult+1 + die2Result+1);

            if ((dieResult + 1 + die2Result + 1) == 6)
            {
                for(int i=0; i< player.PlayerCards.IsCardId(7); i++)
                {
                    carteSpeciale(7);
                }
            }
            else if ((dieResult + 1 + die2Result + 1) == 6)
            {
                for (int i = 0; i < player.PlayerCards.IsCardId(9); i++)
                {
                    carteSpeciale(9);
                }
            }

            // Affichage du gold de chaque joueur
            if (player.Pieces < 0)
                player.Pieces = 0;
            playerGold.Content = player.Pieces;
            if (ordi.Pieces < 0)
                ordi.Pieces = 0;
            aiGold.Content = ordi.Pieces;
            UpdateStyle();


            await Task.Delay(1500);
            ThrowDie.Visibility = Visibility.Collapsed;
            ThrowDie2.Visibility = Visibility.Collapsed;
            diceResult.Visibility = Visibility.Collapsed;

            CheckWin();
            if(!endGame) 
                DisplayChoiceShop();
        }

        //Tour de l'IA
        private async void DisplayAIDiceThrow()
        {
            bool isTwoDices = false;
            if ((ordi.PlayerCards.needTwoDice() == 1 && rng.Next(0, 4) <= 1) || (ordi.PlayerCards.needTwoDice() >= 2 && rng.Next(0, 5) <= 3))
            {
                isTwoDices = true;
                AIDiceThrow.Content = "L'IA lance 2 dés";
                AIDiceThrow.Margin = new Thickness(542, 500, 0, 0);

                ThrowDie.Margin = new Thickness(523, 380, 0, 0);
                ThrowDie.Visibility = Visibility.Visible;
                ThrowDie2.Visibility = Visibility.Visible;
            }
            else
            {
                AIDiceThrow.Content = "L'IA lance 1 dé";
                AIDiceThrow.Margin = new Thickness(565, 500, 0, 0);

                ThrowDie.Margin = new Thickness(600, 380, 0, 0);
                ThrowDie.Visibility = Visibility.Visible;
            }

            die2Result = -1;

            for (int x = 0; x < rng.Next(20, 60); x++)
            {
                await Task.Delay(75);

                dieResult = rng.Next(0, 6);
                ThrowDie.Source = diceSprites[dieResult];

                if (isTwoDices)
                {
                    die2Result = rng.Next(0, 6);
                    ThrowDie2.Source = diceSprites[die2Result];
                }
            }

            AIDiceThrow.Visibility = Visibility.Collapsed;

            aiDie.Source = diceSprites[dieResult];
            if (isTwoDices)
            {
                aiDie2.Visibility = Visibility.Visible;
                aiDie2.Source = diceSprites[die2Result];
            }
            else if (!isTwoDices) aiDie2.Visibility = Visibility.Collapsed;

            diceResult.Margin = new Thickness(542, 500, 0, 0);
            diceResult.Content = $"L'IA a fait un {dieResult + 1 + die2Result + 1} !";
            diceResult.Visibility = Visibility.Visible;
            await Task.Delay(350);

            // Calcul des gains
            player.Pieces += player.PlayerCards.GetCardGain("Blue", dieResult + 1 + die2Result + 1);
            if (ordi.Pieces < player.PlayerCards.GetCardGain("Red", dieResult + 1 + die2Result + 1))
                player.Pieces += ordi.Pieces;
            else
                player.Pieces += player.PlayerCards.GetCardGain("Red", dieResult + 1 + die2Result + 1);

            ordi.Pieces += ordi.PlayerCards.GetCardGain("Blue", dieResult + 1 + die2Result + 1);
            ordi.Pieces += ordi.PlayerCards.GetCardGain("Green", dieResult + 1 + die2Result + 1);
            ordi.Pieces -= player.PlayerCards.GetCardGain("Red", dieResult + 1 + die2Result + 1);

            if ((dieResult + 1 + die2Result + 1) == 6)
            {
                for (int i = 0; i < ordi.PlayerCards.IsCardId(7); i++)
                {
                    carteSpeciale(7);
                }
            }
            else if ((dieResult + 1 + die2Result + 1) == 6)
            {
                for (int i = 0; i < ordi.PlayerCards.IsCardId(9); i++)
                {
                    carteSpeciale(9);
                }
            }

            // Affichage du gold de chaque joueur
            if (player.Pieces < 0)
                player.Pieces = 0;
            playerGold.Content = player.Pieces;
            if (ordi.Pieces < 0)
                ordi.Pieces = 0;
            aiGold.Content = ordi.Pieces;
            UpdateStyle();

            await Task.Delay(1500);

            CheckWin(); 
            if(!endGame)
                DisplayAIChoice();
        }

        private async void DisplayAIChoice()
        {
            int rngAchatAI = 0;
            rngAchatAI = rng.Next(0, ordi.PlayerCards.cards.Count);

            if(rng.Next(ordi.Pieces,20) > 14) {
                if(rng.Next(0, ordi.PlayerCards.cards.Count) <= 5)
                {
                    rngAchatAI = 1;
                }
            }

            if (ordi.Pieces < 3)
                if(rng.Next(0,100) < 60)
                    rngAchatAI = 3;

            if (rngAchatAI <= 1 && ordi.Pieces > 0)
            {
                ThrowDie.Visibility = Visibility.Collapsed;
                ThrowDie2.Visibility = Visibility.Collapsed;
                diceResult.Visibility = Visibility.Collapsed;
                diceResult.Margin = new Thickness(495, 500, 0, 0);

                AIChoice.Margin = new Thickness(465, 600, 0, 0);
                AIChoice.FontSize = 26;
                AIChoice.Content = "L'IA achete une nouvelle carte";
                AIChoice.Visibility = Visibility.Visible;

                for (int i = 0; i < Card.GetCardCosts().Count; i++)
                {
                    if(i != 8)
                    {
                        if (ordi.Pieces >= Card.GetCardCosts()[i] && Card.CardShop[i].Number > 0)
                        {
                            canAIBuy.Add(i);
                        }
                    }   
                }

                if(canAIBuy.Count > 0)
                {
                    ordi.BuyCard(canAIBuy[rng.Next(0, canAIBuy.Count)]);
                }

                AICardBought.Source = imgSource[ordi.PlayerCards.cards[ordi.PlayerCards.cards.Count-1].Id.ToString()];
                AICardBought.Visibility = Visibility.Visible;

                await Task.Delay(2500);
                AICardBought.Visibility = Visibility.Collapsed;

                UpdateCardsCopy();
                UpdateShop();
            }
            else
            {
                AIChoice.Margin = new Thickness(463, 600, 0, 0);
                AIChoice.FontSize = 23;
                AIChoice.Content = "L'IA n'achete pas de nouvelle carte";
                AIChoice.Visibility = Visibility.Visible;
                await Task.Delay(1000);
            }

            ThrowDie.Visibility = Visibility.Collapsed;
            ThrowDie2.Visibility = Visibility.Collapsed;
            diceResult.Visibility = Visibility.Collapsed;
            diceResult.Margin = new Thickness(495, 500, 0, 0);

            nbAiCards.Content = ordi.PlayerCards.cards.Count;
            AIChoice.Visibility = Visibility.Collapsed;
            await Task.Delay(100);

            textTour.Source = playerTextTourSource;
            await Task.Delay(250);

            choiceButtonsBar.Visibility = Visibility.Visible;
            choiceBtnLeft.Visibility = Visibility.Visible;
            choiceBtnRight.Visibility = Visibility.Visible;
            DiceQuestion.Visibility = Visibility.Visible;
            choiceBtnRight.IsEnabled = true;
            choiceBtnLeft.IsEnabled = true;

        }


        private void ClearDisplay()
        {
            choiceButtonsBar.Visibility = Visibility.Collapsed;
            choiceBtnLeft.Visibility = Visibility.Collapsed;
            choiceBtnRight.Visibility = Visibility.Collapsed;
            DiceQuestion.Visibility = Visibility.Collapsed;
            ThrowDie.Visibility = Visibility.Collapsed;
            ThrowDie2.Visibility = Visibility.Collapsed;
            diceResult.Visibility = Visibility.Collapsed;
            shopQuestion.Visibility = Visibility.Collapsed;
            choiceButtonsBarShop.Visibility = Visibility.Collapsed;
            choiceBtnLeftShop.Visibility = Visibility.Collapsed;
            choiceBtnRightShop.Visibility = Visibility.Collapsed;
            textTour.Visibility = Visibility.Collapsed;
        }


        private bool CheckWin()
        {
            if(player.Pieces >= 20 || ordi.Pieces >= 20)
            {
                endGame = true;
                ClearDisplay();
                victoryText.Visibility = Visibility.Visible;
                if (ordi.Pieces == player.Pieces)
                {
                    victoryText.Source = TieText;
                }
                else if (player.Pieces > ordi.Pieces)
                {
                    victoryText.Source = playerWonText;
                }
                else
                {
                    victoryText.Source = AIWonText;
                }
                return true;
            }
            return false;
        }



        private void DisplayChoiceShop()
        {
            shopQuestion.Visibility = Visibility.Visible;
            choiceButtonsBarShop.Visibility = Visibility.Visible;
            choiceBtnLeftShop.Visibility = Visibility.Visible;
            choiceBtnRightShop.Visibility = Visibility.Visible;
        }

        //Boutons choix lancé de dé(s)
        private void choiceBtnRight_Click(object sender, RoutedEventArgs e)
        {

            ThrowDie.Margin = new Thickness(523, 380, 0, 0);
            ThrowDie.Visibility = Visibility.Visible;
            ThrowDie2.Visibility = Visibility.Visible;
            choiceBtnRight.IsEnabled = false;
            choiceBtnLeft.IsEnabled = false;
            DisplayPlayerDiceThrow();
        }

        private void choiceBtnLeft_Click(object sender, RoutedEventArgs e)
        {
            ThrowDie.Margin = new Thickness(583, 380, 0, 0);
            ThrowDie.Visibility = Visibility.Visible;
            choiceBtnRight.IsEnabled = false;
            choiceBtnLeft.IsEnabled = false;
            DisplayPlayerDiceThrow();
        }

        private void choiceBtnLeft_MouseEnter(object sender, MouseEventArgs e)
        {
            imgBtnChoixLeft.Source = choiceBtnLeftEnterSource;
        }

        private void choiceBtnLeft_MouseLeave(object sender, MouseEventArgs e)
        {
            imgBtnChoixLeft.Source = choiceBtnLeftLeaveSource;
        }

        private void choiceBtnRight_MouseEnter(object sender, MouseEventArgs e)
        {
            imgBtnChoixRight.Source = choiceBtnRightEnterSource;
        }

        private void choiceBtnRight_MouseLeave(object sender, MouseEventArgs e)
        {
            imgBtnChoixRight.Source = choiceBtnRightLeaveSource;
        }
        /////////


        // Boutons Choix Shop
        private void choiceBtnLeftShop_Click(object sender, RoutedEventArgs e)
        {
            playerGoldShop.Content = playerGold.Content;

            bgShop.Visibility = Visibility.Visible;
            playerGoldShop.Visibility = Visibility.Visible;
            btnRetourShop.Visibility = Visibility.Visible;

            shopCard1.Visibility = Visibility.Visible;
            shopCard2.Visibility = Visibility.Visible;
            shopCard3.Visibility = Visibility.Visible;
            shopCard4.Visibility = Visibility.Visible;
            shopCard5.Visibility = Visibility.Visible;
            shopCard6.Visibility = Visibility.Visible;
            shopCard7.Visibility = Visibility.Visible;
            shopCard8.Visibility = Visibility.Visible;
            shopCard9.Visibility = Visibility.Visible;
            shopCard10.Visibility = Visibility.Visible;
            shopCard11.Visibility = Visibility.Visible;
            shopCard12.Visibility = Visibility.Visible;
            shopCard13.Visibility = Visibility.Visible;
            shopCard14.Visibility = Visibility.Visible;

            this.WindowTitle = "Boutique - Minivilles";
        }


        private async void AIturn()
        {
            shopQuestion.Visibility = Visibility.Collapsed;
            choiceButtonsBarShop.Visibility = Visibility.Collapsed;
            choiceBtnLeftShop.Visibility = Visibility.Collapsed;
            choiceBtnRightShop.Visibility = Visibility.Collapsed;

            await Task.Delay(100);
            textTour.Source = aiTextTourSource;
            await Task.Delay(250);
            AIDiceThrow.Visibility = Visibility.Visible;
            DisplayAIDiceThrow();
        }


        private void choiceBtnRightShop_Click(object sender, RoutedEventArgs e)
        {
            AIturn();
        }

        private void choiceBtnLeftShop_MouseEnter(object sender, MouseEventArgs e)
        {
            imgBtnChoixLeftShop.Source = choiceBtnLeftEnterShopSource;
        }

        private void choiceBtnLeftShop_MouseLeave(object sender, MouseEventArgs e)
        {
            imgBtnChoixLeftShop.Source = choiceBtnLeftLeaveShopSource;
        }

        private void choiceBtnRightShop_MouseEnter(object sender, MouseEventArgs e)
        {
            imgBtnChoixRightShop.Source = choiceBtnRightEnterShopSource;
        }

        private void choiceBtnRightShop_MouseLeave(object sender, MouseEventArgs e)
        {
            imgBtnChoixRightShop.Source = choiceBtnRightLeaveShopSource;
        }


        private void RetourShop()
        {
            bgShop.Visibility = Visibility.Collapsed;
            playerGoldShop.Visibility = Visibility.Collapsed;
            btnRetourShop.Visibility = Visibility.Collapsed;

            shopCard1.Visibility = Visibility.Collapsed;
            shopCard2.Visibility = Visibility.Collapsed;
            shopCard3.Visibility = Visibility.Collapsed;
            shopCard4.Visibility = Visibility.Collapsed;
            shopCard5.Visibility = Visibility.Collapsed;
            shopCard6.Visibility = Visibility.Collapsed;
            shopCard7.Visibility = Visibility.Collapsed;
            shopCard8.Visibility = Visibility.Collapsed;
            shopCard9.Visibility = Visibility.Collapsed;
            shopCard10.Visibility = Visibility.Collapsed;
            shopCard11.Visibility = Visibility.Collapsed;
            shopCard12.Visibility = Visibility.Collapsed;
            shopCard13.Visibility = Visibility.Collapsed;
            shopCard14.Visibility = Visibility.Collapsed;

            this.WindowTitle = "Jeu - Minivilles";
        }

        // Boutons Shop
        private void btnRetourShop_Click(object sender, RoutedEventArgs e)
        {
            RetourShop();
        }

        private void btnRetourShop_MouseEnter(object sender, MouseEventArgs e)
        {
            imgRetourShop.Source = btnRetourShopEnterSource;
        }

        private void btnRetourShop_MouseLeave(object sender, MouseEventArgs e)
        {
            imgRetourShop.Source = btnRetourShopLeaveSource;
        }
        /////////


        // Affichage cartes du joueur
        public void DisplayPlayerCards()
        {
            Dictionary<string, Image> pVar = new Dictionary<string, Image>()
            {
                {"p_sp1_c1", p_sp1_c1},
                {"p_sp1_c2", p_sp1_c2},
                {"p_sp1_c3", p_sp1_c3},
                {"p_sp2_c1", p_sp2_c1},
                {"p_sp2_c2", p_sp2_c2},
                {"p_sp2_c3", p_sp2_c3},
                {"p_sp3_c1", p_sp3_c1},
                {"p_sp3_c2", p_sp3_c2},
                {"p_sp3_c3", p_sp3_c3},
                {"p_sp4_c1", p_sp4_c1},
                {"p_sp4_c2", p_sp4_c2},
                {"p_sp4_c3", p_sp4_c3},
                {"p_sp5_c1", p_sp5_c1},
                {"p_sp5_c2", p_sp5_c2},
                {"p_sp5_c3", p_sp5_c3},
                {"p_sp6_c1", p_sp6_c1},
                {"p_sp6_c2", p_sp6_c2},
                {"p_sp6_c3", p_sp6_c3},
                {"p_sp7_c1", p_sp7_c1},
                {"p_sp7_c2", p_sp7_c2},
                {"p_sp7_c3", p_sp7_c3},
                {"p_sp8_c1", p_sp8_c1},
                {"p_sp8_c2", p_sp8_c2},
                {"p_sp8_c3", p_sp8_c3},
                {"p_sp9_c1", p_sp9_c1},
                {"p_sp9_c2", p_sp9_c2},
                {"p_sp9_c3", p_sp9_c3},
                {"p_sp10_c1", p_sp10_c1},
                {"p_sp10_c2", p_sp10_c2},
                {"p_sp10_c3", p_sp10_c3}
            };

            int lines = 0;
            int columns = 0;
            foreach(CardsInfo card in player.PlayerCards.cards)
            {
                columns++;
                if (columns == 3) {
                    lines++;
                    columns = 0;
                }
            }

            int cardCounter = 0;
            for (int j = 1; j <= lines; j++)
            {
                for(int i = 1; i <= 3; i++)
                {
                    pVar[$"p_sp{j}_c{i}"].Visibility = Visibility.Visible;
                    pVar[$"p_sp{j}_c{i}"].Source = imgSource[player.PlayerCards.cards[cardCounter].Id.ToString()];
                    pVar[$"p_sp{j}_c{i}"].Cursor = Cursors.Hand;
                    cardCounter++;
                }
            }
            
            for(int x = 1; x <= columns; x++)
            {
                pVar[$"p_sp{lines+1}_c{x}"].Visibility = Visibility.Visible;
                pVar[$"p_sp{lines+1}_c{x}"].Source = imgSource[player.PlayerCards.cards[cardCounter].Id.ToString()];
                pVar[$"p_sp{lines + 1}_c{x}"].Cursor = Cursors.Hand;
                cardCounter++;
            }

        }




        // Affichage cartes de l'adversaire
        public void DisplayAICards()
        {
            Dictionary<string, Image> aiVar = new Dictionary<string, Image>()
            {
                {"ai_sp1_c1", ai_sp1_c1},
                {"ai_sp1_c2", ai_sp1_c2},
                {"ai_sp1_c3", ai_sp1_c3},
                {"ai_sp2_c1", ai_sp2_c1},
                {"ai_sp2_c2", ai_sp2_c2},
                {"ai_sp2_c3", ai_sp2_c3},
                {"ai_sp3_c1", ai_sp3_c1},
                {"ai_sp3_c2", ai_sp3_c2},
                {"ai_sp3_c3", ai_sp3_c3},
                {"ai_sp4_c1", ai_sp4_c1},
                {"ai_sp4_c2", ai_sp4_c2},
                {"ai_sp4_c3", ai_sp4_c3},
                {"ai_sp5_c1", ai_sp5_c1},
                {"ai_sp5_c2", ai_sp5_c2},
                {"ai_sp5_c3", ai_sp5_c3},
                {"ai_sp6_c1", ai_sp6_c1},
                {"ai_sp6_c2", ai_sp6_c2},
                {"ai_sp6_c3", ai_sp6_c3},
                {"ai_sp7_c1", ai_sp7_c1},
                {"ai_sp7_c2", ai_sp7_c2},
                {"ai_sp7_c3", ai_sp7_c3},
                {"ai_sp8_c1", ai_sp8_c1},
                {"ai_sp8_c2", ai_sp8_c2},
                {"ai_sp8_c3", ai_sp8_c3},
                {"ai_sp9_c1", ai_sp9_c1},
                {"ai_sp9_c2", ai_sp9_c2},
                {"ai_sp9_c3", ai_sp9_c3},
                {"ai_sp10_c1", ai_sp10_c1},
                {"ai_sp10_c2", ai_sp10_c2},
                {"ai_sp10_c3", ai_sp10_c3}
            };

            int lines = 0;
            int columns = 0;
            foreach (CardsInfo card in ordi.PlayerCards.cards)
            {
                columns++;
                if (columns == 3)
                {
                    lines++;
                    columns = 0;
                }
            }

            int cardCounter = 0;
            for (int j = 1; j <= lines; j++)
            {
                for (int i = 1; i <= 3; i++)
                {
                    aiVar[$"ai_sp{j}_c{i}"].Visibility = Visibility.Visible;
                    aiVar[$"ai_sp{j}_c{i}"].Source = imgSource[ordi.PlayerCards.cards[cardCounter].Id.ToString()];
                    aiVar[$"ai_sp{j}_c{i}"].Cursor = Cursors.Hand;
                    cardCounter++;
                }
            }

            for (int x = 1; x <= columns; x++)
            {
                aiVar[$"ai_sp{lines + 1}_c{x}"].Visibility = Visibility.Visible;
                aiVar[$"ai_sp{lines + 1}_c{x}"].Source = imgSource[ordi.PlayerCards.cards[cardCounter].Id.ToString()];
                aiVar[$"ai_sp{lines + 1}_c{x}"].Cursor = Cursors.Hand;
                cardCounter++;
            }

        }





        // Player Detailled Cards

        private void p_detailledImage_MouseLeave(object sender, MouseEventArgs e)
        {
            pImageDetailled.Visibility = Visibility.Collapsed;
        }

        private void p_sp1_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp1_c1.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp1_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp1_c2.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp1_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp1_c3.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp2_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp2_c1.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp2_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp2_c2.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp2_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp2_c3.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp3_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp3_c1.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp3_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp3_c2.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp3_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp3_c3.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp4_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp4_c1.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp4_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp4_c2.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp4_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp4_c3.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp5_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp5_c1.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp5_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp5_c2.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp5_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp5_c3.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp6_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp6_c1.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp6_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp6_c2.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp6_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp6_c3.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp7_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp7_c1.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp7_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp7_c2.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp7_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp7_c3.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp8_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp8_c1.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp8_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp8_c2.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp8_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp8_c3.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp9_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp9_c1.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp9_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp9_c2.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp9_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp9_c3.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp10_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp10_c1.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp10_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp10_c2.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }

        private void p_sp10_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            pImageDetailled.Source = p_sp10_c3.Source;
            pImageDetailled.Visibility = Visibility.Visible;
        }




        // IA Detailled Cards

        private void ai_detailledImage_MouseLeave(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Visibility = Visibility.Collapsed;
        }
        
        private void ai_sp1_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp1_c1.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp1_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp1_c2.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp1_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp1_c3.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp2_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp2_c1.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp2_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp2_c2.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp2_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp2_c3.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp3_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp3_c1.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp3_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp3_c2.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp3_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp3_c3.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp4_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp4_c1.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp4_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp4_c2.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp4_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp4_c3.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp5_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp5_c1.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp5_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp5_c2.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp5_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp5_c3.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp6_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp6_c1.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp6_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp6_c2.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp6_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp6_c3.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp7_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp7_c1.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp7_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp7_c2.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp7_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp7_c3.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp8_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp8_c1.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp8_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp8_c2.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp8_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp8_c3.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp9_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp9_c1.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp9_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp9_c2.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp9_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp9_c3.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp10_btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp10_c1.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp10_btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp10_c2.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }

        private void ai_sp10_btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            aiImageDetailled.Source = ai_sp10_c3.Source;
            aiImageDetailled.Visibility = Visibility.Visible;
        }


        private void UpdateCardsCopy()
        {
            shopCardCount1.Content = $"x{Card.GetCard(0).Number}";
            shopCardCount2.Content = $"x{Card.GetCard(1).Number}";
            shopCardCount3.Content = $"x{Card.GetCard(2).Number}";
            shopCardCount4.Content = $"x{Card.GetCard(3).Number}";
            shopCardCount5.Content = $"x{Card.GetCard(4).Number}";
            shopCardCount6.Content = $"x{Card.GetCard(5).Number}";
            shopCardCount7.Content = $"x{Card.GetCard(7).Number}";
            shopCardCount8.Content = $"x{Card.GetCard(9).Number}";
            shopCardCount9.Content = $"x{Card.GetCard(10).Number}";
            shopCardCount10.Content = $"x{Card.GetCard(11).Number}";
            shopCardCount11.Content = $"x{Card.GetCard(12).Number}";
            shopCardCount12.Content = $"x{Card.GetCard(6).Number}";
            shopCardCount13.Content = $"x{Card.GetCard(13).Number}";
            shopCardCount14.Content = $"x{Card.GetCard(14).Number}";
        }

        private void CardBought()
        {
            UpdateShop();
            RetourShop();
            UpdateStyle();
            nbPlayerCards.Content = player.PlayerCards.cards.Count;
            AIturn();
        }


        //Achat Cartes

        private void shopCardBtn1_Click(object sender, RoutedEventArgs e)
        {
            if (player.Pieces >= Card.GetCard(0).Cost && Card.GetCard(0).Number > 0)
            {
                shopCardCount1.Content = $"x{Card.GetCard(0).Number}";
                player.BuyCard(0);
                CardBought();
            }
        }

        private void shopCardBtn2_Click(object sender, RoutedEventArgs e)
        {
            if (player.Pieces >= Card.GetCard(1).Cost && Card.GetCard(1).Number > 0)
            {
                player.BuyCard(1);
                shopCardCount2.Content = $"x{Card.GetCard(1).Number}";
                CardBought();
            }
        }

        private void shopCardBtn3_Click(object sender, RoutedEventArgs e)
        {
            if (player.Pieces >= Card.GetCard(2).Cost && Card.GetCard(2).Number > 0)
            {
                player.BuyCard(2);
                shopCardCount3.Content = $"x{Card.GetCard(2).Number}";
                CardBought();
            }
        }

        private void shopCardBtn4_Click(object sender, RoutedEventArgs e)
        {
            if (player.Pieces >= Card.GetCard(3).Cost && Card.GetCard(3).Number > 0)
            {
                player.BuyCard(3);
                shopCardCount4.Content = $"x{Card.GetCard(3).Number}";
                CardBought();
            }
        }

        private void shopCardBtn5_Click(object sender, RoutedEventArgs e)
        {
            if (player.Pieces >= Card.GetCard(4).Cost && Card.GetCard(4).Number > 0)
            {
                player.BuyCard(4);
                shopCardCount5.Content = $"x{Card.GetCard(4).Number}";
                CardBought();
            }
        }

        private void shopCardBtn6_Click(object sender, RoutedEventArgs e)
        {
            if (player.Pieces >= Card.GetCard(5).Cost && Card.GetCard(5).Number > 0)
            {
                player.BuyCard(5);
                shopCardCount6.Content = $"x{Card.GetCard(5).Number}";
                CardBought();
            }
        }

        private void shopCardBtn7_Click(object sender, RoutedEventArgs e)
        {
            if (player.Pieces >= Card.GetCard(7).Cost && Card.GetCard(7).Number > 0)
            {
                player.BuyCard(7);
                shopCardCount7.Content = $"x{Card.GetCard(7).Number}";
                CardBought();
            }
        }

        private void shopCardBtn8_Click(object sender, RoutedEventArgs e)
        {
            if (player.Pieces >= Card.GetCard(9).Cost && Card.GetCard(9).Number > 0)
            {
                player.BuyCard(9);
                shopCardCount8.Content = $"x{Card.GetCard(9).Number}";
                CardBought();
            }
        }

        private void shopCardBtn9_Click(object sender, RoutedEventArgs e)
        {
            if (player.Pieces >= Card.GetCard(10).Cost && Card.GetCard(10).Number > 0)
            {
                player.BuyCard(10);
                shopCardCount9.Content = $"x{Card.GetCard(10).Number}";
                CardBought();
            }
        }

        private void shopCardBtn10_Click(object sender, RoutedEventArgs e)
        {
            if (player.Pieces >= Card.GetCard(11).Cost && Card.GetCard(11).Number > 0)
            {
                player.BuyCard(11);
                shopCardCount10.Content = $"x{Card.GetCard(11).Number}";
                CardBought();
            }
        }

        private void shopCardBtn11_Click(object sender, RoutedEventArgs e)
        {
            if (player.Pieces >= Card.GetCard(12).Cost && Card.GetCard(12).Number > 0)
            {
                player.BuyCard(12);
                shopCardCount11.Content = $"x{Card.GetCard(12).Number}";
                CardBought();
            }
        }

        private void shopCardBtn12_Click(object sender, RoutedEventArgs e)
        {
            if (player.Pieces >= Card.GetCard(6).Cost && Card.GetCard(6).Number > 0)
            {
                player.BuyCard(6);
                shopCardCount12.Content = $"x{Card.GetCard(6).Number}";
                CardBought();
            }
        }

        private void shopCardBtn13_Click(object sender, RoutedEventArgs e)
        {
            if (player.Pieces >= Card.GetCard(13).Cost && Card.GetCard(13).Number > 0)
            {
                player.BuyCard(13);
                shopCardCount13.Content = $"x{Card.GetCard(13).Number}";
                CardBought();
            }
        }

        private void shopCardBtn14_Click(object sender, RoutedEventArgs e)
        {
            if (player.Pieces >= Card.GetCard(14).Cost && Card.GetCard(14).Number > 0)
            {
                player.BuyCard(14);
                shopCardCount14.Content = $"x{Card.GetCard(14).Number}";
                CardBought();
            }
        }
    }
}
