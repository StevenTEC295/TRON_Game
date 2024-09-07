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
            int startRow = random.Next(0, grid.gridRowsSize);
            int startCol = random.Next(0, grid.gridColumnsSize);

            // Asegúrate de que no esté en la misma posición que otros jugadores o estelas
            Estela.Add(grid.Grid[startRow, startCol]);
            Estela.Add(grid.Grid[startRow, startCol - 1]);
            Estela.Add(grid.Grid[startRow, startCol - 2]);
        }


        public void Move(Node newGridNode)
        {
            // Asignar la imagen de la cabeza del bot a la nueva posición
            newGridNode.PictureBox.Image = Properties.Resources.moto; // Imagen de la cabeza del bot

            Estela.Add(newGridNode);

            // Mantener la longitud de la estela del bot
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
            // Lógica para eliminar al bot cuando muere (colisión)
            for( int segment = 0; segment >= Estela.EstelaMaxima; segment++)
            {
                Estela.Head.GridNode.PictureBox.Image = Properties.Resources.bloque;
            }

            Estela = null;  // Eliminar la estela
        }
    }





}
