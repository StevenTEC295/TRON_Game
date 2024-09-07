using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRON
{
    public class Items
    {
        private int maxSize;
        private int front;
        private int back;
        private int size;
        private Object[] items;

        public Items(int maxSize)
        {
            this.maxSize = maxSize;
            this.front = -1;
            this.back = 0;
            this.size = 0;
            this.items = new Object[maxSize];
        }

        public void Enqueue(Object element)
        {
            if (size == maxSize)
            {
                throw new Exception("Queue is full");
            }
            this.front = (this.front + 1) % this.maxSize;
            this.items[front] = element;
            this.size++;
        }

        public Object Dequeue()
        {
            if (size == 0)
            {
                throw new Exception("Queue is empty");
            }
            Object toReturn = this.items[this.back];
            this.items[this.back] = null;
            this.back = (this.back + 1) % this.maxSize;
            this.size--;
            return toReturn;
        }

        public int Size()
        {
            return this.size;
        }

        public bool IsEmpty()
        {
            return this.size == 0;
        }

        public bool IsFull()
        {
            return this.size == this.maxSize;
        }
    }

}
