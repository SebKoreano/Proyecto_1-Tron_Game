using Proyecto1_Tron;
using PruebasDePOO.Nodes;
using System.Collections.Generic; // Para LinkedList

namespace Proyecto1_Tron
{
    public class Moto
    {
        public LinkedList<Segmento> segmentos; // Lista enlazada que contiene la moto y los segmentos de la estela
        public FourNode currentNode;
        public PictureBox motoPictureBox;
        private Form VentanaPrincipal;
        private Grid grid;
        private Estela estela;

        public int velocidad = 500;
        public int gasolina = 100;
        private int casillasRecorridas = 0;

        private System.Windows.Forms.Timer movimientoTimer;
        private string direccionActual = "Right";

        public Moto(Grid grid, Form ventanaPrincipal, Estela estela)
        {
            this.grid = grid;
            currentNode = grid.GetHead();
            VentanaPrincipal = ventanaPrincipal;
            this.estela = estela;

            segmentos = new LinkedList<Segmento>(); // Inicializa la lista enlazada
            IniciarMoto();

            // Inicializar el Timer
            movimientoTimer = new System.Windows.Forms.Timer();
            movimientoTimer.Interval = velocidad;
            movimientoTimer.Tick += MovimientoAutomatico;
        }

        public void IniciarMovimientoAutomatico()
        {
            movimientoTimer.Start();
        }

        public void DetenerMovimientoAutomatico()
        {
            movimientoTimer.Stop();
            MessageBox.Show("GAME OVER!", "Has perdido!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MovimientoAutomatico(object sender, EventArgs e)
        {
            if (gasolina <= 0)
            {
                DetenerMovimientoAutomatico();
                return;
            }

            FourNode nextNode = null;

            switch (direccionActual)
            {
                case "Up":
                    nextNode = currentNode.Up;
                    break;
                case "Down":
                    nextNode = currentNode.Down;
                    break;
                case "Left":
                    nextNode = currentNode.Left;
                    break;
                case "Right":
                    nextNode = currentNode.Right;
                    break;
            }

            if (nextNode != null)
            {
                estela.ManejarEstela(currentNode);
                Mover(nextNode);
            }
            else
            {
                DetenerMovimientoAutomatico();
            }
        }

        public void Mover(FourNode nextNode)
        {
            currentNode = nextNode;
            motoPictureBox.Location = new Point(currentNode.X, currentNode.Y);
            casillasRecorridas++;

            HitBox();

            if (casillasRecorridas >= 5)
            {
                gasolina -= 1;
                casillasRecorridas = 0;
                VentanaPrincipal.Text = $"gasolina restante: {gasolina}";
            }
        }

        public void HitBox()
        {
            if (currentNode.Imagen != null && currentNode.Ocupante != null)
            {
                if (currentNode.Ocupante == "Moto")
                {
                    //currentNode.Moto;
                }
                else if (currentNode.Ocupante == "Item")
                {
                    currentNode.Item.Ejecutar(currentNode.Imagen, this, estela);
                    currentNode.Item.numImages--;
                    VentanaPrincipal.Controls.Remove(currentNode.Imagen);
                }
                else if (currentNode.Ocupante == "Poder")
                {
                    currentNode.Poder.Ejecutar(currentNode.Imagen, this, estela);
                    currentNode.Poder.numImages--;
                    VentanaPrincipal.Controls.Remove(currentNode.Imagen);
                }
            }
        }

        private void IniciarMoto()
        {
            Image moto = Proyecto1_Tron.Properties.Resources.moto;
            motoPictureBox = new PictureBox
            {
                Image = moto,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(currentNode.X, currentNode.Y)
            };

            VentanaPrincipal.Controls.Add(motoPictureBox);

            // Añadir la moto a la lista de segmentos
            segmentos.AddFirst(new Segmento(motoPictureBox, currentNode, true));
        }


        public void CambiarVelocidad(int nuevaVelocidad)
        {
            velocidad = nuevaVelocidad;
            movimientoTimer.Interval = velocidad;
        }

        public void Incrementargasolina(int cantidad)
        {
            gasolina += cantidad;
            //VentanaPrincipal.Text = $"gasolina restante: {gasolina}";
        }

        public void LeerTeclas(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (currentNode.Up != null)
                        direccionActual = "Up";
                    break;
                case Keys.Down:
                    if (currentNode.Down != null)
                        direccionActual = "Down";
                    break;
                case Keys.Left:
                    if (currentNode.Left != null)
                        direccionActual = "Left";
                    break;
                case Keys.Right:
                    if (currentNode.Right != null)
                        direccionActual = "Right";
                    break;
            }
        }

        public void imprimir(string msg)
        {
            VentanaPrincipal.Text = msg;
        }
    }
}

