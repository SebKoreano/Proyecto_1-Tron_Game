using PruebasDePOO.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Tron
{
    public class RandomPlace
    {
        private Grid grid;
        private Random random;
        private PictureBox randomPictureBox;
        private Form VentanaPrincipal;

        public RandomPlace(Grid grid, Form VentanaPrincipal) 
        { 
            this.grid = grid;
            random = new Random();
            this.VentanaPrincipal = VentanaPrincipal;
        }

        public void PlaceRandomImage(Image image)
        {
            if (VentanaPrincipal.InvokeRequired)
            {
                // Invocar el método en el hilo principal si se llama desde un hilo secundario
                VentanaPrincipal.Invoke(new Action(() => PlaceRandomImage(image)));
            }
            else
            {
                // Crear y configurar PictureBox para la imagen aleatoria
                randomPictureBox = new PictureBox
                {
                    Image = image,
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Visible = false // Inicia oculta
                };
                VentanaPrincipal.Controls.Add(randomPictureBox);

                // Colocar la imagen en una posición aleatoria
                Place();
            }
        }


        private void Place()
        {
            // Obtener el tamaño de la grid
            int colunms = 12; // Asumiendo que la grid es de 5x5
            int rows = 10;

            // Generar posiciones aleatorias dentro de la grid
            int randomColumn = random.Next(colunms);
            int randomRow = random.Next(rows);

            // Navegar hasta la posición aleatoria en la grid
            FourNode currentNode = grid.GetHead();
            for (int i = 0; i < randomColumn; i++)
            {
                currentNode = currentNode.Righ;
            }
            for (int j = 0; j < randomRow; j++)
            {
                currentNode = currentNode.Down;
            }

            // Colocar la imagen en la posición aleatoria
            randomPictureBox.Location = new Point(currentNode.X, currentNode.Y);
            randomPictureBox.Visible = true; // Hacerla visible
        }
    }
}
