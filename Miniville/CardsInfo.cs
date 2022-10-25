using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville
{
    internal class CardsInfo
    {
        string _name;
        string _color;
        string _effect;

        public string name { get { return _name; } set { _name = value; } }
        public string color { get { return _color; } set { _color = value; } }
        public string effect { get { return _effect; } set { _effect = value; } }

        int _cost;
        int _maxDice;
        int _minDice;
        int _gain;

        public int cost { get { return _cost; } set { _cost = value; } }
        public int maxDice { get { return _maxDice; } set { _maxDice = value; } }
        public int minDice { get { return _minDice; } set { _minDice = value; } }
        public int gain { get { return _gain; } set { _gain = value; } }

        public CardsInfo(string name, string color, string effect, int cost, int maxSice, int minDice, int gain)
        {
            _name = name;
            _color = color;
            _effect = effect;

            _cost = cost;
            _maxDice = maxSice;
            _minDice = minDice;
            _gain = gain;
        }
    }
}
