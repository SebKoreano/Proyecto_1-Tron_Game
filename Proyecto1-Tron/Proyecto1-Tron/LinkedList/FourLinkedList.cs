using PruebasDePOO.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PruebasDePOO.Listas
{
    public class FourLinkedList
    {
        private FourNode head;
        private int size;

        public FourLinkedList()
        {
            head = null;
            size = 0;
        }

        public void AddRight(int x, int y)
        {
            FourNode newNode = new FourNode(x, y);
            
            FourNode current = head;
            while (current.Righ != null)
            {
                current = current.Righ;
            }
            current.Righ = newNode;
            newNode.Left = current;
            size++;
        }

        public void AddDown(int x, int y, FourNode TopNode, FourNode LeftNode)
        {
            FourNode newNode = new FourNode(x, y);
            
            FourNode current = TopNode;
            while (current.Down != null)
            {
                current = current.Down;
            }
            current.Down = newNode;
            newNode.Up = current;
            if (LeftNode != null) 
            {
                LeftNode.Righ = newNode;
                newNode.Left = LeftNode;
            }
            
            size++;
        }

        public void AddHead(int x, int y)
        {
            FourNode newNode = new FourNode(x, y);
            if (head == null)
            {
                head = newNode;
            }
            size++;
        }

        public FourNode GetHead()
        {
        return head; 
        }
    }
}