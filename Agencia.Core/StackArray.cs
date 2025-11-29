using System;

namespace Agencia.Core
{
    public class StackArray<T>
    {
        private T[] _datos;
        private int _tope;

        public StackArray(int capacidadInicial = 8)
        {
            _datos = new T[capacidadInicial];
            _tope = 0;
        }

        public int Count => _tope;

        public void Push(T valor)
        {
            if (_tope >= _datos.Length)
            {
                T[] nuevo = new T[_datos.Length * 2];
                Array.Copy(_datos, nuevo, _datos.Length);
                _datos = nuevo;
            }
            _datos[_tope] = valor;
            _tope++;
        }

        // NO exponemos Pop para mantener la regla de no retroceder,
        // pero podrías tener un Pop privado si lo necesitas internamente.
        public T Peek()
        {
            if (_tope == 0) throw new InvalidOperationException("Pila vacía");
            return _datos[_tope - 1];
        }

        public string RecorridoComoTexto()
        {
            if (_tope == 0) return "(sin viajes)";
            string res = _datos[0]?.ToString();
            for (int i = 1; i < _tope; i++)
                res += " -> " + _datos[i];
            return res;
        }
    }
}
