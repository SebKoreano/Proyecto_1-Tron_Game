using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Tron
{
    public class MotorEnemigo : Motor
    {
        private new Enemigo moto;
        public MotorEnemigo(Enemigo moto, Interfaz interfaz, Inventario inventario, int velocidad) : base(moto, interfaz, inventario, velocidad)
        {
            this.moto = moto;
        }

        // Sobrescribir el método de muerte para no mostrar el mensaje de "Game Over"
        public override void DetenerMovimientoAutomatico()
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

            }
        }
    }
}
