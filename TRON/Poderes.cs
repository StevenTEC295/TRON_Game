using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRON
{
    public class Stack
    {
        private int maxSize;
        private Object[] stackArray;
        private int top;

        public Stack(int maxSize)
        {
            this.maxSize = maxSize;
            this.stackArray = new Object[maxSize];
            this.top = -1;
        }

        public void Push(Object newObject)
        {
            if (top >= maxSize - 1)
            {
                throw new Exception("Stack is full");
            }
            this.stackArray[++top] = newObject;
        }

        public Object Pop()
        {
            if (top < 0)
            {
                throw new Exception("Stack is empty");
            }
            return this.stackArray[top--];
        }

        public Object Peek()
        {
            if (top < 0)
            {
                throw new Exception("Stack is empty");
            }
            return this.stackArray[top];
        }

        public int Size()
        {
            return top + 1;
        }

        public bool IsEmpty()
        {
            return top == -1;
        }

        public bool IsFull()
        {
            return top == maxSize - 1;
        }
    }

}
