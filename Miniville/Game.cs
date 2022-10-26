using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Miniville
{
    internal class Game
    {
        public bool turn;
        public bool endGame;
        public bool scoreGoal;
        public int difficulty;

        //Objet
        //private Player player = new Player();
        //private Player ai = new Player();

        private List<int> canAIBuy;
        //private Display display = new Display();
        private Random random = new Random();
        private Dice die;
        public int dice = 0;
        public int dice2 = 0;

        public Game(bool turn, bool endGame, bool scoreGoal, int difficulty, Player player, Player ai, List<int> canAIBuy, Random random, Dice die, int dice, int dice2)
        {
            this.turn = turn;
            this.endGame = endGame;
            this.scoreGoal = scoreGoal;
            this.difficulty = difficulty;
            //this.player = player;
            //this.ai = ai;
            this.canAIBuy = canAIBuy;
            this.random = random;
            this.die = die;
            this.dice = dice;
            this.dice2 = dice2;
        }

        public void GameLoop()
        {

        }

        public void PlayerTurn(Player player, Player opponent, string numberOfDice)
        {
            dice = 0;
            dice2 = 0;

            if (int.Parse(numberOfDice) == 1) { dice = die.Roll(); }
            else
            {
                dice = die.Roll();
                dice2 = die.Roll();
            }

            //Fonction classe player
        }

        public void AiTurn(Player ai, Player opponent, string numberOfDice)
        {
            //Intelligence AI
        }

        public bool CheckEndGame(int scoreGoal, int score)
        {
            bool haveAWinner = false;

            if (score >= scoreGoal) { haveAWinner = true; }
            
            return haveAWinner;
        }

        public void CheckPlayerWin(int scoreGoal, int scorePlayer)
        {
            //En attente de display
        }
    }
}
