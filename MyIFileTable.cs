using System;
using System.Collections.Generic;
using System.IO;
using Exceptions;

namespace LabCsharp
{
    public interface MyIFileTable<T>
    {
        string ToString();
        void add(T item);
        void remove(int key);
        T get(int key);
        int getKey();
        int isEmpty();
        Dictionary<int, T> getDict();
    }

    public class Tuple
    {
        private string filename;
        private StreamReader st;

        public Tuple(string filename,StreamReader st)
        {
            this.filename = filename;
            this.st = st;
        }

        public StreamReader getST(){ return st; }
        public string getName() { return filename; }

    }

    public class MyFileTable<T> : MyIFileTable<T>
    {
        private Dictionary<int, T> fileT;
        private int idx;

        public MyFileTable()
        {
            fileT = new Dictionary<int, T>();
            idx = 0;
        }

        public void add(T item)
        {
            idx = idx + 1;
            fileT.Add(idx,item);
        }

        public T get(int key)
        {
            try
            {
                return fileT[key];
            }
            catch(KeyNotFoundException)
            { throw new DictExceptions("The values doesn't exist! \n"); }
        }

        public Dictionary<int, T> getDict()
        {
           return fileT;
        }

        public int getKey()
        {
            return idx ;
        }

        public int isEmpty()
        {
            if (fileT.Count == 0)
                return 0;
            else
                return 1;
        }

        public void remove(int key)
        {
            fileT.Remove(key);
        }

        public override string ToString()
        {
            string str = null;
            foreach (var item in fileT)
            {
                str = str + item.Key + "->" + item.Value + '\n';
            }
            return str;
        }
    }
}
