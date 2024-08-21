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
        private Estela estela;
        private RandomPlace items;
        private RandomPlace poderes;

        public VentanaPrincipal()
        {
            InitializeComponent();

            // Inicializar la lista y la grid
            grid = new Grid();
            grid.CreateGrid(12,10); // Crear un grid de 12x10

            estela = new Estela(grid, this);
            moto = new Moto(grid, this, estela);
            items = new RandomPlace(grid,this,"items");
            poderes = new RandomPlace(grid, this, "poderes");

            moto.IniciarMoto();
            estela.IniciarEstela();

            // Configurar el formulario para capturar las teclas
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(moto.UpdateMoto);

            // Ejecutar GenerarItems después de que la ventana se haya cargado
            this.Load += SpawnDeObjetos;
        }

        private async void SpawnDeObjetos(object sender, EventArgs e)
        {
            await Task.Run(() => items.GenerarImagenes());
            await Task.Run(() => poderes.GenerarImagenes());
        }

        private void VentanaPrincipal_Load(object sender, EventArgs e)
        {
        }
    }
}
