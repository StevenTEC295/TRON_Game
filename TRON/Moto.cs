using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRON
{
    public class Moto
    {
        public Nodo Cabeza { get; set; }
        public List<Nodo> Estela { get; private set; }

        public Moto(Nodo posicionInicial)
        {
            Cabeza = posicionInicial;
            Estela = new List<Nodo>();
            Estela.Add(Cabeza);
        }

        public void Mover(Nodo nuevoNodo)
        {
            // Desocupar el nodo más antiguo de la estela
            Estela.Last().Ocupado = false;
            Estela.RemoveAt(Estela.Count - 1);

            // Mover la cabeza al nuevo nodo
            Cabeza = nuevoNodo;
            Cabeza.Ocupado = true;

            // Añadir la nueva cabeza a la estela
            Estela.Insert(0, Cabeza);
        }
    }


}
