using System;
using System.Collections;

namespace IEnumerableVsIEnumerator
{
    class Program
    {
        static void Main(string[] args)
        {
            MyIntList x = new MyIntList(new int[] { 1, 2, 3, 4 });
            foreach (var item in x)
            {
                Console.WriteLine(item);
            }
        }
    }

    public class MyIntList : IEnumerable
    {
        public int[] data;
        public MyIntList(int[] items)
        {
            data = items;
        }
        public IEnumerator GetEnumerator() => new Enumerator(this);
    }

    public class Enumerator : IEnumerator
    {
        MyIntList collection;
        int currentIndex = -1;

        public Enumerator(MyIntList items)
        {
            collection = items;
        }

        public object Current
        {
            get
            {
                if (currentIndex == -1)
                    throw new InvalidOperationException("Enumeration not started!!");
                if (currentIndex == collection.data.Length)
                    throw new InvalidOperationException("Past end of list!!");
                return collection.data[currentIndex];
            }
        }

        public bool MoveNext()
        {
            if (currentIndex >= collection.data.Length - 1) 
                return false;
            return ++currentIndex < collection.data.Length;
        }

        public void Reset()
        {
            currentIndex = -1;
        }
    }
}
