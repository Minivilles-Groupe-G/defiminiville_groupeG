using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinivilleConsole
{
    public class Dice
    {
        int facesNb;
        Random random = new Random();
        int _face;
        public int face { get { return _face; } set { _face = value; } }

        public Dice(int facesNb)
        {
            this.facesNb = facesNb;
        }

        public int Roll()
        {
            return random.Next(1, facesNb + 1);
        }

        public override string ToString()
        {
            string toString = string.Format("[nom] à lancé le dé et à fait : {0}", Roll());
            return toString;
        }
    }
}