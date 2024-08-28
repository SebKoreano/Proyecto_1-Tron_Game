using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1_Tron
{
    public class Poderes : GestionImagenes
    {
        private System.Windows.Forms.Timer TimerEscudo;
        private System.Windows.Forms.Timer TimerVelocidad;
        private Moto moto;
        private Estela estela;

        public Poderes(Grid grid, Form VentanaPrincipal, Moto moto, Estela estela) : base(grid, VentanaPrincipal)
        {
            Images = [Properties.Resources.escudo, Properties.Resources.velocidad];
            cantidadImg = 2;

            this.moto = moto;
            this.estela = estela;

            TimerEscudo = new System.Windows.Forms.Timer();
            TimerEscudo.Interval = 3000; //3s
            TimerVelocidad = new System.Windows.Forms.Timer();
            TimerVelocidad.Interval = 3000; //3s
        }

        public new void Ejecutar(PictureBox Imagen)
        {
            if (Imagen.Image.PhysicalDimension.Width == 23)
            {
                Escudo();
            }
            else
            {
                Velocidad();
            }
        }

        public void Escudo()
        {
            moto.puedeMorir = false;
            moto.motoPictureBox.Image = Proyecto1_Tron.Properties.Resources.motoEscudo;

            TimerEscudo.Tick += EjecutaEscudo;
            TimerEscudo.Start(); // Iniciar el Timer
        }

        public void EjecutaEscudo(object sender, EventArgs e)
        {
            moto.puedeMorir = true;
            moto.motoPictureBox.Image = Proyecto1_Tron.Properties.Resources.moto;

            TimerEscudo.Stop(); // Detener el Timer
            TimerEscudo.Dispose(); // Liberar recursos
        }

        public void Velocidad()
        {
            
            moto.CambiarVelocidad(moto.velocidad/2);
            moto.motoPictureBox.Image = Proyecto1_Tron.Properties.Resources.motoVelocidad;

            TimerVelocidad.Tick += EjecutaVelocidad;
            TimerVelocidad.Start(); // Iniciar el Timer
        }

        public void EjecutaVelocidad(object sender, EventArgs e)
        {
            
            moto.CambiarVelocidad(500);
            moto.motoPictureBox.Image = Proyecto1_Tron.Properties.Resources.moto;

            TimerVelocidad.Stop(); // Detener el Timer
            TimerVelocidad.Dispose(); // Liberar recursos
        }
    }
}
