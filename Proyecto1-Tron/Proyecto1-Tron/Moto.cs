using Proyecto1_Tron.LinkedLists;
using PruebasDePOO.Nodes;
using System.Collections.Generic;

namespace Proyecto1_Tron
{
    public class Moto
    {
        internal LinkedList<Segmento> segmentos;
        internal FourNode currentNode;
        internal PictureBox motoPictureBox;
        internal Form VentanaPrincipal;
        internal Grid grid;
        internal Estela estela;
        internal Inventario inventario;
        internal Interfaz interfaz;
        internal Motor motor;
        internal bool puedeMorir = true;
        internal string direccionActual = "Right";

        //internal static List<Moto>? motos;
        //internal static List<PictureBox>? estelas;

        public Moto(Grid grid, Form ventanaPrincipal, Image imageMoto)
        {
            this.grid = grid;
            currentNode = grid.GetHead();
            VentanaPrincipal = ventanaPrincipal;
            //motos = new List<Moto>();
            //estelas = new List<PictureBox>();

            segmentos = new LinkedList<Segmento>();
            IniciarMoto(imageMoto);

            Random random = new Random();
            int velocidad = random.Next(1, 11);

            estela = new Estela(ventanaPrincipal, this);
            interfaz = new Interfaz(VentanaPrincipal);
            inventario = new Inventario(this, VentanaPrincipal, interfaz, imageMoto);
            motor = new Motor(this, interfaz, inventario, velocidad);
        }

        internal void IniciarMoto(Image imageMoto)
        {
            motoPictureBox = new PictureBox
            {
                Image = imageMoto,
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(currentNode.X, currentNode.Y)
            };

            //AddMotos(this);
            //VentanaPrincipal.
            VentanaPrincipal.Controls.Add(motoPictureBox);

            // Añadir la moto a la lista de segmentos
            segmentos.AddFirst(new Segmento(motoPictureBox, currentNode, true));
        }

        public void LeerTeclas(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (currentNode.Up != null)
                        direccionActual = "Up";
                    break;
                case Keys.Down:
                    if (currentNode.Down != null)
                        direccionActual = "Down";
                    break;
                case Keys.Left:
                    if (currentNode.Left != null)
                        direccionActual = "Left";
                    break;
                case Keys.Right:
                    if (currentNode.Right != null)
                        direccionActual = "Right";
                    break;
                case Keys.E:
                    inventario.EjecutarPoder();
                    break;
                case Keys.R:
                    inventario.CambiarOrdenPoderes();
                    break;
            }
        }

        //public void AddEstelas(PictureBox estela)
        //{
        //    estelas.Add(estela);
        //}

        //public void AddMotos(Moto moto)
        //{
        //    motos.Add(moto);
        //}

        //public void RemoveEstelas(PictureBox estela)
        //{
        //    estelas.Remove(estela);
        //}

        //public void CheckEstela()
        //{
        //    try
        //    {
        //        foreach (PictureBox estela in estelas)
        //        {
        //            if (motoPictureBox != null && motoPictureBox.Bounds.IntersectsWith(estela.Bounds))
        //            {
        //                motor.DetenerMovimientoAutomatico();
        //            }
        //        }
        //    }
        //    catch { }
        //}

        //public void CheckMoto()
        //{
        //    foreach (Moto moto in motos)
        //    {
        //        if (motoPictureBox != moto.motoPictureBox)
        //        {
        //            if (motoPictureBox.Bounds.IntersectsWith(moto.motoPictureBox.Bounds))
        //            {
        //                motor.DetenerMovimientoAutomatico();
        //                moto.motor.DetenerMovimientoAutomatico();
        //            }
                            
        //        }
        //    }

        //}
    }
}
