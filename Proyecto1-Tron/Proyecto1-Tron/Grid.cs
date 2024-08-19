using PruebasDePOO.Listas;
using PruebasDePOO.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Tron
{
    public class Grid
    {
        private FourLinkedList newGrid;
        public Grid()
        {
            newGrid = new FourLinkedList();
        }

        public void CreateGrid(int columns, int rows)
        {
            int x = 0;
            int y = 0;

            newGrid.AddHead(x, y);

            FourNode TopNode = newGrid.GetHead();

            for (int col = 0; col < columns; col++)
            {
                FourNode LeftNode = TopNode.Left;

                for (int row = 1; row < rows; row++)
                {
                    if (LeftNode != null)
                    {
                        LeftNode = LeftNode.Down;
                    }

                    y += 75;

                    newGrid.AddDown(x, y, TopNode, LeftNode);
                }

                y = 0;
                if (col < columns - 1)
                {
                    x += 75;
                    newGrid.AddRight(x, y);
                    TopNode = TopNode.Righ;
                }
            }
        }

        public FourNode GetHead()
        {
            return newGrid.GetHead();
        }

        public void TraverseAndDraw(Form form, Image image)
        {
            FourNode currentRow = newGrid.GetHead();

            while (currentRow != null)
            {
                FourNode current = currentRow;

                while (current != null)
                {
                    // Crear un PictureBox para mostrar la imagen en la posición del nodo
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Image = image;
                    pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;

                    // Asignar la posición en el formulario
                    pictureBox.Location = new Point(current.X, current.Y);

                    // Agregar el PictureBox al formulario
                    form.Controls.Add(pictureBox);

                    // Moverse al siguiente nodo a la derecha
                    current = current.Righ;
                }

                // Moverse al siguiente nodo hacia abajo (siguiente fila)
                currentRow = currentRow.Down;
            }
        }
    }
}
