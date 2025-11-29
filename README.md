# Agencia del Tiempo Multiversal 🌀

Proyecto académico para la asignatura de **Estructuras de Datos**, implementado en C# (.NET), que simula una **Agencia del Tiempo** que viaja entre universos de un **multiverso friki**.

Incluye:

- Núcleo de estructuras de datos implementadas **a mano** (sin `List`, `Queue`, `Stack` de .NET para la parte central).
- Una **aplicación de consola** tipo visual novel donde el agente viaja entre universos.
- Una **visualización 3D** en WPF donde se muestra la red de universos como una nube de esferas conectadas por hilos.

---

## Objetivos académicos

- Modelar un **multiverso** como un árbol k-ario / grafo dirigido:
  - Mínimo **36 universos** (este proyecto usa 38).
  - Máximo **6 portales** salientes por universo.
  - Viajes unidireccionales (las aristas son dirigidas).
- Implementar y usar **estructuras de datos propias**:
  - Arreglo dinámico.
  - Pila.
  - Cola circular.
  - Conjuntos disjuntos.
  - Recorridos en grafos (DFS, BFS, A*).
- Usar **POO** y separación por capas:
  - Núcleo (`Agencia.Core`).
  - Interfaz de texto (`Agencia.Consola`).
  - Interfaz gráfica 3D (`Agencia.Visual3D`).

---

## Tecnologías usadas

- Lenguaje: **C# (.NET)**
- Tipo de proyectos:
  - Biblioteca de clases (.NET): `Agencia.Core`
  - Aplicación de consola (.NET): `Agencia.Consola`
  - Aplicación WPF (.NET): `Agencia.Visual3D`
- No se utilizan librerías externas / NuGet.  
  Solo los espacios de nombres estándar de .NET y WPF.

---

## Estructura de la solución

```text
AgenciaDelTiempoMultiversal.sln
├─ Agencia.Core/        # Núcleo de datos y lógica
│  ├─ Universo.cs
│  ├─ Multiverso.cs
│  ├─ DynamicArray.cs
│  ├─ Pila.cs
│  ├─ CircularQueue.cs
│  ├─ DisjointSet.cs
│  └─ GraphAlgorithms.cs
│
├─ Agencia.Consola/     # Juego/novela en consola
│  └─ Program.cs
│
└─ Agencia.Visual3D/    # Visualización 3D del multiverso (WPF)
   ├─ MainWindow.xaml
   └─ MainWindow.xaml.cs
