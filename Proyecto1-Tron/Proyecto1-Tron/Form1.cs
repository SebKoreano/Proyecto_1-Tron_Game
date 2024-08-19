using PruebasDePOO.Listas;
using PruebasDePOO.Nodes;
using System.Drawing.Text;
using static System.Net.WebRequestMethods;

namespace Proyecto1_Tron
{
    public partial class VentanaPrincipal : Form
    {
        private Grid grid;
        private Moto moto;

        public VentanaPrincipal()
        {
            InitializeComponent();

            // Inicializar la lista y la grid
            grid = new Grid();
            grid.CreateGrid(12,10); // Crear una grid de 5x5

            moto = new Moto(grid, this);

        }

        private void VentanaPrincipal_Load(object sender, EventArgs e)
        {
            
        }
    }
}
