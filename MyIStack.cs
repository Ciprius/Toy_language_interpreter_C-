using System;
using System.Collections.Generic;
using System.Collections;
using Exceptions;


namespace LabCsharp
{
    public interface MyIStack<T>
    {
        void push(T obj);
        T pop();
        void isEmpty();
        string ToString();
    }

    public class MyStack<T>:MyIStack<T>
    {
        private Stack<T> stk;

        public MyStack() { stk = new Stack<T>(); }

        public void isEmpty()
        {
            if (stk.Count == 0)
                throw new StackExceptions("Empty stack!!!\n");
        }

        public T pop() {return stk.Pop();}
        public void push(T obj) {stk.Push(obj);}

        public override string ToString()
        {          
            string str=null;
            List<T> arr = new List<T>();
            foreach (T s in stk)
            {
                arr.Add(s);
            }

            foreach (T s in arr)
            {
                str =str + s+'\n';
            }
            return str;
        }
    }
}
