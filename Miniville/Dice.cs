using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville
{
    internal class Dice
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
            int result;
            result = random.Next(0, facesNb + 1);
            return result;
        }

        public override string ToString()
        {
            string toString = String.Format("[nom] à lancé le dé et à fait : {0}", Roll());
            return toString;
        }
    }
}
