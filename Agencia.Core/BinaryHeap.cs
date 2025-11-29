using System;

namespace Agencia.Core
{
    // Min-heap por prioridad de misión (1 = más urgente)
    public class BinaryHeap
    {
        private Mision[] _datos;
        private int _tamaño;

        public BinaryHeap(int capacidad = 16)
        {
            _datos = new Mision[capacidad];
            _tamaño = 0;
        }

        public bool IsEmpty => _tamaño == 0;

        public void Insertar(Mision m)
        {
            if (_tamaño == _datos.Length)
            {
                Mision[] nuevo = new Mision[_datos.Length * 2];
                Array.Copy(_datos, nuevo, _datos.Length);
                _datos = nuevo;
            }
            _datos[_tamaño] = m;
            Subir(_tamaño);
            _tamaño++;
        }

        private void Subir(int i)
        {
            while (i > 0)
            {
                int padre = (i - 1) / 2;
                if (_datos[i].Prioridad >= _datos[padre].Prioridad) break;
                Intercambiar(i, padre);
                i = padre;
            }
        }

        private void Bajar(int i)
        {
            while (true)
            {
                int izq = 2 * i + 1;
                int der = 2 * i + 2;
                int menor = i;

                if (izq < _tamaño && _datos[izq].Prioridad < _datos[menor].Prioridad) menor = izq;
                if (der < _tamaño && _datos[der].Prioridad < _datos[menor].Prioridad) menor = der;

                if (menor == i) break;
                Intercambiar(i, menor);
                i = menor;
            }
        }

        private void Intercambiar(int i, int j)
        {
            var tmp = _datos[i];
            _datos[i] = _datos[j];
            _datos[j] = tmp;
        }

        public Mision ExtraerMin()
        {
            if (_tamaño == 0) throw new InvalidOperationException("Heap vacío");
            Mision min = _datos[0];
            _tamaño--;
            _datos[0] = _datos[_tamaño];
            Bajar(0);
            return min;
        }

        // Heap-Sort de un arreglo de misiones (por prioridad)
        public static void HeapSort(Mision[] arreglo)
        {
            // construir heap
            BinaryHeap heap = new BinaryHeap(arreglo.Length);
            foreach (var m in arreglo)
                heap.Insertar(m);

            int i = 0;
            while (!heap.IsEmpty)
                arreglo[i++] = heap.ExtraerMin();
        }
    }
}
