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
        //private Items items;
        //private Poderes poderes;

        private Random random;
        private PictureBox randomPictureBox;

        public VentanaPrincipal()
        {
            InitializeComponent();

            // Inicializar la lista y la grid
            grid = new Grid();
            grid.CreateGrid(12,10); // Crear un grid de 12x10

            moto = new Moto(grid, this);
            //items = new Items(grid);
            //poderes = new Poderes(grid);
            random = new Random();

            // Crear y configurar PictureBox para la imagen aleatoria
            randomPictureBox = new PictureBox
            {
                Image = Properties.Resources.bomba1,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Visible = false // Inicia oculta
            };
            this.Controls.Add(randomPictureBox);

            // Colocar la imagen en una posición aleatoria
            PlaceRandomImage();

            KeyDown keyDown = new KeyDown(moto);

            // Configurar el formulario para capturar las teclas
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(keyDown.PrecionarFlecha);


        }

        private void PlaceRandomImage()
        {
            // Obtener el tamaño de la grid
            int colunms = 12; // Asumiendo que la grid es de 5x5
            int rows = 10;

            // Generar posiciones aleatorias dentro de la grid
            int randomColumn = random.Next(colunms);
            int randomRow = random.Next(rows);

            // Navegar hasta la posición aleatoria en la grid
            FourNode currentNode = grid.GetHead();
            for (int i = 0; i < randomColumn; i++)
            {
                currentNode = currentNode.Righ;
            }
            for (int j = 0; j < randomRow; j++)
            {
                currentNode = currentNode.Down;
            }

            // Colocar la imagen en la posición aleatoria
            randomPictureBox.Location = new Point(currentNode.X, currentNode.Y);
            randomPictureBox.Visible = true; // Hacerla visible
        }

        private void VentanaPrincipal_Load(object sender, EventArgs e)
        {
            
        }
    }
}
