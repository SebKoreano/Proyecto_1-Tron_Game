using PruebasDePOO.Listas;
using PruebasDePOO.Nodes;
using System.Drawing.Text;
using static System.Net.WebRequestMethods;
using System.Timers;

namespace Proyecto1_Tron
{
    public partial class VentanaPrincipal : Form
    {
        private Grid grid;
        private Moto moto;
        private Items items;
        //private Poderes poderes;
        private RandomPlace imageRandom;

        public VentanaPrincipal()
        {
            InitializeComponent();

            // Inicializar la lista y la grid
            grid = new Grid();
            grid.CreateGrid(12,10); // Crear un grid de 12x10

            moto = new Moto(grid, this);

            items = new Items(grid,this);
            
            //poderes = new Poderes(grid);

            KeyDown keyDown = new KeyDown(moto);
            // Configurar el formulario para capturar las teclas
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(keyDown.PrecionarFlecha);

            // Ejecutar GenerarItems después de que la ventana se haya cargado
            this.Load += MainForm_Load;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await Task.Run(() => items.GenerarItems());
        }

        private void VentanaPrincipal_Load(object sender, EventArgs e)
        {
        }
    }
}
