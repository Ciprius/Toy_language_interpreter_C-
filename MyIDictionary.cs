using System;
using System.Collections.Generic;
using Exceptions;

namespace LabCsharp
{
    public interface MyIDictionary<K,V>
    {
        void add(K key, V value);
        void update(K key,V value);
        V lookup(K key);
        int isDefined(K key);
        string ToString();
    }

    public class MyDictionary<K, V> : MyIDictionary<K, V>
    {
        private Dictionary<K, V> dict;

        public MyDictionary() { dict = new Dictionary<K,V>();}

        public void add(K key, V value) {dict.Add(key,value);}
        public void update(K key, V value) { dict[key] = value; }

        public V lookup(K key)
        {
            if (dict.ContainsKey(key))
                return dict[key];
            else
                throw new DictExceptions("The key does not exist!!! \n");

        }

        public override string ToString()
        {
            string str = null;
            foreach (var item in dict)
                str = str +item.Key+"->"+item.Value+'\n';
            return str;
        }

        public int isDefined(K key)
        {
            if (dict.ContainsKey(key))
                return 1;
            else
                return 0;
        }
    }
}
