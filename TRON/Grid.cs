using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRON
{
    public class Nodo
    {
        public Nodo Arriba { get; set; }
        public Nodo Abajo { get; set; }
        public Nodo Izquierda { get; set; }
        public Nodo Derecha { get; set; }
        public bool Ocupado { get; set; } // Para indicar si la celda está ocupada por la moto o su estela

        public Nodo()
        {
            Ocupado = false;
        }
    }

    public class Grid
    {
        public Nodo[,] Nodos { get; private set; }
        public int Filas { get; private set; }
        public int Columnas { get; private set; }

        public Grid(int filas, int columnas)
        {
            Filas = filas;
            Columnas = columnas;
            Nodos = new Nodo[filas, columnas];

            CrearGrid();
            ConectarNodos();
        }

        private void CrearGrid()
        {
            for (int i = 0; i < Filas; i++)
            {
                for (int j = 0; j < Columnas; j++)
                {
                    Nodos[i, j] = new Nodo();
                }
            }
        }

        private void ConectarNodos()
        {
            for (int i = 0; i < Filas; i++)
            {
                for (int j = 0; j < Columnas; j++)
                {
                    if (i > 0) Nodos[i, j].Arriba = Nodos[i - 1, j];
                    if (i < Filas - 1) Nodos[i, j].Abajo = Nodos[i + 1, j];
                    if (j > 0) Nodos[i, j].Izquierda = Nodos[i, j - 1];
                    if (j < Columnas - 1) Nodos[i, j].Derecha = Nodos[i, j + 1];
                }
            }
        }
    }



}
