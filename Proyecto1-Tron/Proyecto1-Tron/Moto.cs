using Proyecto1_Tron;
using PruebasDePOO.Nodes;
using System.Collections.Generic;

namespace Proyecto1_Tron
{
    public class Moto
    {
        internal LinkedList<Segmento> segmentos;
        internal FourNode currentNode;
        internal PictureBox motoPictureBox;
        internal Form VentanaPrincipal;
        internal Grid grid;
        internal Estela estela;

        internal Inventario inventario;
        internal Interfaz interfaz;
        internal Motor motor;

        internal bool puedeMorir = true;
        internal string direccionActual = "Right";

        public Moto(Grid grid, Form ventanaPrincipal, Estela estela)
        {
            this.grid = grid;
            currentNode = grid.GetHead();
            VentanaPrincipal = ventanaPrincipal;
            this.estela = estela;

            segmentos = new LinkedList<Segmento>();
            IniciarMoto();

            interfaz = new Interfaz(VentanaPrincipal);
            inventario = new Inventario(this, VentanaPrincipal, interfaz);
            motor = new Motor(this, interfaz, inventario);

            interfaz.IniciarDisplays();
            motor.IniciarTimers();
        }

        public void IniciarMoto()
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
                case Keys.E:
                    inventario.EjecutarPoder();
                    break;
                case Keys.R:
                    inventario.CambiarOrdenPoderes();
                    break;
            }
        }
    }
}
