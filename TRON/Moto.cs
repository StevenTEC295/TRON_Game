using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TRON
{
    public class MotoNodo
    {
        public Node GridNode { get; set; } // El nodo del grid donde se encuentra esta parte de la moto
        public MotoNodo Next { get; set; } // El siguiente nodo en la estela
    }
    public class ListaEnlazadaMoto
    {
        public MotoNodo Head { get; private set; }
        //public MotoNodo Tail { get; private set; }
        public int EstelaMaxima { get; private set; }

        public int Velocidad;
        public int Estela_size;
        public int Combustible;
        private Items itemsQueue;
        private Stack powersStack;
        private System.Windows.Forms.Timer itemTimer;

        public ListaEnlazadaMoto()
        {
            this.EstelaMaxima = 4;
            this.itemsQueue = new Items(10);
            this.powersStack = new Stack(10);

            // Inicializar el temporizador para aplicar ítems
            this.itemTimer = new System.Windows.Forms.Timer();
            this.itemTimer.Interval = 1000; // Intervalo de 1 segundo
            //this.itemTimer.Tick += ItemTimer_Tick;
            this.itemTimer.Start();
        }


        // Método para añadir un nodo al inicio de la lista
        public void Add(Node gridNode)
        {
            // Si ya hay una cabeza, marcamos la cabeza antigua como parte de la estela
            if (Head != null)
            {
                Head.GridNode.IsHead = false;  // La cabeza anterior deja de ser la cabeza
                Head.GridNode.IsTrail = true;  // Y ahora es parte de la estela
            }

            MotoNodo newNodo = new MotoNodo { GridNode = gridNode };
            newNodo.GridNode.IsHead = true;  // El nuevo nodo es la nueva cabeza
            newNodo.GridNode.IsTrail = false;  // La cabeza no es parte de la estela

            // Añadir el nuevo nodo al principio de la lista
            newNodo.Next = Head;
            Head = newNodo;
        }







        // Método para mover la moto, manteniendo la longitud de la estela
        public void Move(Node newGridNode)
        {
            if (newGridNode != null)
            {
                newGridNode.PictureBox.Image = Properties.Resources.moto; // Imagen de la cabeza de la moto

                Add(newGridNode);

                // Mantener la longitud de la estela
                MotoNodo current = Head;
                int count = 1;

                while (current.Next != null)
                {
                    if (count == EstelaMaxima)
                    {
                        // Restaurar la imagen del último segmento a 'bloque'
                        current.Next.GridNode.PictureBox.Image = Properties.Resources.bloque;
                        current.Next.GridNode.IsTrail = false;
                        current.Next = null;
                        break;
                    }
                    else
                    {
                        current.Next.GridNode.IsTrail = true;
                        current.Next.GridNode.PictureBox.Image = Properties.Resources.estela; // Imagen de la estela
                    }

                    current = current.Next;
                    count++;
                }
            }
            else { 
                Application.Exit();
            }
        }


    }




}
