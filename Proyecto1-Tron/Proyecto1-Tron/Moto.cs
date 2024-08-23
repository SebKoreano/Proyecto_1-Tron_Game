using PruebasDePOO.Nodes;
using System;
using System.Windows.Forms;
using System.Drawing; // Asegúrate de importar esta librería para utilizar Point

namespace Proyecto1_Tron
{
    public class Moto
    {
        public FourNode currentNode;
        public PictureBox motoPictureBox;
        private Form VentanaPrincipal;
        private Grid grid;
        private Estela estela;

        private int velocidad = 500; // Velocidad en milisegundos 
        private int combustible = 100; // Cantidad inicial de combustible
        private int casillasRecorridas = 0; // Contador de casillas recorridas

        private System.Windows.Forms.Timer movimientoTimer; // Timer para manejar el movimiento automático

        private string direccionActual = "Right"; // Variable para mantener la dirección actual

        public Moto(Grid grid, Form ventanaPrincipal, Estela estela)
        {
            this.grid = grid;
            currentNode = grid.GetHead();
            VentanaPrincipal = ventanaPrincipal;
            this.estela = estela;

            IniciarMoto();
            estela.IniciarEstela();
            // Inicializar el Timer
            movimientoTimer = new System.Windows.Forms.Timer();
            movimientoTimer.Interval = velocidad; // Intervalo basado en la velocidad
            movimientoTimer.Tick += MovimientoAutomatico; // Evento de tick del Timer
        }

        // Método para iniciar el movimiento automático de la moto
        public void IniciarMovimientoAutomatico()
        {
            movimientoTimer.Start(); // Comenzar el Timer
        }

        // Método para detener el movimiento automático de la moto
        public void DetenerMovimientoAutomatico()
        {
            movimientoTimer.Stop(); // Detener el Timer
        }

        // Método que maneja el movimiento automático
        private void MovimientoAutomatico(object sender, EventArgs e)
        {
            if (combustible <= 0)
            {
                DetenerMovimientoAutomatico();
                MessageBox.Show("La moto se ha quedado sin combustible!", "Combustible agotado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FourNode nextNode = null;
            // Determina el próximo nodo basado en la dirección actual
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
                Mover(nextNode);
            }
            else
            {
                DetenerMovimientoAutomatico();
            }
        }

        public void Mover(FourNode nextNode)
        {
            FourNode previousNode = currentNode;
            currentNode = nextNode;

            if (currentNode.Imagen != null && currentNode.Ocupante != null)
            {
                currentNode.Ocupante.Ejecutar(currentNode.Imagen);
                VentanaPrincipal.Controls.Remove(currentNode.Imagen);
                currentNode.Ocupante.numImages--;  // Decrementar el número de imágenes activas
            }

            //currentNode.Ocupante = this;

            // Actualizar la posición del PictureBox
            motoPictureBox.Location = new Point(currentNode.X, currentNode.Y);

            // Actualizar la estela
            estela.UpdateEstela(previousNode);

            // Incrementar el contador de casillas recorridas
            casillasRecorridas++;

            // Verificar si se han recorrido 5 casillas para consumir combustible
            if (casillasRecorridas >= 5)
            {
                combustible -= 1; // Reducir el combustible
                casillasRecorridas = 0; // Reiniciar el contador
                VentanaPrincipal.Text = $"Combustible restante: {combustible}";
            }
        }

        // Método para cambiar la velocidad de la moto
        public void CambiarVelocidad(int nuevaVelocidad)
        {
            velocidad = nuevaVelocidad;
            movimientoTimer.Interval = velocidad;
        }

        // Método para incrementar el combustible (por ejemplo, al recoger un objeto)
        public void IncrementarCombustible(int cantidad)
        {
            combustible += cantidad;
            VentanaPrincipal.Text = $"Combustible restante: {combustible}";
        }

        // Método para inicializar la moto
        public void IniciarMoto()
        {
            // Cargar una imagen 
            Image moto = Properties.Resources.moto;

            // Crear y configurar PictureBox
            motoPictureBox = new PictureBox
            {
                Image = moto,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(currentNode.X, currentNode.Y)
            };

            VentanaPrincipal.Controls.Add(motoPictureBox);
        }

        // Método para manejar las teclas presionadas y cambiar la dirección
        public void LeerTeclas(object sender, KeyEventArgs e)
        {
            // Mover el nodo actual basado en la tecla presionada
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
