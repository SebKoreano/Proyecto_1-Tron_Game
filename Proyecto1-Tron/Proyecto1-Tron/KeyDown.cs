using PruebasDePOO.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Tron
{
    public class KeyDown
    {
        private FourNode currentNode;
        private PictureBox motoPictureBox;
        private Moto moto;

        public KeyDown(Moto moto) 
        {
            this.currentNode = moto.currentNode;
            this.motoPictureBox = moto.motoPictureBox;
            this.moto = moto;
        }
        public void PrecionarFlecha(object sender, KeyEventArgs e)
        {
            FourNode previousNode = currentNode;
            // Mover el nodo actual basado en la tecla presionada
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (currentNode.Up != null)
                        currentNode = currentNode.Up;
                    break;
                case Keys.Down:
                    if (currentNode.Down != null)
                        currentNode = currentNode.Down;
                    break;
                case Keys.Left:
                    if (currentNode.Left != null)
                        currentNode = currentNode.Left;
                    break;
                case Keys.Right:
                    if (currentNode.Righ != null)
                        currentNode = currentNode.Righ;
                    break;
            }

            // Actualizar la posición del PictureBox
            motoPictureBox.Location = new Point(currentNode.X, currentNode.Y);

            // Actualizar la estela
            moto.UpdateEstela(previousNode);
        }
    }
}
