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
        public Items(Grid grid, Form VentanaPrincipal, Moto moto, Estela estela) : base(grid, VentanaPrincipal, moto, estela)
        {
            Images = [Properties.Resources.bomba1, Properties.Resources.gasolina, Properties.Resources.masEstela];
            cantidadImg = 3;
        }

        public void Ejecutar(PictureBox Imagen, FourNode itemNode)
        {
            if (Imagen.Image.PhysicalDimension.Width == 28)
            {
                Bomba();
            }
            else if (Imagen.Image.PhysicalDimension.Width == 38)
            {
                Gasolina(itemNode);
            }
            else
            {
                CreceEstela();
            }
        }

        public void Bomba()
        {
            moto.DetenerMovimientoAutomatico();
        }

        public void Gasolina(FourNode itemNode)
        {
            if ((moto.motor.gasolina + 10) <= 100)
            {
                moto.motor.gasolina += 10;
            }
            else
            {
                moto.inventario.itemsRecogidos.Enqueue(itemNode);
            }
            
        }

        public void CreceEstela()
        {
            estela.IncrementarLongitud(1); 
        }
    }
}
