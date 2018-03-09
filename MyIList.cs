using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCsharp
{
    public interface MyIList<T>
    {
        void add(T obj);
        string ToString();
    }

    public class MyList<T> : MyIList<T>
    {
        private List<T> lst;

        public MyList() {lst = new List<T>();}

        public void add(T obj) {lst.Add(obj);}

        public override string ToString()
        {
            string str=null;
            foreach (T s in lst)
                str = str + s.ToString();
            return str;
        }
    }
}
