using PruebasDePOO.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proyecto1_Tron
{
    public class Moto
    {
        public FourNode currentNode;
        public PictureBox motoPictureBox;
        private Form VentanaPrincipal;
        private Grid grid;
        private Estela estela;

        public Moto(Grid grid, Form ventanaPrincipal, Estela estela)
        {
            this.grid = grid;
            currentNode = grid.GetHead();
            VentanaPrincipal = ventanaPrincipal;
            this.estela = estela;
        }

        public void IniciarMoto()
        {
            // Cargar una imagen 
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

        public void UpdateMoto(object sender, KeyEventArgs e)
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
                    if (currentNode.Right != null)
                        currentNode = currentNode.Right;
                    break;
            }

            if (currentNode.Ocupante == "imagen")
            {

            }
            currentNode.Ocupante = "moto";
            imprimir(currentNode.Ocupante);

            // Actualizar la posición del PictureBox
            motoPictureBox.Location = new Point(currentNode.X, currentNode.Y);

            // Actualizar la estela
            estela.UpdateEstela(previousNode);
        }

        public void imprimir(string msg)
        {
            VentanaPrincipal.Text = msg;
        }
    }
}
