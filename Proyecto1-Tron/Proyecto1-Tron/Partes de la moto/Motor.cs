using PruebasDePOO.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Tron
{
    public class Motor
    {
        internal Moto moto;
        internal Interfaz interfaz;
        internal System.Windows.Forms.Timer movimientoTimer;
        internal int velocidad;
        internal int normalVelocidad;
        internal int gasolina = 100;
        internal int casillasRecorridas = 0;
        internal Inventario inventario;

        public Motor(Moto moto, Interfaz interfaz, Inventario inventario, int velocidad)
        {
            this.moto = moto;
            this.interfaz = interfaz;
            this.inventario = inventario;
            SetVelocidad(velocidad);
            SetTimers();
        }
        
        private void SetVelocidad(int velocidad)
        {
            int velocidadFinal = (500 * (velocidad / 5));

            if ( velocidadFinal > 0)
            {
                this.velocidad = velocidadFinal;
            }
            else
            {
                this.velocidad = 500;
            }

            normalVelocidad = this.velocidad;
        }
        private void SetTimers()
        {
            // Inicializar el Timer
            movimientoTimer = new System.Windows.Forms.Timer();
            movimientoTimer.Interval = velocidad;
            movimientoTimer.Tick += MovimientoAutomatico;
        }

        public void IniciarTimers()
        {
            movimientoTimer.Start();
        }

        private void MovimientoAutomatico(object sender, EventArgs e)
        {
            FourNode nextNode = null;

            switch (moto.direccionActual)
            {
                case "Up":
                    nextNode = moto.currentNode.Up;
                    break;
                case "Down":
                    nextNode = moto.currentNode.Down;
                    break;
                case "Left":
                    nextNode = moto.currentNode.Left;
                    break;
                case "Right":
                    nextNode = moto.currentNode.Right;
                    break;
            }

            if (nextNode != null)
            {
                moto.estela.ManejarEstela(moto.currentNode);
                Mover(nextNode);
            }
            else
            {
                DetenerMovimientoAutomatico();
            }
        }

        public void Mover(FourNode nextNode)
        {
            moto.currentNode = nextNode;
            moto.motoPictureBox.Location = new Point(moto.currentNode.X, moto.currentNode.Y);
            casillasRecorridas++;

            HitBox();

            if (casillasRecorridas >= 5)
            {
                gasolina -= 1;
                casillasRecorridas = 0;
                if (interfaz != null)
                {
                    interfaz.ActualizarGasolina(gasolina);
                }
            }

            if (gasolina < 0)
            {
                DetenerMovimientoAutomatico();
            }
        }

        private void HitBox()
        {
            if (moto.currentNode.Imagen != null && moto.currentNode.Ocupante != null)
            {
                if (moto.currentNode.Ocupante == "Moto")
                {
                    //currentNode.Moto;
                }
                else if (moto.currentNode.Ocupante == "Estela")
                {
                    //DetenerMovimientoAutomatico();
                    
                }
                else if (moto.currentNode.Ocupante == "Item")
                {
                    FourNode itemNode = moto.currentNode;
                    moto.inventario.itemsRecogidos.Enqueue(itemNode);
                    moto.currentNode.Item.numImages--;
                    moto.VentanaPrincipal.Controls.Remove(moto.currentNode.Imagen);
                }
                else if (moto.currentNode.Ocupante == "Poder")
                {
                    FourNode poderNode = moto.currentNode;
                    moto.inventario.poderesRecogidos.Push(poderNode);
                    moto.inventario.ActualizarPoderDisplay();
                    moto.currentNode.Poder.numImages--;
                    moto.VentanaPrincipal.Controls.Remove(moto.currentNode.Imagen);
                }
            }
        }

        public virtual void DetenerMovimientoAutomatico()
        {
            if (moto.puedeMorir)
            {
                // Detener el temporizador de movimiento
                movimientoTimer.Stop();

                // Remover la imagen de la moto
                if (moto.motoPictureBox != null)
                {
                    moto.VentanaPrincipal.Controls.Remove(moto.motoPictureBox);
                    moto.motoPictureBox.Dispose();
                    moto.motoPictureBox = null;
                }

                // Remover todos los segmentos de la estela
                foreach (PictureBox segmento in moto.estela.segmentosEstela)
                {
                    segmento.Visible = false;
                    moto.VentanaPrincipal.Controls.Remove(segmento);
                    segmento.Dispose();
                }

                // Colocar los poderes de la pila en lugares aleatorios del grid
                inventario.ColocarPoderesAleatorios();

                //MessageBox.Show("GAME OVER!", "Has perdido!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CambiarVelocidad(int nuevaVelocidad)
        {
            velocidad = nuevaVelocidad;
            movimientoTimer.Interval = velocidad;
        }
    }
}

