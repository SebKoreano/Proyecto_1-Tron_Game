using PruebasDePOO.Listas;
using PruebasDePOO.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Tron.Grid
{
    internal class Grid
    {
        public void CreateGrid(int columns, int rows)
        {
            FourLinkedList newGrid = new FourLinkedList();
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

                    y += 50;

                    newGrid.AddDown(x, y, TopNode, LeftNode);
                }

                y = 0;
                if (col < columns - 1)
                {
                    x += 50;
                    newGrid.AddRight(x, y);
                    TopNode = TopNode.Righ;
                }
            }
        }
    }
}
