using Proyecto1_Tron;
using PruebasDePOO.Nodes;
using System.Collections.Generic; 

namespace Proyecto1_Tron
{
    public class Moto
    {
        internal LinkedList<Segmento> segmentos; // Lista enlazada que contiene la moto y los segmentos de la estela
        internal FourNode currentNode;
        internal PictureBox motoPictureBox;
        public Form VentanaPrincipal;
        public Grid grid;
        internal Estela estela;
        internal int velocidad = 500;
        internal int gasolina = 100;
        private int casillasRecorridas = 0;
        private System.Windows.Forms.Timer movimientoTimer;
        internal string direccionActual = "Right";

        internal Queue<FourNode> itemsRecogidos = new Queue<FourNode>();
        private FourNode itemNode;
        private Items itemEjecutable;
        internal int itemsVelocidad = 1000;
        private System.Windows.Forms.Timer itemsTimer;

        public Stack<FourNode> poderesRecogidos = new Stack<FourNode>();
        internal bool puedeMorir = true;

        internal Label gasolinaDisplay;
        internal PictureBox poderDisplay;

        public Moto(Grid grid, Form ventanaPrincipal, Estela estela)
        {
            this.estela = estela;
            this.grid = grid;
            currentNode = grid.GetHead();
            VentanaPrincipal = ventanaPrincipal;

            segmentos = new LinkedList<Segmento>(); // Inicializa la lista enlazada

            IniciarMoto();

            SetTimers();

            IniciarDisplays();
        }

        public void SetTimers()
        {
            // Inicializar el Timer
            movimientoTimer = new System.Windows.Forms.Timer();
            movimientoTimer.Interval = velocidad;
            movimientoTimer.Tick += MovimientoAutomatico;

            itemsTimer = new System.Windows.Forms.Timer();
            itemsTimer.Interval = itemsVelocidad;
            itemsTimer.Tick += EjecutarItems;
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
        public void IniciarTimers()
        {
            movimientoTimer.Start();
            itemsTimer.Start();
        }

        public void DetenerMovimientoAutomatico()
        {
            if (puedeMorir)
            {
                // Detener el temporizador de movimiento
                movimientoTimer.Stop();

                // Remover la imagen de la moto
                if (motoPictureBox != null)
                {
                    VentanaPrincipal.Controls.Remove(motoPictureBox);
                    motoPictureBox.Dispose();
                }

                // Remover todos los segmentos de la estela
                foreach (PictureBox segmento in estela.segmentosEstela)
                {
                    segmento.Visible = false;
                    VentanaPrincipal.Controls.Remove(segmento);
                    segmento.Dispose();
                }

                // Colocar los poderes de la pila en lugares aleatorios del grid
                ColocarPoderesAleatorios();

                MessageBox.Show("GAME OVER!", "Has perdido!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ColocarPoderesAleatorios()
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
            FourNode currentNode = grid.GetHead();
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
                gasolinaDisplay.Text = $"{gasolina}";
            }

            if (gasolina < 0)
            {
                DetenerMovimientoAutomatico();
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
                else if (currentNode.Ocupante == "Estela")
                {

                }
                else if (currentNode.Ocupante == "Item")
                {
                    itemsRecogidos.Enqueue(currentNode);
                    currentNode.Item.numImages--;
                    VentanaPrincipal.Controls.Remove(currentNode.Imagen);
                }
                else if (currentNode.Ocupante == "Poder")
                {
                    poderesRecogidos.Push(currentNode);
                    ActualizarPoderDisplay();
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
        }

        // Método para ejecutar el poder en la parte superior de la pila
        public void EjecutarPoder()
        {
            if (poderesRecogidos.Count > 0)
            {
                FourNode poderNode = poderesRecogidos.Pop();
                poderNode.Poder.Ejecutar(poderNode.Imagen);
                poderDisplay.Image = null; // Limpiar la imagen del poder
                ActualizarPoderDisplay(); // Actualizar la imagen del siguiente poder
            }
        }

        // Método para cambiar el orden de la pila de poderes
        public void CambiarOrdenPoderes()
        {
            poderesRecogidos = new Stack<FourNode>(poderesRecogidos);
            ActualizarPoderDisplay(); // Actualizar la imagen del nuevo poder
        }

        // Método para actualizar la imagen del poder actual en el display
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
                    EjecutarPoder(); // Ejecutar el poder en la parte superior de la pila
                    break;
                case Keys.R:
                    CambiarOrdenPoderes(); // Cambiar el orden de la pila de poderes
                    break;
            }
        }

        public void IniciarDisplays()
        {
            // 
            // gasolinaDisplay
            // 
            gasolinaDisplay = new Label();
            gasolinaDisplay.AutoSize = true;
            gasolinaDisplay.BackColor = SystemColors.ActiveBorder;
            gasolinaDisplay.Font = new Font("Microsoft YaHei", 15F, FontStyle.Bold);
            gasolinaDisplay.ForeColor = SystemColors.ButtonHighlight;
            gasolinaDisplay.Location = new Point(791, 730);
            gasolinaDisplay.Name = "gasolinaDisplay";
            gasolinaDisplay.Size = new Size(48, 27);
            gasolinaDisplay.TabIndex = 1;
            gasolinaDisplay.Text = "100";
            VentanaPrincipal.Controls.Add(gasolinaDisplay);
            gasolinaDisplay.BringToFront();
            // 
            // poderDisplay
            // 
            poderDisplay = new PictureBox();
            poderDisplay.BackColor = SystemColors.ActiveBorder;
            poderDisplay.Location = new Point(306, 725);
            poderDisplay.Name = "poderDisplay";
            poderDisplay.Size = new Size(100, 50);
            poderDisplay.SizeMode = PictureBoxSizeMode.AutoSize;
            poderDisplay.TabIndex = 2;
            poderDisplay.TabStop = false;
            VentanaPrincipal.Controls.Add(poderDisplay);
            poderDisplay.BringToFront();
        }
    }
}

