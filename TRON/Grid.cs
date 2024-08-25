using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRON
{
    public class Node
    {
        public PictureBox PictureBox { get; set; }
        public Node Up { get; set; }
        public Node Down { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public class LinkedListGrid
    {
        private int gridRowsSize;
        private int gridColumnsSize;
        private int pictureBoxSize;

        public Node[,] Grid { get; private set; }
        public ListaEnlazadaMoto Moto { get; private set; }

        public LinkedListGrid(int gridRowsSize, int gridColumnsSize, int pictureBoxSize)
        {
            this.gridRowsSize = gridRowsSize;
            this.gridColumnsSize = gridColumnsSize;
            this.pictureBoxSize = pictureBoxSize;
            Grid = new Node[gridRowsSize, gridColumnsSize];
            InitializeGrid();
            InitializeMoto();
        }

        private void InitializeGrid()
        {
            for (int row = 0; row < gridRowsSize; row++)
            {
                for (int col = 0; col < gridColumnsSize; col++)
                {
                    // Crear un nuevo nodo
                    Node node = new Node
                    {
                        PictureBox = new PictureBox
                        {
                            Width = pictureBoxSize,
                            Height = pictureBoxSize,
                            Image = Properties.Resources.bloque,
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            BorderStyle = BorderStyle.FixedSingle,
                            Location = new Point(col * pictureBoxSize, row * pictureBoxSize)
                        }
                    };

                    // Añadir el nodo a la matriz
                    Grid[row, col] = node;

                    // Conectar con nodos vecinos si existen
                    if (row > 0)
                    {
                        node.Up = Grid[row - 1, col];
                        Grid[row - 1, col].Down = node;
                    }
                    if (col > 0)
                    {
                        node.Left = Grid[row, col - 1];
                        Grid[row, col - 1].Right = node;
                    }
                }
            }
        }

        private void InitializeMoto()
        {
            Moto = new ListaEnlazadaMoto(); // Tamaño inicial de la estela

            int headRow = gridRowsSize / 2;
            int headCol = gridColumnsSize / 2;

            // Agregar los segmentos de la estela en orden, comenzando desde la cola
            Moto.Add(Grid[headRow, headCol - 3]);
            Moto.Add(Grid[headRow, headCol - 2]); // Primer segmento de la estela (cola)
            Moto.Add(Grid[headRow, headCol - 1]); // Segundo segmento de la estela
            Moto.Add(Grid[headRow, headCol]);     // Cabeza de la moto
        }

        public void MoverMoto(Direction direccion)
        {
            Node currentNode = Moto.Head.GridNode;
            Node nextNode = null;

            switch (direccion)
            {
                case Direction.Up:
                    nextNode = currentNode.Up;
                    break;
                case Direction.Down:
                    nextNode = currentNode.Down;
                    break;
                case Direction.Left:
                    nextNode = currentNode.Left;
                    break;
                case Direction.Right:
                    nextNode = currentNode.Right;
                    break;
            }

            if (nextNode != null)
            {
                Moto.Move(nextNode);
            }
        }
    }





}
