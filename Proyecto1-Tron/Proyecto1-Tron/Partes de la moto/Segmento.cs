using PruebasDePOO.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Tron.LinkedLists
{
    public class Segmento
    {
        public PictureBox pictureBox;
        public FourNode nodo;
        public bool esMoto; // True si es la moto, false si es parte de la estela

        public Segmento(PictureBox pictureBox, FourNode nodo, bool esMoto)
        {
            this.pictureBox = pictureBox;
            this.nodo = nodo;
            this.esMoto = esMoto;
        }
    }
}
