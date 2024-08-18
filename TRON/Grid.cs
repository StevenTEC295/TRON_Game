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
        public Nodo PrimerNodo { get; private set; }

        public Grid(int filas, int columnas)
        {
            PrimerNodo = CrearGrid(filas, columnas);
        }

        private Nodo CrearGrid(int filas, int columnas)
        {
            Nodo inicio = new Nodo();
            Nodo filaActual = inicio;

            // Crear la primera fila
            for (int j = 1; j < columnas; j++)
            {
                Nodo nuevoNodo = new Nodo();
                filaActual.Derecha = nuevoNodo;
                nuevoNodo.Izquierda = filaActual;
                filaActual = nuevoNodo;
            }

            Nodo filaArriba = inicio;

            // Crear las filas subsecuentes
            for (int i = 1; i < filas; i++)
            {
                Nodo nodoIzquierda = new Nodo();
                filaArriba.Abajo = nodoIzquierda;
                nodoIzquierda.Arriba = filaArriba;

                filaActual = nodoIzquierda;

                for (int j = 1; j < columnas; j++)
                {
                    Nodo nuevoNodo = new Nodo();
                    filaActual.Derecha = nuevoNodo;
                    nuevoNodo.Izquierda = filaActual;

                    if (filaArriba.Derecha != null)
                    {
                        filaArriba = filaArriba.Derecha;
                        filaActual.Arriba = filaArriba;
                        filaArriba.Abajo = filaActual;
                    }

                    filaActual = nuevoNodo;
                }

                filaArriba = nodoIzquierda;
            }

            return inicio;
        }
    }




}
