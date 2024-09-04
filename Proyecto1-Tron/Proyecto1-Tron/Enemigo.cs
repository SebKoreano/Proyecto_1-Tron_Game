using PruebasDePOO.Nodes;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto1_Tron
{
    public class Enemigo : Moto
    {
        private Random random;
        private System.Windows.Forms.Timer movimientoAutomaticoTimer;

        public Enemigo(Grid grid, Form ventanaPrincipal, Image imageMoto) : base(grid, ventanaPrincipal, imageMoto)
        {
            random = new Random();
            IniciarMovimientoAutomatico();
        }

        // Iniciar el movimiento automático del enemigo
        private void IniciarMovimientoAutomatico()
        {
            movimientoAutomaticoTimer = new System.Windows.Forms.Timer();
            movimientoAutomaticoTimer.Interval = 500; 
            movimientoAutomaticoTimer.Tick += MovimientoEnemigo;
            movimientoAutomaticoTimer.Start();
        }

        // Método para manejar el movimiento automático del enemigo
        private void MovimientoEnemigo(object sender, EventArgs e)
        {
            // Movimiento aleatorio
            int movimientoAleatorio = random.Next(0, 4);
            FourNode nextNode = null;

            switch (movimientoAleatorio)
            {
                case 0: // Arriba
                    nextNode = currentNode.Up;
                    direccionActual = "Up";
                    break;
                case 1: // Abajo
                    nextNode = currentNode.Down;
                    direccionActual = "Down";
                    break;
                case 2: // Izquierda
                    nextNode = currentNode.Left;
                    direccionActual = "Left";
                    break;
                case 3: // Derecha
                    nextNode = currentNode.Right;
                    direccionActual = "Right";
                    break;
            }

            ValidarMovimiento(nextNode, sender, e);
        }

        private void ValidarMovimiento(FourNode nextNode, object sender, EventArgs e)
        {
            // Si hay un nodo siguiente válido, mover al enemigo
            if (nextNode != null)
            {
                estela.ManejarEstela(currentNode);
                motor.Mover(nextNode);
                estela.ManejarEstela(currentNode);
                motor.Mover(nextNode);
            }
            else
            {
                // Si no hay movimiento posible, intenta de nuevo
                MovimientoEnemigo(sender, e);
            }
        }

        public new void DetenerMovimientoAutomatico()
        {
            if (puedeMorir)
            {
                movimientoAutomaticoTimer.Stop();
            }
        }
    }
}
