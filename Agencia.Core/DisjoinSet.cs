namespace Agencia.Core
{
    public class DisjointSet
    {
        private int[] padre;
        private int[] rango;

        public DisjointSet(int n)
        {
            padre = new int[n];
            rango = new int[n];
            for (int i = 0; i < n; i++)
            {
                padre[i] = i;
                rango[i] = 0;
            }
        }

        public int Find(int x)
        {
            if (padre[x] != x)
                padre[x] = Find(padre[x]); // compresión
            return padre[x];
        }

        public void Union(int x, int y)
        {
            int rx = Find(x);
            int ry = Find(y);
            if (rx == ry) return;

            if (rango[rx] < rango[ry])
                padre[rx] = ry;
            else if (rango[ry] < rango[rx])
                padre[ry] = rx;
            else
            {
                padre[ry] = rx;
                rango[rx]++;
            }
        }

        public bool MismoConjunto(int x, int y) => Find(x) == Find(y);
    }
}
