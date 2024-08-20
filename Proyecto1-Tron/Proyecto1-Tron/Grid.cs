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
                    TopNode = TopNode.Right;
                }
            }
        }

        public FourNode GetHead()
        {
            return newGrid.GetHead();
        }

        public FourNode FindNodeByCoordinates(int x, int y)
        {
            FourNode currentRow = newGrid.GetHead();

            while (currentRow != null)
            {
                FourNode currentNode = currentRow;
                while (currentNode != null)
                {
                    if (currentNode.X == x && currentNode.Y == y)
                    {
                        return currentNode;
                    }
                    currentNode = currentNode.Right;
                }
                currentRow = currentRow.Down;
            }

            return null; // Retorna null si no se encuentra el nodo con las coordenadas especificadas
        }

    }
}
