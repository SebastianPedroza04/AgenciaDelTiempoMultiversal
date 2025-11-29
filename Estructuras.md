
---

### Bloque 2 — `ESTRUCTURAS.md`

```markdown
# Estructuras de datos implementadas

Este documento describe las estructuras de datos implementadas a mano en el proyecto **Agencia del Tiempo Multiversal**, su complejidad y en qué parte del sistema se usan.

---

## 1. DynamicArray\<T\>

Archivo: `Agencia.Core/DynamicArray.cs`

### Idea

- Arreglo dinámico genérico que crece cuando se llena.
- Se parece conceptualmente a `List<T>`, pero se implementa desde cero.

### Implementación

- Campos:
  - `T[] _data`: arreglo interno.
  - `int Count`: número de elementos válidos.
- Métodos clave:
  - Constructor con capacidad inicial (por defecto 16).
  - `EnsureCapacity(int min)`: si `min > _data.Length`, crea un arreglo nuevo de tamaño `2 * capacidad` (o `min` si es mayor) y copia los elementos.
  - `Add(T value)`: asegura capacidad, agrega en `_data[Count]` y aumenta `Count`.
  - Indexador `this[int index]` para acceso de lectura/escritura con validación de rangos.
  - `GetEnumerator()`: permite usar `foreach`.

### Complejidades

- Acceso por índice: **O(1)**.
- Asignación por índice: **O(1)**.
- `Add`: amortizado **O(1)** (por el redimensionamiento geométrico).
- Iteración completa: **O(n)**.

### Dónde se usa

- `Multiverso` utiliza `DynamicArray<Universo>` como contenedor principal de universos.

---

## 2. Pila\<T\>

Archivo: `Agencia.Core/Pila.cs`

### Idea

- Pila LIFO (Last In, First Out) sobre un arreglo.
- Sin usar `Stack<T>` de .NET.

### Implementación

- Campos:
  - `T[] _data`: arreglo interno.
  - `int _top`: cantidad de elementos (índice del siguiente libre).
- Métodos clave:
  - `Push(T value)`: asegura capacidad y agrega en `_data[_top]`.
  - `Pop()`: reduce `_top` y devuelve el elemento en la cima (lanza excepción si está vacía).
  - `IsEmpty`: propiedad booleana.
  - `Count`: número de elementos.
  - `ToArray()`: devuelve una copia del contenido en orden de inserción (fondo → tope).

### Complejidades

- `Push`: amortizado **O(1)**.
- `Pop`: **O(1)**.
- `ToArray`: **O(n)**.

### Dónde se usa

- `Multiverso` usa `Pila<int>` para mantener el **historial de viajes** entre universos.

---

## 3. CircularQueue\<T\>

Archivo: `Agencia.Core/CircularQueue.cs`

### Idea

- Cola FIFO (First In, First Out) implementada de forma circular.
- Evita mover elementos en el arreglo: reutiliza posiciones libres con aritmética modular.

### Implementación

- Campos:
  - `T[] _data`.
  - `int _front`: índice del siguiente elemento a extraer.
  - `int _rear`: índice donde insertar el siguiente elemento.
  - `int _count`: número de elementos en la cola.
- Métodos clave:
  - `Enqueue(T value)`:
    - Si el arreglo está lleno, duplica tamaño y reacomoda elementos.
    - Inserta en `_data[_rear]`, avanza `_rear` con `(_rear + 1) % _data.Length` e incrementa `_count`.
  - `Dequeue()`:
    - Retorna `_data[_front]`, avanza `_front` y decrementa `_count`.
  - `IsEmpty`: propiedad (`_count == 0`).

### Complejidades

- `Enqueue`: amortizado **O(1)**.
- `Dequeue`: **O(1)**.
- Redimensionamiento: **O(n)**, pero poco frecuente.

### Dónde se usa

- En `GraphAlgorithms.BFS` para el recorrido BFS sobre los universos.

---

## 4. DisjointSet (Union–Find)

Archivo: `Agencia.Core/DisjointSet.cs`

### Idea

- Estructura para manejar **conjuntos disjuntos**:
  - Permite unir conjuntos y consultar si dos elementos pertenecen al mismo conjunto.
- Usa:
  - **Compresión de caminos** en `Find`.
  - **Union by rank** en `Union`.

### Implementación

- Campos:
  - `int[] _parent`.
  - `int[] _rank`.
- Métodos clave:
  - `Find(int x)`:
    - Si `_parent[x] != x`, asigna `_parent[x] = Find(_parent[x])` (compresión).
  - `Union(int a, int b)`:
    - Conecta las raíces de `a` y `b` en base al `rank`.
  - `MismaComponente(int a, int b)`:
    - Compara raíces.

### Complejidades

- `Find`: casi **O(1)** amortizado.
- `Union`: casi **O(1)** amortizado.

### Dónde se podría usar

- Para agrupar universos por “facción” o “familia temporal”.
- Para determinar rápidamente si dos universos pertenecen al mismo grupo.

---

## 5. Modelo de grafos sobre universos

El multiverso se modela como un **grafo dirigido**:

- Cada `Universo` tiene un arreglo de tamaño 6:
  - `Universo[] Hijos;`
- Cada posición en `Hijos` es un **portal saliente**.
- Máximo 6 portales por universo ⇒ desde la perspectiva de cada nodo se parece a un **árbol k-ario acotado**, pero en general es un **grafo dirigido** (puede haber ciclos entre universos).

---

## 6. GraphAlgorithms

Archivo: `Agencia.Core/GraphAlgorithms.cs`

### DFS

```csharp
public static void DFS(Universo origen, Action<Universo> visitar)
