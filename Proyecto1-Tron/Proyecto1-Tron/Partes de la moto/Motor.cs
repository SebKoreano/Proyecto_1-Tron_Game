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
        internal System.Windows.Forms.Timer movimientoTimer;
        internal Interfaz interfaz;
        internal int velocidad;
        internal int normalVelocidad;
        internal int gasolina = 100;
        internal int casillasRecorridas = 0;

        public Motor(Moto moto, Interfaz interfaz, Inventario inventario, int velocidad)
        {
            this.moto = moto;
            this.interfaz = interfaz;
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
                if (moto.currentNode.Ocupante == "Item")
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

                //moto.CheckEstela();
                //moto.CheckMoto();
            }
        }

        public void DetenerMovimientoAutomatico()
        {
            if (moto.puedeMorir)
            {
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

                moto.inventario.ColocarPoderesAleatorios();
            }
        }

        public void CambiarVelocidad(int nuevaVelocidad)
        {
            velocidad = nuevaVelocidad;
            movimientoTimer.Interval = velocidad;
        }
    }
}

