using Proyecto1_Tron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PruebasDePOO.Nodes
{
    public class FourNode
    {
        public int X;
        public int Y;
        public Moto moto;
        public Items Item;
        public FourNode Up;
        public Poderes Poder;
        public FourNode Down;
        public FourNode Left;
        public FourNode Right;
        internal string Ocupante;
        public PictureBox Imagen;

        public FourNode(int x, int y)
        {
            X = x;
            Y = y;
            Up = null;
            Down = null;
            Left = null;
            Right = null;
            Item = null;
            Poder = null;
            moto = null;
            Imagen = null;
        }

        public void SetOcupante(object obj)
        {
            if (obj is Moto) 
            {
                moto = (Moto)obj;
                Ocupante = "Moto";
            }
            else if (obj is Items)
            {
                Item = (Items)obj;
                Ocupante = "Item";
            }
            else if (obj is Poderes)
            {
                Poder = (Poderes)obj;
                Ocupante = "Poder";
            }
            else if (obj == null)
            {
                moto = null;
                Item = null;
                Poder = null;
                Ocupante = "null";
            }
        }

        public object GetOcupante() 
        {
            if (Ocupante == "Moto")
            {
                return (Moto)moto;
            }
            else if (Ocupante == "Item")
            {
                return (Items)Item;
            }
            else if (Ocupante == "Poder")
            {
                return (Poderes)Poder;
            }
            else
            {
                return null;
            }
        }
    }
}
