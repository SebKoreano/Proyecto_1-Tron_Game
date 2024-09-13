using Proyecto1_Tron.LinkedLists;
using PruebasDePOO.Nodes;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto1_Tron
{
    public class Enemigo : Moto
    {
        public Random random;
        private System.Windows.Forms.Timer movimientoAutomaticoTimer;
        private System.Windows.Forms.Timer poderTimer;
        private new Interfaz interfaz;

        public Enemigo(Grid grid, Form ventanaPrincipal, Image imageMoto) : base(grid, ventanaPrincipal, imageMoto) 
        {
            this.grid = grid;
            currentNode = grid.GetHead();
            VentanaPrincipal = ventanaPrincipal;

            segmentos = new LinkedList<Segmento>();
            this.random = new Random();
            int velocidad = random.Next(1, 11);

            estela = new Estela(ventanaPrincipal, this);
            inventario = new Inventario(this, ventanaPrincipal, interfaz, imageMoto);
            motor = new Motor(this, interfaz, inventario, velocidad);
        }

        // Iniciar movimiento automático y activación de poderes automáticos
        public void IniciarMovimientoAutomatico()
        {
            movimientoAutomaticoTimer = new System.Windows.Forms.Timer();
            movimientoAutomaticoTimer.Interval = random.Next(300, 700); // Movimiento en intervalos aleatorios
            movimientoAutomaticoTimer.Tick += MovimientoEnemigo;
            movimientoAutomaticoTimer.Start();

            // Temporizador para ejecutar poderes automáticamente
            poderTimer = new System.Windows.Forms.Timer();
            poderTimer.Interval = 5000; // Ejecuta un poder cada 5 segundos
            poderTimer.Tick += EjecutarPoderAutomatico;
            poderTimer.Start();
        }

        // Método para manejar el movimiento automático del enemigo
        private void MovimientoEnemigo(object sender, EventArgs e)
        {
            if (motoPictureBox != null)
            {
                int movimientoAleatorio = random.Next(0, 4);
                FourNode nextNode = null;

                switch (movimientoAleatorio)
                {
                    case 0: nextNode = currentNode.Up; direccionActual = "Up"; break;
                    case 1: nextNode = currentNode.Down; direccionActual = "Down"; break;
                    case 2: nextNode = currentNode.Left; direccionActual = "Left"; break;
                    case 3: nextNode = currentNode.Right; direccionActual = "Right"; break;
                }

                ValidarMovimiento(nextNode);
            }
            else
            {
                motor.DetenerMovimientoAutomatico();
                poderTimer.Stop();
                movimientoAutomaticoTimer.Stop();
            }
        }

        private void ValidarMovimiento(FourNode nextNode)
        {
            if (nextNode != null)
            {
                estela.ManejarEstela(currentNode); // Actualizar la estela
                motor.Mover(nextNode); // Mover al siguiente nodo
            }
        }

        // Método para ejecutar poderes automáticamente
        private void EjecutarPoderAutomatico(object sender, EventArgs e)
        {
            inventario.EjecutarPoder(); 
        }

    }
}
