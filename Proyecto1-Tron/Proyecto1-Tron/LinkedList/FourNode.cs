using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebasDePOO.Nodes
{
    public class FourNode
    {
        public int X;
        public int Y;
        public FourNode Up;
        public FourNode Down;
        public FourNode Left;
        public FourNode Right;
        public object Ocupante;

        public FourNode(int x, int y)
        {
            X = x;
            Y = y;
            Up = null;
            Down = null;
            Left = null;
            Right = null;
            Ocupante = null;
        }
    }
}
