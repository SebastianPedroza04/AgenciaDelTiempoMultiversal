using System;
using System.Collections.Generic;

namespace Agencia.Core
{
    // Nodo auxiliar para A*
    class NodoAStar
    {
        public int F;
        public int Id;
        public Universo U;

        public NodoAStar(int f, int id, Universo u)
        {
            F = f;
            Id = id;
            U = u;
        }
    }

    public static class GraphAlgorithms
    {
        // DFS sobre el árbol/k-ario de universos
        public static void DFS(Universo origen, Action<Universo> visitar)
        {
            HashSet<int> visitados = new HashSet<int>();
            DFSRec(origen, visitar, visitados);
        }

        private static void DFSRec(Universo u, Action<Universo> visitar, HashSet<int> visitados)
        {
            if (u == null || visitados.Contains(u.Id)) return;
            visitados.Add(u.Id);
            visitar(u);
            foreach (var hijo in u.Hijos)
            {
                if (hijo != null)
                    DFSRec(hijo, visitar, visitados);
            }
        }

        // BFS usando la cola circular propia
        public static void BFS(Universo origen, Action<Universo> visitar)
        {
            HashSet<int> visitados = new HashSet<int>();
            CircularQueue<Universo> cola = new CircularQueue<Universo>();

            cola.Enqueue(origen);
            visitados.Add(origen.Id);

            while (!cola.IsEmpty)
            {
                var u = cola.Dequeue();
                visitar(u);

                foreach (var hijo in u.Hijos)
                {
                    if (hijo != null && !visitados.Contains(hijo.Id))
                    {
                        visitados.Add(hijo.Id);
                        cola.Enqueue(hijo);
                    }
                }
            }
        }

        // A*: cost = riesgo acumulado, heurística = diferencia de riesgo
        public static List<Universo> AStar(Universo inicio, Universo objetivo)
        {
            // conjunto ordenado por F y luego por Id
            var abierta = new SortedSet<NodoAStar>(
                Comparer<NodoAStar>.Create((a, b) =>
                {
                    int cmp = a.F.CompareTo(b.F);
                    if (cmp != 0) return cmp;
                    return a.Id.CompareTo(b.Id);
                })
            );

            var gScore = new Dictionary<int, int>();          // costo acumulado
            var cameFrom = new Dictionary<int, Universo>();   // predecesores

            gScore[inicio.Id] = 0;
            abierta.Add(new NodoAStar(Heuristica(inicio, objetivo), inicio.Id, inicio));

            while (abierta.Count > 0)
            {
                // nodo con F mínimo
                var actualNodo = abierta.Min;
                abierta.Remove(actualNodo);
                var u = actualNodo.U;

                if (u.Id == objetivo.Id)
                    return ReconstruirCamino(cameFrom, objetivo);

                foreach (var vecino in u.Hijos)
                {
                    if (vecino == null) continue;

                    int tentativeG = gScore[u.Id] + vecino.Riesgo;

                    if (!gScore.ContainsKey(vecino.Id) || tentativeG < gScore[vecino.Id])
                    {
                        cameFrom[vecino.Id] = u;
                        gScore[vecino.Id] = tentativeG;
                        int f = tentativeG + Heuristica(vecino, objetivo);
                        abierta.Add(new NodoAStar(f, vecino.Id, vecino));
                    }
                }
            }

            // no se encontró camino
            return new List<Universo>();
        }

        private static int Heuristica(Universo a, Universo b)
        {
            return Math.Abs(a.Riesgo - b.Riesgo);
        }

        private static List<Universo> ReconstruirCamino(Dictionary<int, Universo> cameFrom, Universo objetivo)
        {
            List<Universo> camino = new List<Universo>();
            Universo actual = objetivo;
            camino.Add(actual);

            while (cameFrom.ContainsKey(actual.Id))
            {
                actual = cameFrom[actual.Id];
                camino.Add(actual);
            }

            camino.Reverse();
            return camino;
        }
    }
}
