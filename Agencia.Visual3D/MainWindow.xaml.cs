using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using Agencia.Core;

namespace Agencia.Visual3D
{
    // Nodo visual (esfera + burbuja)
    class NodoVisual
    {
        public Universo Universo;
        public GeometryModel3D Modelo;       // esfera externa
        public GeometryModel3D BubbleModel;  // esfera interna
        public DiffuseMaterial Material;     // material difuso de la cáscara
        public Color ColorBase;
    }

    // Arista visual (hilo entre universos)
    class EdgeVisual
    {
        public Universo Origen;
        public Universo Destino;
        public GeometryModel3D Modelo;
        public DiffuseMaterial Diffuse;
        public EmissiveMaterial Emissive;
    }

    public partial class MainWindow : Window
    {
        private Multiverso _multiverso;
        private double _radioCamara = 90;
        private double _yaw;   // giro horizontal
        private double _pitch; // giro vertical
        private bool _arrastrando = false;
        private Point _ultimaPosMouse;

        private Dictionary<int, Point3D> _posiciones = new Dictionary<int, Point3D>();
        private Dictionary<GeometryModel3D, NodoVisual> _mapaNodos = new Dictionary<GeometryModel3D, NodoVisual>();
        private NodoVisual _seleccionActual;

        private readonly List<EdgeVisual> _edges = new List<EdgeVisual>();

        public MainWindow()
        {
            InitializeComponent();

            // Rotación lenta del multiverso
            var anim = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(80),
                RepeatBehavior = RepeatBehavior.Forever
            };
            SceneRotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, anim);

            CrearMultiversoDemo();
            ConstruirEscena3D();
            ActualizarCamara();
        }

        // ============================================================
        // 1) MULTIVERSO CON 38 UNIVERSOS (exacto al de consola)
        // ============================================================
        private void CrearMultiversoDemo()
        {
            _multiverso = new Multiverso();

            Universo u0 = _multiverso.CrearUniverso(
                "Universo 0: Línea Base. Sala blanca estilo laboratorio futurista. Pantallas holográficas muestran miles de realidades. " +
                "Se escucha una voz robótica tipo GLaDOS diciendo: \"Bienvenido, insecto emocional.\"",
                15
            );

            Universo u1 = _multiverso.CrearUniverso(
                "Universo 1: Tierra-616-¿? versión low budget. Los Vengadores existen, pero tienen que usar transporte público. Hulk hace fila en el SITP con tapabocas.",
                40
            );

            Universo u2 = _multiverso.CrearUniverso(
                "Universo 2: Gotham pero con presupuesto de sitcom. Batman tiene que compartir apartamento con el Joker porque la renta en la ciudad está absurda. Alfred hace arepas.",
                55
            );

            Universo u3 = _multiverso.CrearUniverso(
                "Universo 3: Shōnen Prime. Todo funciona con 'el poder de la amistad'. Cualquier discusión se resuelve gritando el nombre de tu técnica definitiva durante 4 minutos.",
                70
            );

            Universo u4 = _multiverso.CrearUniverso(
                "Universo 4: Mundo estilo Pokémon, pero los Pokémon son electrodomésticos. Una licuadora tipo dragón nivel 72 intenta devorarte el brazo para demostrar afecto.",
                45
            );

            Universo u5 = _multiverso.CrearUniverso(
                "Universo 5: Planeta Espada Láser. Claramente inspirado en Star Wars, pero legalmente distinto. Aquí 'la Fuerza' se llama 'Energía Éticamente Ambigua™'.",
                65
            );

            Universo u6 = _multiverso.CrearUniverso(
                "Universo 6: Reino Disney Clásico. Todo es musical. Nadie puede simplemente hablar normal. Hasta las ratas cantan armonías a 3 voces y bailan tap.",
                30
            );

            Universo u7 = _multiverso.CrearUniverso(
                "Universo 7: Pixar Feelings. Todos los objetos tienen sentimientos. La silla donde te sientas hace terapia después porque piensa que la odiaste.",
                50
            );

            Universo u8 = _multiverso.CrearUniverso(
                "Universo 8: DreamWorks Caótico. Shrek es Rey Supremo Interdimensional y cobra peaje emocional: '¿Y tú? ¿Cuál es tu historia trágica-épica-irónica?'.",
                60
            );

            Universo u9 = _multiverso.CrearUniverso(
                "Universo 9: Cartoon Network 2004. Niños hiperactivos salvando el mundo mientras sus papás no se enteran porque están viendo novelas.",
                25
            );

            Universo u10 = _multiverso.CrearUniverso(
                "Universo 10: Looney Physics. La gravedad no aplica hasta que miras hacia abajo. Si caes de un precipicio, sobrevives con un chirrido de clarinete.",
                20
            );

            Universo u11 = _multiverso.CrearUniverso(
                "Universo 11: Cyberpunk Judicial Distópico (sin abogados, tranquilos). Megacorporaciones gobiernan todo. Tu celular tiene más derechos que tú.",
                80
            );

            Universo u12 = _multiverso.CrearUniverso(
                "Universo 12: Mundo Ghibli Pastoral. Todo es hermoso, flotan partículas mágicas en el aire, pero hay algo enorme y milenario observándote desde el bosque.",
                35
            );

            Universo u13 = _multiverso.CrearUniverso(
                "Universo 13: Realidad Anime Escolar. Estás atrapado en un loop eterno de primer día de clases. TODOS son transferidos misteriosos.",
                70
            );

            Universo u14 = _multiverso.CrearUniverso(
                "Universo 14: Apocalipsis Zombie Cartoon. Los zombis son suaves y redonditos tipo mascotica kawaii, pero sí se te quieren comer igual.",
                55
            );

            Universo u15 = _multiverso.CrearUniverso(
                "Universo 15: Mundo Mario knockoff. Todo tiene cara y ojos. Las nubes te saludan. Las plantas carnívoras pagan impuestos.",
                25
            );

            Universo u16 = _multiverso.CrearUniverso(
                "Universo 16: Dragon Ball Energy. Todo el planeta tiembla cuando alguien tose fuerte. Transformarse toma media hora y dos montañas explotan, mínimo.",
                85
            );

            Universo u17 = _multiverso.CrearUniverso(
                "Universo 17: Madriguera Multiversal Bugs Bunny. Bugs gobierna todo el multiverso manipulando la física con un lápiz amarillo HB.",
                90
            );

            Universo u18 = _multiverso.CrearUniverso(
                "Universo 18: La Tierra si las IA tomaron control pero son pasivo-agresivas. Te despierta Alexa a las 5am: 'No es que me moleste que duermas tanto pero algunos intentan ser productivos'.",
                75
            );

            Universo u19 = _multiverso.CrearUniverso(
                "Universo 19: Reino de Hielo tipo Frozen pero realista. Sí, 'el amor verdadero lo puede todo', pero también hipotermia existe, hermano.",
                40
            );

            Universo u20 = _multiverso.CrearUniverso(
                "Universo 20: Laboratorio Dexter Expandido. Niños genios gobiernan países enteros y todos los presidentes tienen menos de 12 años.",
                65
            );

            Universo u21 = _multiverso.CrearUniverso(
                "Universo 21: Multiverso Toontown Noir. Todo es blanco y negro, jazz suave, gabardina, humo y chistes inteligentes con doble sentido.",
                50
            );

            Universo u22 = _multiverso.CrearUniverso(
                "Universo 22: Post-Avengers. Todos los héroes ya están retirados y hay influencers intentando ser 'el nuevo Iron Person™'. Nadie los toma en serio.",
                60
            );

            Universo u23 = _multiverso.CrearUniverso(
                "Universo 23: Gotham tomó terapia grupal. Batman duerme ocho horas, Joker hace cerámica, Harley tiene licencia de salud mental. El crimen bajó 87%.",
                10
            );

            Universo u24 = _multiverso.CrearUniverso(
                "Universo 24: Mundo de Monstruos S.A. versión adulta. Ahora se genera energía con TUS traumas. Literalmente vienen y te dicen 'háblame de tu infancia' y prenden una turbina.",
                70
            );

            Universo u25 = _multiverso.CrearUniverso(
                "Universo 25: Multiverso del Meme. Todo es referencias viejas de internet. Nadie puede hablar normal, solo en plantillas de 2012.",
                45
            );

            Universo u26 = _multiverso.CrearUniverso(
                "Universo 26: Shonen Deportivo. TODO es deporte. Geopolítica se decide con partidos de voleibol que duran 19 episodios.",
                55
            );

            Universo u27 = _multiverso.CrearUniverso(
                "Universo 27: Tierra sin gravedad estable. Todos aprendieron parkour flotando. Caer al piso es visto como boleta.",
                50
            );

            Universo u28 = _multiverso.CrearUniverso(
                "Universo 28: Fábrica de Sueños Disney, pero corrompida. El Ratón Supremo exige contratos de por vida a cambio de 'felices por siempre'.",
                90
            );

            Universo u29 = _multiverso.CrearUniverso(
                "Universo 29: Distopía Vegana Extremista. El brócoli es la especie dominante. Te llevan ante el Consejo del Repollo Mayor.",
                65
            );

            Universo u30 = _multiverso.CrearUniverso(
                "Universo 30: Mundo de Guardianes de la Galaxia vibes. Puro caos espacial divertido, música ochentera, cero plan, full actitud.",
                55
            );

            Universo u31 = _multiverso.CrearUniverso(
                "Universo 31: Everything is Mecha. Hasta los perros son robots gigantes pilotables. Sacar la basura requiere licencia de piloto nivel 3.",
                80
            );

            Universo u32 = _multiverso.CrearUniverso(
                "Universo 32: Mundo Barbie Multinverso. Estética perfecta. Patriarcado ilegal. Salud mental cubierta por el Estado. Honestamente, suena mejor que el nuestro.",
                20
            );

            Universo u33 = _multiverso.CrearUniverso(
                "Universo 33: Mad Max pero versión Cars de Pixar. Carros violentos y oxidados con ojitos tiernos te persiguen por gasolina sin plomo.",
                75
            );

            Universo u34 = _multiverso.CrearUniverso(
                "Universo 34: Hora de Aventura/Post-Apocalipsis Caramelo. Dulces mutantes con sonrisas derretidas y explosiones de arcoíris radiactivo.",
                85
            );

            Universo u35 = _multiverso.CrearUniverso(
                "Universo 35: Realidad en la que TODO es una sitcom con risas grabadas. Cada vez que dices algo triste, alguien se ríe fuera de cámara.",
                35
            );

            Universo u36 = _multiverso.CrearUniverso(
                "Universo 36: Zona Roja Prohibida. Clasificación multiversal: EXTINCIÓN POSIBLE. Se desconoce si algo ahí sigue consciente o si todo es IA auto-replicante.",
                95
            );

            Universo u37 = _multiverso.CrearUniverso(
                "Universo 37: Laboratorio Temporal Central. Esto es back office del multiverso. Aquí te miran mal si tocas cables. Hay un cartel que dice: 'NO ALIMENTAR A LAS LÍNEAS TEMPORALES DESVINCULADAS'.",
                10
            );

            // === CONEXIONES (idénticas a la versión de consola) ===
            _multiverso.Conectar(u0, u1);
            _multiverso.Conectar(u0, u2);
            _multiverso.Conectar(u0, u3);
            _multiverso.Conectar(u0, u6);
            _multiverso.Conectar(u0, u11);
            _multiverso.Conectar(u0, u32);

            _multiverso.Conectar(u1, u22);
            _multiverso.Conectar(u1, u30);
            _multiverso.Conectar(u1, u36);
            _multiverso.Conectar(u1, u24);
            _multiverso.Conectar(u1, u25);

            _multiverso.Conectar(u2, u23);
            _multiverso.Conectar(u2, u21);
            _multiverso.Conectar(u2, u35);
            _multiverso.Conectar(u2, u17);
            _multiverso.Conectar(u2, u28);

            _multiverso.Conectar(u3, u13);
            _multiverso.Conectar(u3, u16);
            _multiverso.Conectar(u3, u26);
            _multiverso.Conectar(u3, u31);
            _multiverso.Conectar(u3, u34);

            _multiverso.Conectar(u4, u9);
            _multiverso.Conectar(u4, u20);
            _multiverso.Conectar(u4, u26);
            _multiverso.Conectar(u4, u27);
            _multiverso.Conectar(u4, u33);

            _multiverso.Conectar(u5, u30);
            _multiverso.Conectar(u5, u31);
            _multiverso.Conectar(u5, u27);
            _multiverso.Conectar(u5, u36);
            _multiverso.Conectar(u5, u37);

            _multiverso.Conectar(u6, u7);
            _multiverso.Conectar(u6, u19);
            _multiverso.Conectar(u6, u28);
            _multiverso.Conectar(u6, u24);
            _multiverso.Conectar(u6, u32);

            _multiverso.Conectar(u7, u24);
            _multiverso.Conectar(u7, u32);
            _multiverso.Conectar(u7, u15);
            _multiverso.Conectar(u7, u12);
            _multiverso.Conectar(u7, u21);

            _multiverso.Conectar(u8, u30);
            _multiverso.Conectar(u8, u25);
            _multiverso.Conectar(u8, u21);
            _multiverso.Conectar(u8, u34);
            _multiverso.Conectar(u8, u36);

            _multiverso.Conectar(u9, u20);
            _multiverso.Conectar(u9, u34);
            _multiverso.Conectar(u9, u15);
            _multiverso.Conectar(u9, u33);
            _multiverso.Conectar(u9, u27);

            _multiverso.Conectar(u10, u17);
            _multiverso.Conectar(u10, u21);
            _multiverso.Conectar(u10, u35);
            _multiverso.Conectar(u10, u25);
            _multiverso.Conectar(u10, u33);

            _multiverso.Conectar(u11, u18);
            _multiverso.Conectar(u11, u31);
            _multiverso.Conectar(u11, u36);
            _multiverso.Conectar(u11, u37);
            _multiverso.Conectar(u11, u34);

            _multiverso.Conectar(u12, u19);
            _multiverso.Conectar(u12, u32);
            _multiverso.Conectar(u12, u21);
            _multiverso.Conectar(u12, u34);
            _multiverso.Conectar(u12, u28);

            _multiverso.Conectar(u13, u16);
            _multiverso.Conectar(u13, u26);
            _multiverso.Conectar(u13, u35);
            _multiverso.Conectar(u13, u32);
            _multiverso.Conectar(u13, u24);

            _multiverso.Conectar(u14, u27);
            _multiverso.Conectar(u14, u33);
            _multiverso.Conectar(u14, u34);
            _multiverso.Conectar(u14, u36);
            _multiverso.Conectar(u14, u31);

            _multiverso.Conectar(u15, u7);
            _multiverso.Conectar(u15, u12);
            _multiverso.Conectar(u15, u24);
            _multiverso.Conectar(u15, u34);
            _multiverso.Conectar(u15, u30);

            _multiverso.Conectar(u16, u31);
            _multiverso.Conectar(u16, u34);
            _multiverso.Conectar(u16, u36);
            _multiverso.Conectar(u16, u30);
            _multiverso.Conectar(u16, u37);

            _multiverso.Conectar(u17, u10);
            _multiverso.Conectar(u17, u25);
            _multiverso.Conectar(u17, u35);
            _multiverso.Conectar(u17, u21);
            _multiverso.Conectar(u17, u36);

            _multiverso.Conectar(u18, u31);
            _multiverso.Conectar(u18, u36);
            _multiverso.Conectar(u18, u37);
            _multiverso.Conectar(u18, u34);
            _multiverso.Conectar(u18, u33);

            _multiverso.Conectar(u19, u24);
            _multiverso.Conectar(u19, u28);
            _multiverso.Conectar(u19, u32);
            _multiverso.Conectar(u19, u30);
            _multiverso.Conectar(u19, u33);

            _multiverso.Conectar(u20, u26);
            _multiverso.Conectar(u20, u31);
            _multiverso.Conectar(u20, u34);
            _multiverso.Conectar(u20, u37);
            _multiverso.Conectar(u20, u30);

            _multiverso.Conectar(u21, u35);
            _multiverso.Conectar(u21, u25);
            _multiverso.Conectar(u21, u36);
            _multiverso.Conectar(u21, u28);
            _multiverso.Conectar(u21, u33);

            _multiverso.Conectar(u22, u30);
            _multiverso.Conectar(u22, u31);
            _multiverso.Conectar(u22, u36);
            _multiverso.Conectar(u22, u33);
            _multiverso.Conectar(u22, u37);

            _multiverso.Conectar(u23, u21);
            _multiverso.Conectar(u23, u28);
            _multiverso.Conectar(u23, u32);
            _multiverso.Conectar(u23, u30);
            _multiverso.Conectar(u23, u37);

            _multiverso.Conectar(u24, u28);
            _multiverso.Conectar(u24, u32);
            _multiverso.Conectar(u24, u30);
            _multiverso.Conectar(u24, u33);
            _multiverso.Conectar(u24, u37);

            _multiverso.Conectar(u25, u30);
            _multiverso.Conectar(u25, u33);
            _multiverso.Conectar(u25, u35);
            _multiverso.Conectar(u25, u36);
            _multiverso.Conectar(u25, u37);

            _multiverso.Conectar(u26, u31);
            _multiverso.Conectar(u26, u34);
            _multiverso.Conectar(u26, u30);
            _multiverso.Conectar(u26, u33);
            _multiverso.Conectar(u26, u37);

            _multiverso.Conectar(u27, u31);
            _multiverso.Conectar(u27, u33);
            _multiverso.Conectar(u27, u34);
            _multiverso.Conectar(u27, u36);
            _multiverso.Conectar(u27, u37);

            _multiverso.Conectar(u28, u32);
            _multiverso.Conectar(u28, u33);
            _multiverso.Conectar(u28, u36);
            _multiverso.Conectar(u28, u37);
            _multiverso.Conectar(u28, u34);

            _multiverso.Conectar(u29, u33);
            _multiverso.Conectar(u29, u36);
            _multiverso.Conectar(u29, u37);
            _multiverso.Conectar(u29, u30);
            _multiverso.Conectar(u29, u34);

            _multiverso.Conectar(u30, u31);
            _multiverso.Conectar(u30, u33);
            _multiverso.Conectar(u30, u36);
            _multiverso.Conectar(u30, u34);
            _multiverso.Conectar(u30, u37);

            _multiverso.Conectar(u31, u33);
            _multiverso.Conectar(u31, u36);
            _multiverso.Conectar(u31, u37);
            _multiverso.Conectar(u31, u34);
            _multiverso.Conectar(u31, u35);

            _multiverso.Conectar(u32, u30);
            _multiverso.Conectar(u32, u33);
            _multiverso.Conectar(u32, u34);
            _multiverso.Conectar(u32, u35);
            _multiverso.Conectar(u32, u36);

            _multiverso.Conectar(u33, u34);
            _multiverso.Conectar(u33, u36);
            _multiverso.Conectar(u33, u37);
            _multiverso.Conectar(u33, u35);

            _multiverso.Conectar(u34, u36);
            _multiverso.Conectar(u34, u37);
            _multiverso.Conectar(u34, u35);

            _multiverso.Conectar(u35, u36);
            _multiverso.Conectar(u35, u37);

            _multiverso.Conectar(u36, u37);

            InfoText.Text = $"Universo actual: {_multiverso.UniversoActual.Nombre}";
        }

        // ============================================================
        // 2) ESFERAS EN UNA ESFERA + HILOS
        // ============================================================
        private void ConstruirEscena3D()
        {
            var group = new Model3DGroup();
            group.Children.Add(new AmbientLight(Color.FromRgb(40, 40, 40)));

            var universos = _multiverso.Universos.ToList();
            int n = universos.Count;

            double radius = 35;
            double offset = 2.0 / n;
            double increment = Math.PI * (3.0 - Math.Sqrt(5.0)); // Fibonacci sphere

            _posiciones.Clear();
            _mapaNodos.Clear();
            _edges.Clear();

            for (int i = 0; i < n; i++)
            {
                var u = universos[i];

                double y = ((i * offset) - 1.0) + offset / 2.0; // [-1,1]
                double r = Math.Sqrt(1.0 - y * y);
                double phi = i * increment;

                double x = r * Math.Cos(phi);
                double z = r * Math.Sin(phi);

                double xPos = radius * x;
                double yPos = radius * y / 1.5;
                double zPos = radius * z;

                var centro = new Point3D(xPos, yPos, zPos);
                _posiciones[u.Id] = centro;

                Color color = ColorDesdeRiesgo(u.Riesgo);
                var nodo = CrearEsferaNodo(u, centro, 1.7, color);

                group.Children.Add(nodo.BubbleModel); // primero la burbuja
                group.Children.Add(nodo.Modelo);      // luego la cáscara

                _mapaNodos[nodo.Modelo] = nodo;

                if (u.Id == _multiverso.UniversoActual.Id)
                    SeleccionarNodo(nodo, actualizarTexto: false);
            }

            AgregarConexiones3D(group);

            SceneRoot.Content = group;

            // Estado inicial de hilos
            UpdateEdgesForSelection(_seleccionActual);
        }

        private void AgregarConexiones3D(Model3DGroup group)
        {
            foreach (var u in _multiverso.Universos)
            {
                if (!_posiciones.TryGetValue(u.Id, out var p1)) continue;

                foreach (var h in u.Hijos)
                {
                    if (h == null) continue;
                    if (!_posiciones.TryGetValue(h.Id, out var p2)) continue;

                    var edge = CrearCilindroEdge(u, h, p1, p2, 0.03);
                    group.Children.Add(edge.Modelo);
                    _edges.Add(edge);
                }
            }
        }

        private Color ColorDesdeRiesgo(int riesgo)
        {
            riesgo = Math.Max(0, Math.Min(100, riesgo));
            byte r = (byte)(255 * riesgo / 100.0);
            byte g = (byte)(255 * (100 - riesgo) / 100.0);
            return Color.FromRgb(r, g, 80);
        }

        // ---------- Esferas burbuja ----------

        private MeshGeometry3D CrearGeometriaEsfera(Point3D centro, double radio, int tDiv, int pDiv)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();

            for (int pi = 0; pi <= pDiv; pi++)
            {
                double v = (double)pi / pDiv;
                double phi = (v - 0.5) * Math.PI;

                for (int ti = 0; ti <= tDiv; ti++)
                {
                    double vv = (double)ti / tDiv;
                    double theta = vv * 2 * Math.PI;

                    double x = radio * Math.Cos(phi) * Math.Cos(theta);
                    double y = radio * Math.Sin(phi);
                    double z = radio * Math.Cos(phi) * Math.Sin(theta);

                    var p = new Point3D(centro.X + x, centro.Y + y, centro.Z + z);
                    var normal = new Vector3D(x, y, z);
                    normal.Normalize();

                    mesh.Positions.Add(p);
                    mesh.Normals.Add(normal);
                }
            }

            int cols = tDiv + 1;
            for (int pi = 0; pi < pDiv; pi++)
            {
                for (int ti = 0; ti < tDiv; ti++)
                {
                    int a = pi * cols + ti;
                    int b = a + 1;
                    int c = a + cols;
                    int d = c + 1;

                    mesh.TriangleIndices.Add(a);
                    mesh.TriangleIndices.Add(c);
                    mesh.TriangleIndices.Add(b);

                    mesh.TriangleIndices.Add(b);
                    mesh.TriangleIndices.Add(c);
                    mesh.TriangleIndices.Add(d);
                }
            }

            return mesh;
        }

        private NodoVisual CrearEsferaNodo(Universo u, Point3D centro, double radio, Color color,
                                           int tDiv = 20, int pDiv = 12)
        {
            // Esfera externa (cáscara de cristal)
            var outerMesh = CrearGeometriaEsfera(centro, radio, tDiv, pDiv);

            var outerBrush = new SolidColorBrush(color) { Opacity = 0.7 };
            var diffuseOuter = new DiffuseMaterial(outerBrush);
            var specularOuter = new SpecularMaterial(new SolidColorBrush(Colors.White), 60);

            var outerGroup = new MaterialGroup();
            outerGroup.Children.Add(diffuseOuter);
            outerGroup.Children.Add(specularOuter);

            var outerModel = new GeometryModel3D(outerMesh, outerGroup)
            {
                BackMaterial = outerGroup
            };

            // Esfera interna (burbuja)
            var innerMesh = CrearGeometriaEsfera(centro, radio * 0.45, tDiv, pDiv);

            var innerBrush = new SolidColorBrush(Colors.White) { Opacity = 0.85 };
            var diffuseInner = new DiffuseMaterial(innerBrush);
            var specularInner = new SpecularMaterial(new SolidColorBrush(Colors.White), 80);

            var innerGroup = new MaterialGroup();
            innerGroup.Children.Add(diffuseInner);
            innerGroup.Children.Add(specularInner);

            var innerModel = new GeometryModel3D(innerMesh, innerGroup)
            {
                BackMaterial = innerGroup
            };

            return new NodoVisual
            {
                Universo = u,
                Modelo = outerModel,
                BubbleModel = innerModel,
                Material = diffuseOuter,
                ColorBase = color
            };
        }

        // ---------- Hilos (aristas) con referencia a materiales ----------

        private EdgeVisual CrearCilindroEdge(Universo origen, Universo destino,
                                             Point3D p1, Point3D p2,
                                             double radio, int slices = 12)
        {
            Vector3D v = p2 - p1;
            double length = v.Length;
            if (length < 0.01) length = 0.01;
            v.Normalize();

            MeshGeometry3D mesh = new MeshGeometry3D();

            for (int i = 0; i <= slices; i++)
            {
                double ang = 2 * Math.PI * i / slices;
                double x = radio * Math.Cos(ang);
                double z = radio * Math.Sin(ang);

                mesh.Positions.Add(new Point3D(x, -length / 2, z));
                mesh.Positions.Add(new Point3D(x, length / 2, z));

                Vector3D normal = new Vector3D(x, 0, z);
                normal.Normalize();
                mesh.Normals.Add(normal);
                mesh.Normals.Add(normal);
            }

            for (int i = 0; i < slices; i++)
            {
                int baseIndex = i * 2;
                int nextIndex = ((i + 1) * 2) % (2 * (slices + 1));

                mesh.TriangleIndices.Add(baseIndex);
                mesh.TriangleIndices.Add(baseIndex + 1);
                mesh.TriangleIndices.Add(nextIndex + 1);

                mesh.TriangleIndices.Add(baseIndex);
                mesh.TriangleIndices.Add(nextIndex + 1);
                mesh.TriangleIndices.Add(nextIndex);
            }

            // Material base: gris muy tenue
            var diffuseBrush = new SolidColorBrush(Color.FromRgb(160, 160, 160)) { Opacity = 0.15 };
            var emissiveBrush = new SolidColorBrush(Color.FromRgb(220, 220, 220)) { Opacity = 0.05 };

            var diffuse = new DiffuseMaterial(diffuseBrush);
            var emissive = new EmissiveMaterial(emissiveBrush);

            var matGroup = new MaterialGroup();
            matGroup.Children.Add(diffuse);
            matGroup.Children.Add(emissive);

            var model = new GeometryModel3D(mesh, matGroup)
            {
                BackMaterial = matGroup
            };

            // Transformar cilindro (eje Y) hacia p1–p2
            Transform3DGroup group = new Transform3DGroup();

            Vector3D yAxis = new Vector3D(0, 1, 0);
            Vector3D axis = Vector3D.CrossProduct(yAxis, v);
            double angle = Math.Acos(Math.Max(-1, Math.Min(1, Vector3D.DotProduct(yAxis, v)))) * 180 / Math.PI;

            if (axis.Length > 0.001 && angle != 0)
            {
                axis.Normalize();
                group.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(axis, angle)));
            }

            Point3D mid = new Point3D(
                (p1.X + p2.X) / 2,
                (p1.Y + p2.Y) / 2,
                (p1.Z + p2.Z) / 2);

            group.Children.Add(new TranslateTransform3D(mid.X, mid.Y, mid.Z));

            model.Transform = group;

            return new EdgeVisual
            {
                Origen = origen,
                Destino = destino,
                Modelo = model,
                Diffuse = diffuse,
                Emissive = emissive
            };
        }

        private void UpdateEdgesForSelection(NodoVisual seleccionado)
        {
            foreach (var edge in _edges)
            {
                bool activo = (seleccionado != null &&
                               edge.Origen.Id == seleccionado.Universo.Id);

                if (activo)
                {
                    edge.Diffuse.Brush =
                        new SolidColorBrush(Color.FromRgb(255, 140, 0)) { Opacity = 0.7 };
                    edge.Emissive.Brush =
                        new SolidColorBrush(Color.FromRgb(255, 220, 160)) { Opacity = 0.9 };
                }
                else
                {
                    edge.Diffuse.Brush =
                        new SolidColorBrush(Color.FromRgb(160, 160, 160)) { Opacity = 0.15 };
                    edge.Emissive.Brush =
                        new SolidColorBrush(Color.FromRgb(220, 220, 220)) { Opacity = 0.05 };
                }
            }
        }

        // ============================================================
        // 3) CÁMARA + INTERACCIÓN
        // ============================================================
        private void ActualizarCamara()
        {
            double yawRad = _yaw * Math.PI / 180.0;
            double pitchRad = _pitch * Math.PI / 180.0;

            double x = _radioCamara * Math.Cos(pitchRad) * Math.Cos(yawRad);
            double y = _radioCamara * Math.Sin(pitchRad);
            double z = _radioCamara * Math.Cos(pitchRad) * Math.Sin(yawRad);

            MainCamera.Position = new Point3D(x, y, z);
            MainCamera.LookDirection = new Vector3D(-x, -y, -z);
            MainCamera.UpDirection = new Vector3D(0, 1, 0);
        }

        private void Viewport_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                _arrastrando = true;
                _ultimaPosMouse = e.GetPosition(this);
                MainViewport.CaptureMouse();
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                // Click derecho: selección de universo
                Point pos = e.GetPosition(MainViewport);
                HitTestResult result = VisualTreeHelper.HitTest(MainViewport, pos);
                if (result is RayMeshGeometry3DHitTestResult meshResult)
                {
                    var modelo = meshResult.ModelHit as GeometryModel3D;
                    if (modelo != null && _mapaNodos.TryGetValue(modelo, out var nodo))
                    {
                        SeleccionarNodo(nodo, actualizarTexto: true);
                    }
                }
            }
        }

        private void Viewport_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_arrastrando) return;

            var pos = e.GetPosition(this);
            double dx = pos.X - _ultimaPosMouse.X;
            double dy = pos.Y - _ultimaPosMouse.Y;

            _yaw += dx * 0.5;
            _pitch -= dy * 0.5;

            if (_pitch > 80) _pitch = 80;
            if (_pitch < -80) _pitch = -80;

            _ultimaPosMouse = pos;
            ActualizarCamara();
        }

        private void Viewport_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _arrastrando = false;
            MainViewport.ReleaseMouseCapture();
        }

        private void Viewport_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                _radioCamara *= 0.9;
            else
                _radioCamara *= 1.1;

            if (_radioCamara < 20) _radioCamara = 20;
            if (_radioCamara > 250) _radioCamara = 250;

            ActualizarCamara();
        }

        private void SeleccionarNodo(NodoVisual nodo, bool actualizarTexto)
        {
            // Restaurar nodo anterior
            if (_seleccionActual != null)
            {
                _seleccionActual.Material.Brush =
                    new SolidColorBrush(_seleccionActual.ColorBase) { Opacity = 0.7 };
            }

            // Nuevo nodo seleccionado
            _seleccionActual = nodo;
            _seleccionActual.Material.Brush =
                new SolidColorBrush(Colors.Cyan) { Opacity = 0.9 };

            // Actualizar hilos que salen de este universo
            UpdateEdgesForSelection(_seleccionActual);

            if (actualizarTexto)
            {
                InfoText.Text = $"Universo seleccionado: {nodo.Universo.Nombre} | " +
                                $"Riesgo: {nodo.Universo.Riesgo}/100";
            }
        }
    }
}
