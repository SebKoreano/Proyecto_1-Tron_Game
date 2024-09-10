using PruebasDePOO.Listas;
using PruebasDePOO.Nodes;
using System.Drawing.Text;
using static System.Net.WebRequestMethods;
using System.Timers;
using Proyecto1_Tron.Objetos;

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

            grid = new Grid(75, 12, 10);
            grid.CreateGrid();

            moto = new Moto(grid, this, Proyecto1_Tron.Properties.Resources.moto);
            items = new Items(grid, this);
            poderes = new Poderes(grid, this);

            enemigos = new List<Enemigo>();
            AgregarEnemigos(1); // Agregar enemigos

            moto.motor.IniciarTimers();
            moto.interfaz.IniciarDisplays();

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(moto.LeerTeclas);

            this.Load += SpawnDeObjetos;
        }

        private void AgregarEnemigos(int cantidad)
        {
            for (int i = 0; i < cantidad; i++)
            {
                Enemigo nuevoEnemigo = new Enemigo(grid, this, Proyecto1_Tron.Properties.Resources.enemigo);
                nuevoEnemigo.IniciarMovimientoAutomatico(); // Iniciar movimiento y poderes automáticos
                enemigos.Add(nuevoEnemigo);
            }
        }

        private async void SpawnDeObjetos(object sender, EventArgs e)
        {
            await Task.Run(() => items.GenerarImagenes());
            await Task.Run(() => poderes.GenerarImagenes());
        }

        /*private void Update(object sender, EventArgs e)
        {
            foreach (PictureBox moto in )
        }*/
    }
}
