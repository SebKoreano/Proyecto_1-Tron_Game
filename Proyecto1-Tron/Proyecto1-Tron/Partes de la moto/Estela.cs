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

        public Estela(Form ventanaPrincipal, int longitudInicial = 3)
        {
            this.ventanaPrincipal = ventanaPrincipal;
            segmentosEstela = new LinkedList<PictureBox>();
            longitudMaxima = longitudInicial;
        }

        // Función para manejar la estela
        public void ManejarEstela(FourNode nuevaPosicion)
        {
            AgregarSegmento(nuevaPosicion);

            // Verificar si la longitud de la estela supera la longitud máxima permitida
            while (segmentosEstela.Count > longitudMaxima)
            {
                RemoverSegmentoAntiguo();
            }
        }

        // Método para agregar un nuevo segmento a la estela
        private void AgregarSegmento(FourNode posicion)
        {
            // Crear un nuevo segmento visual
            PictureBox nuevoSegmento = new PictureBox
            {
                Size = new Size(10, 10), // Tamaño del segmento de la estela
                BackColor = Color.Blue,
                Location = new Point(posicion.X, posicion.Y) // Posición basada en el nodo
            };

            // Agregar el nuevo segmento a la lista enlazada
            segmentosEstela.AddLast(nuevoSegmento);
            ventanaPrincipal.Controls.Add(nuevoSegmento); // Agregar el segmento al formulario

            // Establecer que el nodo ahora está ocupado por la estela
            posicion.SetOcupante("Estela");
        }

        // Método para remover el segmento más antiguo de la estela
        private void RemoverSegmentoAntiguo()
        {
            if (segmentosEstela.Count > 0)
            {
                // Obtener el PictureBox más antiguo (el primero en la lista)
                PictureBox segmentoAntiguo = segmentosEstela.First.Value;

                // Remover visualmente el PictureBox del formulario
                ventanaPrincipal.Controls.Remove(segmentoAntiguo);

                // Obtener el nodo que correspondía al primer segmento de la estela
                FourNode nodoAntiguo = GetNodeByPictureBox(segmentoAntiguo);

                // Eliminar el ocupante del nodo, ya que el segmento de la estela fue removido
                if (nodoAntiguo != null)
                {
                    nodoAntiguo.SetOcupante(null);
                }
                // Remover el PictureBox de la lista enlazada
                segmentosEstela.RemoveFirst();

                // Liberar recursos del segmento visual
                segmentoAntiguo.Dispose();
            }
        }

        // Método para encontrar el nodo correspondiente a un PictureBox de la estela
        private FourNode GetNodeByPictureBox(PictureBox segmento)
        {
            // Implementar lógica para obtener el nodo correspondiente al PictureBox (si es necesario)
            // Puedes asociar cada segmento con su nodo en un diccionario o directamente en los nodos si hace falta.
            return null; // Reemplazar con la lógica apropiada
        }

        // Método para incrementar la longitud máxima de la estela
        public void IncrementarLongitud(int cantidad)
        {
            longitudMaxima += cantidad;
        }
    }
}
