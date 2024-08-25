using PruebasDePOO.Nodes;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto1_Tron
{
    public class Estela
    {
        private List<PictureBox> segmentosEstela; // Lista de segmentos que conforman la estela
        private Form ventanaPrincipal;
        private int longitudMaxima; // Longitud máxima de la estela

        public Estela(Form ventanaPrincipal, int longitudInicial = 3)
        {
            this.ventanaPrincipal = ventanaPrincipal;
            segmentosEstela = new List<PictureBox>();
            longitudMaxima = longitudInicial; // Valor inicial de longitud máxima
        }

        // Método para iniciar la estela en la posición inicial de la moto
        public void IniciarEstela(FourNode posicionInicial)
        {
            AgregarSegmento(posicionInicial);
        }

        // Método para actualizar la estela a medida que la moto se mueve
        public void UpdateEstela(FourNode nuevaPosicion)
        {
            // Agregar un nuevo segmento a la estela en la nueva posición
            AgregarSegmento(nuevaPosicion);

            // Verificar si la longitud de la estela supera la longitud máxima permitida
            if (segmentosEstela.Count > longitudMaxima)
            {
                // Remover el segmento más antiguo de la estela
                RemoverSegmentoAntiguo();
            }
        }

        // Método para agregar un nuevo segmento a la estela
        private void AgregarSegmento(FourNode posicion)
        {
            PictureBox nuevoSegmento = new PictureBox
            {
                Size = new Size(10, 10), // Tamaño del segmento de la estela
                BackColor = Color.Blue, // Color del segmento
                Location = new Point(posicion.X, posicion.Y) // Posición basada en el nodo
            };

            segmentosEstela.Add(nuevoSegmento);
            ventanaPrincipal.Controls.Add(nuevoSegmento); // Agregar el segmento al formulario
        }

        // Método para remover el segmento más antiguo de la estela
        private void RemoverSegmentoAntiguo()
        {
            if (segmentosEstela.Count > 0)
            {
                PictureBox segmentoAntiguo = segmentosEstela[0];
                ventanaPrincipal.Controls.Remove(segmentoAntiguo); // Remover del formulario
                segmentosEstela.RemoveAt(0); // Remover de la lista
                segmentoAntiguo.Dispose(); // Liberar recursos
            }
        }

        // Método para incrementar la longitud máxima de la estela
        public void IncrementarLongitud(int cantidad)
        {
            longitudMaxima += cantidad;
        }
    }
}
