using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Tron
{

    public class Items : RandomPlace
    {
        private Image[] itemImages;
        private Random random;
        private int maxItems = 5;
        private int numItems = 0;

        public Items(Grid grid, Form VentanaPrincipal) : base(grid, VentanaPrincipal)
        {
            random = new Random();
            itemImages = [Properties.Resources.bomba1, Properties.Resources.gasolina, Properties.Resources.masEstela];
        }

        public async void GenerarItems()
        {
            while (true)
            {
                Image randomImage = itemImages[random.Next(3)];

                if (numItems < maxItems)
                {
                    PlaceRandomImage(randomImage);
                    numItems++;
                }

                await Task.Delay(500);
            }
        }
    }
}
