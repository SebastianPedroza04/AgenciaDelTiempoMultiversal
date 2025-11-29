namespace Agencia.Core
{
    public class AvlNode
    {
        public int ClaveRiesgo;
        public Universo Universo;
        public AvlNode Izq;
        public AvlNode Der;
        public int Altura;

        public AvlNode(int clave, Universo u)
        {
            ClaveRiesgo = clave;
            Universo = u;
            Altura = 1;
        }
    }

    public class AvlTree
    {
        public AvlNode Raiz { get; private set; }

        private int Altura(AvlNode n) => n?.Altura ?? 0;
        private int Balance(AvlNode n) => n == null ? 0 : Altura(n.Izq) - Altura(n.Der);

        private AvlNode RotarDerecha(AvlNode y)
        {
            AvlNode x = y.Izq;
            AvlNode T2 = x.Der;

            x.Der = y;
            y.Izq = T2;

            y.Altura = System.Math.Max(Altura(y.Izq), Altura(y.Der)) + 1;
            x.Altura = System.Math.Max(Altura(x.Izq), Altura(x.Der)) + 1;

            return x;
        }

        private AvlNode RotarIzquierda(AvlNode x)
        {
            AvlNode y = x.Der;
            AvlNode T2 = y.Izq;

            y.Izq = x;
            x.Der = T2;

            x.Altura = System.Math.Max(Altura(x.Izq), Altura(x.Der)) + 1;
            y.Altura = System.Math.Max(Altura(y.Izq), Altura(y.Der)) + 1;

            return y;
        }

        public void Insertar(Universo u)
        {
            Raiz = InsertarRec(Raiz, u.Riesgo, u);
        }

        private AvlNode InsertarRec(AvlNode nodo, int clave, Universo u)
        {
            if (nodo == null) return new AvlNode(clave, u);

            if (clave < nodo.ClaveRiesgo)
                nodo.Izq = InsertarRec(nodo.Izq, clave, u);
            else if (clave > nodo.ClaveRiesgo)
                nodo.Der = InsertarRec(nodo.Der, clave, u);
            else
                return nodo; // claves iguales, podrías manejar listas si quieres

            nodo.Altura = 1 + System.Math.Max(Altura(nodo.Izq), Altura(nodo.Der));

            int balance = Balance(nodo);

            // 4 casos clásicos
            if (balance > 1 && clave < nodo.Izq.ClaveRiesgo)
                return RotarDerecha(nodo);

            if (balance < -1 && clave > nodo.Der.ClaveRiesgo)
                return RotarIzquierda(nodo);

            if (balance > 1 && clave > nodo.Izq.ClaveRiesgo)
            {
                nodo.Izq = RotarIzquierda(nodo.Izq);
                return RotarDerecha(nodo);
            }

            if (balance < -1 && clave < nodo.Der.ClaveRiesgo)
            {
                nodo.Der = RotarDerecha(nodo.Der);
                return RotarIzquierda(nodo);
            }

            return nodo;
        }
    }
}
