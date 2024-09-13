using PruebasDePOO.Nodes;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto1_Tron
{
    public class Estela
    {
        public LinkedList<PictureBox> segmentosEstela;
        private Form ventanaPrincipal;
        private int longitudMaxima;
        private LinkedList<FourNode> posiciones;

        public Estela(Form ventanaPrincipal, int longitudInicial = 3)
        {
            this.ventanaPrincipal = ventanaPrincipal;
            segmentosEstela = new LinkedList<PictureBox>();
            posiciones = new LinkedList<FourNode>();
            longitudMaxima = longitudInicial;
        }

        // Función para manejar la estela
        public void ManejarEstela(FourNode nuevaPosicion)
        {
            AgregarSegmento(nuevaPosicion);

            while (segmentosEstela.Count > longitudMaxima)
            {
                RemoverSegmentoAntiguo();
            }
        }

        // Método para agregar un nuevo segmento a la estela
        private void AgregarSegmento(FourNode posicion)
        {
            PictureBox nuevoSegmento = new PictureBox
            {
                Size = new Size(10, 10), 
                BackColor = Color.Blue,
                Location = new Point(posicion.X, posicion.Y) 
            };

            posiciones.AddLast(posicion);
            segmentosEstela.AddLast(nuevoSegmento);
            ventanaPrincipal.Controls.Add(nuevoSegmento); 

            //posicion.SetOcupante(this);
        }

        // Método para remover el segmento más antiguo de la estela
        private void RemoverSegmentoAntiguo()
        {
            if (segmentosEstela.Count > 0)
            {
                PictureBox segmentoAntiguo = segmentosEstela.First.Value;
                FourNode nodoAntiguo = posiciones.First.Value;

                ventanaPrincipal.Controls.Remove(segmentoAntiguo);
                //nodoAntiguo.SetOcupante(null);

                segmentosEstela.RemoveFirst();
                segmentoAntiguo.Dispose();
            }
        }

        // Método para incrementar la longitud máxima de la estela
        public void IncrementarLongitud(int cantidad)
        {
            longitudMaxima += cantidad;
        }
    }
}
