using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Tron
{
    public class Interfaz
    {
        private Form VentanaPrincipal;
        internal Label gasolinaDisplay;
        internal PictureBox poderDisplay;

        public Interfaz(Form ventanaPrincipal)
        {
            VentanaPrincipal = ventanaPrincipal;
        }

        public void IniciarDisplays()
        {
            // Inicializar y configurar displays
            gasolinaDisplay = new Label();
            gasolinaDisplay.AutoSize = true;
            gasolinaDisplay.BackColor = SystemColors.ActiveBorder;
            gasolinaDisplay.Font = new Font("Microsoft YaHei", 13F, FontStyle.Regular, GraphicsUnit.Point);
            gasolinaDisplay.Location = new Point(565, 72);
            gasolinaDisplay.Name = "gasolinaDisplay";
            gasolinaDisplay.Size = new Size(64, 24);
            gasolinaDisplay.Text = "100%";

            poderDisplay = new PictureBox();
            poderDisplay.SizeMode = PictureBoxSizeMode.AutoSize;
            poderDisplay.Location = new Point(565, 120);
            poderDisplay.Name = "poderDisplay";
            poderDisplay.Size = new Size(100, 100);

            VentanaPrincipal.Controls.Add(gasolinaDisplay);
            VentanaPrincipal.Controls.Add(poderDisplay);
        }

        public void ActualizarGasolina(int gasolina)
        {
            gasolinaDisplay.Text = $"{gasolina}%";
        }
    }
}

