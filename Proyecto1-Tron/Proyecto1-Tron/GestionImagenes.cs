using PruebasDePOO.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Tron
{
    public class GestionImagenes
    {
        internal Grid grid;
        internal Random random;
        internal PictureBox randomPictureBox;
        internal Form VentanaPrincipal;
        internal int maxImages = 3;
        internal int numImages = 0;
        internal int cantidadImg;
        internal Image[] Images;

        public GestionImagenes(Grid grid, Form VentanaPrincipal) 
        { 
            this.grid = grid;
            random = new Random();
            this.VentanaPrincipal = VentanaPrincipal;
        }

        public async void GenerarImagenes()
        {
            while (true)
            {
                Image randomImage = Images[random.Next(cantidadImg)];

                if (numImages < maxImages)
                {
                    PlaceRandomImage(randomImage);
                }

                await Task.Delay(500);
            }
        }

        private void PlaceRandomImage(Image image)
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
                currentNode = currentNode.Right;
            }
            for (int j = 0; j < randomRow; j++)
            {
                currentNode = currentNode.Down;
            }

            if (currentNode.Ocupante == null)
            {
                currentNode.Ocupante = "imagen";
                // Colocar la imagen en la posición aleatoria
                randomPictureBox.Location = new Point(currentNode.X, currentNode.Y);
                randomPictureBox.Visible = true; // Hacerla visible
                numImages++;
            }
        }
    }
}
