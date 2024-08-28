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
        private Items items;
        private Poderes poderes;
        private List<Enemigo> enemigos; // Lista de enemigos

        public VentanaPrincipal()
        {
            InitializeComponent();

            grid = new Grid();
            grid.CreateGrid(12, 10);

            estela = new Estela(this);
            moto = new Moto(grid, this, estela);
            items = new Items(grid, this);
            poderes = new Poderes(grid, this, moto, estela);

            enemigos = new List<Enemigo>();
            //AgregarEnemigos(3); // Agregar 3 enemigos

            moto.IniciarTimers();

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(moto.LeerTeclas);

            this.Load += SpawnDeObjetos;
        }

        private void AgregarEnemigos(int cantidad)
        {
            for (int i = 0; i < cantidad; i++)
            {
                Enemigo nuevoEnemigo = new Enemigo(grid, this, estela);
                enemigos.Add(nuevoEnemigo);
            }
        }

        private async void SpawnDeObjetos(object sender, EventArgs e)
        {
            await Task.Run(() => items.GenerarImagenes());
            await Task.Run(() => poderes.GenerarImagenes());
        }
    }
}
