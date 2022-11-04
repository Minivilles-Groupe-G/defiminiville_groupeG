using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinivilleConsole
{
    public class Cards
    {
        //Contient toute les informations relative au carte
        public int id;
        public int limit;
        public string color;
        public string type;
        public int cost;
        public string name;
        public string effect;
        public int maxDice;
        public int minDice;
        public int gain;


        public Cards(int id, int limit, string color, string type, int cost, string name, string effect, int minDice, int maxDice, int gain)
		{
			this.id = id;
			this.limit = limit;
			this.color = color;
            this.type = type;
			this.cost = cost;
			this.name = name;
			this.effect = effect;
			this.maxDice = maxDice;
			this.minDice = minDice;
            this.gain = gain;
        }
    }
}