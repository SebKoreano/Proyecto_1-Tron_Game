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
        private int tamaño;
        public Grid(int tamaño)
        {
            newGrid = new FourLinkedList();
            this.tamaño = tamaño;
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

                    y += tamaño;

                    newGrid.AddDown(x, y, TopNode, LeftNode);
                }

                y = 0;
                if (col < columns - 1)
                {
                    x += tamaño;
                    newGrid.AddRight(x, y);
                    TopNode = TopNode.Right;
                }
            }
        }

        public FourNode GetHead()
        {
            return newGrid.GetHead();
        }
    }
}
