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

        public new void Ejecutar(PictureBox Imagen, Moto moto, Estela estela)
        {
            if (Imagen.Image.PhysicalDimension.Width == 23)
            {
                Escudo(moto, estela);
            }
            else
            {
                Velocidad(moto, estela);
            }
        }

        public void Escudo(Moto moto, Estela estela)
        {
            moto.imprimir("esoo!");
        }
        public void Velocidad(Moto moto, Estela estela)
        {
            moto.CambiarVelocidad(moto.velocidad/2);
        }
    }
}
