using PruebasDePOO.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Tron
{
    public class Inventario
    {
        internal Queue<FourNode> itemsRecogidos = new Queue<FourNode>();
        private Stack<FourNode> poderesRecogidos = new Stack<FourNode>();
        private Form VentanaPrincipal;
        private FourNode itemNode;
        private Items itemEjecutable;
        private System.Windows.Forms.Timer itemsTimer;
        internal int itemsVelocidad = 1000;
        internal PictureBox poderDisplay;
        private Moto moto;

        public Inventario(Moto moto, Form ventanaPrincipal)
        {
            VentanaPrincipal = ventanaPrincipal;
            this.moto = moto;
            SetItemsTimer();
        }

        private void SetItemsTimer()
        {
            itemsTimer = new System.Windows.Forms.Timer();
            itemsTimer.Interval = itemsVelocidad;
            itemsTimer.Tick += EjecutarItems;
            itemsTimer.Start();
        }

        public void EjecutarItems(object sender, EventArgs e)
        {
            if (itemsRecogidos.Count > 0)
            {
                itemNode = itemsRecogidos.Dequeue();
                itemEjecutable = itemNode.Item;
                itemEjecutable.Ejecutar(itemNode.Imagen, itemNode);
            }
        }

        public void EjecutarPoder()
        {
            if (poderesRecogidos.Count > 0)
            {
                FourNode poderNode = poderesRecogidos.Pop();
                poderNode.Poder.Ejecutar(poderNode.Imagen);
                poderDisplay.Image = null;
                ActualizarPoderDisplay();
            }
        }

        public void CambiarOrdenPoderes()
        {
            poderesRecogidos = new Stack<FourNode>(poderesRecogidos);
            ActualizarPoderDisplay();
        }

        public void ActualizarPoderDisplay()
        {
            if (poderesRecogidos.Count > 0)
            {
                poderDisplay.Image = poderesRecogidos.Peek().Imagen.Image;
            }
            else
            {
                poderDisplay.Image = null;
            }
        }

        public void ColocarPoderesAleatorios()
        {
            while (poderesRecogidos.Count > 0)
            {
                FourNode poderNode = poderesRecogidos.Pop();
                if (poderNode.Poder != null)
                {
                    // Obtener una posición aleatoria en la grid
                    FourNode nodoAleatorio = ObtenerNodoAleatorio();

                    // Mover el poder a la nueva posición
                    if (nodoAleatorio != null && nodoAleatorio.GetOcupante() == null)
                    {
                        nodoAleatorio.SetOcupante(poderNode.Poder);
                        nodoAleatorio.Imagen = poderNode.Imagen;
                        nodoAleatorio.Poder = poderNode.Poder;
                        nodoAleatorio.Imagen.Location = new Point(nodoAleatorio.X, nodoAleatorio.Y);
                        nodoAleatorio.Imagen.Visible = true;

                        // Agregar la imagen al formulario si no está ya agregada
                        if (!VentanaPrincipal.Controls.Contains(nodoAleatorio.Imagen))
                        {
                            VentanaPrincipal.Controls.Add(nodoAleatorio.Imagen);
                        }
                    }
                }
            }
        }

        private FourNode ObtenerNodoAleatorio()
        {
            int colunms = 12;
            int rows = 10;
            Random random = new Random();

            // Generar posiciones aleatorias dentro de la grid
            int randomColumn = random.Next(colunms);
            int randomRow = random.Next(rows);

            // Navegar hasta la posición aleatoria en la grid
            FourNode currentNode = moto.grid.GetHead();
            for (int i = 0; i < randomColumn; i++)
            {
                currentNode = currentNode.Right;
            }
            for (int j = 0; j < randomRow; j++)
            {
                currentNode = currentNode.Down;
            }

            return currentNode;
        }
    }
}
