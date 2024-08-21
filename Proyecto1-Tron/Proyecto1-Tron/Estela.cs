using PruebasDePOO.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Tron
{
    public class Estela
    {
        private List<PictureBox> estela;
        private int estelaLength = 3;
        public FourNode currentNode;
        private Form VentanaPrincipal;
        private Grid grid;
        public FourNode lastNode;

        public Estela(Grid grid, Form ventanaPrincipal)
        {
            this.grid = grid;
            VentanaPrincipal = ventanaPrincipal;
            currentNode = grid.GetHead();
            estela = new List<PictureBox>();
        }
        public void IniciarEstela()
        {
            for (int i = 0; i < estelaLength; i++)
            {
                PictureBox estelaPictureBox = new PictureBox
                {
                    Image = Properties.Resources.estela3,
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Location = new Point(currentNode.X, currentNode.Y)
                };
                estela.Add(estelaPictureBox);
                VentanaPrincipal.Controls.Add(estelaPictureBox);
            }
        }

        public void UpdateEstela(FourNode previousNode)
        {
            // Actualizo el ultimo nodo para que ya no tenga estela
            lastNode = grid.FindNodeByCoordinates(estela[estela.Count - 1].Location.X, estela[estela.Count - 1].Location.Y);
            lastNode.Ocupante = null;

            // Mover cada imagen de la estela a la posición de la imagen de adelante
            for (int i = estela.Count - 1; i > 0; i--)
            {
                estela[i].Location = estela[i - 1].Location;
            }

            previousNode.Ocupante = "estela"; // Agrego estela al nodo antes de la moto
            // La primera imagen de la estela sigue a la moto
            estela[0].Location = new Point(previousNode.X, previousNode.Y);
        }
    }
}
