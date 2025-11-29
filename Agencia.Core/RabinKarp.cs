using System.Collections.Generic;

namespace Agencia.Core
{
    public static class RabinKarp
    {
        // Devuelve índices donde se encontró el patrón en el texto.
        public static List<int> Buscar(string texto, string patron)
        {
            List<int> posiciones = new List<int>();
            if (string.IsNullOrEmpty(texto) || string.IsNullOrEmpty(patron) || patron.Length > texto.Length)
                return posiciones;

            int m = patron.Length;
            int n = texto.Length;
            int d = 256;
            int q = 101; // número primo

            int h = 1;
            for (int i = 0; i < m - 1; i++)
                h = (h * d) % q;

            int p = 0; // hash patron
            int t = 0; // hash ventana texto

            for (int i = 0; i < m; i++)
            {
                p = (d * p + patron[i]) % q;
                t = (d * t + texto[i]) % q;
            }

            for (int i = 0; i <= n - m; i++)
            {
                if (p == t)
                {
                    bool iguales = true;
                    for (int j = 0; j < m; j++)
                    {
                        if (texto[i + j] != patron[j]) { iguales = false; break; }
                    }
                    if (iguales) posiciones.Add(i);
                }

                if (i < n - m)
                {
                    t = (d * (t - texto[i] * h) + texto[i + m]) % q;
                    if (t < 0) t += q;
                }
            }

            return posiciones;
        }
    }
}
