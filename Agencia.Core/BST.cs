namespace Agencia.Core
{
    public class BstNode
    {
        public int ClaveRiesgo;
        public Universo Universo;
        public BstNode Izq;
        public BstNode Der;

        public BstNode(int clave, Universo u)
        {
            ClaveRiesgo = clave;
            Universo = u;
        }
    }

    public class BinarySearchTree
    {
        public BstNode Raiz { get; private set; }

        public void Insertar(Universo u)
        {
            Raiz = InsertarRec(Raiz, u.Riesgo, u);
        }

        private BstNode InsertarRec(BstNode nodo, int clave, Universo u)
        {
            if (nodo == null) return new BstNode(clave, u);
            if (clave < nodo.ClaveRiesgo)
                nodo.Izq = InsertarRec(nodo.Izq, clave, u);
            else
                nodo.Der = InsertarRec(nodo.Der, clave, u);
            return nodo;
        }

        public void Inorden(System.Action<Universo> visitar)
        {
            InordenRec(Raiz, visitar);
        }

        private void InordenRec(BstNode n, System.Action<Universo> visitar)
        {
            if (n == null) return;
            InordenRec(n.Izq, visitar);
            visitar(n.Universo);
            InordenRec(n.Der, visitar);
        }
    }
}
