using PruebasDePOO.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1_Tron
{
    public class Moto
    {
        public FourNode currentNode;
        public PictureBox motoPictureBox;
        private Form VentanaPrincipal;
        private List<PictureBox> estela;
        private int estelaLength = 3;

        public Moto(Grid grid, Form ventanaPrincipal)
        {
            // Inicializar el nodo actual (inicia en el head)
            currentNode = grid.GetHead();
            VentanaPrincipal = ventanaPrincipal;

            IniciarMoto();

            // Inicializar la estela
            estela = new List<PictureBox>();
            IniciarEstela();
        }

        private void IniciarMoto()
        {
            // Cargar una imagen desde un archivo o recurso
            Image moto = Properties.Resources.moto;

            // Crear y configurar PictureBox
            motoPictureBox = new PictureBox
            {
                Image = moto,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(currentNode.X, currentNode.Y)
            };

            VentanaPrincipal.Controls.Add(motoPictureBox);
        }

        private void IniciarEstela()
        {
            for (int i = 0; i < estelaLength; i++)
            {
                PictureBox estelaPictureBox = new PictureBox
                {
                    Image = Properties.Resources.estela2,
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Location = new Point(currentNode.X, currentNode.Y)
                };
                estela.Add(estelaPictureBox);
                VentanaPrincipal.Controls.Add(estelaPictureBox);
            }
        }

        public void UpdateEstela(FourNode previousNode)
        {
            // Mover cada imagen de la estela a la posición de la imagen de adelante
            for (int i = estela.Count - 1; i > 0; i--)
            {
                estela[i].Location = estela[i - 1].Location;
            }
            // La primera imagen de la estela sigue a la moto
            estela[0].Location = new Point(previousNode.X, previousNode.Y);
        }
    }
}
