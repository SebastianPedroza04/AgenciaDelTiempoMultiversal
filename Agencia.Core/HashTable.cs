using System;

namespace Agencia.Core
{
    public class HashEntry
    {
        public string Clave;
        public Universo Valor;
        public HashEntry Siguiente;

        public HashEntry(string clave, Universo valor)
        {
            Clave = clave;
            Valor = valor;
        }
    }

    public class HashTable
    {
        private HashEntry[] _buckets;

        public HashTable(int capacidad = 101)
        {
            _buckets = new HashEntry[capacidad];
        }

        private int Hash(string clave)
        {
            long h = 0;
            foreach (char c in clave)
                h = (31 * h + c) % _buckets.Length;
            return (int)h;
        }

        public void Insertar(string clave, Universo u)
        {
            int idx = Hash(clave);
            var nuevo = new HashEntry(clave, u);
            nuevo.Siguiente = _buckets[idx];
            _buckets[idx] = nuevo;
        }

        public Universo Buscar(string clave)
        {
            int idx = Hash(clave);
            var e = _buckets[idx];
            while (e != null)
            {
                if (e.Clave == clave) return e.Valor;
                e = e.Siguiente;
            }
            return null;
        }
    }
}
