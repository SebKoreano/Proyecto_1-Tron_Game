using PruebasDePOO.Nodes;
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

        public void Ejecutar(PictureBox Imagen, Moto moto, Estela estela, FourNode itemNode)
        {
            if (Imagen.Image.PhysicalDimension.Width == 28)
            {
                Bomba(moto, estela);
            }
            else if (Imagen.Image.PhysicalDimension.Width == 38)
            {
                Gasolina(moto, estela, itemNode);
            }
            else
            {
                CreceEstela(moto, estela);
            }
        }

        public void Bomba(Moto moto, Estela estela)
        {
            moto.DetenerMovimientoAutomatico();
        }

        public void Gasolina(Moto moto, Estela estela, FourNode itemNode)
        {
            if ((moto.gasolina + 10) <= 100)
            {
                moto.gasolina += 10;
            }
            else
            {
                moto.itemsRecogidos.Enqueue(itemNode);
            }
            
        }

        public void CreceEstela(Moto moto, Estela estela)
        {
            estela.IncrementarLongitud(1); 
        }
    }
}
