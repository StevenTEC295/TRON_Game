using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRON
{
    public class Bot : ListaEnlazadaMoto
    {
        public ListaEnlazadaMoto Estela { get; private set; }
        private LinkedListGrid grid;
        public Direction currentDirection;
        private Random random;

        public Bot(LinkedListGrid grid)
        {
            this.grid = grid;
            this.Estela = new ListaEnlazadaMoto();
            this.currentDirection = (Direction)new Random().Next(0, 4);

            random = new Random();
            InitializeEstela();
        }
        private void InitializeEstela()
        {
            // Inicializar la estela del bot con 3 nodos
            int startRow = random.Next(5, grid.gridRowsSize);
            int startCol = random.Next(10, grid.gridColumnsSize);

            
            Estela.Add(grid.Grid[startRow, startCol]);
            Estela.Add(grid.Grid[startRow, startCol - 1]);
            Estela.Add(grid.Grid[startRow, startCol - 2]);
        }


        public void Move(Node newGridNode)
        {
            
            newGridNode.PictureBox.Image = Properties.Resources.moto; // Imagen de la cabeza del bot

            Estela.Add(newGridNode);

            
            MotoNodo current = Estela.Head;
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
                    // Asignar la imagen de la estela
                    current.Next.GridNode.PictureBox.Image = Properties.Resources.estela; // Imagen de la estela del bot
                    current.Next.GridNode.IsTrail = true;
                }   

                current = current.Next;
                count++;
            }
        }



        public void Die()
        {
            
            Console.WriteLine("Colisión");

            Estela = null;  // Eliminar la estela
            
        }
    }





}
