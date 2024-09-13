using PruebasDePOO.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Tron.Objetos
{
    public class Items : GestionImagenes
    {
        private Random random;
        public Items(Grid grid, Form VentanaPrincipal) : base(grid, VentanaPrincipal)
        {
            Images = [Properties.Resources.bomba1, Properties.Resources.gasolina, Properties.Resources.masEstela];
            cantidadImg = 3;
            random = new Random();
        }

        public void Ejecutar(PictureBox Imagen, FourNode itemNode, Moto moto, Estela estela)
        {
            if (Imagen.Image.PhysicalDimension.Width == 28)
            {
                Bomba(moto);
            }
            else if (Imagen.Image.PhysicalDimension.Width == 38)
            {
                Gasolina(itemNode, moto);
            }
            else
            {
                CreceEstela(estela);
            }
        }

        private void Bomba(Moto moto)
        {
            moto.motor.DetenerMovimientoAutomatico();
        }

        private void Gasolina(FourNode itemNode, Moto moto)
        {
            // Generar un valor aleatorio entre 5 y 15 para la gasolina
            int gasolinaExtra = random.Next(5, 16);

            if (moto.motor.gasolina + gasolinaExtra <= 100)
            {
                moto.motor.gasolina += gasolinaExtra;
            }
            else
            {
                moto.inventario.itemsRecogidos.Enqueue(itemNode);
            }

        }

        private void CreceEstela(Estela estela)
        {
            // Generar un valor aleatorio entre 1 y 10 para la longitud de la estela
            int estelaExtra = random.Next(1, 11);
            estela.IncrementarLongitud(estelaExtra);
        }
    }
}
