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

        public bool IsTrail { get; set; } // Indica si este nodo es parte de una estela
        public bool IsHead { get; set; }  // Indica si este nodo es la cabeza de una moto o bot

        public Power Power { get; set; }

        public Item Item { get; set; }
    }

    public class LinkedListGrid
    {
        public int gridRowsSize;
        public int gridColumnsSize;
        private int pictureBoxSize;

        public Node[,] Grid { get; private set; }
        public ListaEnlazadaMoto Moto { get; private set; }
        public List<Bot> Bots { get; private set; } // Lista de bots
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
            Node nextNode = currentNode.Right;

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

            // Verificamos si hay colisión antes de movernos
            if (nextNode != null && Form1.CheckCollision(nextNode))
            {
                // Si es el jugador, termina el juego
                Application.Exit(); // O cualquier lógica que uses para finalizar el juego
            }
            else if (nextNode != null)
            {
                Moto.Move(nextNode);
            }
            else
            {
                // Si el próximo nodo es null, significa que la moto salió del grid (colisión con la pared)
                Application.Exit();
            }
        }
    }
    }
