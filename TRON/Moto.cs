using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRON
{
    public class NodoMoto
    {
        public NodoMoto Siguiente { get; set; }
        public Nodo PosicionActual { get; set; }

        public NodoMoto(Nodo posicion)
        {
            PosicionActual = posicion;
            Siguiente = null;
        }
    }

    public class Moto
    {
        public NodoMoto Cabeza { get; set; }

        public Moto(Nodo posicionInicial)
        {
            Cabeza = new NodoMoto(posicionInicial);
        }

        public void Mover(Nodo nuevoNodo)
        {
            NodoMoto nuevoNodoMoto = new NodoMoto(nuevoNodo);
            nuevoNodoMoto.Siguiente = Cabeza;
            Cabeza = nuevoNodoMoto;

            // Opcional: Puedes eliminar el nodo más antiguo para mantener la longitud de la estela.
        }
    }

}
