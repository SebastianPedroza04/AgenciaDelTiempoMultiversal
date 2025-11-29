using System;

namespace Agencia.Core
{
    public class DynamicArray<T>
    {
        private T[] _datos;
        public int Count { get; private set; }

        public DynamicArray(int capacidadInicial = 8)
        {
            _datos = new T[capacidadInicial];
            Count = 0;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
                return _datos[index];
            }
            set
            {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
                _datos[index] = value;
            }
        }

        public void Add(T item)
        {
            if (Count >= _datos.Length)
            {
                T[] nuevo = new T[_datos.Length * 2];
                Array.Copy(_datos, nuevo, _datos.Length);
                _datos = nuevo;
            }
            _datos[Count] = item;
            Count++;
        }
    }
}
