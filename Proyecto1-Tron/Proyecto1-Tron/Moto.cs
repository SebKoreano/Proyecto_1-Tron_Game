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
        private FourNode currentNode;
        private PictureBox pictureBox;
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

            // Configurar el formulario para capturar las teclas
            VentanaPrincipal.KeyPreview = true;
            VentanaPrincipal.KeyDown += new KeyEventHandler(PrecionaFlecha);
        }

        public void IniciarMoto()
        {
            // Cargar una imagen desde un archivo o recurso
            Image moto = Properties.Resources.moto;

            // Crear y configurar PictureBox
            pictureBox = new PictureBox
            {
                Image = moto,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(currentNode.X, currentNode.Y)
            };

            VentanaPrincipal.Controls.Add(pictureBox);
        }

        private void PrecionaFlecha(object sender, KeyEventArgs e)
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
            pictureBox.Location = new Point(currentNode.X, currentNode.Y);

            // Actualizar la estela
            UpdateEstela(previousNode);
        }

        private void UpdateEstela(FourNode previousNode)
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
