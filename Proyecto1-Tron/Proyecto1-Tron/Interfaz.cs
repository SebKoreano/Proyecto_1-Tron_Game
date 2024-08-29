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
            // 
            // gasolinaDisplay
            // 
            gasolinaDisplay = new Label();
            gasolinaDisplay.AutoSize = true;
            gasolinaDisplay.BackColor = SystemColors.ActiveBorder;
            gasolinaDisplay.Font = new Font("Microsoft YaHei", 15F, FontStyle.Bold);
            gasolinaDisplay.ForeColor = SystemColors.ButtonHighlight;
            gasolinaDisplay.Location = new Point(791, 730);
            gasolinaDisplay.Name = "gasolinaDisplay";
            gasolinaDisplay.Size = new Size(48, 27);
            gasolinaDisplay.TabIndex = 1;
            gasolinaDisplay.Text = "100";
            VentanaPrincipal.Controls.Add(gasolinaDisplay);
            gasolinaDisplay.BringToFront();
            // 
            // poderDisplay
            // 
            poderDisplay = new PictureBox();
            poderDisplay.BackColor = SystemColors.ActiveBorder;
            poderDisplay.Location = new Point(306, 725);
            poderDisplay.Name = "poderDisplay";
            poderDisplay.Size = new Size(100, 50);
            poderDisplay.SizeMode = PictureBoxSizeMode.AutoSize;
            poderDisplay.TabIndex = 2;
            poderDisplay.TabStop = false;
            VentanaPrincipal.Controls.Add(poderDisplay);
            poderDisplay.BringToFront();
        }

        public void ActualizarGasolina(int gasolina)
        {
            gasolinaDisplay.Text = $"{gasolina}%";
        }
    }
}

