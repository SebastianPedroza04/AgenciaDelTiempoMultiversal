using System;
using System.Collections.Generic;

namespace Agencia.Core
{
    public class Multiverso
    {
        // === Campos internos (estructuras de datos) ===

        private DynamicArray<Universo> _universos = new DynamicArray<Universo>();
        private StackArray<int> _historialViajes = new StackArray<int>();
        private CircularQueue<Mision> _colaMisiones = new CircularQueue<Mision>();
        private BinaryHeap _heapMisiones = new BinaryHeap();
        private BinarySearchTree _bstPorRiesgo = new BinarySearchTree();
        private AvlTree _avlPorRiesgo = new AvlTree();
        private HashTable _hashCodigos;
        private DisjointSet _facciones;

        private int _contadorMisiones = 1;

        // === Propiedades públicas ===

        public Universo UniversoInicial { get; private set; }
        public Universo UniversoActual { get; private set; }
        public void EstablecerUniversoActual(Universo u)
        {
            if (u == null) return;
            UniversoActual = u;
        }

        // === Constructor ===

        public Multiverso(int capacidadEsperada = 64)
        {
            _hashCodigos = new HashTable(capacidadEsperada);
            _facciones = new DisjointSet(capacidadEsperada);
        }

        // === Creación de universos ===

        // Versión completa (usa todos los índices e índices auxiliares)
        public Universo CrearUniverso(string codigo, string nombre, string descripcion, int riesgo, FaccionTemporal faccion)
        {
            int id = _universos.Count;
            var u = new Universo(id, codigo, nombre, descripcion, riesgo, faccion);
            _universos.Add(u);

            if (UniversoInicial == null)
            {
                UniversoInicial = u;
                UniversoActual = u;
                _historialViajes.Push(u.Id);
            }

            _bstPorRiesgo.Insertar(u);
            _avlPorRiesgo.Insertar(u);
            _hashCodigos.Insertar(codigo, u);

            // Usamos Union-Find para agrupar por facción simbólicamente
            _facciones.Union(id, (int)faccion);

            return u;
        }

        // Sobrecarga sencilla para el Program.cs “narrativo”
        public Universo CrearUniverso(string descripcion, int riesgo)
        {
            string codigo = $"U{_universos.Count}";
            string nombre = $"Universo {_universos.Count}";

            var facciones = (FaccionTemporal[])Enum.GetValues(typeof(FaccionTemporal));
            FaccionTemporal faccion = facciones[_universos.Count % facciones.Length];

            return CrearUniverso(codigo, nombre, descripcion, riesgo, faccion);
        }

        // === Conexiones entre universos (árbol k-ario / grafo dirigido) ===

        public void ConectarComoHijo(Universo padre, Universo hijo)
        {
            bool ok = padre.AgregarHijo(hijo);
            if (!ok)
            {
                Console.WriteLine($"El universo {padre.Id} ya tiene 6 destinos posibles.");
            }
        }

        // Alias para usar desde Program.cs
        public void Conectar(Universo origen, Universo destino)
        {
            ConectarComoHijo(origen, destino);
        }

        // === Navegación ===

        // Viajar a un universo concreto (si es hijo del actual)
        public void ViajarA(Universo destino)
        {
            bool esHijo = false;
            foreach (var h in UniversoActual.Hijos)
            {
                if (h != null && h.Id == destino.Id)
                {
                    esHijo = true;
                    break;
                }
            }

            if (!esHijo)
            {
                Console.WriteLine("Solo puedes viajar a universos conectados por esta línea temporal.");
                return;
            }

            UniversoActual = destino;
            _historialViajes.Push(destino.Id);
        }

        // Versión por índice de portal (0–5) usada en consola
        public void Viajar(int indicePortal)
        {
            if (indicePortal < 0 || indicePortal >= UniversoActual.Hijos.Length)
            {
                Console.WriteLine("Portal inválido.");
                return;
            }

            var destino = UniversoActual.Hijos[indicePortal];
            if (destino == null || !destino.Activo)
            {
                Console.WriteLine("Ese portal no existe o el universo está destruido.");
                return;
            }

            ViajarA(destino);
            Console.WriteLine($">>> SALTO REALIZADO hacia {destino.Nombre}.");
        }

        public string HistorialTexto() => _historialViajes.RecorridoComoTexto();

        // === Eventos y misiones (ejemplo de uso de otras estructuras) ===

        public void AsignarEvento(Universo u, string titulo, string descripcion)
        {
            u.Eventos.AddLast(new Evento(titulo, descripcion));
        }

        public void EncolarMision(int universoDestinoId, string descripcion, int prioridad)
        {
            var m = new Mision(_contadorMisiones++, universoDestinoId, descripcion, prioridad);
            _colaMisiones.Enqueue(m);
            _heapMisiones.Insertar(m);
        }

        public Mision AtenderSiguienteMisionFIFO()
        {
            if (_colaMisiones.IsEmpty) return null;
            return _colaMisiones.Dequeue();
        }

        public Mision AtenderMisionMasUrgente()
        {
            if (_heapMisiones.IsEmpty) return null;
            return _heapMisiones.ExtraerMin();
        }

        // === Búsquedas y consultas ===

        public Universo BuscarPorCodigo(string codigo)
        {
            return _hashCodigos.Buscar(codigo);
        }

        public IEnumerable<Universo> Universos => EnumerarUniversos();

        private IEnumerable<Universo> EnumerarUniversos()
        {
            for (int i = 0; i < _universos.Count; i++)
                yield return _universos[i];
        }

        public bool MismaFaccion(Universo a, Universo b)
        {
            return _facciones.MismoConjunto(a.Id, b.Id);
        }

        public List<Universo> BuscarTextoEnDescripciones(string patron)
        {
            List<Universo> resultado = new List<Universo>();
            foreach (var u in Universos)
            {
                var pos = RabinKarp.Buscar(u.Descripcion, patron);
                if (pos.Count > 0)
                    resultado.Add(u);
            }
            return resultado;
        }

        // === Operaciones “cinemáticas” para la consola ===

        public void PodarActual()
        {
            UniversoActual.Activo = false;
            Console.WriteLine($">>> Universo {UniversoActual.Id} ha sido PODADO (marcado como destruido).");
        }

        public void MostrarEstado()
        {
            Console.WriteLine();
            Console.WriteLine("======================================================");
            Console.WriteLine($"UNIVERSO ACTUAL: {UniversoActual.Nombre}");
            Console.WriteLine($"ID: {UniversoActual.Id} | Riesgo: {UniversoActual.Riesgo}/100");
            Console.WriteLine($"Estado: {(UniversoActual.Activo ? "ACTIVO" : "PODADO")}");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Descripción:");
            Console.WriteLine(UniversoActual.Descripcion);
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Portales disponibles:");
            for (int i = 0; i < UniversoActual.Hijos.Length; i++)
            {
                var h = UniversoActual.Hijos[i];
                if (h != null && h.Activo)
                    Console.WriteLine($" [{i}] {h.Nombre}");
            }
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Ruta recorrida:");
            Console.WriteLine(HistorialTexto());
            Console.WriteLine("======================================================");
            Console.WriteLine();
        }
    }
}
