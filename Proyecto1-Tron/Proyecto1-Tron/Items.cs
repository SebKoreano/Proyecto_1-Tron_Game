using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Tron
{
    public class Items : GestionImagenes
    {
        public Items(Grid grid, Form VentanaPrincipal) : base(grid, VentanaPrincipal)
        {
            Images = [Properties.Resources.bomba1, Properties.Resources.gasolina, Properties.Resources.masEstela];
            cantidadImg = 3;
        }

        public void Ejecutar(PictureBox Imagen)
        {
            if (Imagen.Image == Properties.Resources.bomba1)
            {
                Bomba();
            }
            else
            {
                if (Imagen.Image == Properties.Resources.gasolina)
                {
                    Gasolina();
                }
                else
                {
                    CreceEstela();
                }
            }
        }

        public void Bomba()
        {

        }

        public void Gasolina()
        {

        }

        public void CreceEstela()
        {

        }
    }
}
