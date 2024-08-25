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

        public int velocidad = 500; // Velocidad en milisegundos 
        public int gasolina = 100; // Cantidad inicial de gasolina
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
            MessageBox.Show("GAME OVER!", "Has perdido!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Método que maneja el movimiento automático
        private void MovimientoAutomatico(object sender, EventArgs e)
        {
            if (gasolina <= 0)
            {
                DetenerMovimientoAutomatico();
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

            //imprimir($"{currentNode.Ocupante}");
            //currentNode.SetOcupante(this);

            // Actualizar la posición del PictureBox
            motoPictureBox.Location = new Point(currentNode.X, currentNode.Y);

            // Actualizar la estela
            estela.UpdateEstela(previousNode);

            // Incrementar el contador de casillas recorridas
            casillasRecorridas++;

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

            // Verificar si se han recorrido 5 casillas para consumir gasolina
            if (casillasRecorridas >= 5)
            {
                gasolina -= 1; // Reducir el gasolina
                casillasRecorridas = 0; // Reiniciar el contador
                VentanaPrincipal.Text = $"gasolina restante: {gasolina}";
            }
        }

        // Método para cambiar la velocidad de la moto
        public void CambiarVelocidad(int nuevaVelocidad)
        {
            velocidad = nuevaVelocidad;
            movimientoTimer.Interval = velocidad;
        }

        // Método para incrementar el gasolina (por ejemplo, al recoger un objeto)
        public void Incrementargasolina(int cantidad)
        {
            gasolina += cantidad;
            VentanaPrincipal.Text = $"gasolina restante: {gasolina}";
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
