using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Tron
{
    public class Poderes : GestionImagenes
    {
        public Poderes(Grid grid, Form VentanaPrincipal) : base(grid, VentanaPrincipal)
        {
            Images = [Properties.Resources.escudo, Properties.Resources.velocidad];
            cantidadImg = 2;
        }

        public void Ejecutar(PictureBox Imagen)
        {
            if (Imagen.Image == Properties.Resources.escudo)
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
            VentanaPrincipal.Text = $"ENCONTRE ESCUDO!";
        }
        public void Velocidad()
        {
        }
    }
}
