using System;

namespace Agencia.Core
{
    public class CircularQueue<T>
    {
        private T[] _datos;
        private int _frente;
        private int _fin;
        private int _count;

        public CircularQueue(int capacidadInicial = 8)
        {
            _datos = new T[capacidadInicial];
            _frente = 0;
            _fin = 0;
            _count = 0;
        }

        public bool IsEmpty => _count == 0;

        public void Enqueue(T item)
        {
            if (_count == _datos.Length)
            {
                // redimensionar
                T[] nuevo = new T[_datos.Length * 2];
                for (int i = 0; i < _count; i++)
                    nuevo[i] = _datos[(_frente + i) % _datos.Length];

                _datos = nuevo;
                _frente = 0;
                _fin = _count;
            }

            _datos[_fin] = item;
            _fin = (_fin + 1) % _datos.Length;
            _count++;
        }

        public T Dequeue()
        {
            if (_count == 0) throw new InvalidOperationException("Cola vacía");
            T item = _datos[_frente];
            _frente = (_frente + 1) % _datos.Length;
            _count--;
            return item;
        }
    }
}
