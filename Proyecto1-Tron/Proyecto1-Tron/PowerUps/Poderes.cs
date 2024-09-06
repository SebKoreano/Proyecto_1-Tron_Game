using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1_Tron.Objetos
{
    public class Poderes : GestionImagenes
    {
        private System.Windows.Forms.Timer TimerEscudo;
        private System.Windows.Forms.Timer TimerVelocidad;

        public Poderes(Grid grid, Form VentanaPrincipal) : base(grid, VentanaPrincipal)
        {
            Images = [Properties.Resources.escudo, Properties.Resources.velocidad];
            cantidadImg = 2;

            TimerEscudo = new System.Windows.Forms.Timer();
            TimerEscudo.Interval = 3000; //3s
            TimerVelocidad = new System.Windows.Forms.Timer();
            TimerVelocidad.Interval = 3000; //3s
        }

        public void Ejecutar(PictureBox Imagen, Moto moto)
        {
            if (Imagen.Image.PhysicalDimension.Width == 23)
            {
                Escudo(moto);
            }
            else
            {
                Velocidad(moto);
            }
        }

        private void Escudo(Moto moto)
        {
            moto.puedeMorir = false;
            moto.motoPictureBox.Image = Properties.Resources.motoEscudo;

            TimerEscudo.Tick += (sender, e) => EjecutaEscudo(sender, e, moto);
            TimerEscudo.Start(); // Iniciar el Timer
        }

        private void EjecutaEscudo(object sender, EventArgs e, Moto moto)
        {
            moto.puedeMorir = true;
            moto.motoPictureBox.Image = Properties.Resources.moto;

            TimerEscudo.Stop(); // Detener el Timer
            TimerEscudo.Dispose(); // Liberar recursos
        }

        private void Velocidad(Moto moto)
        {

            moto.motor.CambiarVelocidad(moto.motor.velocidad / 2);
            moto.motoPictureBox.Image = Properties.Resources.motoVelocidad;

            TimerVelocidad.Tick += (sender, e) => EjecutaVelocidad(sender, e, moto);
            TimerVelocidad.Start(); // Iniciar el Timer
        }

        private void EjecutaVelocidad(object sender, EventArgs e, Moto moto)
        {

            moto.motor.CambiarVelocidad(moto.motor.normalVelocidad);
            moto.motoPictureBox.Image = Properties.Resources.moto;

            TimerVelocidad.Stop(); // Detener el Timer
            TimerVelocidad.Dispose(); // Liberar recursos
        }
    }
}
