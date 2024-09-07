using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRON
{
    public class Item
    {
        public string Name { get; set; }
        public Action<ListaEnlazadaMoto> ApplyEffect { get; set; }

        public Item(string name, Action<ListaEnlazadaMoto> applyEffect)
        {
            Name = name;
            ApplyEffect = applyEffect;
        }
    }

    public class Power
    {
        public string Name { get; set; }
        public Action<ListaEnlazadaMoto> ApplyEffect { get; set; }

        public Power(string name, Action<ListaEnlazadaMoto> applyEffect)
        {
            Name = name;
            ApplyEffect = applyEffect;
        }
    }

}
